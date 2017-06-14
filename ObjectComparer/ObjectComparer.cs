using System;
using System.Collections.Generic;
using System.Linq;

namespace ObjectComparer
{
	public class ObjectComparer<T>
	{
		ObjectComparatorSettings Settings;
		IEnumerable<string> AttributesToCheck;

		public ObjectComparer(): this(new ObjectComparatorSettings()) {}

		public ObjectComparer(ObjectComparatorSettings settings)
		{
			this.Settings = settings;
			this.AttributesToCheck = this.GetProperties().Where(x => !this.Settings.Ignore.Contains(x));
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
				var valueA = this.GetPropertyValue(property, A);
				var valueB = this.GetPropertyValue(property, B);

				var equal = Object.Equals(valueA, valueB);

				if (!equal)
				{
					result.AreEqual = false;
					result.Differences.Add(property);
				}
			}
			
			return result;
		}

		private System.Reflection.PropertyInfo[] GetPropertiesOfT()
		{
			return typeof(T).GetProperties();
		}

		private List<string> GetProperties()
		{
			var list = new List<string>();

			foreach(var property in this.GetPropertiesOfT())
			{
				list.Add(property.Name);
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
