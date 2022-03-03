using NUnit.Framework;
using System.IO;

namespace day_11.Tests
{
    public class Tests {
        [Test]
        public void PartOneTestInput()
        {
            Assert.AreEqual(3, Program.SolvePartOne("ne,ne,ne"));
            Assert.AreEqual(0, Program.SolvePartOne("ne,ne,sw,sw"));
            Assert.AreEqual(2, Program.SolvePartOne("ne,ne,s,s"));
            Assert.AreEqual(3, Program.SolvePartOne("se,sw,se,sw,sw"));
        }

        [Test]
        public void PartOneRealInput()
        {
            var result = Program.SolvePartOne(File.ReadAllLines("..\\..\\..\\day-11\\input.txt")[0]);
            Assert.AreEqual(784, result);
        }

        [Test]
        public void PartTwoTestInput() 
        {
            Assert.AreEqual(3, Program.SolvePartTwo("ne,ne,ne"));
            Assert.AreEqual(2, Program.SolvePartTwo("ne,ne,sw,sw"));
            Assert.AreEqual(2, Program.SolvePartTwo("ne,ne,s,s"));
            Assert.AreEqual(3, Program.SolvePartTwo("se,sw,se,sw,sw"));
        }

        [Test]
        public void PartTwoRealInput()
        {
            var result = Program.SolvePartTwo(File.ReadAllLines("..\\..\\..\\day-11\\input.txt")[0]);
            Assert.AreEqual(1558, result);
        }
    }
}