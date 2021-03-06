﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ObjectComparer
{
    public class ObjectComparer<T>
    {
        ComparerParameters Settings;
        IEnumerable<PropertiesSettings> AttributesToCheck;

        public ObjectComparer() : this(new ComparerParameters()) { }

        public ObjectComparer(ComparerParameters settings)
        {
            this.Settings = settings;

            this.AttributesToCheck = this.GetPropertiesSettings();
        }

        public Result Compare(T A, T B)
        {
            var result = new Result()
            {
                AreEqual = true,
                Differences = new List<string>()
            };

            foreach (var property in this.AttributesToCheck)
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

        private IComparer GetComparer(PropertiesSettings property)
        {
            if (property.Type == typeof(string))
                return new StringComparer(property.Flags);
            if (property.Type == typeof(char))
                return new CharacterComparer(property.Flags);
            if (property.Type == typeof(DateTime))
                return new DateTimeComparer(property.Flags);
            return new GenericComparer();
        }

        private bool CompareProperties(PropertiesSettings property, object valueA, object valueB)
        {
            return this.GetComparer(property).Compare(valueA, valueB);
        }

        private System.Reflection.PropertyInfo[] GetPropertiesOfT()
        {
            return typeof(T).GetProperties();
        }

        private List<PropertiesSettings> GetPropertiesSettings()
        {
            var list = new List<PropertiesSettings>();
            var globalFlags = this.Settings.Flags;

            foreach (var property in this.GetPropertiesOfT())
            {
                if (this.Settings.Ignore.Contains(property.Name))
                    continue;

                var propertySettings = this.Settings.Properties.FirstOrDefault(x => x.Name.Equals(property.Name));

                list.Add(new PropertiesSettings()
                {
                    Name = property.Name,
                    Type = property.PropertyType,
                    Flags = propertySettings != null ? propertySettings.Flags.Union(globalFlags).Distinct().ToList() : globalFlags
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
