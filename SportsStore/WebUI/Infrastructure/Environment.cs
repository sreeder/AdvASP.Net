using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace WebUI.Infrastructure
{
    public static class Environment
    {
        public static bool IsLive => Convert.ToBoolean(ConfigurationManager.AppSettings.Get("IsLive"));
        public static string Email => IsLive
            ? ConfigurationManager.AppSettings.Get("Email")
            : ConfigurationManager.AppSettings.Get("TestEmail");

    }
}