using System;

public class SimpleRandomWheel : IWheel
{
    public int Spin()
    {
        return new Random().Next(0, 36 + 1);
    }
}