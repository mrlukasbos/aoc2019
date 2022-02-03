using NUnit.Framework;

namespace aoc2017.day_7 {
    [TestFixture]
    public class Tests {
        [Test]
        public void PartOneTestInput() {
            Assert.That(Program.SolvePartOneForFile("../../day-7/test-input.txt"), Is.EqualTo("tknk"));
        }
        
        [Test]
        public void PartOneRealInput() {
            Assert.That(Program.SolvePartOneForFile("../../day-7/real-input.txt"), Is.EqualTo("rqwgj"));
        }
        
        // [Test]
        // public void PartTwoTestInput() {
        //     Assert.That(Program.SolvePartTwoForFile("../../day-5/test-input.txt"), Is.EqualTo(10));
        // }
        //
        // [Test]
        // public void PartTwoRealInput() {
        //     Assert.That(Program.SolvePartTwoForFile("../../day-5/real-input.txt"), Is.EqualTo(21841249));
        // }
    }
}