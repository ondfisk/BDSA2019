using System.Text.Json.Serialization;

namespace BDSA2019.Lecture10.Web.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ImageType
    {
        Portrait,
        Background
    }
}
