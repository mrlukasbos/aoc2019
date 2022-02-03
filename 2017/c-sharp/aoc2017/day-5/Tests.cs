using NUnit.Framework;

namespace aoc2017.day_5 {
    [TestFixture]
    public class Tests {
        [Test]
        public void PartOneTestInput() {
            Assert.That(Program.SolvePartOneForFile("../../day-5/test-input.txt"), Is.EqualTo(5));
        }
        
        [Test]
        public void PartOneRealInput() {
            Assert.That(Program.SolvePartOneForFile("../../day-5/real-input.txt"), Is.EqualTo(326618));
        }
        
        [Test]
        public void PartTwoTestInput() {
            Assert.That(Program.SolvePartTwoForFile("../../day-5/test-input.txt"), Is.EqualTo(10));
        }
        
        [Test]
        public void PartTwoRealInput() {
            Assert.That(Program.SolvePartTwoForFile("../../day-5/real-input.txt"), Is.EqualTo(21841249));
        }
    }
}