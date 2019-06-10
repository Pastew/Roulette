﻿namespace Tests
{
    internal class FakeWheelReturningGivenNumber : IWheel
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
}