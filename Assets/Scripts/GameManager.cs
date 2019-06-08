using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static BetDef;

public class GameManager : MonoBehaviour
{
    private BetsHolder betsHolder;
    private IWheel wheel;
    private WinCalculator winCalculator;

    private void Awake()
    {
        winCalculator = new WinCalculator();
        betsHolder = new BetsHolder();
        wheel = new SimpleRandomWheel();
    }

    public void Play()
    {
        // Gather bets from player
        betsHolder.AddPlayerBet(BetType.Straight, 10, 4);
        betsHolder.AddPlayerBet(BetType.Red, 25);
        betsHolder.AddPlayerBet(BetType.Split, 50, 5, 8);

        // Spin and gather winning bets
        int winningNumber = wheel.Spin();
        List<Bet> winningBets = betsHolder.GetWinningBets(winningNumber);

        // Calculate final win
        int playerWinAmount = winCalculator.CalculatePlayerWinningAmount(winningBets);
    }
}
