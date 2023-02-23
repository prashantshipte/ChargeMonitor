using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChargeMonitor.Services
{
    public class SettingsService : ISettingsService
    {
        public T Get<T>(string key, T defaultValue)
        {
            return Preferences.Default.Get<T>(key, defaultValue);
        }

        public void Save<T>(string key, T value)
        {
            Preferences.Default.Set(key, value);            
        }
    }
}
