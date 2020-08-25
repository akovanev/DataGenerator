namespace Akov.DataGenerator.Failures
{
    internal class FailureObject
    {
        internal FailureObject(FailureType failureType, Range randomRange)
        {
            FailureType = failureType;
            RandomRage = randomRange;
        }

        internal FailureType FailureType { get; }
        internal Range RandomRage { get; }
    }
}