using Newtonsoft.Json;
using System;

namespace RestAPIFramework.Utilities
{
    public class JSONUtils
    {
        public static T JsonConverter<T>(string strStringToConvert)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(strStringToConvert);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return default(T);
        }

        public static string SerializeObject(object obj)
        {
            try
            {
                return JsonConvert.SerializeObject(obj);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return null;
        }
    }
}
