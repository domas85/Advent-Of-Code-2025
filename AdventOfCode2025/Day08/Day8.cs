using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Security;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace AOC
{
    public class Day8
    {
        public static string line = "";

        public static List<Vector3> Input = new List<Vector3>();

        public static List<List<int>> Circuits = new List<List<int>>();

        public static long Result = 1;

        public static void Run()
        {
            Console.Write("Starting... \n");
            ReadInput();
            //SolvePart1();
            SolvePart2();
        }

        public static void SolvePart2()
        {
            Dictionary<Tuple<int, int>, Int64> DistancesForAllPairs = GetClosestPositionIndexes();
            var sortedDict = from entry in DistancesForAllPairs orderby entry.Value ascending select entry;

            KeyValuePair<Tuple<int, int>, Int64> element = new KeyValuePair<Tuple<int, int>, Int64>();

            for (int i = 0; sortedDict.Count() > i; i++)
            {
                element = sortedDict.ElementAt(i);
            
                AddClosestBoxesToCircuit(element.Key);
            
                if(Circuits.Count() == 1 && Circuits[0].Count == Input.Count())
                {
                    break;
                }
            }

            int Pos1_X = (int)Input[element.Key.Item1].X;
            int Pos2_X = (int)Input[element.Key.Item2].X;

            Console.WriteLine("\nJunction box 1 X: " + Pos1_X);
            Console.WriteLine("\nJunction box 2 X: " + Pos2_X);

            Result = Pos1_X * Pos2_X;

            Console.WriteLine("\nResult: " + Result);
        }

        public static void SolvePart1()
        {
            Dictionary<Tuple<int, int>, Int64> DistancesForAllPairs = GetClosestPositionIndexes();
            var sortedDict = from entry in DistancesForAllPairs orderby entry.Value ascending select entry;

            for (int i = 0; 1000 > i; i++)
            {
                var element = sortedDict.ElementAt(i);

                AddClosestBoxesToCircuit(element.Key);
            }
            int count2 = Circuits.SelectMany(list => list).Distinct().Count();

            var sortedCircuits = Circuits.OrderByDescending(circuit => circuit.Count());

            for (int x = 0; 3 > x; x++)
            {
                Result *= sortedCircuits.ElementAt(x).Count();

                Console.Write(sortedCircuits.ElementAt(x).Count() + " ");
            }

            Console.WriteLine("\nResult: " + Result);
        }

        public static void AddClosestBoxesToCircuit(Tuple<int, int> BoxPair)
        {
            int box1 = BoxPair.Item1;
            int box2 = BoxPair.Item2;
            int InstancesCount = 0;
            int List_Index1 = -1;
            int List_Index2 = -1;

            foreach (List<int> circuit in Circuits)
            {
                if (circuit.Contains(box1) && circuit.Contains(box2))
                {
                    return;
                }
            }

            foreach (List<int> circuit in Circuits)
            {
                if (circuit.Contains(box1))
                {
                    List_Index1 = Circuits.IndexOf(circuit);
                    InstancesCount++;
                    continue;
                }
                if (circuit.Contains(box2))
                {
                    List_Index2 = Circuits.IndexOf(circuit);
                    InstancesCount++;
                    continue;
                }
            }
            if (InstancesCount >= 2)
            {
                Circuits[List_Index2].ForEach(p => Circuits[List_Index1].Add(p));

                Circuits.Remove(Circuits[List_Index2]);

                return;
            }

            foreach (List<int> circuit in Circuits)
            {
                if (circuit.Contains(box1) && !circuit.Contains(box2))
                {
                    circuit.Add(box2);
                    return;
                }
                if (circuit.Contains(box2) && !circuit.Contains(box1))
                {
                    circuit.Add(box1);
                    return;
                }
            }

            Circuits.Add(new List<int> { box1, box2 });
        }

        public static Dictionary<Tuple<int, int>, Int64> GetClosestPositionIndexes()
        {
            Dictionary<Tuple<int, int>, Int64> ClosestDistances = new Dictionary<Tuple<int, int>, Int64>();

            foreach (Vector3 position1 in Input)
            {
                foreach (Vector3 position2 in Input)
                {
                    if (position1 == position2) continue;
                    int index1 = Input.IndexOf(position1);
                    int index2 = Input.IndexOf(position2);

                    var ClosestPosIndex = new Tuple<int, int>(index1, index2);
                    var ClosestPosIndex2 = new Tuple<int, int>(index2, index1);

                    if (!ClosestDistances.ContainsKey(ClosestPosIndex) && !ClosestDistances.ContainsKey(ClosestPosIndex2))
                    {
                        float LengthBetweenPositions = GetDistanceMagnitude(Input[ClosestPosIndex.Item1], Input[ClosestPosIndex.Item2]);
                        ClosestDistances.Add(ClosestPosIndex, (Int64)LengthBetweenPositions);
                    }
                }
            }
            return ClosestDistances;
        }
        public static float GetDistanceMagnitude(Vector3 vector1, Vector3 vector2)
        {
            var distance = vector1 - vector2;
            return distance.Length();
        }

        public static void ReadInput()
        {
            StreamReader sr = new StreamReader(@"../../../Day08/Input.txt");
            while (line != null)
            {
                line = sr.ReadLine();

                if (line != null)
                {
                    string[] splitAxis = line.Split(',', StringSplitOptions.RemoveEmptyEntries);

                    Vector3 Position = new Vector3();

                    Position.X = float.Parse(splitAxis[0]);
                    Position.Y = float.Parse(splitAxis[1]);
                    Position.Z = float.Parse(splitAxis[2]);

                    Input.Add(Position);
                }

                Console.WriteLine(line);
            }
        }
    }
}
