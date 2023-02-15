#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using JsonException = Newtonsoft.Json.JsonException;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;

namespace MarkovTest.Converters
{
    public class PatternConverter : JsonConverter
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
                    writer.WriteValue(obj.GetValue(i, j));
                }

                writer.WriteEndArray();
            }

            writer.WriteEndArray();
        }

        public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            var matrix = new List<List<object>>();
            matrix.Add(new List<object>());

            if (reader.TokenType != JsonToken.StartArray)
                throw new JsonException($"Invalid state - must be starting array , instead = {reader.TokenType}");
            reader.Read();
            if (reader.TokenType != JsonToken.StartArray)
                throw new JsonException($"Invalid state - must be starting array , instead = {reader.TokenType}");

            while (reader.Read())
            {
                if (reader.TokenType == JsonToken.EndArray)
                {
                    reader.Read();
                    if (reader.TokenType == JsonToken.EndArray)
                        break;
                    if (reader.TokenType == JsonToken.StartArray)
                    {
                        matrix.Add(new List<object>());
                        reader.Read();
                    }
                }

                matrix.Last().Add(reader.Value);
            }

            if (matrix.GroupBy(x => x.Count).Count() > 1)
                throw new JsonException($"Invalid matrix form");

            var mainType = objectType.GetElementType().GetGenericArguments()[0];

            var xSize = matrix.Count;
            var ySize = matrix.First().Count;
            var result = new IEquatable<byte>[xSize, ySize];

            for (var x = 0; x < xSize; x++)
            for (var y = 0; y < ySize; y++)
                result[x, y] = (byte)JsonConvert.DeserializeObject(matrix[x][y].ToString(), mainType); 

            return result;
        }

        public override bool CanConvert(Type objectType)
        {
            if (!objectType.IsArray) return false;
            if (objectType.GetArrayRank() != 2) return false;
            if (objectType.MakeArrayType().GetInterfaces().Contains(typeof(IEquatable<>))) return false;
            return true;
        }
    }
}