using NUnit.Framework;

namespace day_13.Tests
{
    public class Tests {
        [Test]
        public void PartOneTestInput()
        {
            Assert.AreEqual(24, Program.SolvePartOne("..\\..\\..\\day-13\\test-input.txt"));
        }

        [Test]
        public void PartOneRealInput()
        {
            Assert.AreEqual(2160, Program.SolvePartOne("..\\..\\..\\day-13\\real-input.txt"));
        }

        [Test]
        public void PartTwoTestInput()
        {
            Assert.AreEqual(10, Program.SolvePartTwo("..\\..\\..\\day-13\\test-input.txt"));
        }

        [Test]
        public void PartTwoRealInput()
        {
            Assert.AreEqual(3907470, Program.SolvePartTwo("..\\..\\..\\day-13\\real-input.txt"));
        }
    }
}