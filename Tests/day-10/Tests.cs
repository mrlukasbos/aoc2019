using NUnit.Framework;

namespace day_10.Tests
{
    public class Tests {
        [Test]
        public void PartOneTestInput()
        {
            int[] lengths = { 3, 4, 1, 5 };
            Assert.AreEqual(12, Program.SolvePartOne(5, lengths));
        }

        [Test]
        public void PartOneRealInput()
        {
            int[] lengths = { 70, 66, 255, 2, 48, 0, 54, 48, 80, 141, 244, 254, 160, 108, 1, 41 };
            Assert.AreEqual(7888, Program.SolvePartOne(256, lengths));
        }

        [Test]
        public void PartTwoTestInput()
        {
            Assert.AreEqual("3efbe78a8d82f29979031a4aa0b16a9d", Program.SolvePartTwo(256, "1,2,3"));
            Assert.AreEqual("a2582a3a0e66e6e86e3812dcb672a272", Program.SolvePartTwo(256, ""));
            Assert.AreEqual("63960835bcdc130f0b66d7ff4f6a5a8e", Program.SolvePartTwo(256, "1,2,4"));
            Assert.AreEqual("33efeb34ea91902bb2f59c9920caa6cd", Program.SolvePartTwo(256, "AoC 2017"));
        }

        [Test]
        public void PartTwoRealInput()
        {
            string lengths = "70,66,255,2,48,0,54,48,80,141,244,254,160,108,1,41";
            Assert.AreEqual("decdf7d377879877173b7f2fb131cf1b", Program.SolvePartTwo(256, lengths));
        }

    }
}