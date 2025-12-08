using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Security;
using static System.Net.Mime.MediaTypeNames;

namespace AOC
{
    public class Day7
    {
        public static string line = "";

        public static string[,] Input;


        public static Int64 Result;

        public static int MergesCount;

        public static void Run()
        {
            Console.Write("Starting... \n");
            ReadInput();
            //SolvePart1();
            SolvePart2();
        }

        public static void SolvePart2()
        {
            bool FoundStart = false;
            for (int y = 0; y < Input.GetLength(1); y++)
            {
                if (FoundStart) break;

                for (int x = 0; x < Input.GetLength(0); x++)
                {
                    if (Input[x, y] == "S")
                    {
                        QuantumTachyonBeams(x, y);
                        FoundStart = true;
                        break;
                    }
                }
            }

            for (int x = 0; x < Input.GetLength(0); x++)
            {
                if(Input[x, Input.GetLength(1) - 1] != ".")
                {
                    Result += Int64.Parse(Input[x, Input.GetLength(1) - 1]);
                }
            }

            PrintInput();
            Console.WriteLine("\nResult: " + Result);
        }

        public static void SolvePart1()
        {
            for (int y = 0; y < Input.GetLength(1); y++)
            {
                for (int x = 0; x < Input.GetLength(0); x++)
                {
                    if (Input[x, y] == "S")
                    {
                        TachyonBeams(x, y);
                    }
                }
            }
            PrintInput();
            Console.WriteLine("\nResult: " + Result);
        }

        public static void QuantumTachyonBeams(int StartX, int StartY)
        {
            Input[StartX, StartY + 1] = "1";
            for (int y = StartY + 1; y < Input.GetLength(1); y++)
            {
                for (int x = 0; x < Input.GetLength(0); x++)
                {
                    var current = Input[x, y];
                    if (current != "." && y + 1 <= Input.GetLength(0))
                    {
                        var Bellow = Input[x, y + 1];

                        if (Bellow != "^" && Bellow != ".")
                        {
                            Input[x, y + 1] = (Int64.Parse(current) + Int64.Parse(Bellow)).ToString();
                            continue;
                        }

                        if (Bellow == "." && current != "^")
                        {
                            Input[x, y + 1] = current;
                            continue;
                        }
                        if (Bellow == "^" && current != "^")
                        {
                            var right = Input[x + 1, y + 1];
                            var left = Input[x - 1, y + 1];

                            if (left != ".")
                            {
                                Input[x - 1, y + 1] = (Int64.Parse(current) + Int64.Parse(left)).ToString();
                            }
                            if (right != ".")
                            {
                                Input[x + 1, y + 1] = (Int64.Parse(current) + Int64.Parse(right)).ToString();
                            }

                            if (left == ".")
                            {
                                Input[x - 1, y + 1] = current;
                            }
                            if (right == ".")
                            {
                                Input[x + 1, y + 1] = current;
                            }
                        }
                    }
                }
            }
        }

        public static void TachyonBeams(int x, int y)
        {
            while (y + 1 < Input.GetLength(1) && Input[x, y + 1] != "^")
            {
                y++;
                Input[x, y] = "|";
            }

            if (y + 1 < Input.GetLength(1) && x + 1 < Input.GetLength(0) && Input[x + 1, y + 1] != "|")
            {
                Input[x + 1, y + 1] = "|";
                TachyonBeams(x + 1, y);
            }

            if (y + 1 < Input.GetLength(1) && x - 1 >= 0 && Input[x - 1, y + 1] != "|")
            {
                Result++;
                Input[x - 1, y + 1] = "|";
                TachyonBeams(x - 1, y);
            }
        }

        public static void PrintInput()
        {
            for (int y = 0; y < Input.GetLength(1); y++)
            {
                for (int x = 0; x < Input.GetLength(0); x++)
                {
                    Console.Write(Input[x, y]);
                    Console.Write(' ');
                }
                Console.Write(' ');
                Console.Write(y);
                Console.Write("\n");
            }
            Console.Write("\n");
        }

        public static void ReadInput()
        {
            string[] sr = File.ReadAllLines(@"../../../Day07/Input.txt");

            if (sr != null)
            {
                Input = new string[sr[0].Length, sr.Length];

                int x = 0;

                for (int y = 0; y < sr.Length; y++)
                {
                    foreach (char s in sr[y])
                    {
                        Input[x, y] = s.ToString();
                        x++;
                        Console.Write(s);
                    }
                    x = 0;
                    Console.Write("\n");
                }
            }
            Console.Write("\n");
        }
    }
}
