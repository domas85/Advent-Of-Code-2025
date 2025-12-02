using System;
using System.Collections.Generic;
using System.Numerics;
using System.Security;

namespace AOC
{
    public class Day2
    {
        public static string line = "";

        public static List<Tuple<long, long>> ID_Ranges = new List<Tuple<long, long>>();

        public static long Result;

        public static void Run()
        {
            Console.Write("Starting... \n");
            ReadInput();
            //SolvePart1();
            SolvePart2();
        }

        public static void SolvePart2()
        {
            foreach (Tuple<long, long> ID_Range in ID_Ranges)
            {
                long limit = ID_Range.Item1 + ID_Range.Item2;

                long ID = ID_Range.Item1;

                while (ID <= ID_Range.Item2)
                {
                    CheckIDWithExtraRules(ID);

                    ID++;
                }

            }

            Console.WriteLine("\n Result: " + Result);
        }

        public static void SolvePart1()
        {
            foreach (Tuple<long, long> ID_Range in ID_Ranges)
            {
                long limit = ID_Range.Item1 + ID_Range.Item2;

                long ID = ID_Range.Item1;

                while (ID <= ID_Range.Item2)
                {
                    CheckIDRules(ID);

                    ID++;
                }

            }

            Console.WriteLine("\n Result: " + Result);
        }

        public static void CheckIDRules(long ID)
        {
            string ID_String = ID.ToString();

            if(ID_String.Length <= 1)
            {
                return;
            }

            float stringLenght = ID_String.Length;

            IEnumerable<string> splitString = Split(ID_String, (int)Math.Ceiling(stringLenght / 2f));

            var stringArray = splitString.ToArray();

            if (stringArray[0].Contains(stringArray[1]) && stringArray[1].Contains(stringArray[0]))
            {
                Console.WriteLine("\n Invalid ID: " + ID_String);

                Result += ID;
            }
        }

        public static void CheckIDWithExtraRules(long ID)
        {
            string ID_String = ID.ToString();

            if (ID_String.Length <= 1)
            {
                return;
            }

            float stringLenght = ID_String.Length;

            IEnumerable<string> splitString = Split(ID_String, (int)Math.Ceiling(stringLenght / 2f));

            var stringArray = splitString.ToArray();

            if (stringArray[0].Contains(stringArray[1]) && stringArray[1].Contains(stringArray[0]))
            {
                Console.WriteLine("\n Invalid ID: " + ID_String);

                Result += ID;
                return;
            }

            CheckRepeatingSequences(ID);
        }

        public static void CheckRepeatingSequences(long ID)
        {
            string ID_String = ID.ToString();

            string Sequence = "";

            bool valid = true;

            for (int i = 0; i < ID_String.Length - 1 ; i++)
            {
                Sequence += ID_String[i];

                if(Sequence.Length > 0)
                {
                    int count = ID_String.Split(Sequence).Length - 1;

                    if(count > 1 && Sequence.Length * count == ID_String.Length)
                    {
                        valid = false;
                    }
                }
            }

            if(!valid)
            {
                Console.WriteLine("\n Invalid ID: " + ID_String);

                Result += ID;
                return;
            }
        }

        public static void ReadInput()
        {
            StreamReader sr = new StreamReader("C:\\files\\projects\\Advent-Of-Code-2025\\AdventOfCode2025\\Day02\\Input.txt");
            while (line != null)
            {
                line = sr.ReadLine();

                if (line != null)
                {
                    string[] splitIds = line.Split(',', StringSplitOptions.RemoveEmptyEntries);

                    for (int i = 0; i < splitIds.Length; i++)
                    {
                        string[] numberRange = splitIds[i].Split("-", StringSplitOptions.RemoveEmptyEntries);

                        ID_Ranges.Add(new Tuple<long, long>(long.Parse(numberRange[0]), long.Parse(numberRange[1])));
                    }
                }

                Console.WriteLine(line);
            }
        }

        static IEnumerable<string> Split(string str, int Amount)
        {
            while (str.Length > 0)
            {
                yield return new string(str.Take(Amount).ToArray());
                str = new string(str.Skip(Amount).ToArray());
            }
        }

        public static IEnumerable<long> CreateRange(long start, long count)
        {
            var limit = start + count;

            while (start < limit)
            {
                yield return start;
                start++;
            }
        }
    }
}
