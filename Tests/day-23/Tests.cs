using NUnit.Framework;

namespace day_23.Tests
{
    public class Tests {
        [Test]
        public void PartOneTestInput()
        {
            Assert.AreEqual(5, Program.SolvePartOne("..\\..\\..\\day-23\\test-input.txt"));
        }

        [Test]
        public void PartOneRealInput()
        {
            Assert.AreEqual(9409, Program.SolvePartOne("..\\..\\..\\day-23\\real-input.txt"));
        }

        [Test]
        public void PartTwoRealInput()
        {
            Assert.AreEqual(913, Program.SolvePartTwo());
        }
    }
}