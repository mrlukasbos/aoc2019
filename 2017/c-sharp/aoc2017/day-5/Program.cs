using System.IO;
using System.Linq;

namespace aoc2017.day_5 {
    public class Program {
        public static int SolvePartOneForFile(string file) {
            var instructions = File.ReadAllLines(file).Select(int.Parse).ToList();
            var currentInstructionIndex = 0;
            var steps = 0;
            while (currentInstructionIndex >= 0 && currentInstructionIndex < instructions.Count()) {
                var instruction = instructions[currentInstructionIndex];
                var newIndex = currentInstructionIndex + instruction;
                instructions[currentInstructionIndex]++;
                currentInstructionIndex = newIndex;
                steps++;
            }

            return steps;
        }

    
        public static int SolvePartTwoForFile(string file) {
            var instructions = File.ReadAllLines(file).Select(int.Parse).ToList();
            var currentInstructionIndex = 0;
            var steps = 0;
            while (currentInstructionIndex >= 0 && currentInstructionIndex < instructions.Count()) {
                var instruction = instructions[currentInstructionIndex];
                var newIndex = currentInstructionIndex + instruction;
                
                if (instruction >= 3) {
                    instructions[currentInstructionIndex]--;
                }
                else {
                    instructions[currentInstructionIndex]++;
                }
                
                currentInstructionIndex = newIndex;
                steps++;
            }

            return steps;            
        }
    }
}