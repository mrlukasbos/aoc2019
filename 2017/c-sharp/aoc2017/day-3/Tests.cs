using NUnit.Framework;

namespace aoc2017.day_3 {
    [TestFixture]
    public class Tests {
        [Test]
        public void PartOneTestInput() {
            Assert.That(Program.solvePartOneForInput(1), Is.EqualTo(0));
            Assert.That(Program.solvePartOneForInput(12), Is.EqualTo(3));
            Assert.That(Program.solvePartOneForInput(23), Is.EqualTo(2));
            Assert.That(Program.solvePartOneForInput(24), Is.EqualTo(3));
            Assert.That(Program.solvePartOneForInput(22), Is.EqualTo(3));
            Assert.That(Program.solvePartOneForInput(1024), Is.EqualTo(31));
        }
        
        [Test]
        public void PartOneRealInput() {
            Assert.That(Program.solvePartOneForInput(368078), Is.EqualTo(371));
        }

        [Test]
        public void PartTwoRealInput() {
            Assert.That(Program.solvePartTwoForInput(368078), Is.EqualTo(369601));
        }
    }
}