using System.Text.Json.Serialization;

namespace BDSA2019.Lecture11.Shared
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Gender
    {
        Female,
        Male,
        Other
    }
}
