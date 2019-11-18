using System.Text.Json.Serialization;

namespace BDSA2019.Lecture10.Entities
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Gender
    {
        Female,
        Male,
        Other
    }
}
