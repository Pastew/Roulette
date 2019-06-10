using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static BetDef;

public class GameManager : MonoBehaviour
{
    private Roulette roulette;
    private WinningNumberText winningNumberText;
    private PlayerBetText playerBetText;
    private int winningNumber;

    public static GameManager instance;

    private void Awake()
    {
        instance = this;
        roulette = new Roulette();
    }

    void Start()
    {
        winningNumberText = FindObjectOfType<WinningNumberText>();
        playerBetText = FindObjectOfType<PlayerBetText>();
    }

    public void Play()
    {
        roulette.AddPlayerBet(BetType.Straight, 10, 4);
        roulette.AddPlayerBet(BetType.Red, 25);
        roulette.AddPlayerBet(BetType.Split, 50, 5, 8);
    }

    public void AddBet(BetField betField, Chip chip)
    {
        roulette.AddPlayerBet(betField.betType, chip.value, betField.GetRelatedNumbers());
        int currentRoundBet = roulette.GetCurrentRoundBet();
        playerBetText.SetText(currentRoundBet.ToString());
    }

    public void SpinButtonPressed()
    {
        winningNumberText.SetText("");
        winningNumber = roulette.SpinResult();
        Invoke("SpinFinished", 3f); //TODO: Use something better than Invoke
    }

    private void SpinFinished()
    {
        winningNumberText.SetText(winningNumber.ToString());
        List<Bet> winningBets = roulette.GetWinningBets(winningNumber);        
        int playerWinAmount = roulette.CalculatePlayerWinningAmount(winningBets);
    }
}
