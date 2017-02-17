using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace JSON.NetSimulation
{
    public class SimulatedDeserializer
    {
        public object Deserialize(
            string value,
            Type valueType)
        {
            using (StringReader reader = new StringReader(value))
            {
                return DeserializeObject(
                    reader,
                    valueType);
            }
        }

        private static object DeserializeObject(
            StringReader reader,
            Type valueType)
        {
            if (valueType.IsPrimitive ||
                valueType == typeof(string) ||
                valueType == typeof(DateTime))
            {
                return DeserializePrimitive(
                    reader.ReadLine(),
                    valueType);
            }

            if (valueType.IsArray)
            {
                reader.ReadLine(); // [

                return DeserializeArray(
                    reader,
                    valueType);
            }

            if (valueType.IsGenericType)
            {
                Type genericType = valueType.GetGenericTypeDefinition();

                if (genericType == typeof(IList<>) ||
                    genericType == typeof(List<>))
                {
                    return DeserializeList(
                        reader,
                        valueType);
                }
            }

            reader.ReadLine(); // {

            return DeserializeComplexObject(
                reader,
                valueType);
        }

        private static object DeserializePrimitive(
            string value,
            Type valueType)
        {
            return Convert.ChangeType(value, valueType);
        }

        private static object DeserializeArray(
            StringReader reader,
            Type valueType)
        {
            List<object> values = new List<object>();

            Type elementType = valueType.GetElementType();

            string serializedValue = reader.ReadLine();

            while (serializedValue.TrimStart('\t') != "]")
            {
                if (elementType.IsPrimitive ||
                    elementType == typeof(string) ||
                    elementType == typeof(DateTime))
                {
                    values.Add(
                        DeserializePrimitive(
                            serializedValue,
                            elementType));
                }
                else if (elementType.IsArray)
                {
                    values.Add(
                        DeserializeArray(
                            reader,
                            elementType));
                }
                else if (elementType.IsGenericType)
                {
                    Type genericType = elementType.GetGenericTypeDefinition();

                    if (genericType == typeof(IList<>) ||
                        genericType == typeof(List<>))
                    {
                        values.Add(
                            DeserializeList(
                                reader,
                                elementType));
                    }
                }
                else
                {
                    values.Add(
                        DeserializeComplexObject(
                            reader,
                            elementType));
                }

                serializedValue = reader.ReadLine();
            }

            Array valueArray = Array.CreateInstance(elementType, values.Count);

            for (int i = 0; i < values.Count; i++)
            {
                valueArray.SetValue(
                    values[i],
                    i);
            }

            return valueArray;
        }

        private static object DeserializeList(
            StringReader reader,
            Type valueType)
        {
            Type[] elementTypes = valueType.GetGenericArguments();

            Type listType = typeof(List<>).MakeGenericType(elementTypes);

            IList values = (IList)Activator.CreateInstance(listType);

            Type elementType = elementTypes[0];

            string serializedValue = reader.ReadLine();

            while (serializedValue.TrimStart('\t') != "]")
            {
                if (elementType.IsPrimitive ||
                    elementType == typeof(string) ||
                    elementType == typeof(DateTime))
                {
                    values.Add(
                        DeserializePrimitive(
                            serializedValue.TrimStart('\t'),
                            elementType));
                }
                else if (elementType.IsArray)
                {
                    values.Add(
                        DeserializeArray(
                            reader,
                            elementType));
                }
                else if (elementType.IsGenericType)
                {
                    Type genericType = elementType.GetGenericTypeDefinition();

                    if (genericType == typeof(IList<>) ||
                        genericType == typeof(List<>))
                    {
                        values.Add(
                            DeserializeList(
                                reader,
                                elementType));
                    }
                }
                else
                {
                    values.Add(
                        DeserializeComplexObject(
                            reader,
                            elementType));
                }

                serializedValue = reader.ReadLine();
            }

            return values;
        }

        private static object DeserializeComplexObject(
            StringReader reader,
            Type valueType)
        {
            object value = Activator.CreateInstance(valueType);

            string serializedValue = reader.ReadLine();

            while (serializedValue.TrimStart('\t') != "}")
            {
                string propertyName = serializedValue.TrimStart('\t').Split('=')[0];

                PropertyInfo property = valueType.GetProperty(propertyName);

                Type propertyType = property.PropertyType;

                object propertyValue = null;

                if (propertyType.IsPrimitive ||
                    propertyType == typeof(string) ||
                    propertyType == typeof(DateTime))
                {
                    propertyValue = DeserializePrimitive(
                        serializedValue.Split('=')[1],
                        propertyType);
                }
                else if (propertyType.IsArray)
                {
                    reader.ReadLine();

                    propertyValue = DeserializeArray(
                        reader,
                        propertyType);
                }
                else if (propertyType.IsGenericType)
                {
                    Type genericType = propertyType.GetGenericTypeDefinition();

                    if (genericType == typeof(IList<>) ||
                        genericType == typeof(List<>))
                    {
                        reader.ReadLine();

                        propertyValue = DeserializeList(
                            reader,
                            propertyType);
                    }
                }
                else
                {
                    reader.ReadLine();

                    propertyValue = DeserializeComplexObject(
                        reader,
                        propertyType);
                }

                property.SetValue(
                    value,
                    propertyValue);

                serializedValue = reader.ReadLine();
            }

            return value;
        }
    }
}
