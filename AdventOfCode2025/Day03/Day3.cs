using System;
using System.Collections.Generic;
using System.Numerics;
using System.Security;

namespace AOC
{
    public class Day3
    {
        public static string line = "";

        public static List<string> BaterieBanks = new List<string>();

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
            foreach (string bank in BaterieBanks)
            {
                string highestJoltage = "";
                GetHighestJoltageBetter(bank, 0, 12, ref highestJoltage);
                Console.WriteLine("\nBank: " + bank);
                Console.WriteLine("Joltage: " + highestJoltage);
                Result += long.Parse(highestJoltage);
            }

            Console.WriteLine("\nResult: " + Result);
        }

        public static void SolvePart1()
        {
            foreach (string bank in BaterieBanks)
            {
                Result += GetHighestJoltage(bank);
            }

            Console.WriteLine("\n Result: " + Result);
        }

        public static Int64 GetHighestJoltage(string bank)
        {
            int Joltage = int.Parse(bank.Substring(0, 2));

            for (int i = 0; i < bank.Length; i++)
            {
                char batt1 = bank[i];
                string temp = "";
                temp += batt1;

                for (int j = i + 1; j < bank.Length; j++)
                {
                    char batt2 = bank[j];
                    temp += batt2;

                    int newJoltage = int.Parse(temp);
                    if (newJoltage > Joltage)
                    {
                        Joltage = newJoltage;
                    }
                    temp = temp.Remove(temp.Length - 1);
                }
            }

            Console.WriteLine("\nBank: " + bank);
            Console.WriteLine("Joltage: " + Joltage);

            return Joltage;
        }

        public static void GetHighestJoltageBetter(string bank, int startingIndex, int maxBatteries, ref string highestJoltage)
        {
            if(maxBatteries <= 0)
            {
                return;
            }
            string Joltage = bank.Substring(0, 12);

            int currentBattery = 0;

            for (int i = startingIndex; i < bank.Length; i++)
            {
                int temp = bank[i] - '0';

                if (temp > currentBattery && bank.Length - i >= maxBatteries)
                {
                    currentBattery = temp;
                    startingIndex = i;
                }
            }

            highestJoltage += currentBattery;
            GetHighestJoltageBetter(bank, startingIndex + 1, maxBatteries - 1, ref highestJoltage);
        }

        public static void ReadInput()
        {
            StreamReader sr = new StreamReader(@"../../../Day03/Input.txt");
            while (line != null)
            {
                line = sr.ReadLine();

                if (line != null)
                {
                    BaterieBanks.Add(line);
                }

                Console.WriteLine(line);
            }
        }
    }
}
