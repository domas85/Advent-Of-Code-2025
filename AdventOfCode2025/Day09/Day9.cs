using System.Numerics;

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
                    if (IsRectangleWithinBorder_AxisAligned(Input[i], Input[j], AllPerpendicularBorderEdges))
                    {
                        var area = GetAreaBetweenTwoPoints(Input[i], Input[j]);

                        if (area > BiggestArea)
                        {
                            BiggestArea = area;
                            BiggestSquere = new Tuple<Vector2, Vector2>(Input[i], Input[j]);
                        }
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

        public static bool IsRectangleWithinBorder_AxisAligned(Vector2 pos1, Vector2 pos2, List<Tuple<Vector2, Vector2>> borderEdges)
        {
            if (pos1.X == pos2.X || pos1.Y == pos2.Y)
            {
                return true;
            }

            float minX = MathF.Min(pos1.X, pos2.X);
            float maxX = MathF.Max(pos1.X, pos2.X);
            float minY = MathF.Min(pos1.Y, pos2.Y);
            float maxY = MathF.Max(pos1.Y, pos2.Y);

            // Corners
            var corners = new Vector2[]
            {
                new Vector2(minX, minY),
                new Vector2(minX, maxY),
                new Vector2(maxX, minY),
                new Vector2(maxX, maxY)
            };

            // Check that all corners are inside or on the border
            foreach (var c in corners)
            {
                int pip = PointInPolygon(c, borderEdges, float.Epsilon);
                if (pip < 0) // outside
                {
                    return false;
                }
            }

            // Check that no border edge crosses the rectangle interior
            foreach (var edge in borderEdges)
            {
                var a = edge.Item1;
                var b = edge.Item2;

                // vertical edge
                if (MathF.Abs(a.X - b.X) < float.Epsilon)
                {
                    float ex = a.X;
                    float eMinY = MathF.Min(a.Y, b.Y);
                    float eMaxY = MathF.Max(a.Y, b.Y);

                    // inside in x
                    if (ex > minX + float.Epsilon && ex < maxX - float.Epsilon)
                    {
                        float overlapMinY = MathF.Max(eMinY, minY);
                        float overlapMaxY = MathF.Min(eMaxY, maxY);

                        // overlap length strictly positive -> edge crosses rectangle interior
                        if (overlapMaxY - overlapMinY > float.Epsilon)
                        {
                            return false;
                        }
                    }
                }
                else // horizontal edge
                {
                    float ey = a.Y;
                    float eMinX = MathF.Min(a.X, b.X);
                    float eMaxX = MathF.Max(a.X, b.X);

                    if (ey > minY + float.Epsilon && ey < maxY - float.Epsilon)
                    {
                        float overlapMinX = MathF.Max(eMinX, minX);
                        float overlapMaxX = MathF.Min(eMaxX, maxX);
                        if (overlapMaxX - overlapMinX > float.Epsilon)
                        {
                            return false;
                        }
                    }
                }
            }

            return true;
        }

        private static int PointInPolygon(Vector2 p, List<Tuple<Vector2, Vector2>> edges, float eps)
        {
            // check boundary first
            foreach (var edge in edges)
            {
                var a = edge.Item1;
                var b = edge.Item2;

                if (MathF.Abs(a.X - b.X) < eps) // vertical
                {
                    if (MathF.Abs(p.X - a.X) < eps)
                    {
                        float ymin = MathF.Min(a.Y, b.Y) - eps;
                        float ymax = MathF.Max(a.Y, b.Y) + eps;
                        if (p.Y >= ymin && p.Y <= ymax) return 0;
                    }
                }
                else // horizontal
                {
                    if (MathF.Abs(p.Y - a.Y) < eps)
                    {
                        float xmin = MathF.Min(a.X, b.X) - eps;
                        float xmax = MathF.Max(a.X, b.X) + eps;
                        if (p.X >= xmin && p.X <= xmax) return 0;
                    }
                }
            }

            // ray casting
            int count = 0;
            foreach (var edge in edges)
            {
                var a = edge.Item1;
                var b = edge.Item2;

                if (MathF.Abs(a.X - b.X) < eps) // vertical edge
                {
                    float ex = a.X;
                    float ymin = MathF.Min(a.Y, b.Y);
                    float ymax = MathF.Max(a.Y, b.Y);

                    // include ymin, exclude ymax to avoid double count at vertices
                    if (ex > p.X + eps && p.Y >= ymin && p.Y < ymax)
                        count++;
                }
            }

            return (count % 2 == 1) ? 1 : -1;
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
            StreamReader sr = new StreamReader(@"../../../Day09/Input.txt");
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
