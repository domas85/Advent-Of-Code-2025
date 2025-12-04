using System;
using System.Security;

namespace AOC
{
    public class Day1
    {
        public static string line = "";

        public static List<char> inputRotation = new List<char>();
        public static List<Int32> inputDistance = new List<Int32>();

        public static bool wasZero = false;

        public static Int64 currentRotation = 50;
        public static Int64 result = 0;
        public static bool wasAdded = false;

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
                wasAdded = false;
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
                    wasAdded = true;
                    currentRotation += inputDistance[i];
                }
                if (inputRotation[i] == 'L')
                {
                    currentRotation -= inputDistance[i];
                }

                if (currentRotation == 100)
                {
                    currentRotation = 0;
                }

                CountRotations();


                if (currentRotation == 0)
                {
                    result++;
                }

                Console.WriteLine("\n Input: " + inputRotation[i] + inputDistance[i]);
                Console.WriteLine("\n Current Rotation: " + currentRotation);
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
            bool wasModified = false;
            if (currentRotation < -99 || currentRotation > 99)
            {
                string tempString = currentRotation.ToString();
                string rotationCount = tempString.Substring(0, tempString.Length - 2);

                result += int.Abs(int.Parse(rotationCount));
                currentRotation = currentRotation - (100 * int.Parse(rotationCount));
                wasModified = true;
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
                if (currentRotation == 0 && wasAdded)
                {
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
                if (wasModified)
                {
                    result++;
                }
            }
        }

        public static void ReadInput()
        {
            StreamReader sr = new StreamReader(@"../../../Day01/Input.txt");
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
