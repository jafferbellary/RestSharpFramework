using System;
using System.IO;

namespace RestAPIFramework.Utilities
{
    public class FileUtils
    {
        public static T ReadTextToDictonary<T>(string FileName)
        {
            string _readFile = null;
            try
            {
                _readFile = File.ReadAllText(FileName);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return JSONUtils.JsonConverter<T>(_readFile);
        }
    }
}
