using System;
using System.ComponentModel;

namespace BDSA2019.Lecture11.MobileApp.Models
{
    public interface ISettings
    {
        Uri BackendUrl { get; }
        string ClientId { get; }
        string ResourceId { get; }
        string[] Scopes { get; }
        string TenantId { get; }
    }
}