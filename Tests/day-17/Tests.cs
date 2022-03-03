using NUnit.Framework;

namespace day_17.Tests
{
    public class Tests {
        [Test]
        public void PartOneTestInput()
        {
            Assert.AreEqual(638, Program.SolvePartOne(3));
        }

        [Test]
        public void PartOneRealInput()
        {
            Assert.AreEqual(180, Program.SolvePartOne(316));
        }

        [Test]
        public void PartTwoRealInput()
        {
            Assert.AreEqual(13326437, Program.SolvePartTwo(316));
        }
    }
}