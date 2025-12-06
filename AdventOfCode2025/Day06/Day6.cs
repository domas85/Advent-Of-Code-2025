using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Security;
using static System.Net.Mime.MediaTypeNames;

namespace AOC
{
    public class Day6
    {
        public static string line = "";

        public static List<List<Int32>> Input = new List<List<Int32>>();

        public static List<string> operation = new List<string>();

        public static Int64 Result;

        public static void Run()
        {
            Console.Write("Starting... \n");
            //ReadInput();
            //SolvePart1();

            CephalopodReadInput();
            SolvePart2();
        }

        public static void SolvePart2()
        {
            for (int j = 0; operation.Count > j; j++)
            {
                Int64 columnResult = Input[j][0];
                for (int i = 0; i < Input[j].Count; i++)
                {
                    if (i == 0) continue;
                    if (operation[j] == "*")
                    {
                        columnResult *= Input[j][i];
                    }
                    else if (operation[j] == "+")
                    {
                        columnResult += Input[j][i];
                    }
                }
                Result += columnResult;
            }

            Console.WriteLine("\nResult: " + Result);
        }

        public static void SolvePart1()
        {
            for (int j = 0; operation.Count > j; j++)
            {
                Int64 columnResult = Input[j][0];
                for (int i = 0; i < Input[j].Count; i++)
                {
                    if (i == 0) continue;
                    if (operation[j] == "*")
                    {
                        columnResult *= Input[j][i];
                    }
                    else if (operation[j] == "+")
                    {
                        columnResult += Input[j][i];
                    }
                }
                Result += columnResult;
            }

            Console.WriteLine("\nResult: " + Result);
        }

        public static void CephalopodReadInput()
        {
            string[] sr = File.ReadAllLines(@"../../../Day06/Input.txt");
            bool skipToNextProblem = false;
            operation.Clear();
            Input.Clear();
            Input.Add(new List<Int32>());
            int index = 0;

            for (int i = sr[0].Length; i > 0; i--)
            {
                if (skipToNextProblem)
                {
                    Input.Add(new List<Int32>());
                    skipToNextProblem = false;
                    index++;
                    continue;
                }

                string numString = "";
                for (int lineIndex = 0; lineIndex < sr.Length; lineIndex++)
                {
                    char currentChar = sr[lineIndex][i - 1];
                    numString += currentChar;
                }
                if (numString.Contains('*') || numString.Contains('+'))
                {
                    string op = numString.Substring(numString.IndexOf('*') != -1 ? numString.IndexOf('*') : numString.IndexOf('+'), 1);
                    string num = numString.Trim('*', '+', ' ');
                    operation.Add(op);
                    Input[index].Add(Int32.Parse(num));

                    skipToNextProblem = true;
                    continue;
                }
                string trimmedNum = numString.Trim(' ');
                Input[index].Add(Int32.Parse(trimmedNum));
            }

            for (int i = 0; i < sr.Length; i++)
            {
                Console.WriteLine(sr[i]);
            }
        }

        public static void ReadInput()
        {
            StreamReader sr = new StreamReader(@"../../../Day06/Input.txt");

            while (line != null)
            {
                line = sr.ReadLine();

                if (line != null && line.Contains('*') || line != null && line.Contains('+'))
                {
                    string[] splitOperators = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                    operation.AddRange(splitOperators);
                    Console.WriteLine(line);
                    continue;
                }

                if (line != null)
                {
                    string[] splitStrings = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                    for (int i = 0; i < splitStrings.Length; i++)
                    {
                        if (Input.Count <= i)
                        {
                            Input.Add(new List<Int32>());
                        }

                        Input[i].Add(Int32.Parse(splitStrings[i]));

                    }

                }

                Console.WriteLine(line);
            }
        }
    }
}
