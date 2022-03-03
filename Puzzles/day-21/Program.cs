
namespace day_21 { 
    public class Program {

        internal class Rule
        {
            internal List<char[]> matchable;
            internal List<char[]> result;
        }

        public static int SolvePartOne(int iterations, string filePath) {
            var lines = File.ReadAllLines(filePath).ToList();
            var rules = new List<Rule>();

            foreach (var line in lines) {
                var splitted = line.Split(new string[] { " => " }, StringSplitOptions.None);
                var matchable = splitted[0].Split('/').Select(x => x.ToCharArray()).ToList();
                var resultOfMatchable = splitted[1].Split('/').Select(x => x.ToCharArray()).ToList();

                for (int i = 0; i < 8; i++) {
                    if (i == 4) { Flip(matchable); }
                    Rotate(matchable);
                    var newRule = new Rule() {
                        matchable = matchable,
                        result = resultOfMatchable,
                    };
                    rules.Add(newRule);
                }
            }

            var startingPattern = new List<char[]>
                {
                    ".#.".ToCharArray(), "..#".ToCharArray(), "###".ToCharArray(),
                };

            var currentPattern = startingPattern;
            for (int i = 0; i < iterations; i++) {
                var patterns = Divide(currentPattern);
                for (int x = 0; x < patterns.Count; x++) {
                    for (int y = 0; y < patterns.Count; y++)
                    {
                        patterns[y][x] = Match(patterns[y][x], rules);
                    }
                }
                currentPattern = Join(patterns);
            }
            return string.Concat(currentPattern).Count(c => c == '#');   
        }

        private static List<char[]> Match(List<char[]> pattern, List<Rule> rules)
        {
            foreach (var rule in rules)
            {
                if (patternsAreEqual(rule.matchable, pattern))
                {
                    return rule.result;
                }
            }

            Console.WriteLine("Could not find match for pattern:");
            throw new Exception("Could not find match");
        }

        // Merge blocks into one pattern
        // Example: [[abc, def, ghi], [jkl, mno, pqr], [stu, vwx, yz0], [123, 456, 789] => [abcjklstu123, defmnovwx456, ghipqryz0789];
        public static List<char[]> Join(List<List<List<char[]>>> patterns)
        {
            // we have a grid of patterns
            var patternSize = patterns[0][0].Count;
            var finalSize = patternSize * patterns.Count;
            var result = new List<char[]>(finalSize);

            for (int i = 0; i < finalSize; i++)
            {
                var arr = new char[3];
                result.Add(arr);
            }

            // For each row in the pattern grid
            for (int row = 0; row < patterns.Count; row++)
            {
                for (int col = 0; col < patterns.Count; col++)
                {
                    var pattern = patterns[row][col];
                    var offset = row * pattern.Count;

                    // iterate over each row in the pattern
                    for (int line = 0; line < pattern.Count; line++)
                    {
                        result[offset + line].Concat(pattern[line]);
                    }
                }
            }

            return result;
        }


        // Divide a single pattern into a grid of patterns (with size of 2, or else blocks of 3
        // Example: [abcd, efgh] => [[ab, ef], [cd, gh]];
        public static List<List<List<char[]>>> Divide(List<char[]> pattern)
        {
            var size = ((pattern.Count % 2) == 0) ? 2 : 3; // Whether it is divided by two or three
            var divisions = pattern.Count / size; // The amount of rows, and also the amount of objects in row

            List<List<List<char[]>>> result = new List<List<List<char[]>>>();
            for (int row = 0; row < divisions; row++)
            {
                var newRow = new List<List<char[]>>();
                for (int col = 0; col < divisions; col++)
                {
                    var newPattern = new List<char[]>();
                    for (var line = 0; line < size; line++)
                    {
                        var newLine = pattern[(row * size) + line][(col * size)..size]; //Substring(col * size, size)
                        newPattern.Add(newLine);
                    }
                    newRow.Add(newPattern);
                }
                result.Add(newRow);
            }
            return result;
        }

        private static void PrintPattern(List<char[]> pattern)
        {
            foreach (var line in pattern)
            {
           //     Console.WriteLine(line);
            }
       //     Console.WriteLine();
        }


        private static void Flip(List<char[]> pattern)
        {
            (pattern[0], pattern[^1]) = (pattern[^1], pattern[0]);
        }

        private static void Rotate2(List<char[]> pattern)
        {
            List<char[]> clone = new List<char[]>(2);

            clone[0][0] = pattern[0][0];
            clone[0][1] = pattern[0][1];
            clone[1][0] = pattern[1][0];
            clone[1][1] = pattern[1][1];


            (pattern[0][0], pattern[0][1], pattern[1][0], pattern[1][1])
                = (clone[1][0], clone[0][0], clone[1][1], clone[0][1]);
        }

        // 90 deg rotate
        private static void Rotate3(List<char[]> pattern)
        {


            List<char[]> clone = new List<char[]>(3);

            clone[0][0] = pattern[0][0];
            clone[0][1] = pattern[0][1];
            clone[0][2] = pattern[0][2];
            clone[1][0] = pattern[1][0];
            clone[1][1] = pattern[1][1];
            clone[1][2] = pattern[1][2];
            clone[2][0] = pattern[2][0];
            clone[2][1] = pattern[2][1];
            clone[2][2] = pattern[2][2];


            //pattern[0][0] = pattern[2][0];
            //pattern[1][0] = pattern[2][1];
            //pattern[2][0] = pattern[2][2];
            //pattern[2][1] = pattern[1][2];
            //pattern[2][2] = pattern[0][2];
            //pattern[1][2] = pattern[0][1];
            //pattern[0][2] = pattern[0][0];
            //pattern[0][1] = pattern[1][0];


            (pattern[0][0], pattern[0][1], pattern[0][2], 
            pattern[1][0],                      pattern[1][2],
            pattern[2][0], pattern[2][1], pattern[2][2])
        = 
        (clone[2][0], clone[1][0], clone[0][0],
            clone[2][1], clone[0][1],
            clone[2][2], clone[1][2], clone[0][2]);

        }

        private static void Rotate(List<char[]> pattern)
        {
            if (pattern.Count == 2)
            {
                Rotate2(pattern);
            } else
            {
                Rotate3(pattern);
            }
        }

        private static bool patternsAreEqual(List<char[]> p1, List<char[]> p2)
        {
            var size = p1.Count;
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++) {
                    if (p1[i][j] != p2[i][j]) return false;
                }
            }
            return true;
        }
    }
}