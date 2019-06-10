using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roulette
{
    private BetsHolder betsHolder;
    private IWheel wheel;
    private WinCalculator winCalculator;

    public Roulette()
    {
        winCalculator = new WinCalculator();
        betsHolder = new BetsHolder();
        wheel = new SimpleRandomWheel();
    }

    internal void AddPlayerBet(BetDef.BetType betType, int chips, params int[] numbers)
    {
        betsHolder.AddPlayerBet(betType, chips, numbers);
    }

    internal int SpinResult()
    {
        return wheel.SpinResult();
    }

    internal int CalculatePlayerWinningAmount(List<Bet> winningBets)
    {
        return winCalculator.CalculatePlayerWinningAmount(winningBets);
    }

    internal List<Bet> GetWinningBets(int winningNumber)
    {
        return betsHolder.GetWinningBets(winningNumber);
    }

    internal int GetCurrentRoundBet()
    {
        return betsHolder.GetTotalChips();
    }
}
