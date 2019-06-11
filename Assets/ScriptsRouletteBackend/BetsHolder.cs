using System;
using System.Collections.Generic;
using System.Linq;
using static BetDef;

public class BetsHolder
{
    private IWheel wheel;
    private List<Bet> playerBets;

    public List<Bet> PlayerBets { get => playerBets; }

    public BetsHolder()
    {
        playerBets = new List<Bet>();
        wheel = new SimpleRandomWheel();
    }

    public void AddPlayerBet(BetType betType, int coins, params int[] numbers)
    {
        Bet playerNewBet = new Bet(betType, coins, numbers);

        foreach (Bet bet in PlayerBets)
        {
            if (bet.Equals(playerNewBet))
            {
                bet.Chips += coins;
                return;
            }
        }

        PlayerBets.Add(playerNewBet);
    }

    public List<Bet> GetWinningBets(int winningNumber)
    {
        return PlayerBets.FindAll(bet => bet.Numbers.Contains(winningNumber));
    }

    internal int GetTotalChips()
    {
        return playerBets.Sum(el => el.Chips);
    }

    internal void RemoveAllBets()
    {
        playerBets.Clear();
    }
}
