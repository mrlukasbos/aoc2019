using System;
using System.IO;
using System.Linq;

namespace aoc2017.day_4 {
    public class Program {
        
        public static int SolvePartOneForFile(string file) {
            var lines = File.ReadAllLines(file);
            var count = 0;
            foreach (var line in lines) {
                var words = line.Split(' ');
                var isValid = true;
                for (int i = 0; i < words.Length; i++) {
                    var word = words[i];
                    for (int j = 0; j < words.Length; j++) {
                        if (i != j) {
                            if (word == words[j]) {
                                isValid = false;
                                break;
                            }
                        }
                    }
                    if (!isValid) {
                        break;
                    }
                }

                if (isValid) {
                    count++;
                }
            }
            return count;
        }

        private static bool IsAnagram(string a, string b) {
            var aSorted = a.ToCharArray();
            Array.Sort(aSorted);
            var bSorted = b.ToCharArray();
            Array.Sort(bSorted);
            return aSorted.SequenceEqual(bSorted);
        }

        public static int SolvePartTwoForFile(string file) {
            var lines = File.ReadAllLines(file);
            var count = 0;
            foreach (var line in lines) {
                var words = line.Split(' ');
                
                var isValid = true;
                for (int i = 0; i < words.Length; i++) {
                    var word = words[i];
                    for (int j = 0; j < words.Length; j++) {
                        if (i != j) { // skip the same word
                            if (IsAnagram(word, words[j])) {
                                isValid = false;
                                break;
                            }
                        }
                    }
                    if (!isValid) {
                        break;
                    }
                }

                if (isValid) {
                    count++;
                }
            }
            return count;
           
        }
        
    }
}