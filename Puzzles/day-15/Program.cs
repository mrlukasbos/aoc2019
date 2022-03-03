
using System.Diagnostics;

namespace day_15
{
    public class Program
    {
        public static int SolvePartOne(long genA, long genB) {
            int factorA = 16807;
            int factorB = 48271;
            int divider = 2147483647; // this is equal to maxInt

            int count = 0;
            for (int i = 0; i < 40000000; i++) {
                genA = (genA * factorA) % divider;
                genB = (genB * factorB) % divider;

                if (((genA ^ genB) & 0xFFFF) == 0) {
                    count++;
                }
            }
            return count;
        }

        public static int SolvePartTwo(long genA, long genB)
        {
            int factorA = 16807;
            int factorB = 48271;
            int divider = 2147483647; // this is equal to maxInt
            int count = 0;

            for (int i = 0; i < 5000000; i++)
            {
                do
                {
                    genA = (genA * factorA) % divider;
                } while ((genA % 4) != 0);

                do
                {
                    genB = (genB * factorB) % divider;
                } while ((genB % 8) != 0);

                if (((genA ^ genB) & 0xFFFF) == 0)
                {
                    count++;
                }

            }
            return count;
        }
    }
}