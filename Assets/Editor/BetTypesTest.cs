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
        private readonly int betCoins = 20;

        [Test]
        public void RedBetTest()
        {
            Bet bet = new Bet(BetDef.BetType.Red, betCoins);

            List<int> actual = bet.Numbers;
            List<int> expected = BetDef.numbers[BetDef.BetType.Red].ToList();

            Assert.IsTrue(expected.SequenceEqual(actual), "Expected: " + string.Join(",", expected) + ", actual: " + string.Join(",", actual));
        }

        [Test]
        public void StraightBetTest()
        {
            int playerNumberChoose = 20;
            Bet bet = new Bet(BetDef.BetType.Straight, betCoins, playerNumberChoose);

            List<int> actual = bet.Numbers;
            List<int> expected = new List<int> { playerNumberChoose };

            Assert.IsTrue(expected.SequenceEqual(actual), "Expected: " + string.Join(",", expected) + ", actual: " + string.Join(",", actual));
        }

        [Test]
        public void SplitBetTest()
        {
            List<int> playerNumbersChoose = new List<int> { 17, 20};
            Bet bet = new Bet(BetDef.BetType.Straight, betCoins, playerNumbersChoose.ToArray());

            List<int> actual = bet.Numbers;
            List<int> expected = playerNumbersChoose;

            Assert.IsTrue(expected.SequenceEqual(actual), "Expected: " + string.Join(",", expected) + ", actual: " + string.Join(",", actual));
        }
    }
}
