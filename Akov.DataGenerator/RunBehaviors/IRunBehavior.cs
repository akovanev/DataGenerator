namespace Akov.DataGenerator.RunBehaviors;

public interface IRunBehavior
{
    bool SaveResult(string type, string data);
    
    string ReadLast(string type);
    
    bool ClearResult(string type);
}