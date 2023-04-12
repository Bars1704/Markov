#nullable enable
using System;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;

namespace MarkovTest.Converters
{
    public class PatternConverter3D : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            var obj = (Array)value;

            writer.WriteStartArray();
            for (var i = 0; i < obj.GetLength(0); i++)
            {
                writer.WriteStartArray();
                for (var j = 0; j < obj.GetLength(1); j++)
                {
                    writer.WriteStartArray();
                    for (var k = 0; k < obj.GetLength(2); k++)
                    {
                        writer.WriteValue(obj.GetValue(i, j, k));
                    }

                    writer.WriteEndArray();
                }

                writer.WriteEndArray();
            }

            writer.WriteEndArray();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JArray array = JArray.Load(reader);
            byte[,,] result = new byte[array.Count, array[0].Count(), array[0][0].Count()];
            int i = 0, j = 0, k = 0;

            foreach (JArray subArray2 in array)
            {
                foreach (JArray subArray1 in subArray2)
                {
                    foreach (JValue value in subArray1)
                    {
                        result[i, j, k] = Convert.ToByte(value.Value);
                        k++;
                    }
                    j++;
                    k = 0;
                }
                i++;
                j = 0;
            }

            return result;
        }
        

        public override bool CanConvert(Type objectType)
        {
            if (!objectType.IsArray) return false;
            if (objectType.GetArrayRank() != 3) return false;
            if (objectType.MakeArrayType().GetInterfaces().Contains(typeof(IEquatable<>))) return false;
            return true;
        }
    }
}