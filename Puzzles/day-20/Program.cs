
namespace day_20
{
    public class Program {
        class Particle
        {
            public int id;
            public (int x, int y, int z) p;
            public (int x, int y, int z) v;
            public (int x, int y, int z) a;
        }


        // Performance can be improved by checking for smallest acceleration during string parsing, and ignoring the velocity and position entirely.
        public static int SolvePartOne(string filePath) {
            List<Particle> particles = parseParticles(filePath);

            // Return the particle with the lowest total acceleration
            var sortedParticles = particles.OrderBy(particle => Math.Abs(particle.a.x) + Math.Abs(particle.a.y) + Math.Abs(particle.a.z)).ToList();
            return sortedParticles[0].id;
        }

        // This solution is really not clean, and may fail for other inputs.
        // It just simulates the model for 200 times and hopes for the best
        public static int SolvePartTwo(string filePath)
        {
            List<Particle> particles = parseParticles(filePath);

            for (int i = 0; i < 200; i++)
            {
                Step(particles);
                removeCollisions(particles);
                Console.WriteLine(particles.Count);
            }

            return particles.Count;
        }

        private static List<Particle> parseParticles(string filePath)
        {
            return File.ReadAllLines(filePath).Select((line, index) =>
            {
                var partsAsString = line.Split('<').Select(part => part.Split('>')[0]).Skip(1);
                var parts = partsAsString.Select(part => part.Split(',').Select(int.Parse).ToList()).ToList();
                return new Particle()
                {
                    id = index,
                    p = (parts[0][0], parts[0][1], parts[0][2]),
                    v = (parts[1][0], parts[1][1], parts[1][2]),
                    a = (parts[2][0], parts[2][1], parts[2][2]),
                };
            }).ToList();
        }

        private static void Step(List<Particle> particles)
        {
            foreach (var particle in particles)
            {
                particle.v.x += particle.a.x;
                particle.v.y += particle.a.y;
                particle.v.z += particle.a.z;
                particle.p.x += particle.v.x;
                particle.p.y += particle.v.y;
                particle.p.z += particle.v.z;
            }
        }

        private static void removeCollisions(List<Particle> particles)
        {
            // Sort in place
            particles.Sort((particleX, particleY) =>
            {
                var sumX = Math.Abs(particleX.p.x) + Math.Abs(particleX.p.y) + Math.Abs(particleX.p.z);
                var sumY = Math.Abs(particleY.p.x) + Math.Abs(particleY.p.y) + Math.Abs(particleY.p.z);
                return sumX.CompareTo(sumY);
            });

            bool didRemove = false;
            for (int i = particles.Count - 2; i >= 0; i--) {
                var particle = particles[i];
                if (particle.p == particles[i+1].p)
                {
                    particles.RemoveAt(i+1);
                    didRemove = true;
                } else
                {
                    if (didRemove)
                    {
                        particles.RemoveAt(i+1);
                        didRemove = false;
                    }
                }
            }
            if (didRemove)
            {
                particles.RemoveAt(0);
            }

        }

    }
}