using System;
using MarkovTest.TwoDimension;
using Newtonsoft.Json;

namespace MarkovTest.Serialization
{
    public static class SimulationSerializer
    {
        private static JsonSerializerSettings _serializerSettings;

        static SimulationSerializer()
        {
            _serializerSettings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
        }

        public static string SerializeSim<T>(MarkovSimulationTwoDim<T> simulation) where T : IEquatable<T>
        {
            return JsonConvert.SerializeObject(simulation, Formatting.Indented, _serializerSettings);
        }

        public static MarkovSimulationTwoDim<T> DeserializeSim<T>(string json) where T : IEquatable<T>
        {
            return JsonConvert.DeserializeObject<MarkovSimulationTwoDim<T>>(json, _serializerSettings);
        }
    }
}