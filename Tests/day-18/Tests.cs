using NUnit.Framework;

namespace day_18.Tests
{
    public class Tests {
        [Test]
        public void PartOneTestInput()
        {
            Assert.AreEqual(4, Program.SolvePartOne("..\\..\\..\\day-18\\test-input.txt"));
        }

        [Test]
        public void PartOneRealInput()
        {
            Assert.AreEqual(9423, Program.SolvePartOne("..\\..\\..\\day-18\\real-input.txt"));
        }

        [Test]
        public void PartTwoTestInput()
        {
            Assert.AreEqual(3, Program.SolvePartTwo("..\\..\\..\\day-18\\test-input-2.txt"));
        }

        [Test]
        public void PartTwoRealInput()
        {
            Assert.AreEqual(7620, Program.SolvePartTwo("..\\..\\..\\day-18\\real-input.txt"));
        }
    }
}