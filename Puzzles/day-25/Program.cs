
namespace day_25
{

    public class Program
    {

        public enum Direction
        {
            Left,
            Right,
        }

        public class Instruction
        {
            public int write;
            public Direction move;
            public char nextState;
        }

        public static (int, int) Solve(string filePath)
        {
            var lines = File.ReadAllLines(filePath);
            var currentState = 'A';
            int totalSteps = 0;

            Dictionary<char, Instruction[]> instructions = new Dictionary<char, Instruction[]>();

            char parsingCurrentState = '#';
            int parsingCondition = -1;
            foreach (var line in lines)
            {
                var words = line.Trim().Split(' ');

                if (line.StartsWith("Begin in state"))
                {
                    currentState = words[3][0];

                }

                else if (line.StartsWith("Perform"))
                {
                    totalSteps = int.Parse(words[5]);
                } 
                
                else if (line.StartsWith("In state"))
                {
                    parsingCurrentState = words[2][0];
                    var empty = new Instruction[2] { new Instruction(), new Instruction() };
                    

                    instructions.Add(parsingCurrentState, empty);
                }

                else if (line.StartsWith("  If the current value is 0:"))
                {
                    parsingCondition = 0;
                } 
                
                else if (line.StartsWith("  If the current value is 1:"))
                {
                    parsingCondition = 1;
                }

                else if (line.StartsWith("    - Write the value"))
                {
                    var param = words[4][..1];
                    instructions[parsingCurrentState][parsingCondition].write = int.Parse(param);
                }

                else if (line.StartsWith("    - Move"))
                {
                    Direction dir = words[6].StartsWith("left") ? Direction.Left : Direction.Right;
                    instructions[parsingCurrentState][parsingCondition].move = dir;
                }

                else if (line.StartsWith("    - Continue"))
                {
                    instructions[parsingCurrentState][parsingCondition].nextState = words[4][0];
                }


                if (line == "")
                {
                    parsingCondition = -1;
                    parsingCurrentState = '#';
                }
            }

            Dictionary<int, int> slots = new Dictionary<int, int>();

            var currentSlot = 0;
            // At this point the input should be parsed into a result
            for (int step = 0; step < totalSteps; step++)
            {
                var currentSlotValue = slots.ContainsKey(currentSlot) ? slots[currentSlot] : 0;
                var instruction = instructions[currentState][currentSlotValue];
                
                slots[currentSlot] = instruction.write;
                currentSlot = instruction.move == Direction.Left ? currentSlot - 1 : currentSlot + 1;
                currentState = instruction.nextState;
            }

            var result = slots.Values.Count(x => x == 1);

            return (result, 0);
        }

        public static long SolvePartOne(string filePath)
        {
            return Solve(filePath).Item1;
        }

        public static long SolvePartTwo(string filePath)
        {
            return Solve(filePath).Item2;
            
        }
    }
}