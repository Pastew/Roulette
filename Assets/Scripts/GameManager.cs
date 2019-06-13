using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static BetDef;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get => instance; private set => instance = value; }

    private PlayerWallet playerWallet;
    private Roulette roulette;

    private int winningNumber;

    private void Awake()
    {
        Instance = this;
        roulette = new Roulette();
    }

    void Start()
    {
        playerWallet = FindObjectOfType<PlayerWallet>();
        HUD.Instance.UpdatePlayerBalanceText(playerWallet.PlayerBalance.ToString());
    }

    public void AddBet(BetField betField, Chip chip)
    {
        playerWallet.SubtractChips(chip.value);

        roulette.AddPlayerBet(betField.BetType, chip.value, betField.GetRelatedNumbers());
        int currentRoundBet = roulette.GetCurrentRoundBet();
        HUD.Instance.UpdateBetText(currentRoundBet.ToString());
        HUD.Instance.UpdatePlayerBalanceText(playerWallet.PlayerBalance.ToString());

        TurnOffAllHiglights();
    }

    private void TurnOffAllHiglights()
    {
        foreach (BetField betField in FindObjectsOfType<BetField>()) // TODO: optimize, this can be predefined
            betField.TurnHighlight(false);
    }

    public void SpinButtonPressed()
    {
        HUD.Instance.UpdateWinningNumberText("...");
        winningNumber = roulette.SpinWheel();
        Invoke("SpinFinished", 3f); //TODO: Use something better than Invoke
    }

    private void SpinFinished()
    {
        HUD.Instance.UpdateWinningNumberText(winningNumber.ToString());

        List<Bet> winningBets = roulette.GetWinningBets(winningNumber);
        int playerWinAmount = roulette.CalculatePlayerWinningAmount(winningBets);
        HUD.Instance.UpdateLastRewardText(playerWinAmount.ToString());

        if (playerWinAmount > 0)
        {
            HUD.Instance.ShowWinPanel(playerWinAmount.ToString());
            playerWallet.AddChips(playerWinAmount);
            HUD.Instance.UpdatePlayerBalanceText(playerWallet.PlayerBalance.ToString());
        }
        else
        {
            HUD.Instance.ShowLosePanel();
        }
        DestroyAllBets();
    }

    private void DestroyAllBets()
    {
        roulette.RemoveAllBets();
        HUD.Instance.UpdateBetText("-");

        foreach (Chip chip in FindObjectsOfType<Chip>())
            Destroy(chip.gameObject);
    }
}
