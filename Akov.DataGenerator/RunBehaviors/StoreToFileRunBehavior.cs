using System;
using System.IO;
using Akov.DataGenerator.Common;

namespace Akov.DataGenerator.RunBehaviors;

public class StoreToFileRunBehavior : IRunBehavior
{
    private readonly IOHelper _ioHelper = new();

    public bool SaveResult(string type, string data)
    {
        try
        {
            if(File.Exists(type))
                File.Delete(type);
        
            _ioHelper.SaveData(type, data);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public string ReadLast(string type)
    {
        if(!File.Exists(type))
            throw new ArgumentException($"File {type} doesn't exist");

        return _ioHelper.GetFileContent(type);
    }

    public bool ClearResult(string type)
    {
        try
        {
            if(File.Exists(type))
                File.Delete(type);
            
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}