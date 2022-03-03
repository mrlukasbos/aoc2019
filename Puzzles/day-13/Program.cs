
using System.Diagnostics;

namespace day_13
{
    public class Program
    {

        public static int SolvePartOne(string filePath)
        {
            Dictionary<int, int> layers;
            int maxLayer;
            parse(filePath, out layers, out maxLayer);

            return CalculateSeverity(layers, maxLayer);

        }
        public static int SolvePartTwo(string filePath)
        {
            Dictionary<int, int> layers;
            int maxLayer;
            parse(filePath, out layers, out maxLayer);

            var caught = true;
            var delay = 0;
            while (caught)
            {
                delay++; // This could be optimized by looking at the periods of the scanners, which should rule out quite some possibilities.
                caught = CheckIfGetCaught(layers, maxLayer, delay);
            }
            return delay;
        }

        private static void parse(string filePath, out Dictionary<int, int> layers, out int maxLayer)
        {
            var lines = File.ReadAllLines(filePath);
            layers = new Dictionary<int, int>();
            maxLayer = int.MinValue;
            foreach (var line in lines)
            {
                var splitted = line.Split(new string[] { ": " }, StringSplitOptions.None);
                var layer = int.Parse(splitted[0]);
                layers[layer] = int.Parse(splitted[1]);
                if (layer > maxLayer)
                {
                    maxLayer = layer;
                }
            }
        }

        private static int CalculateSeverity(Dictionary<int, int> layers, int maxLayer)
        {
            int severity = 0;
            for (int i = 0; i < maxLayer + 1; i++)
            {
                if (layers.ContainsKey(i))
                {
                    var depth = layers[i];
                    var scanIndex = ((i) % (2 * (depth - 1)));
                    if (scanIndex == 0)
                    {
                        severity += i * depth;
                    }
                }
            }
            return severity;
        }

        private static bool CheckIfGetCaught(Dictionary<int, int> layers, int maxLayer, int delay = 0)
        {
            for (int i = 0; i < maxLayer + 1; i++)
            {
                if (layers.ContainsKey(i) && (i + delay) % (2 * (layers[i] - 1)) == 0)
                {
                    return true;
                }
            }
            return false;
        }
    }
}