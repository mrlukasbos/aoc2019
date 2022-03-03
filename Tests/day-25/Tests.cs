using NUnit.Framework;

namespace day_25.Tests
{
    public class Tests {
        [Test]
        public void PartOneTestInput()
        {
            Assert.AreEqual(3, Program.SolvePartOne("..\\..\\..\\day-25\\test-input.txt"));
        }

        [Test]
        public void PartOneRealInput()
        {
            Assert.AreEqual(4387, Program.SolvePartOne("..\\..\\..\\day-25\\real-input.txt"));
        }
    }
}