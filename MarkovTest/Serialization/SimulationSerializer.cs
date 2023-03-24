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

        public static string SerializeSim<T>(MarkovSimulation<T> simulation) where T : IEquatable<T>
        {
            return JsonConvert.SerializeObject(simulation, Formatting.Indented, _serializerSettings);
        }

        public static MarkovSimulation<T> DeserializeSim<T>(string json) where T : IEquatable<T>
        {
            return JsonConvert.DeserializeObject<MarkovSimulation<T>>(json, _serializerSettings);
        }
    }
}