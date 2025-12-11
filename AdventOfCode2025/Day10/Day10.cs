using System.Numerics;
using Microsoft.Z3;

namespace AOC
{
    public class Day10
    {
        public static string line = "";

        public static List<Machine> Input = new List<Machine>();

        public static Int64 Result = 0;

        public static void Run()
        {
            Console.Write("Starting... \n");
            ReadInput();
            //SolvePart1();
            SolvePart2();
        }

        public static void SolvePart2()
        {
            // Using Z3 Solver to find the optimal button presses to reach the specified joltages
            foreach (var machine in Input)
            {
                Context ctx = new Context();
                Optimize opt = ctx.MkOptimize();

                // Create integer variables for each button wiring
                IntExpr[] buttonWirings = Enumerable.Range(0, machine.Buttons.Count)
                    .Select(i => ctx.MkIntConst($"p{i}"))
                    .ToArray();

                // Add constraints: each button wiring must be non-negative
                foreach (IntExpr buttons in buttonWirings)
                    opt.Add(ctx.MkGe(buttons, ctx.MkInt(0)));

                // Add constraints: the sum of button wirings affecting each light must equal the target joltage
                for (int i = 0; i < machine.Joltages.Length; i++)
                {
                    IntExpr[] affecting = buttonWirings.Where((buttons, index) => machine.Buttons[index].Contains(i)).ToArray();
                    if (affecting.Length > 0)
                    {
                        ArithExpr sum = affecting.Length == 1 ? affecting[0] : ctx.MkAdd(affecting);
                        opt.Add(ctx.MkEq(sum, ctx.MkInt(machine.Joltages[i])));
                    }
                }

                // minimize the total number of button presses
                opt.MkMinimize(buttonWirings.Length == 1 ? buttonWirings[0] : ctx.MkAdd(buttonWirings));
                if(opt.Check() != Status.SATISFIABLE)
                {
                    Console.Write("No solution found for a machine. \n");
                    ctx.Dispose();
                    continue;
                }

                opt.Check();
                Model model = opt.Model;

                foreach (IntExpr buttons in buttonWirings)
                {
                    Result += ((IntNum)model.Evaluate(buttons, true)).Int64;

                    Console.WriteLine($"{buttons} = {model.Evaluate(buttons, true)}");
                }
                Console.WriteLine("\n");
                ctx.Dispose();
            }

            Console.Write($"Result: {Result} \n");
        }

        public static void SolvePart1()
        {
            foreach (var machine in Input)
            {
                int minPresses = CountMinButtonPresses(machine);
                if (minPresses == -1)
                {
                    Console.Write("No solution found for a machine. \n");
                    continue;
                }
                Result += minPresses;
            }
            Console.Write($"Result: {Result} \n");
        }

        public static int CountMinButtonPresses(Machine machine)
        {
            for (int pressCount = 0; pressCount <= machine.Buttons.Count; pressCount++)
            {
                if (FindSolution(machine, pressCount, 0, 0, new bool[machine.TargetState.Length]))
                {
                    return pressCount;
                }
            }
            return -1;
        }

        private static bool FindSolution(Machine machine, int target, int start, int depth, bool[] state)
        {
            if (depth == target)
            {
                return state.SequenceEqual(machine.TargetState);
            }

            for (int i = start; i <= machine.Buttons.Count - (target - depth); i++)
            {
                FlipLights(machine.Buttons[i], state);

                if (FindSolution(machine, target, i + 1, depth + 1, state))
                {
                    return true;
                }

                FlipLights(machine.Buttons[i], state);
            }
            return false;
        }

        private static void FlipLights(HashSet<int> LightIndexes, bool[] LightState)
        {
            foreach (int index in LightIndexes)
            {
                if (index >= 0 && index < LightState.Length)
                {
                    LightState[index] = !LightState[index];
                }
            }
        }

        public static void ReadInput()
        {
            StreamReader sr = new StreamReader(@"../../../Day10/Input.txt");
            while (line != null)
            {
                line = sr.ReadLine();

                if (line != null)
                {
                    string[] manual = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                    bool[] Target = new bool[0];
                    List<HashSet<int>> buttonsInput = new List<HashSet<int>>();
                    int[] JoltageInput = new int[0];

                    foreach (var input in manual)
                    {
                        if (input.Contains('['))
                        {
                            string targetString = input.Substring(1, input.Length - 2);
                            Target = new bool[targetString.Length];
                            for (int i = 0; i < targetString.Length; i++)
                            {
                                Target[i] = targetString[i] == '#' ? true : false;
                            }
                            continue;
                        }

                        if (input.Contains('('))
                        {
                            string buttonsString = input.Substring(1, input.Length - 2);
                            string[] buttons = buttonsString.Split(",", StringSplitOptions.RemoveEmptyEntries);

                            HashSet<int> buttonSet = new HashSet<int>();

                            foreach (string button in buttons)
                            {
                                buttonSet.Add(int.Parse(button));
                            }

                            buttonsInput.Add(buttonSet);
                            continue;
                        }

                        if (input.Contains('{'))
                        {
                            string joltagesString = input.Substring(1, input.Length - 2);
                            string[] joltages = joltagesString.Split(",", StringSplitOptions.RemoveEmptyEntries);
                            int[] joltageArray = new int[joltages.Length];
                            for (int i = 0; i < joltages.Length; i++)
                            {
                                joltageArray[i] = int.Parse(joltages[i]);
                            }
                            JoltageInput = joltageArray;
                            continue;
                        }
                    }

                    Input.Add(new Machine(Target, buttonsInput, JoltageInput));
                }

                Console.WriteLine(line);
            }
        }

        public record Machine(bool[] TargetState, List<HashSet<int>> Buttons, int[] Joltages);
    }
}
