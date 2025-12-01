using System;

namespace AOC
{
    public class Day1
    {
        public static string line = "";

        public static void Run()
        {
            Console.Write("Starting...");
            ReadInput();
            SolvePart1();
            SolvePart2();
        }

        public static void SolvePart2()
        {
        
        }

        public static void SolvePart1()
        {
   
        }

        public static void ReadInput()
        {
            StreamReader sr = new StreamReader("C:\\files\\projects\\Advent-Of-Code-2025\\AdventOfCode2025\\Day1\\Input.txt");
            while (line != null)
            {
                line = sr.ReadLine();

                Console.WriteLine("\n " + line);
            }
        }
    }
}
