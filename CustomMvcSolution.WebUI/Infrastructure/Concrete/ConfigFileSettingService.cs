using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using CustomMvcSolution.WebUI.Infrastructure.Abstract;

namespace CustomMvcSolution.WebUI.Infrastructure.Concrete
{
    public class ConfigFileSettingService : ISettingService
    {
        #region ISettingService Members

        public IDictionary<string, string> GetAllSettings()
        {
            NameValueCollection appSettings = ConfigurationManager.AppSettings;

            return appSettings.AllKeys.ToDictionary(setting => setting, setting => appSettings[setting]);
        }

        public T GetSetting<T>(string key)
        {
            T result = default(T);
            string setting = ConfigurationManager.AppSettings[key];
            if (!string.IsNullOrEmpty(setting))
                result = CommonUtils.To<T>(setting);
            return result;
        }

        public void SetSetting<T>(string key, T value)
        {
            throw new InvalidOperationException("Cannot save settings to the application configuration file");
        }

        #endregion
    }
}

public class CommonUtils
{
    public static T To<T>(string val)
    {
        return (T) Convert.ChangeType(val, Type.GetTypeCode(typeof (T)));
    }
}