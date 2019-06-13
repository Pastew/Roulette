using System;

public interface IWheel
{
    int SpinResult();
}

public class SimpleRandomWheel : IWheel
{
    public int SpinResult()
    {
        return new Random().Next(0, 36 + 1);
    }
}

public class FakeWheelReturningGivenNumber : IWheel
{
    private int alywasReturnedValue;

    public FakeWheelReturningGivenNumber(int alwaysReturnedValue)
    {
        this.alywasReturnedValue = alwaysReturnedValue;
    }

    public int SpinResult()
    {
        return alywasReturnedValue;
    }
}
