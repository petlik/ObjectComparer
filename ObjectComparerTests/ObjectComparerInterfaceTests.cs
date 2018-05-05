using Microsoft.VisualStudio.TestTools.UnitTesting;
using ObjectComparer;
using System.Diagnostics.CodeAnalysis;

namespace ObjectComparerTests
{
    interface IExampleInterface
    {
        string String { get; set; }
    }

    [ExcludeFromCodeCoverage]
    class ExampleInterfaceClassA : IExampleInterface
    {
        public int Integer { get; set; }
        public string String { get; set; }
    }

    [ExcludeFromCodeCoverage]
    class ExampleInterfaceClassB : IExampleInterface
    {
        public char Character { get; set; }
        public string String { get; set; }
    }

    [TestClass]
    [ExcludeFromCodeCoverage]
    public class ObjectComparatorInterfaceTests
    {
        // Setup
        private ObjectComparer<IExampleInterface> GetObjectComparator()
        {
            return new ObjectComparer<IExampleInterface>();
        }

        private ExampleInterfaceClassA GetExampleInterfaceClassA()
        {
            return new ExampleInterfaceClassA()
            {
                Integer = 13,
                String = "Hello"
            };
        }

        private ExampleInterfaceClassB GetExampleInterfaceClassB()
        {
            return new ExampleInterfaceClassB()
            {
                Character = 'h',
                String = "Hello"
            };
        }

        [TestMethod]
        public void Compare_InterfaceSameObject()
        {
            // Arrange
            var sut = this.GetObjectComparator();
            var objectA = this.GetExampleInterfaceClassA();

            // Act
            var result = sut.Compare(objectA, objectA);

            // Assert
            Assert.IsTrue(result.AreEqual);
            Assert.AreEqual(0, result.Differences.Count);
        }

        [TestMethod]
        public void Compare_DifferenceOutsideInterface()
        {
            // Arrange
            var sut = this.GetObjectComparator();
            var objectA = this.GetExampleInterfaceClassA();
            var objectB = this.GetExampleInterfaceClassA();
            objectB.Integer = objectB.Integer + 1;

            // Act
            var result = sut.Compare(objectA, objectB);

            // Assert
            Assert.IsTrue(result.AreEqual);
            Assert.AreEqual(0, result.Differences.Count);
        }

        [TestMethod]
        public void Compare_DifferentObjectsWithSameInterface()
        {
            // Arrange
            var sut = this.GetObjectComparator();
            var objectA = this.GetExampleInterfaceClassA();
            var objectB = this.GetExampleInterfaceClassB();

            // Act
            var result = sut.Compare(objectA, objectB);

            // Assert
            Assert.IsTrue(result.AreEqual);
            Assert.AreEqual(0, result.Differences.Count);
        }

    }
}