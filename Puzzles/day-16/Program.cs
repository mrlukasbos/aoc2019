
using System.Diagnostics;

namespace day_16
{
    public class Program
    {

        class ProgramList
        {
            public List<char> programs = new List<char>();
            public int header = 0;
        }

        public static string SolvePartOne(string filePath, char[] programsAsChar) {
            var danceMovesStr = File.ReadAllLines(filePath).First().Split(',');
            ProgramList programList = new ProgramList {
                programs = programsAsChar.ToList(),
            };

            foreach (var moveStr in danceMovesStr) {
                var move = moveStr[0];
                var moveDetails = moveStr.Substring(1);

                switch (move) {
                    case 's':
                        Spin(programList, int.Parse(moveDetails));
                        break;
                    case 'x':
                        var positions = moveDetails.Split('/').Select(int.Parse).ToList();
                        Exchange(programList, positions[0], positions[1]);
                        break;
                    case 'p': 
                        var names = moveDetails.Split('/');
                        Partner(programList, names[0][0], names[1][0]);
                        break;
                }
            }

         return printResult(programList);
        }


        public static string SolvePartTwo(string filePath, char[] programsAsChar)
        {
            var danceMovesStr = File.ReadAllLines(filePath).First().Split(',');
            ProgramList programList = new ProgramList {
                programs = programsAsChar.ToList(),
            };

            List<string> results = new List<string>();

            var initialResult = printResult(programList);
            results.Add(initialResult);

            int count = 0;
            // Parse all danceMoves
           while (true) {
                count++;
                foreach (var moveStr in danceMovesStr)
                {
                    var move = moveStr[0];
                    var moveDetails = moveStr.Substring(1);

                    switch (move)
                    {
                        case 's':
                            Spin(programList, int.Parse(moveDetails));
                            break;
                        case 'x':
                            var positions = moveDetails.Split('/').Select(int.Parse).ToList();
                            Exchange(programList, positions[0], positions[1]);
                            break;
                        case 'p':
                            var names = moveDetails.Split('/');
                            Partner(programList, names[0][0], names[1][0]);
                            break;
                    }
                }

                var result = printResult(programList);
                results.Add(result);
                if (result.Equals(results[0]))
                {
                    break;
                }
            }
            var index = 1000000000 % count;
            return results[index];
        }

        private static string printResult(ProgramList programList)
        {
            char[] result = new char[programList.programs.Count];
            var header = programList.header;
            var programs = programList.programs;
            for (int i = 0; i < programs.Count; i++)
            {
                int location = (header + i) % programs.Count;
                result[i] = programs[location];
            }
            return new string(result);
        }

        private static void Exchange(ProgramList programList, int positionA, int positionB) {
            var locationA = (programList.header + positionA) % programList.programs.Count;
            var locationB = (programList.header + positionB) % programList.programs.Count;
            char temp = programList.programs[locationA];
            programList.programs[locationA] = programList.programs[locationB];
            programList.programs[locationB] = temp;
        }

        private static void Partner(ProgramList programList, char nameA, char nameB) {
            var locationA = programList.programs.IndexOf(nameA);
            var locationB = programList.programs.IndexOf(nameB);
            char temp = programList.programs[locationA];
            programList.programs[locationA] = programList.programs[locationB];
            programList.programs[locationB] = temp;
        }

        private static void Spin(ProgramList programList, int size) {
            programList.header = (programList.header + (programList.programs.Count - size)) % programList.programs.Count;
        }

    }
}