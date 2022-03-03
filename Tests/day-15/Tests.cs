using NUnit.Framework;

namespace day_15.Tests
{
    public class Tests {
        [Test]
        public void PartOneTestInput()
        {
            Assert.AreEqual(588, Program.SolvePartOne(65, 8921));
        }

        [Test]
        public void PartOneRealInput()
        {
           Assert.AreEqual(577, Program.SolvePartOne(618, 814));
        }

        [Test]
        public void PartTwoTestInput()
        {
            Assert.AreEqual(309, Program.SolvePartTwo(65, 8921));
        }

        [Test]
        public void PartTwoRealInput()
        {
            Assert.AreEqual(316, Program.SolvePartTwo(618, 814));
        }

    }
}