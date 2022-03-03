
namespace day_19
{
    public class Program {


        enum Direction { 
            Unknown,
            Left,
            Right,
            Up,
            Down,
        }

        public static string SolvePartOne(string filePath)
        {
            return Solve(filePath).Item1;
        }

        public static int SolvePartTwo(string filePath)
        {
            return Solve(filePath).Item2;
        }


        public static (string, int) Solve(string filePath) {
            var coordinates = File.ReadAllLines(filePath).ToList();

            // Get the starting coordinates
            var y = 0;
            var x = coordinates[0].IndexOf('|');
            var direction = Direction.Down;

            var maxY = coordinates.Count;
            var maxX = coordinates[0].Length;

            string word ="";
            int steps = 1; // first step is already found
            while (true)
            {
                int prevX = x;
                int prevY = y;

                step(direction, ref x, ref y);
                steps++;
                var symbol = coordinates[y][x];
                switch (symbol)
                {
                    case '+':
                        direction = switchDirection(coordinates, direction, ref x, ref y, maxX, maxY);
                        break;
                    case ' ':
                        // First undo the last step, then switch direction from there
                        x = prevX;
                        y = prevY;
                        steps--;
                        direction = switchDirection(coordinates, direction, ref x, ref y, maxX, maxY);
                        break;
                    case '-':
                    case '|':
                        // we don't care, just continue
                        break;
                    default:
                        word += symbol;
                        break;
                }
                if (direction == Direction.Unknown)
                {
                    break;
                }
            }
            return (word, steps);
        }


        private static Direction switchDirection(List<string> Coordinates, Direction direction, ref int x, ref int y, int maxX, int maxY)
        {
            if (direction == Direction.Up || direction == Direction.Down)
            {
                // we must go either left or right
                if (x >= 0 && Coordinates[y][x - 1] != ' ' && Coordinates[y][x - 1] != '|') return Direction.Left;
                if (x <= maxX && Coordinates[y][x + 1] != ' ' && Coordinates[y][x + 1] != '|') return Direction.Right;
            }
            else
            {
                // we must go up or down
                if (y >= 0 && Coordinates[y-1][x] != ' ' && Coordinates[y - 1][x] != '-') return Direction.Up;
                if (y <= maxY && Coordinates[y+1][x] != ' ' && Coordinates[y + 1][x] != '-') return Direction.Down;

            }
            return Direction.Unknown;
        }

        private static void step (Direction direction, ref int x, ref int y)
        {
            switch (direction)
            {
                case Direction.Left: 
                    x--;
                    break;
                case Direction.Right:
                    x++;
                    break;
                case Direction.Up:
                    y--;
                    break;
                case Direction.Down:
                    y++;
                    break;
            }
        }

    }
}