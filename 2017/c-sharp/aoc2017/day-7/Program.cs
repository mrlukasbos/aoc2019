using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace aoc2017.day_7 {
    public class Program {
        public static string SolvePartOneForFile(string file) {
            var lines = File.ReadAllLines(file);
            
            var children = new List<string>();
            var parents = new List<string>();


            foreach (var line in lines) {
                var data = line.Split('>');

                var nameAndWeight = data[0].Split(' ');
                var name = nameAndWeight[0];
                parents.Add(name);
                // var weight = int.Parse(nameAndWeight[1].TrimStart('(').TrimEnd(') -'));

                if (data.Length > 1) {
                    children.AddRange(data[1].Split(',').Select(s => s.Trim()));
                }
            }

            return parents.Except(children).First();
        }



        public static int SolvePartTwoForFile(string file) {
            return 0;
        }
    }
}