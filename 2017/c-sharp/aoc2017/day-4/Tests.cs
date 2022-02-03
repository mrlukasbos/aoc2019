using NUnit.Framework;

namespace aoc2017.day_4 {
    [TestFixture]
    public class Tests {
        [Test]
        public void PartOneTestInput() {
            Assert.That(Program.SolvePartOneForFile("../../day-4/test-input.txt"), Is.EqualTo(2));
        }
        
        [Test]
        public void PartOneRealInput() {
            Assert.That(Program.SolvePartOneForFile("../../day-4/real-input.txt"), Is.EqualTo(337));
        }
        
        [Test]
        public void PartTwoTestInput() {
            Assert.That(Program.SolvePartTwoForFile("../../day-4/test-input-2.txt"), Is.EqualTo(3));
        }
        
        [Test]
        public void PartTwoRealInput() {
            Assert.That(Program.SolvePartTwoForFile("../../day-4/real-input.txt"), Is.EqualTo(231));
        }
    }
}