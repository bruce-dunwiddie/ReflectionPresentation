using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using ReflectionPresentation.Helpers;

namespace ReflectionPresentation.Introspection
{
	public static class FieldsDisplay
	{
        public static void Display(Logger log, Type typeToDisplay)
		{
			log.LogMessage("Name: " + TypeDisplayHelper.GetPrettyName(typeToDisplay));

			FieldInfo[] publicFields = typeToDisplay.GetFields(
					BindingFlags.Instance |
					BindingFlags.Public);

            if (publicFields.Any())
			{
				log.LogMessage("Public Fields:");
				foreach (FieldInfo field in publicFields)
				{
                    log.LogMessage("\t" + TypeDisplayHelper.GetPrettyName(field.FieldType) + " : " + field.Name);
                    log.LogMessage("");
				}
			}

			FieldInfo[] privateFields = typeToDisplay.GetFields(
					BindingFlags.Instance |
					BindingFlags.NonPublic);

			if (privateFields.Any())
			{
				log.LogMessage("Non Public Fields:");
				foreach (FieldInfo field in privateFields)
				{
                    log.LogMessage("\t" + TypeDisplayHelper.GetPrettyName(field.FieldType) + " : " + field.Name);
                    log.LogMessage("");
				}
			}
		}
	}
}
