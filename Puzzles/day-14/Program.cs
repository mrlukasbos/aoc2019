
using System.Diagnostics;

namespace day_14
{
    public class Program
    {

        public static int SolvePartTwo(string input) {
            List<string> grid = new List<string>();
            for (int i = 0; i < 128; i++)
            {
                var hashInput = (input + "-" + i);
                var hash = Hash(hashInput);
                var row = "";
                foreach (char c in hash)
                {
                    row += Convert.ToString(int.Parse(c.ToString(), System.Globalization.NumberStyles.HexNumber), 2).PadLeft(4, '0');
                }
                grid.Add(row);
            }


            Dictionary<(int, int), int> map = new Dictionary<(int, int), int>();
            var regionNumber = 0;
            for (int y = 0; y < 128; y++) {
                for (int x =0; x < 128; x++) {
                    var isUsed = (grid[x][y] == '1');
                    var isMarked = map.ContainsKey((x, y));

                    if (isUsed && !isMarked)
                    {
                        Mark(grid, map, x, y, regionNumber);
                        regionNumber++;
                    }
                }
            }

            return regionNumber;
        }

        private static void Mark(List<string> grid, Dictionary<(int, int), int> map, int x, int y, int regionNumber)
        {
            map[(x, y)] = regionNumber;

            var neighbourOffsets = new (int x , int y)[]{ (-1, 0), (1, 0), (0, 1), (0, -1) };
            foreach (var nb in neighbourOffsets)
            {
                var nbx = x + nb.x;
                var nby = y + nb.y;
                if (nbx < 0 || nbx > 127) continue;
                if (nby < 0 || nby > 127) continue;

                var isUsed = (grid[nbx][nby] == '1');
                var isMarked = map.ContainsKey((nbx, nby));
                if (isUsed && !isMarked)
                {
                    Mark(grid, map, nbx, nby, regionNumber);
                }
            }
           
        }


        public static int SolvePartOne(string input)
        {
            var totalUsed = 0;
            for (int i = 0; i < 128; i++)
            {
                var hashInput = (input + "-" + i);
                var hash = Hash(hashInput);

                foreach (char c in hash)
                {
                    var binary = Convert.ToString(int.Parse(c.ToString(), System.Globalization.NumberStyles.HexNumber), 2);
                    Console.WriteLine("binary representation of " + c + ": " + binary);

                    var count = binary.Count(c => c == '1');
                    totalUsed += count;

                }
            }

            return totalUsed;
        }

        private static void KnotHash(in int[] lengths, List<int> list, ref int currentPosition, ref int skipSize)
        {
            foreach (int length in lengths) {
                var endIndex = (currentPosition + length - 1) % list.Count;
                for (int j = 0; j <= length / 2; j++)
                {
                    var posA = (currentPosition + j) % list.Count;
                    var posB = (list.Count + endIndex - j) % list.Count;
                    if (posA == posB) break;
                    if (posB == (currentPosition - 1 + j) % list.Count) break;
                    //Console.WriteLine("swap posA: " + posA + " ["+ list[posA]+"] with posB: " + posB + " [" + list[posB] + "]");
                    (list[posA], list[posB]) = (list[posB], list[posA]);
                }
                currentPosition = (currentPosition + skipSize + length) % list.Count;
                skipSize++;
                //Console.WriteLine(String.Join(", ", list));
            }
        }

        private static string Hash(string input) {
            // Generate the initial list
            List<int> list = new List<int>();
            for (int i = 0; i < 256; i++)
            {
                list.Add(i);
            }

            // convert each int to char, then cast back to int
            var lengths = input.Select(Convert.ToInt32).ToList();
            int[] suffix = { 17, 31, 73, 47, 23 };
            lengths.AddRange(suffix);

            var currentPosition = 0;
            var skipSize = 0;
            // repeat 64 times
            for (int i = 0; i < 64; i++) {
                KnotHash(lengths.ToArray(), list, ref currentPosition, ref skipSize);
            }

            List<int> denseHash = new List<int>();
            // Xor the blocks
            for (int i = 0; i < 16; i++)
            {
                var blockStart = i * 16;
                var num = list[blockStart];
                for (int j = 1; j < 16; j++)
                {
                    num ^= list[blockStart + j];
                }
                denseHash.Add(num);
            }
            return string.Concat(denseHash.Select(i => Convert.ToString(i, 16).PadLeft(2, '0')));
        }
    }
}