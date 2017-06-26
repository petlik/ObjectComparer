using System;
using System.Collections.Generic;
using System.Linq;
using ObjectComparer.Results;
using ObjectComparer.Parameters;

namespace ObjectComparer
{
	public class ObjectComparer<T>
	{
		ObjectComparatorParameters Settings;
		IEnumerable<PropertiesSettings> AttributesToCheck;

		public ObjectComparer(): this(new ObjectComparatorParameters()) {}

		public ObjectComparer(ObjectComparatorParameters settings)
		{
			this.Settings = settings;
			
			this.AttributesToCheck = this.GetPropertiesSettings();
		}

		public CompareResult Compare(T A, T B)
		{
			var result = new CompareResult()
			{
				AreEqual = true,
				Differences = new List<string>()
			};

			foreach(var property in this.AttributesToCheck)
			{
				var valueA = this.GetPropertyValue(property.Name, A);
				var valueB = this.GetPropertyValue(property.Name, B);

				var equal = CompareProperties(property, valueA, valueB);

				if (!equal)
				{
					result.AreEqual = false;
					result.Differences.Add(property.Name);
				}
			}
			
			return result;
		}

		private bool CompareProperties(PropertiesSettings property, object valueA, object valueB)
		{
			if (property.Type.FullName == "System.String" && property.Flags.Contains(PropertiesParametersFlags.CaseInsensitive))
				return String.Equals(((string)valueA).ToLower(), ((string)valueB).ToLower());
			return Object.Equals(valueA, valueB);
		}

		private System.Reflection.PropertyInfo[] GetPropertiesOfT()
		{
			return typeof(T).GetProperties();
		}

		private List<PropertiesSettings> GetPropertiesSettings()
		{
			var list = new List<PropertiesSettings>();

			foreach(var property in this.GetPropertiesOfT())
			{
				if (this.Settings.Ignore.Contains(property.Name))
					continue;

				var propertySettings = this.Settings.PropertiesParameters.FirstOrDefault(x => x.Name.Equals(property.Name));

				list.Add(new PropertiesSettings()
				{
					Name = property.Name,
					Type = property.PropertyType,
					Flags = propertySettings != null ? propertySettings.Flags : new List<PropertiesParametersFlags>()
				});
			}

			return list;
		}

		private Type GetPropertyType(string propertyName)
		{
			return typeof(T).GetProperty(propertyName).PropertyType;
		}
		
		private object GetPropertyValue(string propertyName, T A)
		{
			return typeof(T).GetProperty(propertyName).GetValue(A, null);
		}
	}
}
