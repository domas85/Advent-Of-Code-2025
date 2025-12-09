using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Security;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace AOC
{
    public class Day9
    {
        public static string line = "";

        public static List<Vector2> Input = new List<Vector2>();

        public static List<Tuple<Vector2, Vector2>> AllPerpendicularBorderEdges = new List<Tuple<Vector2, Vector2>>();

        public static Int64 BiggestArea = 0;

        public static Int64 Result = 0;

        public static void Run()
        {
            Console.Write("Starting... \n");
            ReadInput();
            //SolvePart1();
            SolvePart2();
        }

        public static void SolvePart2()
        {
            AllPerpendicularBorderEdges = GetPerpendicularPoints();

            Tuple<Vector2, Vector2> BiggestSquere = new Tuple<Vector2, Vector2>(new Vector2(), new Vector2());
            for (int i = 0; i < Input.Count; i++)
            {
                for (int j = i + 1; j < Input.Count; j++)
                {
                    if (CheckWithinShape(Input[i], Input[j]))
                    {

                    }

                    var area = GetAreaBetweenTwoPoints(Input[i], Input[j]);

                    if (area > BiggestArea)
                    {
                        BiggestArea = area;
                        BiggestSquere = new Tuple<Vector2, Vector2>(Input[i], Input[j]);
                    }
                }
            }

            Result = BiggestArea;
            Console.WriteLine("\nResult: " + Result);
        }

        public static void SolvePart1()
        {
            Tuple<Vector2, Vector2> BiggestSquere = new Tuple<Vector2, Vector2>(new Vector2(), new Vector2());
            for (int i = 0; i < Input.Count; i++)
            {
                for (int j = i + 1; j < Input.Count; j++)
                {
                    var area = GetAreaBetweenTwoPoints(Input[i], Input[j]);
                    if (area > BiggestArea)
                    {
                        BiggestArea = area;
                        BiggestSquere = new Tuple<Vector2, Vector2>(Input[i], Input[j]);
                    }
                }
            }

            Result = BiggestArea;
            Console.WriteLine("\nResult: " + Result);
        }

        public static bool CheckWithinShape(Vector2 pos1, Vector2 pos2)
        {
            if (pos1.X == pos2.X || pos1.Y == pos2.Y)
            {
                return true;
            }
            List<Tuple<Vector2, Vector2>> edges = new List<Tuple<Vector2, Vector2>>();

            edges.Add(new Tuple<Vector2, Vector2>(new Vector2(pos1.X, pos1.Y), new Vector2(pos1.X, pos2.Y)));
            edges.Add(new Tuple<Vector2, Vector2>(new Vector2(pos1.X, pos1.Y), new Vector2(pos2.X, pos1.Y)));


            edges.Add(new Tuple<Vector2, Vector2>(new Vector2(pos2.X, pos2.Y), new Vector2(pos1.X, pos2.Y)));
            edges.Add(new Tuple<Vector2, Vector2>(new Vector2(pos2.X, pos2.Y), new Vector2(pos2.X, pos1.Y)));

            return false;
        }

        public static List<Tuple<Vector2, Vector2>> GetPerpendicularPoints()
        {
            List<Tuple<Vector2, Vector2>> Points = new List<Tuple<Vector2, Vector2>>();

            Vector2 firstPoint = Input[0];

            for (int i = 0; i < Input.Count; i++)
            {
                for (int j = i + 1; j < Input.Count; j++)
                {
                    if (Points.Contains(new Tuple<Vector2, Vector2>(Input[i], Input[j])) || Points.Contains(new Tuple<Vector2, Vector2>(Input[j], Input[i]))) continue;

                    if (Input[i].X == Input[j].X || Input[i].Y == Input[j].Y)
                    {
                        Points.Add(new Tuple<Vector2, Vector2>(Input[i], Input[j]));
                        break;
                    }
                }
            }
            Points.Add(new Tuple<Vector2, Vector2>(Points.Last().Item2, firstPoint));

            return Points;
        }

        public static Int64 GetAreaBetweenTwoPoints(Vector2 pos1, Vector2 pos2)
        {
            var distance = pos1 - pos2;
            Int64 area = (Int64)(MathF.Abs(distance.X) + 1) * (Int64)(MathF.Abs(distance.Y) + 1);

            return area;
        }

        public static void ReadInput()
        {
            StreamReader sr = new StreamReader(@"../../../Day09/SampleInput.txt");
            while (line != null)
            {
                line = sr.ReadLine();

                if (line != null)
                {
                    string[] splitAxis = line.Split(',', StringSplitOptions.RemoveEmptyEntries);

                    Input.Add(new Vector2(int.Parse(splitAxis[0]), int.Parse(splitAxis[1])));
                }

                Console.WriteLine(line);
            }
        }
    }
}
