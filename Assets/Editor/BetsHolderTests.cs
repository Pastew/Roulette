using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System.Linq;
using System;

namespace Tests
{
    public class BetsHolderTests
    {
        private int chips = 10;

        [Test]
        public void StraightLose()
        {
            BetsHolder betsHolder = new BetsHolder();
            betsHolder.AddPlayerBet(BetDef.BetType.Straight, chips, 4);
            betsHolder.AddPlayerBet(BetDef.BetType.Straight, chips, 5);

            int wheelResult = 1;
            List<Bet> winningBets = betsHolder.GetWinningBets(wheelResult);

            Assert.AreEqual(winningBets.Count, 0);
        }

        [Test]
        public void StraightWin()
        {
            BetsHolder betsHolder = new BetsHolder();
            betsHolder.AddPlayerBet(BetDef.BetType.Straight, chips, 4);
            betsHolder.AddPlayerBet(BetDef.BetType.Straight, chips, 5);

            int wheelResults = 4;
            List<Bet> winningBets = betsHolder.GetWinningBets(wheelResults);

            Assert.AreEqual(1, winningBets.Count);
            winningBets.ForEach(bet => Assert.IsTrue(bet.Numbers.Contains(wheelResults)));
        }

        [Test]
        public void RedWin()
        {
            BetsHolder betsHolder = new BetsHolder();
            betsHolder.AddPlayerBet(BetDef.BetType.Red, chips);

            List<int> redNumbers = BetDef.numbers[BetDef.BetType.Red].ToList();

            foreach (int winningNumber in redNumbers)
            {
                List<Bet> winningBets = betsHolder.GetWinningBets(winningNumber);

                Assert.AreEqual(1, winningBets.Count);

                Bet playerBet = betsHolder.PlayerBets.First();
                Assert.IsTrue(playerBet.Numbers.Contains(winningNumber));
            }
        }

        [Test]
        public void RedLose()
        {
            BetsHolder betsHolder = new BetsHolder();
            betsHolder.AddPlayerBet(BetDef.BetType.Red, chips);

            List<int> allPossibleNumbers = GetAllPossibleValues();
            List<int> redNumbers = BetDef.numbers[BetDef.BetType.Red].ToList();

            List<int> allNumbersWithoutRedNumbers = allPossibleNumbers.FindAll(number => !redNumbers.Contains(number));

            foreach (int winningNumber in allNumbersWithoutRedNumbers)
            {
                List<Bet> winningBets = betsHolder.GetWinningBets(winningNumber);

                Assert.AreEqual(0, winningBets.Count);
            }
        }

        private List<int> GetAllPossibleValues()
        {
            return Enumerable.Range(0, 36 + 1).ToList();
        }
    }
}
