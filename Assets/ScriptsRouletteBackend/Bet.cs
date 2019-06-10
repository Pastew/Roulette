using System;
using System.Collections.Generic;
using System.Linq;

public class Bet : IEquatable<Bet>
{
    private BetDef.BetType betType;
    private int chips;
    private List<int> numbers;

    public int Chips { get => chips; set => chips = value; }
    public List<int> Numbers { get => numbers; set => numbers = value; }
    public BetDef.BetType BetType { get => betType; set => betType = value; }

    public Bet(BetDef.BetType betType, int chips, params int[] numbers)
    {
        this.BetType = betType;
        Chips = chips;
        Numbers = numbers.ToList<int>();

        if (numbers.Length == 0)
            try
            {
                Numbers = BetDef.betFixedNumbers[betType].ToList<int>();
            }
            catch(KeyNotFoundException ex)
            {
                throw new KeyNotFoundException("You probably try to create inside bet without providing any number");
            }
    }

    public bool Equals(Bet other)
    {
        return (other.BetType == BetType
            && other.Numbers.SequenceEqual(Numbers));
    }
}
