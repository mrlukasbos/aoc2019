
namespace day_24
{

    public class Program
    {

        public static (int, int) Solve(string filePath)
        {
            // Get input and parse them into a list of tuples, where the first value is always the smallest
            var pieces = File.ReadAllLines(filePath).Select(pc =>
            {
                var parts = pc.Split('/').Select(int.Parse).ToList();
                parts.Sort();
                return (parts[0], parts[1]);
            }).ToList();

            var maxForLength = new Dictionary<int, int>();
            var maxWeight= IterateBridges(pieces, 0, 0, 0, maxForLength);

            // part two
            var longestBridgeLengths = maxForLength.Keys.ToList();
            longestBridgeLengths.Sort();
            var longestBridgeLength = longestBridgeLengths.Last();

            //  1899 too low
            return (maxWeight, maxForLength[longestBridgeLength]);
        }

        public static long SolvePartOne(string filePath)
        {
            return Solve(filePath).Item1;
        }


        public static long SolvePartTwo(string filePath)
        {
            return Solve(filePath).Item2;
            
        }

        private static int IterateBridges(List<(int, int)> pieces, int port, int previousMax, int length, Dictionary<int, int> maxForLength)
        {
            var max = previousMax;
            var fittingPieces = GetFittingPieces(port, pieces);
            foreach (var piece in fittingPieces)
            {
                List<(int, int)> piecesCopy = new List<(int, int)>(pieces);
                piecesCopy.Remove(piece);

                int newPort = piece.Item1 == port ? piece.Item2 : piece.Item1;
                int total = IterateBridges(piecesCopy, newPort, previousMax + getSum(piece), length+1, maxForLength);
                max = Math.Max(total, max);

                if (maxForLength.ContainsKey(length))
                {
                    maxForLength[length] = Math.Max(maxForLength[length], max);
                }
                else
                {
                    maxForLength.Add(length, max);
                }
            }
            return max;
        }

        private static int getSum((int, int) piece)
        {
            return piece.Item1 + piece.Item2;   
        }


        private static List<(int, int)> GetFittingPieces(int port, List<(int, int)> pieces)
        {
            return pieces.Where(p => p.Item1 == port || p.Item2 == port).ToList();
        } 

  
    }
}