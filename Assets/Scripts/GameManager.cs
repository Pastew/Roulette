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
    private LastRewardText lastRewardText;

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
        lastRewardText = FindObjectOfType<LastRewardText>();

        playerBalanceText.SetText(playerWallet.GetPlayerBalance().ToString());
    }

    public void AddBet(BetField betField, Chip chip)
    {
        playerWallet.SubtractChips(chip.value);

        roulette.AddPlayerBet(betField.betType, chip.value, betField.GetRelatedNumbers());
        int currentRoundBet = roulette.GetCurrentRoundBet();
        playerBetText.SetText(currentRoundBet.ToString());
        playerBalanceText.SetText(playerWallet.PlayerBalance.ToString());

        TurnOffAllHiglights();
    }

    private void TurnOffAllHiglights()
    {
        foreach (BetField betField in FindObjectsOfType<BetField>())
            betField.TurnHighlightsForRelatedFields(false);
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
        lastRewardText.SetText(playerWinAmount.ToString());

        if (playerWinAmount > 0)
        {
            playerWinPanel.SetText(playerWinAmount.ToString());
            playerWinPanel.GetComponent<Animator>().SetTrigger("show");
            playerWallet.AddChips(playerWinAmount);
            playerBalanceText.SetText(playerWallet.PlayerBalance.ToString());
        }
        else
        {
            playerLosePanel.GetComponent<Animator>().SetTrigger("show");
        }
        DestroyAllBets();
    }

    private void DestroyAllBets()
    {
        roulette.RemoveAllBets();
        playerBetText.SetText("-");
        foreach (Chip chip in FindObjectsOfType<Chip>())
            Destroy(chip.gameObject);
    }
}
