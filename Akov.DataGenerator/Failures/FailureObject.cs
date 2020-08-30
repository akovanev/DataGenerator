namespace Akov.DataGenerator.Failures
{
    internal class FailureObject
    {
        public FailureObject(FailureType failureType, Range randomRange)
        {
            FailureType = failureType;
            RandomRage = randomRange;
        }

        public FailureType FailureType { get; }
        public Range RandomRage { get; }
    }
}