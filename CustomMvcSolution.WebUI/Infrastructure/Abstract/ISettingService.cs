﻿using System.Collections.Generic;

namespace CustomMvcSolution.WebUI.Infrastructure.Abstract
{
    public interface ISettingService
    {
        IDictionary<string, string> GetAllSettings();
        T GetSetting<T>(string key);
        void SetSetting<T>(string key, T value);
    }
}
