using NUnit.Framework;

namespace day_12.Tests
{
    public class Tests {
        [Test]
        public void PartOneTestInput()
        {
            Assert.AreEqual(6, Program.SolvePartOne("..\\..\\..\\day-12\\test-input.txt"));
        }

        [Test]
        public void PartOneRealInput()
        {
            Assert.AreEqual(306, Program.SolvePartOne("..\\..\\..\\day-12\\real-input.txt"));
        }

        [Test]
        public void PartTwoTestInput()
        {
            Assert.AreEqual(2, Program.SolvePartTwo("..\\..\\..\\day-12\\test-input.txt"));
        }

        [Test]
        public void PartTwoRealInput()
        {
            Assert.AreEqual(200, Program.SolvePartTwo("..\\..\\..\\day-12\\real-input.txt"));
        }
    }
}