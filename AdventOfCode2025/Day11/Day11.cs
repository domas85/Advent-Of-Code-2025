using System.Numerics;
using System.Linq;

namespace AOC
{
    public class Day11
    {
        public static string line = "";

        public static Dictionary<string, List<string>> Connections = new Dictionary<string, List<string>>();

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
            Int64 svr_dac = DFS_FindAllPaths("svr", "dac");
            Int64 svr_fft = DFS_FindAllPaths("svr", "fft");
            Int64 dac_out = DFS_FindAllPaths("dac", "out");
            Int64 dac_fft = DFS_FindAllPaths("dac", "fft");
            Int64 fft_dac = DFS_FindAllPaths("fft", "dac");
            Int64 fft_out = DFS_FindAllPaths("fft", "out");

            Result = (svr_dac * dac_fft * fft_out) + (svr_fft * fft_dac * dac_out);

            Console.WriteLine($"Result: {Result}");

        }

        public static void SolvePart1()
        {
            if (Connections.TryGetValue("you", out List<string> youConnections) && youConnections != null)
            {
                int connectionCount = 0;
                int totalConnections = GetConnectionCount(youConnections, ref connectionCount);
                Console.WriteLine($"Result: {totalConnections}");
            }
        }

        public static Int64 DFS_FindAllPaths(string startNode, string targetNode)
        {
            var visited = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            var memo = new Dictionary<string, Int64>(StringComparer.OrdinalIgnoreCase);
            Int64 found = DFS_FindAllPathsInternal(startNode, targetNode, visited, memo);
            return found;
        }

        private static Int64 DFS_FindAllPathsInternal(string currentNode, string targetNode, HashSet<string> visited, Dictionary<string, Int64> memo)
        {
            if (currentNode == targetNode)
            {
                return 1;
            }

            if (!Connections.TryGetValue(currentNode, out List<string> nextConnections) || nextConnections == null)
            {
                return 0;
            }

            // memoization check
            string memoKey = currentNode;
            if (memo.TryGetValue(memoKey, out Int64 cached))
            {
                return cached;
            }

            // mark current on path
            if (!visited.Add(currentNode))
            {
                // already on current path no new paths from here
                return 0;
            }

            Int64 pathsFromHere = 0;
            foreach (string connection in nextConnections)
            {
                if (visited.Contains(connection))
                    continue;

                pathsFromHere += DFS_FindAllPathsInternal(connection, targetNode, visited, memo);
            }

            // backtrack: allow other branches to use this node
            visited.Remove(currentNode);

            // store result for this subproblem
            memo[memoKey] = pathsFromHere;
            return pathsFromHere;
        }

        public static int GetConnectionCount(List<string> deviceConnections, ref int count)
        {
            foreach (var connection in deviceConnections)
            {
                if (Connections.TryGetValue(connection, out var nextConnections) && nextConnections != null)
                {
                    if (nextConnections.Contains("out"))
                    {
                        count++;
                    }
                    else
                    {
                        GetConnectionCount(nextConnections, ref count);
                    }
                }
            }

            return count;
        }

        public static void ReadInput()
        {
            StreamReader sr = new StreamReader(@"../../../Day11/Input.txt");
            while (line != null)
            {
                line = sr.ReadLine();

                if (line != null)
                {
                    string[] devices = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                    string deviceKey = devices[0].TrimEnd(':');

                    List<string> deviceConnections = new List<string>();

                    foreach (var output in devices)
                    {
                        if (!output.Contains(':'))
                        {
                            deviceConnections.Add(output);
                        }
                    }
                    Connections.Add(deviceKey, deviceConnections);
                }

                Console.WriteLine(line);
            }
        }
    }
}
