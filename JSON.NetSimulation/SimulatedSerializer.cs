using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace JSON.NetSimulation
{
    public class SimulatedSerializer
    {
        public string Serialize(
            object value,
            Formatting formatting)
        {
            StringBuilder stream = new StringBuilder();

            SerializeObject(
                value,
                stream,
                formatting,
                0);

            return stream.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value">
        ///     Object to be serialized into the stream.
        /// </param>
        /// <param name="stream">
        ///     Holder stream for receiving serialized portions of objects.
        /// </param>
        /// <param name="formatting">
        ///     
        /// </param>
        /// <param name="level">
        ///     Current indentation level, if needed for indented formatting.
        /// </param>
        private static void SerializeObject(
               object value,
               StringBuilder stream,
               Formatting formatting,
               int level)
        {
            Type valueType = value.GetType();

            if (valueType.IsPrimitive ||
                valueType == typeof(string) ||
                valueType == typeof(DateTime))
            {
                stream.AppendLine(value.ToString());

                return;
            }

            if (typeof(IEnumerable).IsAssignableFrom(valueType))
            {
                stream.AppendLine("[");

                IEnumerable list = (IEnumerable)value;

                foreach (object listValue in list)
                {
                    if (formatting == Formatting.Indented)
                    {
                        stream.Append(new string('\t', level + 1));
                    }

                    SerializeObject(
                        listValue,
                        stream,
                        formatting,
                        level + 1);
                }

                if (formatting == Formatting.Indented)
                {
                    stream.Append(new string('\t', level));
                }

                stream.AppendLine("]");

                return;
            }

            // if it reaches here, it's a complex object
            // so we'll serialize all public properties

            stream.AppendLine("{");

            foreach (PropertyInfo property in value.GetType().GetProperties()
                .Where(p => p.CanRead && p.CanWrite)
                .OrderBy(p => p.Name))
            {
                if (formatting == Formatting.Indented)
                {
                    stream.Append(new string('\t', level + 1));
                }

                stream.Append(property.Name);
                stream.Append("=");

                object propertyValue = property.GetValue(value);
                Type propertyType = property.PropertyType;

                if (propertyValue != null &&
                    !propertyType.IsPrimitive &&
                    propertyType != typeof(string) &&
                    propertyType != typeof(DateTime))
                {
                    stream.AppendLine();

                    if (formatting == Formatting.Indented)
                    {
                        stream.Append(new string('\t', level + 1));
                    }
                }

                SerializeObject(
                    propertyValue,
                    stream,
                    formatting,
                    level + 1);
            }

            if (formatting == Formatting.Indented)
            {
                stream.Append(new string('\t', level));
            }

            stream.AppendLine("}");
        }
    }
}
