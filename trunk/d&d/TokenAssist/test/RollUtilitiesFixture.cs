using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;

using TokenAssist;

namespace TokenAssistTest
{
    [TestFixture]
    public class RollUtilitiesFixture
    {
        [Test]
        public void TestEvaluateMaximumConstant()
        {
            int input = 1337;
            int output = RollUtilities.EvaluateMaximum(input.ToString());

            Assert.AreEqual(input, output);
        }

        [Test]
        public void TestEvaluateMaximumDiceRoll()
        {
            int diceRolls = 2;
            int diceFaces = 4;
            string input = string.Format("{0}d{1}", diceRolls, diceFaces);
            int output = RollUtilities.EvaluateMaximum(input);

            Assert.AreEqual(diceRolls * diceFaces, output);
        }

        [Test]
        public void TestEvaluateMaximumAddition()
        {
            int input1 = 1;
            int input2 = 5;
            string input = string.Format("{0} + {1}", input1, input2);
            int output = RollUtilities.EvaluateMaximum(input);

            Assert.AreEqual(input1 + input2, output);
        }

        [Test]
        public void TestEvaluateMaximumSubtraction()
        {
            int input1 = 7;
            int input2 = 3;
            string input = string.Format("{0} - {1}", input1, input2);
            int output = RollUtilities.EvaluateMaximum(input);

            Assert.AreEqual(input1 - input2, output);
        }
    }
}
