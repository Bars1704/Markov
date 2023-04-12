using Newtonsoft.Json;
using MarkovSimulation2D = MarkovTest.TwoDimension.MarkovSimulation<byte>;
using MarkovSimulation3D = MarkovTest.ThreeDimension.MarkovSimulation<byte>;


namespace MarkovTest.Serialization
{
    public static class SimulationSerializer
    {
        private static JsonSerializerSettings _serializerSettings;

        static SimulationSerializer()
        {
            _serializerSettings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Objects };
        }

        public static string SerializeSim(MarkovSimulation2D simulation) 
        {
            return JsonConvert.SerializeObject(simulation, Formatting.Indented, _serializerSettings);
        }

        public static MarkovSimulation2D DeserializeSim2D(string json) 
        {
            return JsonConvert.DeserializeObject<MarkovSimulation2D>(json, _serializerSettings);
        }
        
        public static MarkovSimulation3D DeserializeSim3D(string json)
        {
            return JsonConvert.DeserializeObject<MarkovSimulation3D>(json, _serializerSettings);
        }
        
        public static string SerializeSim(MarkovSimulation3D simulation) 
        {
            return JsonConvert.SerializeObject(simulation, Formatting.Indented, _serializerSettings);
        }

    }
}