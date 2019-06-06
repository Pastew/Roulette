using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System.Linq;
using System;

namespace Tests
{
    public class WinCalculatorTest
    {
        private int chips = 10;

        [Test]
        public void StraightOneNumber()
        {
            Bet bet = new Bet(BetDef.BetType.Straight, chips, 15);
            List<Bet> bets = new List<Bet> { bet };

            WinCalculator winCalculator = new WinCalculator();

            int expected = chips * BetDef.payout[BetDef.BetType.Straight];
            int actual = winCalculator.CalculatePlayerWinningAmount(bets);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void StraightTwoNumbers()
        {
            List<Bet> bets = new List<Bet> {
                new Bet(BetDef.BetType.Straight, chips, 15),
                new Bet(BetDef.BetType.Straight, chips, 16)
            };

            WinCalculator winCalculator = new WinCalculator();

            int expected = 2 * chips * BetDef.payout[BetDef.BetType.Straight];
            int actual = winCalculator.CalculatePlayerWinningAmount(bets);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void RedWin()
        {
            List<Bet> bets = new List<Bet> {
                new Bet(BetDef.BetType.Red, chips),
            };

            WinCalculator winCalculator = new WinCalculator();

            int expected = chips * BetDef.payout[BetDef.BetType.Red];
            int actual = winCalculator.CalculatePlayerWinningAmount(bets);

            Assert.AreEqual(expected, actual);
        }
    }
}
