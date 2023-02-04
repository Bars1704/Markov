using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace MarkovTest.TwoDimension.Patterns
{
    /// <summary>
    /// Represents rotation angle
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum RotationAngle
    {
        Degrees0 = 0,
        Degrees90 = 90,
        Degrees180 = 180,
        Degrees270 = 270
    }
}