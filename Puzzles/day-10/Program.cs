
using System.Diagnostics;

namespace day_10
{
    public class Program
    {

        public static int SolvePartOne(int listSize, int[] lengths)
        {

            // Generate the initial list
            List<int> list = new List<int>();
            for (int i = 0; i < listSize; i++)
            {
                list.Add(i);
            }

            var currentPosition = 0;
            var skipSize = 0;
            KnotHash(lengths, list, ref currentPosition, ref skipSize);

            return list[0] * list[1];

        }

        private static void KnotHash(in int[] lengths, List<int> list, ref int currentPosition, ref int skipSize)
        {
            foreach (int length in lengths)
            {
                
                var endIndex = (currentPosition + length - 1) % list.Count;
                // Console.WriteLine("------------------");
                // Console.WriteLine("currentposition: " + currentPosition);
                // Console.WriteLine("endIndex: " + endIndex);
                // Console.WriteLine("length: " + length);
                // Console.WriteLine("skipsize (i): " + skipSize);

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

        public static string SolvePartTwo(int listSize, string input)
        {

            // Generate the initial list
            List<int> list = new List<int>();
            for (int i = 0; i < listSize; i++)
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
            for (int i = 0; i < 64; i++)
            {
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