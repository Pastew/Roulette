using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static BetDef;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private PlayerWallet playerWallet;

    private Roulette roulette;
    private int winningNumber;

    private PlayerBalanceText playerBalanceText;
    private PlayerWinPanel playerWinPanel;
    private PlayerLosePanel playerLosePanel;
    private WinningNumberText winningNumberText;
    private PlayerBetText playerBetText;

    private void Awake()
    {
        instance = this;
        roulette = new Roulette();
    }

    void Start()
    {
        playerWallet = FindObjectOfType<PlayerWallet>();

        winningNumberText = FindObjectOfType<WinningNumberText>();
        playerBetText = FindObjectOfType<PlayerBetText>();
        playerBalanceText = FindObjectOfType<PlayerBalanceText>();
        playerWinPanel = FindObjectOfType<PlayerWinPanel>();
        playerLosePanel = FindObjectOfType<PlayerLosePanel>();

        playerBalanceText.SetText(playerWallet.GetPlayerBalance().ToString());
    }

    public void AddBet(BetField betField, Chip chip)
    {
        roulette.AddPlayerBet(betField.betType, chip.value, betField.GetRelatedNumbers());
        int currentRoundBet = roulette.GetCurrentRoundBet();
        playerBetText.SetText(currentRoundBet.ToString());
    }

    public void SpinButtonPressed()
    {
        winningNumberText.SetText("...");
        winningNumber = roulette.SpinResult();
        Invoke("SpinFinished", 3f); //TODO: Use something better than Invoke
    }

    private void SpinFinished()
    {
        winningNumberText.SetText(winningNumber.ToString());
        List<Bet> winningBets = roulette.GetWinningBets(winningNumber);        
        int playerWinAmount = roulette.CalculatePlayerWinningAmount(winningBets);


        if (playerWinAmount > 0)
        {
            playerWinPanel.SetText(playerWinAmount.ToString());
            playerWinPanel.GetComponent<Animator>().SetTrigger("show");
        }
        else
        {
            playerLosePanel.GetComponent<Animator>().SetTrigger("show");
        }
    }
}
