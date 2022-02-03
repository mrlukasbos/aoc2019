using NUnit.Framework;

namespace aoc2017.day_2 {
    [TestFixture]
    public class Tests {
        [Test]
        public void PartOneTestInput() {
            Assert.That(Program.SolvePartOneForFile("../../day-2/test-input-1.txt", ' '), Is.EqualTo(18));
        }
        
        [Test]
        public void PartOneRealInput() {
            Assert.That(Program.SolvePartOneForFile("../../day-2/real-input.txt", '\t'), Is.EqualTo(34925));
        }
        
        [Test]
        public void PartTwoTestInput() {
            Assert.That(Program.SolvePartTwoForFile("../../day-2/test-input-2.txt", ' '), Is.EqualTo(9));
        }
        
        [Test]
        public void PartTwoRealInput() {
            Assert.That(Program.SolvePartTwoForFile("../../day-2/real-input.txt", '\t'), Is.EqualTo(221));
        }
    }
}