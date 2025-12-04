using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Security;
using static System.Net.Mime.MediaTypeNames;

namespace AOC
{
    public class Day4
    {
        public static string line = "";

        public static char[,] Input;

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
            char[,] InputNew = new char[Input.GetLength(0), Input.GetLength(1)];
            Array.Copy(Input, InputNew, Input.Length);

            RecusiveRemoveRolls(ref InputNew);

            Console.WriteLine("\nResult: " + Result);
        }

        public static void SolvePart1()
        {
            for (int y = 0; y < Input.GetLength(1); y++)
            {
                for (int x = 0; x < Input.GetLength(0); x++)
                {
                    if (Input[x, y] == '.') continue;

                    if (CheckAdjacentRollsCount(x, y) < 4)
                    {
                        Result++;
                    }
                }
            }

            Console.WriteLine("\nResult: " + Result);
        }

        public static void RecusiveRemoveRolls(ref char[,] InputNew)
        {
            int ChangeAmount = 0;
            for (int y = 0; y < Input.GetLength(1); y++)
            {
                for (int x = 0; x < Input.GetLength(0); x++)
                {
                    if (Input[x, y] == '.') continue;

                    if (CheckAdjacentRollsCount(x, y) < 4)
                    {
                        InputNew[x,y] = '.';
                        ChangeAmount++;
                        Result++;
                    }
                }
            }

            if(ChangeAmount > 0)
            {
                Input = InputNew;
                RecusiveRemoveRolls(ref InputNew);
            }
        }

        public static int CheckAdjacentRollsCount(int x, int y)
        {
            char current = Input[x, y];
            string adjacent = "";
            // Right
            if (x + 1 < Input.GetLength(0))
            {
                char right = Input[x + 1, y];
                adjacent += right;
            }
            // Down-Right
            if (x + 1 < Input.GetLength(0) && y + 1 < Input.GetLength(1))
            {
                char downRight = Input[x + 1, y + 1];
                adjacent += downRight;
            }
            // Down
            if (y + 1 < Input.GetLength(1))
            {
                char down = Input[x, y + 1];
                adjacent += down;
            }
            // Down-Left
            if (x - 1 >= 0 && y + 1 < Input.GetLength(1))
            {
                char downLeft = Input[x - 1, y + 1];
                adjacent += downLeft;
            }
            // Left
            if (x - 1 >= 0)
            {
                char left = Input[x - 1, y];
                adjacent += left;
            }
            // Up-Left
            if (x - 1 >= 0 && y - 1 >= 0)
            {
                char upLeft = Input[x - 1, y - 1];
                adjacent += upLeft;
            }
            // Up
            if (y - 1 >= 0)
            {
                char up = Input[x, y - 1];
                adjacent += up;
            }
            // Up-Right
            if (x + 1 < Input.GetLength(0) && y - 1 >= 0)
            {
                char upRight = Input[x + 1, y - 1];
                adjacent += upRight;
            }

            return adjacent.Split("@").Length - 1;
        }

        public static void ReadInput()
        {
            string[] sr = File.ReadAllLines(@"../../../Day04/Input.txt");

            if (sr != null)
            {
                Input = new char[sr[0].Length, sr.Length];

                int x = 0;

                for (int y = 0; y < sr.Length; y++)
                {
                    foreach (char s in sr[y])
                    {
                        Input[x, y] = s;
                        x++;
                        Console.Write(s);
                    }
                    x = 0;
                    Console.Write("\n");
                }
            }
        }
    }
}
