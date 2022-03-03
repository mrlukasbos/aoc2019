using NUnit.Framework;

namespace day_19.Tests
{
    public class Tests {
        [Test]
        public void PartOneTestInput()
        {
            Assert.AreEqual("ABCDEF", Program.SolvePartOne("..\\..\\..\\day-19\\test-input.txt"));
        }

        [Test]
        public void PartOneRealInput()
        {
            Assert.AreEqual("BPDKCZWHGT", Program.SolvePartOne("..\\..\\..\\day-19\\real-input.txt"));
        }

        [Test]
        public void PartTwoTestInput()
        {
            Assert.AreEqual(38, Program.SolvePartTwo("..\\..\\..\\day-19\\test-input.txt"));
        }

        [Test]
        public void PartTwoRealInput()
        {
            Assert.AreEqual(17728, Program.SolvePartTwo("..\\..\\..\\day-19\\real-input.txt"));
        }
    }
}