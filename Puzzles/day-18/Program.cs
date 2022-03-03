
namespace day_18
{
    public class Program
    {
        class Instruction
        {
            public string operand { get; init; }
            public string register { get; init; }
            public string? argument { get; init; }
        }


        public static long SolvePartOne(string filePath)
        {
            Dictionary<string, long> registers = new Dictionary<string, long>();
            List<Instruction> instructions = parseInstructions(filePath);

            var pc = 0;
            Queue<long> queue = new Queue<long>();
            while (true)
            {
                var instruction = instructions[pc];
                if (instruction.operand == "rcv")
                {
                    var registerName = instruction.register;
                    if (registers.ContainsKey(registerName) && registers[registerName] != 0)
                    {
                        return queue.Last();
                    }
                    pc++;
                }
                else
                {
                    pc = executeInstruction(registers, pc, instruction, queue);
                }
            }
        }

        public static long SolvePartTwo(string filePath)
        {
            Dictionary<string, long> registersA = new Dictionary<string, long>();
            Dictionary<string, long> registersB = new Dictionary<string, long>();

            // give the programs an ID
            registersA.Add("p", 0);
            registersB.Add("p", 1);

            List<Instruction> instructions = parseInstructions(filePath);

            var pcA = 0;
            var pcB = 0;

            Queue<long> queueA = new Queue<long>();
            Queue<long> queueB = new Queue<long>();

            var receivedFromProgramBCount = 0;

            while (true) {

                var noDataA = false;
                var noDataB = false;

                var instructionA = instructions[pcA];
                if (instructionA.operand == "rcv") {
                    if (queueB.Count != 0)
                    {

                        var registerName = instructionA.register;
                        if (registersA.ContainsKey(registerName))
                        {
                            registersA[registerName] = queueB.Dequeue();
                        } else
                        {
                            registersA.Add(registerName, queueB.Dequeue());

                        }
                        Console.WriteLine("Received message from B: " + registersA[registerName]);

                        receivedFromProgramBCount++;
                        pcA++;
                    } else
                    {
                        noDataA = true;
                    }
                } else
                {
                    pcA = executeInstruction(registersA, pcA, instructionA, queueA);
                }

                var instructionB = instructions[pcB];
                if (instructionB.operand == "rcv")
                {
                    if (queueA.Count != 0)
                    {
                        var registerName = instructionB.register;
                        if (registersB.ContainsKey(registerName))
                        {
                            registersB[registerName] = queueA.Dequeue();
                        }
                        else
                        {
                            registersB.Add(registerName, queueA.Dequeue());

                        }
                        Console.WriteLine("Received message from A: " + registersB[registerName]);
                        pcB++;
                    }
                    else
                    {
                        noDataB = true;
                    }
                }
                else
                {
                    pcB = executeInstruction(registersB, pcB, instructionB, queueB);
                }

                if (noDataA && noDataB)
                {
                    return receivedFromProgramBCount;
                }
            }
        //   return receivedFromProgramBCount;
        }

        private static List<Instruction> parseInstructions(string filePath)
        {
            var instructions = File.ReadAllLines(filePath).Select(line =>
            {
                var parts = line.Split(' ');
                return new Instruction()
                {
                    operand = parts[0],
                    register = parts[1],
                    argument = parts.Length > 2 ? parts[2] : null,
                };
            }
            ).ToList();
            return instructions;
        }

        private static int executeInstruction(Dictionary<string, long> registers, int pc, Instruction instruction, Queue<long> queue) {

            switch (instruction.operand) {
                case "set": return Set(registers, pc, instruction);
                case "add": return Add(registers, pc, instruction);
                case "mul": return Mul(registers, pc, instruction);
                case "mod": return Mod(registers, pc, instruction);
                case "jgz": return Jgz(registers, pc, instruction);
                case "snd": return Snd(registers, pc, instruction, queue);
                default: throw new Exception("invalid instruction operand!");
            }
        }


        private static int Set(Dictionary<string, long> registers, int pc, Instruction instruction) {
            var registerName = instruction.register;

            long NumericArgument;
            bool isNumerical = long.TryParse(instruction.argument, out NumericArgument);
            long argument = isNumerical ? NumericArgument : registers[instruction.argument];

            if (registers.ContainsKey(registerName)) {
                registers[registerName] = argument;
            } else {
                registers.Add(registerName, argument);
            }
            return pc + 1;
        }

        private static int Add(Dictionary<string, long> registers, int pc, Instruction instruction) {
            var registerName = instruction.register;

            long NumericArgument;
            bool isNumerical = long.TryParse(instruction.argument, out NumericArgument);
            long argument = isNumerical ? NumericArgument : registers[instruction.argument];

            if (registers.ContainsKey(registerName)) {
                registers[registerName] += argument;
            } else {
                registers.Add(registerName, argument);
            }
            return pc + 1;
        }

        private static int Mul(Dictionary<string, long> registers, int pc, Instruction instruction) {
            var registerName = instruction.register;

            long NumericArgument;
            bool isNumerical = long.TryParse(instruction.argument, out NumericArgument);
            long argument = isNumerical ? NumericArgument : registers[instruction.argument];

            if (registers.ContainsKey(registerName)){
                registers[registerName] *= argument;
            } else {
                registers.Add(registerName, 0);
            }
            return pc + 1;
        }

        private static int Mod(Dictionary<string, long> registers, int pc, Instruction instruction) {
            var registerName = instruction.register;

            long NumericArgument;
            bool isNumerical = long.TryParse(instruction.argument, out NumericArgument);
            long argument = isNumerical ? NumericArgument : registers[instruction.argument];

            if (registers.ContainsKey(registerName)) {
                registers[registerName] %= argument;
            } else {
                registers.Add(registerName, 0);
            }
            return pc + 1;
        }
      

        private static int Snd(Dictionary<string, long> registers, int pc, Instruction instruction, Queue<long> queue) {

            int NumericArgument;
            bool isNumerical = int.TryParse(instruction.register, out NumericArgument);
            int argument = isNumerical ? NumericArgument : (int)registers[instruction.register];

            queue.Enqueue(argument);
            Console.WriteLine("sending message: " + argument);
            return pc + 1;
        }

        private static int Jgz(Dictionary<string, long> registers, int pc, Instruction instruction)
        {

            int registerArgument;

            bool regIsNumerical = int.TryParse(instruction.register, out registerArgument);

            var reg = 0;
            if (regIsNumerical)
            {
                reg = registerArgument;
            } else
            {
                reg = registers.ContainsKey(instruction.register) ? (int) registers[instruction.register] : 0;
            }


            int NumericArgument;
            bool isNumerical = int.TryParse(instruction.argument, out NumericArgument);
            int argument = isNumerical ? NumericArgument : (int) registers[instruction.argument];

            if (reg > 0)
            {
                return  pc + argument;
            }
            return pc + 1;
        }


    }
}