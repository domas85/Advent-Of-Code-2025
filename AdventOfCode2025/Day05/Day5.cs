using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Security;
using static System.Net.Mime.MediaTypeNames;

namespace AOC
{
    public class Day5
    {
        public static string line = "";

        public static List<Tuple<long, long>> ID_Ranges = new List<Tuple<long, long>>();

        public static List<long> IDs = new List<long>();

        public static Int64 Result;

        public static void Run()
        {
            Console.Write("Starting... \n");
            ReadInput();
            //SolvePart1();
            SolvePart2();
        }

        public static void SolvePart2()
        {
            List<Tuple<long, long>> SortedList = ID_Ranges.OrderBy(o => o.Item1).ToList();

            long CurrentHighestMax = 0;
            bool wasInvalid = false;

            for (int i = 0; i < SortedList.Count; i++)
            {
                // skip invalid ids, for ex. 10-5
                if (SortedList[i].Item1 > SortedList[i].Item2)
                {
                    wasInvalid = true;
                    continue;
                }

                if (i - 1 >= 0 && SortedList[i - 1].Item2 >= CurrentHighestMax)
                {
                    CurrentHighestMax = SortedList[i - 1].Item2;
                }

                if (i - 1 >= 0 && SortedList[i - 1].Item2 >= SortedList[i].Item1)
                {
                    // skip if current ids max is <= Highest known Id max
                    if (SortedList[i].Item2 <= CurrentHighestMax)
                    {
                        wasInvalid = false;
                        continue;
                    }

                    long FreshID_CountNew = SortedList[i].Item2 - CurrentHighestMax;

                    Result += FreshID_CountNew;
                    wasInvalid = false;
                    continue;
                }
                // skip if previous case was invalid, for ex. 5-10
                if (wasInvalid)
                {
                    long FreshID_CountReal = SortedList[i].Item2 - SortedList[i].Item1;
                    wasInvalid = false;
                    Result += FreshID_CountReal;
                    continue;
                }

                long FreshID_Count = SortedList[i].Item2 - SortedList[i].Item1 + 1;
                wasInvalid = false;

                Result += FreshID_Count;
            }
            Console.WriteLine("\nResult: " + Result);
        }

        public static void SolvePart1()
        {
            foreach (long ID in IDs)
            {
                if (IsFresh(ID))
                {
                    Result++;
                    Console.WriteLine("ID: " + ID + " is Fresh");
                }
                else
                {

                    Console.WriteLine("ID: " + ID + " is not Fresh");
                }
            }

            Console.WriteLine("\nResult: " + Result);
        }

        public static bool IsFresh(long ID)
        {
            foreach (Tuple<long, long> ID_Range in ID_Ranges)
            {
                if (ID >= ID_Range.Item1 && ID <= ID_Range.Item2)
                {
                    return true;
                }
            }
            return false;
        }

        public static void ReadInput()
        {
            string[] sr = File.ReadAllLines(@"../../../Day05/Input.txt");

            int idsIndex = 0;
            for (int i = 0; i < sr.Length; i++)
            {
                idsIndex = i;
                if (sr[i] == "")
                {
                    break;
                }

                string[] idRange = sr[i].Split("-", StringSplitOptions.RemoveEmptyEntries);

                ID_Ranges.Add(new Tuple<long, long>(long.Parse(idRange[0]), long.Parse(idRange[1])));
            }

            for (int i = idsIndex + 1; i < sr.Length; i++)
            {
                IDs.Add(long.Parse(sr[i]));
            }
        }
    }
}
