namespace Akov.DataGenerator.Constants
{
    /// <summary>
    /// The type of failure.
    /// </summary>
    public enum FailureType
    {
        None, //Shows that there was no failure on the current iteration.
        Custom,
        Nullable,
        Range
    }
}