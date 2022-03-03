using NUnit.Framework;
using System.Collections.Generic;

namespace day_21.Tests
{
    public class Tests {
        [Test]
        public void PartOneTestInput()
        {
            Assert.AreEqual(12, Program.SolvePartOne(2, "..\\..\\..\\day-21\\test-input.txt"));
        }

        //[Test]
        //public void JoinTest()
        //{
        //    var b1 = new List<string> { "12".ToCharArray(), "34".ToCharArray() };
        //    var b2 = new List<string> { "ab".ToCharArray(), "cd".ToCharArray() };
        //    var b3 = new List<string> { "ef".ToCharArray(), "gh".ToCharArray() };
        //    var b4 = new List<string> { "ij".ToCharArray(), "kl".ToCharArray() };
        //    var row1 = new List<List<string>>() { b1, b2 };
        //    var row2 = new List<List<string>>() { b3, b4 };
        //    var merged = new List<List<List<string>>>() { row1, row2 };

        //    var joined = Program.Join(merged);
        //    // "12ab", "34cd", "efij", "ghkl"
        //    Assert.AreEqual("12ab", joined[0]);
        //    Assert.AreEqual("34cd", joined[1]);
        //    Assert.AreEqual("efij", joined[2]);
        //    Assert.AreEqual("ghkl", joined[3]);
        //}

        //[Test]
        //public void DivideTest()
        //{
        //    var joined = new List<char[]> { 
        //        "12ab".ToCharArray(), 
        //        "34cd".ToCharArray(), 
        //        "efij".ToCharArray(), 
        //        "ghkl".ToCharArray(),
        //    };
        //    var divided = Program.Divide(joined);
        //    Assert.AreEqual("12", divided[0][0][0]);
        //    Assert.AreEqual("34", divided[0][0][1]);
        //    Assert.AreEqual("ab", divided[0][1][0]);
        //    Assert.AreEqual("cd", divided[0][1][1]);
        //    Assert.AreEqual("ef", divided[1][0][0]);
        //    Assert.AreEqual("gh", divided[1][0][1]);
        //    Assert.AreEqual("ij", divided[1][1][0]);
        //    Assert.AreEqual("kl", divided[1][1][1]);
        //}

        [Test]
        public void PartOneRealInput()
        {
            Assert.AreEqual(123, Program.SolvePartOne(5, "..\\..\\..\\day-21\\real-input.txt"));
        }

        [Test]
        public void PartTwoRealInput()
        {
            Assert.AreEqual(1984683, Program.SolvePartOne(18, "..\\..\\..\\day-21\\real-input.txt"));
        }

        //[Test]
        //public void PatternMatchTest()
        //{

        //    List<string> patternToMatch = new List<string>
        //        {
        //            "#..", "..#", "##.",
        //        };


        //    List<string> matchable = new List<string>
        //        {
        //            "##.", "..#", "#..",
        //        };

        //    Assert.IsTrue(Program.PatternsMatch(patternToMatch, matchable));
        //}

       

    }
}