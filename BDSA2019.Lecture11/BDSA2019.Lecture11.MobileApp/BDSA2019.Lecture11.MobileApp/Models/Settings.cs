using System;

namespace BDSA2019.Lecture11.MobileApp.Models
{
    public class Settings : ISettings
    {
        public Uri BackendUrl => new Uri("http://bdsa2019lecture11.azurewebsites.net/");

        public string[] Scopes => new[] { "api://ba3d2c8b-8e56-4304-911e-740dde15bb0d/user_impersonation" };

        public string ClientId => "06229d29-172d-4bd3-ae0c-322a24acd3e3";

        public string TenantId => "b461d90e-0c15-44ec-adc2-51d14f9f5731";
    }
}
