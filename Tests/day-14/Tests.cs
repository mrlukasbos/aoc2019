using NUnit.Framework;

namespace day_14.Tests
{
    public class Tests {
        [Test]
        public void PartOneTestInput()
        {
            Assert.AreEqual(8108, Program.SolvePartOne("flqrgnkx"));
        }

        [Test]
        public void PartOneRealInput()
        {
            Assert.AreEqual(8216, Program.SolvePartOne("nbysizxe"));
        }

        [Test]
        public void PartTwoTestInput()
        {
            Assert.AreEqual(1242, Program.SolvePartTwo("flqrgnkx"));
        }

        [Test]
        public void PartTwoRealInput()
        {
            Assert.AreEqual(1139, Program.SolvePartTwo("nbysizxe"));
        }
    }
}