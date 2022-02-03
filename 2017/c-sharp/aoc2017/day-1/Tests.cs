using System;
using System.IO;
using NUnit.Framework;

namespace aoc2017.day_1 {
    [TestFixture]
    public class Tests {
        [Test]
        public void PartOneTestInput() {
            Assert.That(Program.solvePartOneForInput("1122"), Is.EqualTo(3));
            Assert.That(Program.solvePartOneForInput("1111"), Is.EqualTo(4));
            Assert.That(Program.solvePartOneForInput("1234"), Is.EqualTo(0));
            Assert.That(Program.solvePartOneForInput("91212129"), Is.EqualTo(9));
        }
        
        [Test]
        public void PartOneRealInput() {
            var input = File.ReadAllText("../../day-1/input.txt");
            Assert.That(Program.solvePartOneForInput(input), Is.EqualTo(995));
        }
        
        [Test] 
        public void PartTwoTestInput() {
            Assert.That(Program.solvePartTwoForInput("1212"), Is.EqualTo(6));
            Assert.That(Program.solvePartTwoForInput("1221"), Is.EqualTo(0));
            Assert.That(Program.solvePartTwoForInput("123425"), Is.EqualTo(4));
            Assert.That(Program.solvePartTwoForInput("123123"), Is.EqualTo(12));
            Assert.That(Program.solvePartTwoForInput("12131415"), Is.EqualTo(4));
        }
        
        [Test]
        public void PartTwoRealInput() {
            var input = File.ReadAllText("../../day-1/input.txt");
            Assert.That(Program.solvePartTwoForInput(input), Is.EqualTo(1130));
        }
    }
}