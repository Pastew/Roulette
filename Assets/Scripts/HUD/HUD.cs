using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUD : MonoBehaviour
{
    private static HUD instance;

    private PlayerBalanceText playerBalanceText;
    private PlayerWinPanel playerWinPanel;
    private PlayerLosePanel playerLosePanel;
    private WinningNumberText winningNumberText;
    private PlayerBetText playerBetText;
    private LastRewardText lastRewardText;

    public static HUD Instance { get => instance; set => instance = value; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        winningNumberText = FindObjectOfType<WinningNumberText>();
        playerBetText = FindObjectOfType<PlayerBetText>();
        playerBalanceText = FindObjectOfType<PlayerBalanceText>();
        playerWinPanel = FindObjectOfType<PlayerWinPanel>();
        playerLosePanel = FindObjectOfType<PlayerLosePanel>();
        lastRewardText = FindObjectOfType<LastRewardText>();
    }

    public void UpdatePlayerBalanceText(string playerBalance)
    {
        playerBalanceText.SetText(playerBalance.ToString());
    }

    internal void UpdateBetText(string currentRoundBet)
    {
        playerBetText.SetText(currentRoundBet.ToString());
    }

    internal void UpdateWinningNumberText(string newText)
    {
        winningNumberText.SetText(newText);
    }

    internal void UpdateLastRewardText(string lastReward)
    {
        lastRewardText.SetText(lastReward);
    }

    internal void ShowWinPanel(string chipsWon)
    {
        playerWinPanel.SetText(chipsWon);
        playerWinPanel.GetComponent<Animator>().SetTrigger("show");

    }

    internal void ShowLosePanel()
    {
        playerLosePanel.GetComponent<Animator>().SetTrigger("show");
    }
}
