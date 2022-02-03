using System;
using System.IO;
using System.Linq;

namespace aoc2017.day_2 {
    public class Program {
        
        public static int SolvePartOneForFile(string file, char delimiter) {
            var lines = File.ReadAllLines(file);

            var sum = 0;
            foreach (var line in lines) {
                var nums = line.Split(delimiter).ToList();
                
                var min = Int32.MaxValue;
                var max = Int32.MinValue;
                foreach (var numAsString in nums) {
                    var number = int.Parse(numAsString);
                    if (number < min) min = number;
                    if (number > max) max = number;
                }

                var diff = max - min;
                sum += diff;
            }

            return sum;
        }

        public static int SolvePartTwoForFile(string file, char delimiter) {
            var lines = File.ReadAllLines(file);

            var sum = 0;
            foreach (var line in lines) {
                var nums = line.Split(delimiter).ToList().Select(int.Parse);
                
                var result = 0;
                foreach (var a in nums) {
                    foreach (var b in nums) {
                        if (a == b) continue;
                        if (a % b == 0) {
                            result = a / b;
                            break;
                        }
                    }

                    if (result != 0) {
                        break;
                    }
                }
                
                sum += result;
            }

            return sum;
        }
        
        
    }
}