using NUnit.Framework;

namespace aoc2017.day_6 {
    [TestFixture]
    public class Tests {
        [Test]
        public void PartOneTestInput() {
            int[] banks = {
                0,
                2,
                7,
                0,
            };
            Assert.That(Program.SolvePartOne(banks), Is.EqualTo(5));
        }
        
        [Test]
        public void PartOneRealInput() {
            int[] banks = {
                5,
                1,
                10,
                0,
                1,
                7,
                13,
                14,
                3,
                12,
                8,
                10,
                7,
                12,
                0,
                6,
            };
            Assert.That(Program.SolvePartOne(banks), Is.EqualTo(5042));
        }
        
        
        [Test]
        public void PartTwoTestInput() {
            int[] banks = {
                0,
                2,
                7,
                0,
            };
            Assert.That(Program.SolvePartTwo(banks), Is.EqualTo(4));
        }
        
        [Test]
        public void PartTwoRealInput() {
            int[] banks = {
                5,
                1,
                10,
                0,
                1,
                7,
                13,
                14,
                3,
                12,
                8,
                10,
                7,
                12,
                0,
                6,
            };
            Assert.That(Program.SolvePartTwo(banks), Is.EqualTo(1086));
        }
    }
}