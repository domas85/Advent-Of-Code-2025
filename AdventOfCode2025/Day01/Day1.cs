using System;
using System.Security;

namespace AOC
{
    public class Day1
    {
        public static string line = "";

        public static List<char> inputRotation = new List<char>();
        public static List<int> inputDistance = new List<int>();

        public static bool wasZero = false;

        public static Int32 currentRotation = 50;
        public static Int32 result = 0;

        public static void Run()
        {
            Console.Write("Starting...");
            ReadInput();
            //SolvePart1();
            SolvePart2();
        }

        public static void SolvePart2()
        {
            Console.WriteLine("\n Starting Rotation: " + currentRotation);
            Console.WriteLine("\n");

            for (int i = 0; i < inputRotation.Count; i++)
            {
                if (currentRotation == 0)
                {
                    wasZero = true;
                }
                else
                {
                    wasZero = false;
                }

                if (inputRotation[i] == 'R')
                {
                    currentRotation += inputDistance[i];
                }
                if (inputRotation[i] == 'L')
                {
                    currentRotation -= inputDistance[i];
                }

                if(currentRotation == 100)
                {
                    currentRotation = 0;
                }

                CountRotations();

                //if (wasZero && currentRotation > 100 || wasZero && currentRotation < -100)
                //{
                //    wasZero = false;
                //}

                //if (currentRotation == 100)
                //{
                //    wasZero = true;
                //    currentRotation = 0;
                //}
                //else
                //{
                //    ClampBackToLockAmountWithRotationCount();
                //}

                Console.WriteLine("\n Input: " + inputRotation[i] + inputDistance[i]);
                Console.WriteLine("\n Current Rotation: " + currentRotation);

                if (currentRotation == 0)
                {
                    wasZero = true;
                    result++;
                }
                Console.WriteLine("\n Current Result Value: " + result);
                Console.WriteLine("\n");
            }
            Console.WriteLine("\n " + result);
        }

        public static void SolvePart1()
        {
            Console.WriteLine("\n Starting Rotation: " + currentRotation);
            Console.WriteLine("\n");

            for (int i = 0; i < inputRotation.Count; i++)
            {
                if (inputRotation[i] == 'R')
                {
                    currentRotation += inputDistance[i];
                }
                if (inputRotation[i] == 'L')
                {
                    currentRotation -= inputDistance[i];
                }
                ClampBackToLockAmount();
                Console.WriteLine("\n Input: " + inputRotation[i] + inputDistance[i]);
                Console.WriteLine("\n Current Rotation: " + currentRotation);

                if (currentRotation == 0)
                {
                    result++;
                }
                Console.WriteLine("\n Current Result Value: " + result);
                Console.WriteLine("\n");
            }
            Console.WriteLine("\n " + result);
        }

        public static void ClampBackToLockAmountWithRotationCount()
        {
            while (currentRotation < 0 || currentRotation > 99)
            {
                if (currentRotation == 100)
                {
                    currentRotation = 0;
                }

                if (currentRotation < 0)
                {
                    if (wasZero)
                    {
                        wasZero = false;
                        result--;
                    }
                    result++;
                    currentRotation = 100 + currentRotation;
                }

                if (currentRotation > 99)
                {
                    if (wasZero)
                    {
                        wasZero = false;
                        result--;
                    }
                    result++;
                    currentRotation = currentRotation - 100;
                }
            }
        }

        public static void ClampBackToLockAmount()
        {
            while (currentRotation < 0 || currentRotation > 99)
            {
                if (currentRotation == 100)
                {
                    currentRotation = 0;
                }

                if (currentRotation < 0)
                {
                    currentRotation = 100 + currentRotation;
                }

                if (currentRotation > 99)
                {
                    currentRotation = currentRotation - 100;
                }
            }
        }

        public static void CountRotations()
        {
            if (currentRotation < -99 || currentRotation > 99)
            {
                string tempString = currentRotation.ToString();
                string rotationCount = tempString.Substring(0, tempString.Length - 2);

                result += int.Abs(int.Parse(rotationCount));

                currentRotation = currentRotation - (100 * int.Parse(rotationCount));
                if (wasZero && currentRotation == 0)
                {
                    wasZero = false;
                    result--;
                }
            }

            if (currentRotation < 0)
            {
                if (wasZero)
                {
                    wasZero = false;
                    result--;
                }
                result++;
                currentRotation = 100 + currentRotation;
            }
        }

        public static void ReadInput()
        {
            StreamReader sr = new StreamReader("C:\\files\\projects\\Advent-Of-Code-2025\\AdventOfCode2025\\Day01\\Input.txt");
            while (line != null)
            {
                line = sr.ReadLine();

                if (line != null && line.Contains("L"))
                {
                    inputRotation.Add('L');
                }
                if (line != null && line.Contains("R"))
                {
                    inputRotation.Add('R');
                }
                if (line != null)
                {
                    string distanceString = line.Replace("L", "").Replace("R", "");
                    int distance = Int32.Parse(distanceString);
                    inputDistance.Add(distance);
                }

                Console.WriteLine("\n " + line);
            }
        }
    }
}
