
using System.Diagnostics;

namespace day_12
{
    public class Program
    {


        public static int SolvePartOne(string filePath)
        {
            return Solve(filePath).Item1;
        }
        public static int SolvePartTwo(string filePath)
        {
            return Solve(filePath).Item2;
        }


        public static (int, int) Solve(string filePath)
        {
            var lines = File.ReadAllLines(filePath);

            Dictionary<int, List<int>> pipes = new Dictionary<int, List<int>>();

            foreach (var line in lines) {
                var splitted = line.Split(new string[] { " <-> " }, StringSplitOptions.None);
                var programId = int.Parse(splitted[0]);
                var childrenString = splitted[1];
                var children = childrenString.Split(new string[] { ", " }, StringSplitOptions.None).Select(int.Parse);

                // Can performance be improved by storing an Enumberable in the map instead of a list?
                pipes.Add(programId, children.ToList());
            }
            Debug.WriteLine("counting children...");

            Dictionary<int, int> depths = new Dictionary<int, int>();
            depths.Add(0, 0);
            var partOne = CountChildren(pipes, depths, 0);


            var amountOfGroups = 1; // it is one because we seperately solve the group with pipe 0 for part one.
            foreach (var pipe in pipes)
            {
                if (depths.ContainsKey(pipe.Key))
                {
                    continue;
                } else
                {
                    CountChildren(pipes, depths, pipe.Key);
                    amountOfGroups++;
                }
            }



            return (partOne, amountOfGroups);
        }

        private static int CountChildren(Dictionary<int, List<int>> pipes, Dictionary<int, int> depths, int programId) {
            Debug.WriteLine("Count programId: " + programId);
            var total = 1;
            var children = pipes[programId];
            foreach (var child in children) {
                var depth = 0;
                if (depths.ContainsKey(child)) {
                    continue;
                } else {
                    depths.Add(child, 1);
                    depth = CountChildren(pipes, depths, child);
                }
                total += depth;
            }
            return total;
        }
    }
}