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
        Zero, // zero field
        Straight, // Any single number
        Split, // any two adjoining numbers vertical or horizontal
        Street, // any three numbers horizontal (1, 2, 3 or 4, 5, 6, etc.)
        Corner, // any four adjoining numbers in a block (1, 2, 4, 5 or 17, 18, 20, 21, etc.)
        Basket, // 0, 1, 2, 3
        SixLine, // any six numbers from two horizontal rows (1, 2, 3, 4, 5, 6 or 28, 29, 30, 31, 32, 33 etc.)
        FirstColumn, // 1, 4, 7, 10, 13, 16, 19, 22, 25, 28, 31, 34
        SecondColumn, // 2, 5, 8, 11, 14, 17, 20, 23, 26, 29, 32, 35
        ThirdColumn, // 3, 6, 9, 12, 15, 18, 21, 24, 27, 30, 33, 36
        FirstDozen, // 1 through 12
        SecondDozen, // 13 through 24
        ThirdDozen, // 25 through 36
        Odd, // 1, 3, 5, ..., 35
        Even, // 2, 4, 6, ..., 36
        Red, // 1, 3, 5, 7, 9, 12, 14, 16, 18, 19, 21, 23, 25, 27, 30, 32, 34, 36
        Black, // 2, 4, 6, 8, 10, 11,13, 15, 17, 20, 22, 24,26, 28, 29, 31, 33, 35
        From1To8, // 1, 2, 3, ..., 18
        From19To36 // 19, 20, 21, ..., 36
    }

    public static Dictionary<BetType, int[]> numbers = new Dictionary<BetType, int[]> {
            { BetType.Zero, new int[]{0 } },
            { BetType.Red, new int[] { 1, 3, 5, 7, 9, 12, 14, 16, 18, 19, 21, 23, 25, 27, 30, 32, 34, 36  } }
    };

    public static Dictionary<BetType, int> payout = new Dictionary<BetType, int> {
            { BetType.Zero, 35},
            { BetType.Straight, 35 },
            { BetType.Red, 1 }
    };
}