using NUnit.Framework;

namespace day_22.Tests
{
    public class Tests {
        [Test]
        public void PartOneTestInput()
        {
            Assert.AreEqual(5, Program.SolvePartOne(7, "..\\..\\..\\day-22\\test-input.txt"));
            Assert.AreEqual(41, Program.SolvePartOne(70, "..\\..\\..\\day-22\\test-input.txt"));
            Assert.AreEqual(5587, Program.SolvePartOne(10000, "..\\..\\..\\day-22\\test-input.txt"));
        }

        [Test]
        public void PartOneRealInput()
        {
            Assert.AreEqual(5261, Program.SolvePartOne(10000, "..\\..\\..\\day-22\\real-input.txt"));
        }

        [Test]
        public void PartTwoTestInput()
        {
            Assert.AreEqual(26, Program.SolvePartTwo(100, "..\\..\\..\\day-22\\test-input.txt"));
            Assert.AreEqual(2511944, Program.SolvePartTwo(10000000, "..\\..\\..\\day-22\\test-input.txt"));
        }

        [Test]
        public void PartTwoRealInput()
        {
            Assert.AreEqual(2511927, Program.SolvePartTwo(10000000, "..\\..\\..\\day-22\\real-input.txt"));
        }
    }
}