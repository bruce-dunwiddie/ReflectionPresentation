using System;

namespace JSON.NetSimulation
{
    public static class SimulatedConvert
    {
        public static string SerializeObject(
            object value,
            Formatting formatting)
        {
            // route the serialization call to the serializer class
            return new SimulatedSerializer().Serialize(
                value,
                formatting);
        }

        public static T DeserializeObject<T>(
            string value)
        {
            // unwrap the generics and route the deserialization call to the deserialization class
            return (T)new SimulatedDeserializer().Deserialize(
                value,
                typeof(T));
        }
    }
}
