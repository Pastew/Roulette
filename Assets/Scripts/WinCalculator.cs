using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCalculator : MonoBehaviour
{
    public int CalculatePlayerWinningAmount(List<Bet> bets)
    {
        int finalAmount = 0;

        foreach(Bet bet in bets)
        {
            finalAmount += bet.Chips * BetDef.payout[bet.BetType];
        }

        return finalAmount;
    }
}
