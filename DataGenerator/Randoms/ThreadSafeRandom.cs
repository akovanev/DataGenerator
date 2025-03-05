namespace Akov.DataGenerator.Randoms;

/// <summary>
/// https://andrewlock.net/building-a-thread-safe-random-implementation-for-dotnet-framework/
/// </summary>
public static class ThreadSafeRandom
{
    [ThreadStatic]
    private static Random? _local;
    private static readonly Random Global = new(); // 👈 Global instance used to generate seeds

    public static Random Instance
    {
        get
        {
            if (_local is not null) return _local;

            int seed;
            lock (Global) // 👈 Ensure no concurrent access to Global
            {
                seed = Global.Next();
            }

            _local = new Random(seed); // 👈 Create [ThreadStatic] instance with specific seed

            return _local;
        }
    }
}