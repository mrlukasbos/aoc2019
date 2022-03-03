using NUnit.Framework;

namespace day_24.Tests
{
    public class Tests {
        [Test]
        public void PartOneTestInput()
        {
            Assert.AreEqual(31, Program.SolvePartOne("..\\..\\..\\day-24\\test-input.txt"));
        }

        [Test]
        public void PartOneRealInput()
        {
            Assert.AreEqual(1940, Program.SolvePartOne("..\\..\\..\\day-24\\real-input.txt"));
        }

        [Test]
        public void PartTwoTestInput()
        {
            Assert.AreEqual(19, Program.SolvePartTwo("..\\..\\..\\day-24\\test-input.txt"));
        }

        [Test]
        public void PartTwoRealInput()
        {
            Assert.AreEqual(1928, Program.SolvePartTwo("..\\..\\..\\day-24\\real-input.txt"));
        }
    }
}