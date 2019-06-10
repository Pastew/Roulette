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
