using System;
using System.Collections.Generic;
using System.Linq;

namespace aoc2017.day_6 {
    public class Program {
        public static int SolvePartOne(int[] banks) {
            var initialConfig = hash(banks);
            var configurations = new List<long>();
            configurations.Add(initialConfig);

            while (true) {
                var max = FindBankMax(banks, out var maxIndex);
                Redistribute(banks, maxIndex, max);

                // check if we saw this configuration before
                var config = hash(banks);
                if (configurations.Contains(config)) {
                    return configurations.Count();
                }
                
                // store the configuration
                configurations.Add(config);
            }
        }
        public static int SolvePartTwo(int[] banks) {
            var initialConfig = hash(banks);
            var configurations = new List<long>();
            configurations.Add(initialConfig);

            while (true) {
                var max = FindBankMax(banks, out var maxIndex);
                Redistribute(banks, maxIndex, max);

                // check if we saw this configuration before
                var config = hash(banks);
                Console.WriteLine("current configuration: " + config);
                if (configurations.Contains(config)) {
                    Console.WriteLine("found existing config!");

                    for (int i = 0; i < configurations.Count; i++) {
                        if (configurations[i] == config) {
                            return configurations.Count - i; 
                        }
                    }
                }
                
                // store the configuration
                configurations.Add(config);
            }
        }
        
        private static void Redistribute(int[] banks, int maxIndex, int max) {
            banks[maxIndex] = 0;
            var redistributionIndex = maxIndex;
            var toDistribute = max;
            while (toDistribute > 0) {
                redistributionIndex = (redistributionIndex + 1) % banks.Length;
                banks[redistributionIndex]++;
                toDistribute--;
            }
        }

        private static int FindBankMax(int[] banks, out int maxIndex) {
            var max = Int32.MinValue;
            maxIndex = 0;
            for (var i = 0; i < banks.Length; i++) {
                if (banks[i] <= max) continue;
                max = banks[i];
                maxIndex = i;
            }

            return max;
        }
        
        private static long hash(int[] banks) {
            return (long) banks.Select((bank, index) => bank * Math.Pow(10, banks.Length - 1 - index)).Sum();
        }
    }
}