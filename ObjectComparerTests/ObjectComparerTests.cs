using Microsoft.VisualStudio.TestTools.UnitTesting;
using ObjectComparer.Parameters;
using ObjectComparer.Results;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ObjectComparer
{
	class ExampleSimpleClass
	{
		public bool Bool { get; set; }
		public int Integer { get; set; }
        public char Character { get; set; }
        public string String { get; set; }
	}

	[TestClass()]
	public class ObjectComparatorTests
	{
		// Setup
		private ObjectComparer<ExampleSimpleClass> GetObjectComparator()
		{
			return new ObjectComparer<ExampleSimpleClass>();
		}

		private ObjectComparer<ExampleSimpleClass> GetObjectComparator(ObjectComparatorParameters settings)
		{
			return new ObjectComparer<ExampleSimpleClass>(settings);
		}

		private ExampleSimpleClass GetExampleClass()
		{
			return new ExampleSimpleClass()
			{
				Bool = true,
				Integer = 13,
                Character = 'h',
				String = "Hello"
			};
		}

		[TestMethod()]
		public void Compare_SameObject()
		{
			// Arrange
			var sut = this.GetObjectComparator();
			var objectA = this.GetExampleClass();

			// Act
			var result = sut.Compare(objectA, objectA);

			// Assert
			Assert.IsTrue(result.AreEqual);
			Assert.AreEqual(0, result.Differences.Count);
		}

		[TestMethod()]
		public void Compare_DifferenceInBool()
		{
			// Arrange
			var sut = this.GetObjectComparator();
			var objectA = this.GetExampleClass();
			var objectB = this.GetExampleClass();
			objectB.Bool = !objectB.Bool;

			// Act
			var result = sut.Compare(objectA, objectB);

			// Assert
			Assert.IsFalse(result.AreEqual);
			Assert.AreEqual(1, result.Differences.Count);
			Assert.IsTrue(result.Differences.Contains("Bool"));
		}

		[TestMethod()]
		public void Compare_DifferenceInInteger()
		{
			// Arrange
			var sut = this.GetObjectComparator();
			var objectA = this.GetExampleClass();
			var objectB = this.GetExampleClass();
			objectB.Integer = objectB.Integer + 1;

			// Act
			var result = sut.Compare(objectA, objectB);

			// Assert
			Assert.IsFalse(result.AreEqual);
			Assert.AreEqual(1, result.Differences.Count);
			Assert.IsTrue(result.Differences.Contains("Integer"));
		}
		
		[TestMethod()]
		public void Compare_DifferenceInString()
		{
			// Arrange
			var sut = this.GetObjectComparator();
			var objectA = this.GetExampleClass();
			var objectB = this.GetExampleClass();
			objectB.String = objectB.String + " Test";

			// Act
			var result = sut.Compare(objectA, objectB);

			// Assert
			Assert.IsFalse(result.AreEqual);
			Assert.AreEqual(1, result.Differences.Count);
			Assert.IsTrue(result.Differences.Contains("String"));
		}

		[TestMethod()]
		public void Compare_IgnoreParameter()
		{
			// Arrange
			var settings = new ObjectComparatorParameters()
			{
				Ignore = new List<string> { "String" }
			};

			var sut = this.GetObjectComparator(settings);
			var objectA = this.GetExampleClass();
			var objectB = this.GetExampleClass();
			objectB.String = objectB.String + " Test";

			// Act
			var result = sut.Compare(objectA, objectB);

			// Assert
			Assert.IsTrue(result.AreEqual);
			Assert.AreEqual(0, result.Differences.Count);
		}

		[TestMethod()]
		public void Compare_CaseInsevsitive()
		{
			// Arrange
			var settings = new ObjectComparatorParameters()
			{
				Parameters = new List<PropertiesParametersFlags>() { PropertiesParametersFlags.CaseInsensitive }
			};

			var sut = this.GetObjectComparator(settings);
			var objectA = this.GetExampleClass();
			var objectB = this.GetExampleClass();
			objectB.String = objectB.String.ToUpper();

			// Act
			var result = sut.Compare(objectA, objectB);

			// Assert
			Assert.IsTrue(result.AreEqual);
			Assert.AreEqual(0, result.Differences.Count);
		}

		[TestMethod()]
		public void Compare_CaseInsevsitiveForParameterForCharacter()
		{
			// Arrange
			var settings = new ObjectComparatorParameters()
			{
				PropertiesParameters = new List<PropertiesParameters>() {
					new PropertiesParameters() {
						Name = "Character",
						Flags = new List<PropertiesParametersFlags>() { PropertiesParametersFlags.CaseInsensitive }
					}
				}
			};

			var sut = this.GetObjectComparator(settings);
			var objectA = this.GetExampleClass();
			var objectB = this.GetExampleClass();
			objectB.Character = char.ToUpper(objectB.Character);

			// Act
			var result = sut.Compare(objectA, objectB);

			// Assert
			Assert.IsTrue(result.AreEqual);
			Assert.AreEqual(0, result.Differences.Count);
		}

        [TestMethod()]
        public void Compare_CaseInsevsitiveForParameterForString()
        {
            // Arrange
            var settings = new ObjectComparatorParameters()
            {
                PropertiesParameters = new List<PropertiesParameters>() {
                    new PropertiesParameters() {
                        Name = "String",
                        Flags = new List<PropertiesParametersFlags>() { PropertiesParametersFlags.CaseInsensitive }
                    }
                }
            };

            var sut = this.GetObjectComparator(settings);
            var objectA = this.GetExampleClass();
            var objectB = this.GetExampleClass();
            objectB.String = objectB.String.ToUpper();

            // Act
            var result = sut.Compare(objectA, objectB);

            // Assert
            Assert.IsTrue(result.AreEqual);
            Assert.AreEqual(0, result.Differences.Count);
        }


        [TestMethod()]
		public void GetPropertiesSettings_SampleClass()
		{
			// Arrange
			var sut = this.GetObjectComparator();
			var listOfProperties = new List<string>() { "Bool", "Integer", "Character", "String" };

			// Act
			MethodInfo dynMethod = sut.GetType().GetMethod("GetPropertiesSettings", BindingFlags.NonPublic | BindingFlags.Instance);
			var list = (IList)dynMethod.Invoke(sut, null);
			
			// Assert
			Assert.AreEqual(listOfProperties.Count, list.Count);
			foreach(var item in list)
			{
				var name = item.GetType().GetProperty("Name").GetValue(item, null);
				Assert.IsTrue(listOfProperties.Contains(name));
			}
		}

		[TestMethod()]
		public void GetPropertyType_SampleClass()
		{
			// Arrange
			var sut = this.GetObjectComparator();

			// Act
			MethodInfo dynMethod = sut.GetType().GetMethod("GetPropertyType", BindingFlags.NonPublic | BindingFlags.Instance);
			var boolType = (Type)dynMethod.Invoke(sut, new object[] { "Bool" });
			var integerType = (Type)dynMethod.Invoke(sut, new object[] { "Integer" });
			var stringType = (Type)dynMethod.Invoke(sut, new object[] { "String" });

			// Assert
			Assert.AreEqual("System.Boolean", boolType.FullName);
			Assert.AreEqual("System.Int32", integerType.FullName);
			Assert.AreEqual("System.String", stringType.FullName);
		}
		
	}
}