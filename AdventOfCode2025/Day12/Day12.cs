using System.Numerics;
using System.Linq;

namespace AOC
{
    public class Day12
    {
        public static string line = "";

        public const int SHAPE_SIZE = 3;

        public const int MAX_SHAPE_SIZE = 50;

        public static List<char[,]> Shapes = new List<char[,]>();

        public static List<Tuple<int, int>> TreeRegionSizes = new List<Tuple<int, int>>();

        public static Dictionary<string, List<int[,]>> MemoShapes = new Dictionary<string, List<int[,]>>();

        public static List<int[]> TreeRegionIndexCounts = new List<int[]>();

        public static Int64 Result = 0;

        public static void Run()
        {
            Console.Write("Starting... \n");
            ReadInput();
            SolvePart1();
            //SolvePart2();
        }

        public static void SolvePart2()
        {

        }

        public static void SolvePart1()
        {
            var index = 0;
            foreach (Tuple<int, int> regionSize in TreeRegionSizes)
            {
                char[,] region = new char[regionSize.Item1, regionSize.Item2];
                for (int i = 0; i < regionSize.Item1; i++)
                {
                    for (int j = 0; j < regionSize.Item2; j++)
                    {
                        region[i, j] = '.';
                    }
                }

                var shapesIndexCounts = TreeRegionIndexCounts[index];
                if (CanCompactToRegion(region, shapesIndexCounts))
                {
                    Result++;
                }
            }

            Console.WriteLine("\n Result: " + Result);
        }

        public static bool CanCompactToRegion(char[,] region, int[] ShapesIndexCounts)
        {
            // check if all shapes have been used
            if (ShapesIndexCounts.All(count => count == 0))
            {
                return true;
            }

            int largestCount = ShapesIndexCounts.Max();
            int maxIndex = ShapesIndexCounts.ToList().IndexOf(largestCount);

            while (ShapesIndexCounts.Sum() > 0)
            {
                
               
                break;

            }


            return false;

        }

        public static List<char[,]> GetAllShapeVariations(char[,] shape)
        {
            List<char[,]> variations = new List<char[,]>();
            char[,] currentShape = shape;
            for (int i = 0; i < 4; i++)
            {
                currentShape = RotateShape(currentShape);
                variations.Add(currentShape);
                variations.Add(FlipShape(currentShape));
            }
            return variations;
        }

        public static bool CanPlaceShape(char[,] region, char[,] shape, int startRow, int startCol)
        {
            for (int i = 0; i < shape.GetLength(0); i++)
            {
                for (int j = 0; j < shape.GetLength(1); j++)
                {
                    if (region[startRow + j, startCol + i] != '.')
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public static void PlaceShape(char[,] region, char[,] shape, int startRow, int startCol, char fillChar)
        {
            for (int i = 0; i < SHAPE_SIZE; i++)
            {
                for (int j = 0; j < SHAPE_SIZE; j++)
                {
                    if (shape[i, j] == '#')
                    {
                        region[startRow + i, startCol + j] = fillChar;
                    }
                }
            }
        }


        public static char[,] RotateShape(char[,] shape)
        {
            char[,] rotated = new char[SHAPE_SIZE, SHAPE_SIZE];
            for (int i = 0; i < SHAPE_SIZE; i++)
            {
                for (int j = 0; j < SHAPE_SIZE; j++)
                {
                    rotated[j, SHAPE_SIZE - 1 - i] = shape[i, j];
                }
            }
            return rotated;
        }

        public static char[,] FlipShape(char[,] shape)
        {
            char[,] flipped = new char[SHAPE_SIZE, SHAPE_SIZE];
            for (int i = 0; i < SHAPE_SIZE; i++)
            {
                for (int j = 0; j < SHAPE_SIZE; j++)
                {
                    flipped[i, SHAPE_SIZE - 1 - j] = shape[i, j];
                }
            }
            return flipped;
        }

        public static void PrintShape(char[,] shape)
        {
            for (int i = 0; i < shape.GetLength(0); i++)
            {
                for (int j = 0; j < shape.GetLength(1); j++)
                {
                    Console.Write(shape[i, j]);
                }
                Console.WriteLine();
            }
            Console.WriteLine("\n");
        }

        public static void ReadInput()
        {
            StreamReader sr = new StreamReader(@"../../../Day12/Input.txt");

            int shapeIndex = -1;
            int rowIndex = 0;

            while (line != null)
            {
                line = sr.ReadLine();
                if (line != null)
                {
                    if (line.Contains('x'))
                    {
                        string[] splitLine = line.Split(" ", StringSplitOptions.RemoveEmptyEntries);

                        // Get tree region size
                        string sizeString = splitLine[0].TrimEnd('x', ':');
                        int[] size = sizeString.Split('x').Select(s => Int32.Parse(s)).ToArray();
                        Tuple<int, int> sizeTuple = new Tuple<int, int>(size[0], size[1]);

                        // Get tree region preset indices counts
                        var PresentList = splitLine.ToList();
                        PresentList.RemoveAt(0);
                        int[] presets = PresentList.Select(s => Int32.Parse(s)).ToArray();

                        TreeRegionSizes.Add(sizeTuple);
                        TreeRegionIndexCounts.Add(presets);
                    }

                    if (line.Contains(':') && !line.Contains('x'))
                    {
                        shapeIndex++;
                        rowIndex = 0;
                        Shapes.Add(new char[SHAPE_SIZE, SHAPE_SIZE]);
                        continue;
                    }

                    if (line != "" && rowIndex < SHAPE_SIZE)
                    {
                        line.Select((c, i) => Shapes[shapeIndex][rowIndex, i] = c).ToArray();
                        rowIndex++;
                    }

                }
                Console.WriteLine(line);
            }
        }
    }
}
