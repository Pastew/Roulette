﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WinCalculator
{
    public int CalculatePlayerWinningAmount(List<Bet> winningBets)
    {
        int finalAmount = 0;

        foreach(Bet bet in winningBets)
        {
            finalAmount += bet.Chips * BetDef.payout[bet.BetType] + bet.Chips;
        }

        return finalAmount;
    }
}
