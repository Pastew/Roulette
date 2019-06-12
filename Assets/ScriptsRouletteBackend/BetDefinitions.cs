using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//  http:// www.outsidebet.net/different-bets-on-a-roulette-table/
public static class BetDef
{
    public enum BetType
    {
        Straight,
        Split,
        Street,
        Corner, // aka square
        Trio012,
        Trio023,
        FirstFour,
        SixLine,
        FirstColumn,
        SecondColumn,
        ThirdColumn,
        FirstDozen,
        SecondDozen,
        ThirdDozen,
        Odd,
        Even,
        Red,
        Black,
        From1To18,
        From19To36
    };

    public static Dictionary<BetType, int[]> betFixedNumbers = new Dictionary<BetType, int[]> {
        {BetType.FirstFour, new int[]{0, 1, 2, 3} }, 
        {BetType.Trio012, new int[]{0, 1, 2} }, 
        {BetType.Trio023, new int[]{0, 2, 3} }, 
        {BetType.FirstColumn, new int[]{1, 4, 7, 10, 13, 16, 19, 22, 25, 28, 31, 34} }, 
        {BetType.SecondColumn, new int[]{2, 5, 8, 11, 14, 17, 20, 23, 26, 29, 32, 35} }, 
        {BetType.ThirdColumn, new int[]{3, 6, 9, 12, 15, 18, 21, 24, 27, 30, 33, 36} }, 
        {BetType.FirstDozen, new int[]{1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12} }, 
        {BetType.SecondDozen, new int[]{13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24} }, 
        {BetType.ThirdDozen, new int[]{25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36} }, 
        {BetType.Odd, new int[]{1, 3, 5, 7, 9, 11, 13, 15, 17, 19, 21, 23, 25, 27, 29, 31, 33, 35} }, 
        {BetType.Even, new int[]{2, 4, 6, 8, 10, 12, 14, 16, 18, 20, 22, 24, 26, 28, 30, 32, 34, 36} }, 
        {BetType.Red, new int[]{1, 3, 5, 7, 9, 12, 14, 16, 18, 19, 21, 23, 25, 27, 30, 32, 34, 36} }, 
        {BetType.Black, new int[]{2, 4, 6, 8, 10, 11, 13, 15, 17, 20, 22, 24, 26, 28, 29, 31, 33, 35} }, 
        {BetType.From1To18, new int[]{1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18} }, 
        {BetType.From19To36, new int[]{19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36} }
    };

    // BetTypes below don't have fixed numbers
    // ----------------------------------------
    //{BetType.Straight, new int[]{Any single number} }, 
    //{BetType.Split, new int[]{any two adjoining numbers vertical or horizontal} }, 
    //{BetType.Street, new int[]{any three numbers horizontal (1, 2, 3 or 4, 5, 6, etc.)} }, 
    //{BetType.Corner, new int[]{any four adjoining numbers in a block (1, 2, 4, 5 or 17, 18, 20, 21, etc.)} }, 
    //{BetType.SixLine, new int[]{any six numbers from two horizontal rows (1, 2, 3, 4, 5, 6 or 28, 29, 30, 31, 32, 33 etc.)} }, 

    // https://www.roulettephysics.com/roulette-bets-odds-and-payouts/
    public static Dictionary<BetType, int> payout = new Dictionary<BetType, int> {
        {BetType.Straight, 35}, 
        {BetType.Split, 17}, 
        {BetType.Street, 11}, 
        {BetType.Corner, 8},
        {BetType.FirstFour, 8}, 
        {BetType.Trio012, 11}, // Took this multiplier from here: https://www.888casino.com/blog/how-to-play-roulette-beginners-guide
        {BetType.Trio023, 11}, 
        {BetType.SixLine, 5}, 
        {BetType.FirstColumn, 2}, 
        {BetType.SecondColumn, 2}, 
        {BetType.ThirdColumn, 2}, 
        {BetType.FirstDozen, 2}, 
        {BetType.SecondDozen, 2}, 
        {BetType.ThirdDozen, 2}, 
        {BetType.Odd, 1}, 
        {BetType.Even, 1}, 
        {BetType.Red, 1}, 
        {BetType.Black, 1}, 
        {BetType.From1To18, 1}, 
        {BetType.From19To36, 1}
    };
}
