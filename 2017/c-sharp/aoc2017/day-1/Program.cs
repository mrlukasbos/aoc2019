using System;
using System.IO;

namespace aoc2017.day_1 {
    public class Program {
        private static int GetTotal(string input, int offset) {
            var sum = 0;
            for (var i = 0; i < input.Length; i++) {
                if (input[i] == input[(i + offset) % input.Length]) {
                    // the digit matches the next digit
                    sum += int.Parse(input[i].ToString());
                }
            }
            return sum;
        }
        
        public static int solvePartOneForInput(string input) {
            return GetTotal(input, 1);
        }

        public static int solvePartTwoForInput(string input) {
            return GetTotal(input, input.Length/2);
        }
    }
}