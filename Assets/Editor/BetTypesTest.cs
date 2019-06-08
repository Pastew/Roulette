using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System.Linq;

namespace Tests
{
    public class BetTypesTest
    {
        private readonly int chips = 20;

        [Test]
        public void RedBetConstructorTest()
        {
            Bet bet = new Bet(BetDef.BetType.Red, chips);

            List<int> actual = bet.Numbers;
            List<int> expected = new List<int>(){ 1, 3, 5, 7, 9, 12, 14, 16, 18, 19, 21, 23, 25, 27, 30, 32, 34, 36 };

            Assert.IsTrue(expected.SequenceEqual(actual), "Expected: " + string.Join(",", expected) + ", actual: " + string.Join(",", actual));
        }

        [Test]
        public void ThirdDozenBetConstructorTest()
        {
            Bet bet = new Bet(BetDef.BetType.ThirdDozen, chips);

            List<int> actual = bet.Numbers;
            List<int> expected = new List<int>() { 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36 };

            Assert.IsTrue(expected.SequenceEqual(actual), "Expected: " + string.Join(",", expected) + ", actual: " + string.Join(",", actual));
        }
    }
}
