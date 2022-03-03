namespace day_11
{
    public class Program
    {
        public static int SolvePartOne(string inputStr)
        {
            return Solve(inputStr).Item1;
        }

        public static int SolvePartTwo(string inputStr)
        {
            return Solve(inputStr).Item2;
        }

        public static (int, int) Solve(string inputStr)
        {
            var input = inputStr.Split(',');

            int x = 0;
            int y = 0;
            int partTwo = int.MinValue;
            foreach (var direction in input)
            {
                switch (direction)
                {
                    case "n":
                        y += 2;
                        break;
                    case "ne":
                        y++;
                        x++;
                        break;
                    case "nw":
                        y++;
                        x--;
                        break;
                    case "sw":
                        y--;
                        x--;
                        break;
                    case "s":
                        y -= 2;
                        break;
                    case "se":
                        y--;
                        x++;
                        break;
                    default:
                        throw new Exception("unexpected input!" + direction);
                }
                partTwo = Math.Max(partTwo, CalcDistance(x, y));
            }

            var partOne = CalcDistance(x, y);
            return (partOne, partTwo);
        }

        private static int CalcDistance(int x, int y)
        {
            var dx = Math.Abs(x);
            var dy = Math.Abs(y);
            var diagonalsteps = Math.Min(dx, dy);
            return diagonalsteps + (dy - diagonalsteps) / 2;
        }
    }
}