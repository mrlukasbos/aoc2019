
namespace day_23 {

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

            var muls = 0;
            var pc = 0;
            while (pc >= 0 && pc < instructions.Count)
            {
                var instruction = instructions[pc];
                if (instruction.operand == "mul")
                {
                    muls++;
                }
             
                pc = executeInstruction(registers, pc, instruction);
            }
            return muls;
        }

        public static long SolvePartTwo() {
            int h = 0;

            for (int b = 109900; b <= 126900; b += 17)
            {
                bool isPrime = true;
                for (int d = 2; d != b; d++)
                {
                    //e = 2;
                    //do
                    //{
                    //    if (d * e == b) // if there are two factors leading up to be b, then it is not prime
                    //    {
                    //        f = 0;
                    //        break;
                    //    }
                    //    e++;
                    //} while (e != b);
                    if ((b % d) == 0)
                    {
                        // Console.WriteLine("d: " + d + " b:" + b);
                        isPrime = false;
                        break;
                    }
                }

                if (!isPrime)
                {
                    h++;
                }
            }
            return h;
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

        private static int executeInstruction(Dictionary<string, long> registers, int pc, Instruction instruction)
        {

            switch (instruction.operand)
            {
                case "set": return Set(registers, pc, instruction);
                case "sub": return Sub(registers, pc, instruction);
                case "add": return Add(registers, pc, instruction);
                case "mul": return Mul(registers, pc, instruction);
                case "mod": return Mod(registers, pc, instruction);
                case "jgz": return Jgz(registers, pc, instruction);
                case "jnz": return Jnz(registers, pc, instruction);
                default: throw new Exception("invalid instruction operand!");
            }
        }


        private static int Set(Dictionary<string, long> registers, int pc, Instruction instruction)
        {
            var registerName = instruction.register;

            long NumericArgument;
            bool isNumerical = long.TryParse(instruction.argument, out NumericArgument);
            long argument = isNumerical ? NumericArgument : registers[instruction.argument];

            if (registers.ContainsKey(registerName))
            {
                registers[registerName] = argument;
            }
            else
            {
                registers.Add(registerName, argument);
            }
            return pc + 1;
        }

        private static int Add(Dictionary<string, long> registers, int pc, Instruction instruction)
        {
            var registerName = instruction.register;

            long NumericArgument;
            bool isNumerical = long.TryParse(instruction.argument, out NumericArgument);
            long argument = isNumerical ? NumericArgument : registers[instruction.argument];

            if (registers.ContainsKey(registerName))
            {
                registers[registerName] += argument;
            }
            else
            {
                registers.Add(registerName, argument);
            }
            return pc + 1;
        }

        private static int Sub(Dictionary<string, long> registers, int pc, Instruction instruction)
        {
            var registerName = instruction.register;

            long NumericArgument;
            bool isNumerical = long.TryParse(instruction.argument, out NumericArgument);
            long argument = isNumerical ? NumericArgument : registers[instruction.argument];

            if (registers.ContainsKey(registerName))
            {
                registers[registerName] -= argument;
            }
            else
            {
                registers.Add(registerName, -argument);
            }
            return pc + 1;
        }

        private static int Mul(Dictionary<string, long> registers, int pc, Instruction instruction)
        {
            var registerName = instruction.register;

            long NumericArgument;
            bool isNumerical = long.TryParse(instruction.argument, out NumericArgument);
            long argument = isNumerical ? NumericArgument : registers[instruction.argument];

            if (registers.ContainsKey(registerName))
            {
                registers[registerName] *= argument;
            }
            else
            {
                registers.Add(registerName, 0);
            }
            return pc + 1;
        }

        private static int Mod(Dictionary<string, long> registers, int pc, Instruction instruction)
        {
            var registerName = instruction.register;

            long NumericArgument;
            bool isNumerical = long.TryParse(instruction.argument, out NumericArgument);
            long argument = isNumerical ? NumericArgument : registers[instruction.argument];

            if (registers.ContainsKey(registerName))
            {
                registers[registerName] %= argument;
            }
            else
            {
                registers.Add(registerName, 0);
            }
            return pc + 1;
        }


        //private static int Snd(Dictionary<string, long> registers, int pc, Instruction instruction, Queue<long> queue)
        //{

        //    int NumericArgument;
        //    bool isNumerical = int.TryParse(instruction.register, out NumericArgument);
        //    int argument = isNumerical ? NumericArgument : (int)registers[instruction.register];

        //    queue.Enqueue(argument);
        //    Console.WriteLine("sending message: " + argument);
        //    return pc + 1;
        //}

        private static int Jgz(Dictionary<string, long> registers, int pc, Instruction instruction)
        {

            int registerArgument;

            bool regIsNumerical = int.TryParse(instruction.register, out registerArgument);

            var reg = 0;
            if (regIsNumerical)
            {
                reg = registerArgument;
            }
            else
            {
                reg = registers.ContainsKey(instruction.register) ? (int)registers[instruction.register] : 0;
            }


            int NumericArgument;
            bool isNumerical = int.TryParse(instruction.argument, out NumericArgument);
            int argument = isNumerical ? NumericArgument : (int)registers[instruction.argument];

            if (reg > 0)
            {
                return pc + argument;
            }
            return pc + 1;
        }

        private static int Jnz(Dictionary<string, long> registers, int pc, Instruction instruction)
        {

            int registerArgument;

            bool regIsNumerical = int.TryParse(instruction.register, out registerArgument);

            var reg = 0;
            if (regIsNumerical)
            {
                reg = registerArgument;
            }
            else
            {
                reg = registers.ContainsKey(instruction.register) ? (int)registers[instruction.register] : 0;
            }


            int NumericArgument;
            bool isNumerical = int.TryParse(instruction.argument, out NumericArgument);
            int argument = isNumerical ? NumericArgument : (int)registers[instruction.argument];

            if (reg != 0)
            {
                return pc + argument;
            }
            return pc + 1;
        }


    }
}




//set b 99
//set c b
//jnz a 2
//jnz 1 5
//mul b 100
//sub b -100000
//set c b
//sub c -17000
//set f 1
//set d 2
//set e 2

//set g d
//mul g e
//sub g b
//jnz g 2
//set f 0
//sub e -1
//set g e
//sub g b
//jnz g -8

//sub d -1
//set g d
//sub g b

//jnz g -13
//jnz f 2
//sub h -1
//set g b
//sub g c
//jnz g 2
//jnz 1 3
//sub b -17
//jnz 1 -23