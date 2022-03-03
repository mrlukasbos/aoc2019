using NUnit.Framework;

namespace day_16.Tests
{
    public class Tests {
        [Test]
        public void PartOneTestInput()
        {
            char[] programs = "abcde".ToCharArray();
            Assert.AreEqual("baedc", Program.SolvePartOne("..\\..\\..\\day-16\\test-input.txt", programs));
        }

        [Test]
        public void PartOneRealInput()
        {
            char[] programs = "abcdefghijklmnop".ToCharArray();
            Assert.AreEqual("bkgcdefiholnpmja", Program.SolvePartOne("..\\..\\..\\day-16\\real-input.txt", programs));
        }

        [Test]
        public void PartTwoTestInput()
        {
            char[] programs = "abcde".ToCharArray();
            Assert.AreEqual("abcde", Program.SolvePartTwo("..\\..\\..\\day-16\\test-input.txt", programs));
        }

        [Test]
        public void PartTwoRealInput()
        {
            char[] programs = "abcdefghijklmnop".ToCharArray();
            Assert.AreEqual("knmdfoijcbpghlea", Program.SolvePartTwo("..\\..\\..\\day-16\\real-input.txt", programs));
        }
    }
}