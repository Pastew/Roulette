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

        // =======
        // Helpers
        // =======
        private void PerformTest(int expectedReward, List<Bet> bets)
        {
            int actualReward = new WinCalculator().CalculatePlayerWinningAmount(bets);
            Assert.AreEqual(expectedReward, actualReward);
        }

        private void PerformTest(int expectedReward, Bet bet)
        {
            PerformTest(expectedReward, new List<Bet>() { bet });
        }

        // ================================================================================================
        // Test scenarios taken from here: https://www.roulettephysics.com/roulette-bets-odds-and-payouts/
        // ================================================================================================

        // (1) Straight (1 number): 35-1 payout (pays your original bet PLUS 35 units). Example covers number 2
        [Test]
        public void StraightTest()
        {
            Bet bet = new Bet(BetDef.BetType.Straight, chips, 2);
            int expectedReward = chips + 35 * chips;

            PerformTest(expectedReward, bet);
        }

        // (2) Split (2 numbers): 17-1 payout (pays your original bet PLUS 17 units). The example covers numbers 2 & 6.
        [Test]
        public void SplitTest()
        {
            Bet bet = new Bet(BetDef.BetType.Split, chips, 2, 6);
            int expectedReward = chips + 17 * chips;

            PerformTest(expectedReward, bet);
        }

        // (3) Street (3 numbers) : 11-1 payout (pays your original bet PLUS 11 units). The example covers 7, 8 & 9.
        [Test]
        public void StreetTest()
        {
            Bet bet = new Bet(BetDef.BetType.Street, chips, 7, 8, 9);
            int expectedReward = chips + 11 * chips;

            PerformTest(expectedReward, bet);
        }

        // (4) Square (4 numbers): 8-1 payout (pays your original bet PLUS 8 units). The example covers 8, 9, 11 & 12.
        [Test]
        public void SquareTest()
        {
            Bet bet = new Bet(BetDef.BetType.Corner, chips, 8, 9, 11, 12);
            int expectedReward = chips + 8 * chips;

            PerformTest(expectedReward, bet);
        }

        // (5) Six Line (6 numbers): 5-1 payout (pays your original bet PLUS 5 units). The example covers numbers 13, 14, 15, 16, 17 & 18.
        [Test]
        public void SixLineTest()
        {
            Bet bet = new Bet(BetDef.BetType.SixLine, chips, 13, 14, 15, 16, 17, 18);
            int expectedReward = chips + 5 * chips;

            PerformTest(expectedReward, bet);
        }

        // (6) Black Color (18 numbers): 1:1 payout (pays your original bet PLUS 1 unit). 
        [Test]
        public void BlackTest()
        {
            int expectedReward = chips + 1 * chips;

            Bet bet = new Bet(BetDef.BetType.Black, chips);
            PerformTest(expectedReward, bet);
        }

        // Red Color
        [Test]
        public void RedTest()
        {
            int expectedReward = chips + 1 * chips;

            Bet bet = new Bet(BetDef.BetType.Red, chips);
            PerformTest(expectedReward, bet);
        }

        // (7) Dozens (12 Numbers): 2-1 payout (pays your original bet PLUS 2 units).
        [Test]
        public void DozensTest()
        {
            int expectedReward = chips + 2 * chips;

            Bet bet = new Bet(BetDef.BetType.FirstDozen, chips);
            PerformTest(expectedReward, bet);

            bet = new Bet(BetDef.BetType.SecondDozen, chips);
            PerformTest(expectedReward, bet);

            bet = new Bet(BetDef.BetType.ThirdDozen, chips);
            PerformTest(expectedReward, bet);
        }

        // (8) Highs / Lows (1-18 or 19-36): 1-1 payout (pays your original bet PLUS 1 unit). 
        [Test]
        public void From1To18Test()
        {
            int expectedReward = chips + 1 * chips;

            Bet bet = new Bet(BetDef.BetType.From1To18, chips);
            PerformTest(expectedReward, bet);
        }

        [Test]
        public void From19To36Test()
        {
            int expectedReward = chips + 1 * chips;

            Bet bet = new Bet(BetDef.BetType.From19To36, chips);
            PerformTest(expectedReward, bet);
        }

        // (9) Odds/Evens (18 numbers): 1-1 payout (pays your original bet PLUS 1 unit). 
        [Test]
        public void OddsTest()
        {
            int expectedReward = chips + 1 * chips;

            Bet bet = new Bet(BetDef.BetType.Odd, chips);
            PerformTest(expectedReward, bet);
        }

        [Test]
        public void EvensTest()
        {
            int expectedReward = chips + 1 * chips;

            Bet bet = new Bet(BetDef.BetType.Even, chips);
            PerformTest(expectedReward, bet);
        }

        //(10) Columns (12 numbers): 2-1 payout (pays your original bet PLUS 2 units).
        [Test]
        public void FirstColumnTest()
        {
            int expectedReward = chips + 2 * chips;

            Bet bet = new Bet(BetDef.BetType.FirstColumn, chips);
            PerformTest(expectedReward, bet);
        }

        [Test]
        public void SecondColumnTest()
        {
            int expectedReward = chips + 2 * chips;

            Bet bet = new Bet(BetDef.BetType.SecondColumn, chips);
            PerformTest(expectedReward, bet);
        }

        [Test]
        public void ThirdColumnTest()
        {
            int expectedReward = chips + 2 * chips;

            Bet bet = new Bet(BetDef.BetType.ThirdColumn, chips);
            PerformTest(expectedReward, bet);
        }

        // ================================
        // Tests not covered on the website
        // ================================
        [Test]
        public void FirstFourTest()
        {
            Bet bet = new Bet(BetDef.BetType.FirstFour, chips);
            int expectedReward = chips + 8 * chips;

            PerformTest(expectedReward, bet);
        }

        [Test]
        public void Trio012Test()
        {
            Bet bet = new Bet(BetDef.BetType.Trio012, chips);
            int expectedReward = chips + 11 * chips;

            PerformTest(expectedReward, bet);
        }

        [Test]
        public void Trio023Test()
        {
            Bet bet = new Bet(BetDef.BetType.Trio023, chips);
            int expectedReward = chips + 11 * chips;

            PerformTest(expectedReward, bet);
        }

        // ===================
        // Multiple bets tests
        // ===================
        [Test]
        public void BlackAndEvenTest()
        {

            int chipsOnBlack = 25;
            int chipsOnEven = 100;
            int expectedReward = chipsOnBlack + chipsOnBlack * 1 + chipsOnEven + chipsOnEven * 1; // 250
            Bet betOnBlack = new Bet(BetDef.BetType.Black, chipsOnBlack);
            Bet betOnEven = new Bet(BetDef.BetType.Red, chipsOnEven);

            PerformTest(expectedReward, new List<Bet>() { betOnBlack, betOnEven });
        }

        [Test]
        public void BlackAndEvenAndStraightTest()
        {
            int chipsOnBlack = 25;
            int chipsOnEven = 100;
            int chipsOnStraight = 100;
            int expectedReward = chipsOnBlack + chipsOnBlack * 1 + chipsOnEven + chipsOnEven * 1 + chipsOnStraight + chipsOnStraight * 35; // 50+200+3600 = 3850
            Bet betOnBlack = new Bet(BetDef.BetType.Black, chipsOnBlack);
            Bet betOnEven = new Bet(BetDef.BetType.Red, chipsOnEven);
            Bet betOnStraight = new Bet(BetDef.BetType.Straight, chipsOnStraight, 2);

            PerformTest(expectedReward, new List<Bet>() { betOnBlack, betOnEven, betOnStraight});
        }
    }
}
