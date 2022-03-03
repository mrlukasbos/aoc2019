
namespace day_17
{
    public class Program
    {


        public static int SolvePartOne(int steps) {
        
            List<int> buffer = new List<int>();

            buffer.Add(0);


            var currentPosition = 0;
            for (int i = 1; i < 2018; i++)
            {
                currentPosition = ((currentPosition + steps) % buffer.Count) + 1;

                buffer.Insert(currentPosition, i);
                if (i == 2017)
                {
                    return buffer[currentPosition+1];
                }
            }
            return 0;
        }


        public static int SolvePartTwo(int steps) {
            int bufferSize = 1;
            var result = 0;
            var currentPosition = 0;
            for (int i = 1; i <= 50000000; i++) {
                currentPosition = ((currentPosition + steps) % bufferSize) + 1;
                if (currentPosition == 1) {
                    result = i;
                }
                bufferSize++;
            }
            return result;
        }
    }
}