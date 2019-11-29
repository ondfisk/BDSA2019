using System.Text.Json.Serialization;

namespace BDSA2019.Lecture11.Web.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ImageType
    {
        Portrait,
        Background
    }
}
