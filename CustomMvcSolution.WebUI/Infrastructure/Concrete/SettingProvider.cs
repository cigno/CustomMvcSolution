using System.Collections.Generic;
using CustomMvcSolution.WebUI.Infrastructure.Abstract;

namespace CustomMvcSolution.WebUI.Infrastructure.Concrete
{
    public class SettingProvider : ISettingService
    {
        private readonly ISettingService _service = new ConfigFileSettingService();
        
        #region Implementation of ISettingService

        public IDictionary<string, string> GetAllSettings()
        {
            return _service.GetAllSettings();
        }

        public T GetSetting<T>(string key)
        {
            return _service.GetSetting<T>(key);
        }

        public void SetSetting<T>(string key, T value)
        {
            _service.SetSetting(key, value);
        }

        #endregion
    }
}