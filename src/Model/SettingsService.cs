using System.Collections.Generic;

namespace MultiPropertyValidationExample.Model
{
    
    /// <summary>
    /// This class emulates any given settings service as global AppSettings or anything alike...
    /// </summary>
    public static class SettingsService
    {
        private static readonly Dictionary<string, object> Settings = new Dictionary<string, object>();
              
        public static object GetSetting(string key)
        {
            return Settings.ContainsKey(key) ? Settings[key] : null;
        }

        public static void SetSetting(string key, object value)
        {
            if (Settings.ContainsKey(key))
            {
                Settings[key] = value;
            }
            else
            {
                Settings.Add(key, value);    
            }            
        }
    }
}
