using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static BetDef;

public class GameManager : MonoBehaviour
{
    private Roulette roulette;
    private WinningNumberText winningNumberText;
    private int winningNumber;

    private void Awake()
    {
        roulette = new Roulette();
    }

    void Start()
    {
        winningNumberText = FindObjectOfType<WinningNumberText>();
    }

    void Update()
    {
        
    }

    public void Play()
    {
        roulette.AddPlayerBet(BetType.Straight, 10, 4);
        roulette.AddPlayerBet(BetType.Red, 25);
        roulette.AddPlayerBet(BetType.Split, 50, 5, 8);
    }

    public void SpinButtonPressed()
    {
        winningNumberText.SetText("");

        winningNumber = roulette.SpinResult();
        print("Spin result = " + winningNumber);

        Invoke("SpinFinished", 3f); //TODO: Use something better than Invoke
    }

    private void SpinFinished()
    {
        winningNumberText.SetText(winningNumber.ToString());
        List<Bet> winningBets = roulette.GetWinningBets(winningNumber);        
        int playerWinAmount = roulette.CalculatePlayerWinningAmount(winningBets);
    }
}
