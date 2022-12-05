using System;

namespace Akov.DataGenerator.RunBehaviors;

public class NullRunBehavior : IRunBehavior
{
    private const string ErrorMessage = $"{nameof(NullRunBehavior)} methods should not be used";
    
    public bool SaveResult(string type, string data) => throw new InvalidOperationException(ErrorMessage);
    public string ReadLast(string type) => throw new InvalidOperationException(ErrorMessage);
    public bool ClearResult(string type) => throw new InvalidOperationException(ErrorMessage);
}