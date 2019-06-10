using System;

public class SimpleRandomWheel : IWheel
{
    public int SpinResult()
    {
        return new Random().Next(0, 36 + 1);
    }
}