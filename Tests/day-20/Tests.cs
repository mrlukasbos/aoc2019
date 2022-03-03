using NUnit.Framework;

namespace day_20.Tests
{
    public class Tests {
        [Test]
        public void PartOneTestInput()
        {
            Assert.AreEqual(0, Program.SolvePartOne("..\\..\\..\\day-20\\test-input.txt"));
        }

        [Test]
        public void PartOneRealInput()
        {
            Assert.AreEqual(91, Program.SolvePartOne("..\\..\\..\\day-20\\real-input.txt"));
        }


        [Test]
        public void PartTwoTestInput()
        {
            Assert.AreEqual(1, Program.SolvePartTwo("..\\..\\..\\day-20\\test-input-2.txt"));
        }

        [Test]
        public void PartTwoRealInput()
        {
            Assert.AreEqual(567, Program.SolvePartTwo("..\\..\\..\\day-20\\real-input.txt"));
        }

    }
}