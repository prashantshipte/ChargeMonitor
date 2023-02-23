using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChargeMonitor.Services
{
    public interface ISettingsService
    {
        T Get<T>(string key, T defaultValue);

        void Save<T>(string key, T value);
    }
}
