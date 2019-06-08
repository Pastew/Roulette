using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System.Linq;

namespace Tests
{
    public class FullGameTests
    {
        BetsHolder betsHolder;
        WinCalculator winCalculator;
        IWheel fakeWheelAlwaysReturningNumber2;

        [SetUp]
        public void TestSetup()
        {
            winCalculator = new WinCalculator();
            betsHolder = new BetsHolder();
            fakeWheelAlwaysReturningNumber2 = new FakeWheelReturningGivenNumber(2);
        }

        [Test]
        public void WinningBlackAndEvenAndStraightTest()
        {
            // Gather bets from player
            int chipsOnBlack = 25;
            int chipsOnEven = 100;
            int chipsOnStraight = 100;
            int expectedReward = chipsOnBlack + chipsOnBlack * 1 + chipsOnEven + chipsOnEven * 1 + chipsOnStraight + chipsOnStraight * 35; // 250 + 35*100 = 3750
            betsHolder.AddPlayerBet(BetDef.BetType.Black, chipsOnBlack);
            betsHolder.AddPlayerBet(BetDef.BetType.Even, chipsOnEven);
            betsHolder.AddPlayerBet(BetDef.BetType.Straight, chipsOnStraight, 2);
            
            // Spin and gather winning bets
            int winningNumber = fakeWheelAlwaysReturningNumber2.Spin();
            List<Bet> winningBets = betsHolder.GetWinningBets(winningNumber);

            // Calculate final win
            int actualReward = winCalculator.CalculatePlayerWinningAmount(winningBets);
            Assert.AreEqual(expectedReward, actualReward);
        }

        // Same test as above, but added two losing bets. Reward should be the same
        [Test]
        public void WinningBlackAndEvenAndStraightAndLosingStraightAndFirstColumnTest()
        {
            // Gather bets from player
            int chipsOnBlack = 25;
            int chipsOnEven = 100;
            int chipsOnStraight = 100;            
            int expectedReward = chipsOnBlack + chipsOnBlack * 1 + chipsOnEven + chipsOnEven * 1 + chipsOnStraight + chipsOnStraight * 35; // 250 + 35*100 = 3750
            betsHolder.AddPlayerBet(BetDef.BetType.Black, chipsOnBlack);
            betsHolder.AddPlayerBet(BetDef.BetType.Even, chipsOnEven);
            betsHolder.AddPlayerBet(BetDef.BetType.Straight, chipsOnStraight, 2);

            int chipsOnLosingStraight = 100;
            int chipsOnLosingFirstColumn = 100;
            betsHolder.AddPlayerBet(BetDef.BetType.Straight, chipsOnLosingStraight, 19);
            betsHolder.AddPlayerBet(BetDef.BetType.FirstColumn, chipsOnLosingFirstColumn);

            // Spin and gather winning bets
            int winningNumber = fakeWheelAlwaysReturningNumber2.Spin();
            List<Bet> winningBets = betsHolder.GetWinningBets(winningNumber);

            // Calculate final win
            int actualReward = winCalculator.CalculatePlayerWinningAmount(winningBets);
            Assert.AreEqual(expectedReward, actualReward);
        }
        
    }
}
