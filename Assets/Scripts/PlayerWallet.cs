using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallet : MonoBehaviour
{
    private int playerBalance = 1000;

    public int PlayerBalance { get => playerBalance; private set => playerBalance = value; }

    public void AddChips(int chips)
    {
        PlayerBalance += (int)chips;
    }

    public void SubtractChips(int chips)
    {
        if (PlayerBalance - chips < 0)
            throw new ArgumentException("Balance would be < 0, can't subtract");
        else
            PlayerBalance -= (int)chips;
    }

    public int GetPlayerBalance()
    {
        return 1000;
    }
}
