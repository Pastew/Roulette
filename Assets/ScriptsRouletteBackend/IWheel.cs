using System;

public interface IWheel
{
    int Spin();
}

public class SimpleRandomWheel : IWheel
{
    public int Spin()
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

    public int Spin()
    {
        return alywasReturnedValue;
    }
}
