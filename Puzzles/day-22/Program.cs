
namespace day_22 {
    public class Program {


        enum Direction
        {
            Up = 0,
            Right = 1,
            Down = 2,
            Left = 3, 
        }

        enum State
        {
            Clean,
            Weakened,
            Infected,
            Flagged,
        }

        public static int SolvePartOne(int bursts, string filePath)
        {
            var lines = File.ReadAllLines(filePath).ToList();
            var map = new Dictionary<(int y, int x), bool>();
            var currentDirection = Direction.Up;

            // set the initial state. 
            // The starting position = [0,0]
            for (var y = 0; y < lines.Count; y++)
            {
                for (var x = 0; x < lines[y].Length; x++)
                {
                    map[(y - (lines[0].Length / 2), x - (lines.Count / 2))] = lines[y][x] == '#';
                }
            }

            (int y, int x) currentPosition = (0, 0);


            var infections = 0;
            for (int i = 0; i < bursts; i++)
            {
                // If infected
                if (map.ContainsKey(currentPosition) && map[currentPosition])
                {
                    // Turn Right
                    currentDirection = (Direction) ((int)( currentDirection + 1) % 4);

                    // Current node becomes clean
                    map[currentPosition] = false;
                } else // if infected
                {
                    // Turn Left
                    currentDirection = (Direction)((int)(currentDirection + 3) % 4);

                    // Current node becomes infected
                    map[currentPosition] = true;
                    infections++;
                }

                Step(currentDirection, ref currentPosition);
            }
            return infections;
        }

        public static int SolvePartTwo(int bursts, string filePath)
        {
            var lines = File.ReadAllLines(filePath).ToList();
            var map = new Dictionary<(int y, int x), State>();
            var currentDirection = Direction.Up;

            // set the initial state. 
            // The starting position = [0,0]
            for (var y = 0; y < lines.Count; y++)
            {
                for (var x = 0; x < lines[y].Length; x++)
                {
                    map[(y - (lines[0].Length / 2), x - (lines.Count / 2))] = (lines[y][x] == '#' ? State.Infected : State.Clean);
                }
            }

            (int y, int x) currentPosition = (0, 0);

            var infections = 0;
            for (int i = 0; i < bursts; i++)
            {

                // If clean
                if (!map.ContainsKey(currentPosition) || map[currentPosition] == State.Clean)
                {
                    // Turn Left
                    currentDirection = (Direction)((int)(currentDirection + 3) % 4);

                    map[currentPosition] = State.Weakened;
                }
                else if (map[currentPosition] == State.Weakened)
                {
                    // Keep direction

                    map[currentPosition] = State.Infected;
                    infections++;

                }
                else if (map[currentPosition] == State.Flagged)
                {
                    // Reverse Direction
                    currentDirection = (Direction)((int)(currentDirection + 2) % 4);

                    map[currentPosition] = State.Clean;
                }
                else // Infected
                {
                    // Turn Right
                    currentDirection = (Direction)((int)(currentDirection + 1) % 4);

                    map[currentPosition] = State.Flagged;
                }

                Step(currentDirection, ref currentPosition);
            }
            return infections;
        }


        private static void Step(Direction direction, ref (int y, int x) pos) {
            // Take one step in the given direction
            switch (direction)
            {
                case Direction.Up:
                    pos.y--;
                    return;
                case Direction.Right:
                    pos.x++;
                    return;
                case Direction.Down:
                    pos.y++;
                    return;
                case Direction.Left:
                    pos.x--;
                    return;
            }
        }
    }
}