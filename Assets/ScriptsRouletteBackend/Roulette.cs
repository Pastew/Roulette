using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roulette
{
    private BetsHolder betsHolder;
    private IWheel wheel;

    public Roulette()
    {
        betsHolder = new BetsHolder();
        wheel = new SimpleRandomWheel();
        //wheel = new FakeWheelReturningGivenNumber(2);
    }

    internal void AddPlayerBet(BetDef.BetType betType, int chips, params int[] numbers)
    {
        betsHolder.AddPlayerBet(betType, chips, numbers);
    }

    internal int SpinWheel()
    {
        return wheel.Spin();
    }

    internal int CalculatePlayerWinningAmount(List<Bet> winningBets)
    {
        return WinCalculator.CalculatePlayerWinningAmount(winningBets);
    }

    internal List<Bet> GetWinningBets(int winningNumber)
    {
        return betsHolder.GetWinningBets(winningNumber);
    }

    internal int GetCurrentRoundBet()
    {
        return betsHolder.GetTotalChips();
    }

    internal void RemoveAllBets()
    {
        betsHolder.RemoveAllBets();
    }
}
