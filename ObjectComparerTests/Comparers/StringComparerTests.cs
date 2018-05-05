using Microsoft.VisualStudio.TestTools.UnitTesting;
using ObjectComparer;
using System.Collections.Generic;

namespace ObjectComparerTests.Comparers
{
    [TestClass]
    public class StringComparerTests
    {
        [TestMethod]
        public void Compare_SameString()
        {
            // Arrange
            var stringA = "Hello";
            var stringB = "Hello";

            var sut = new StringComparer();

            // Act
            var result = sut.Compare(stringA, stringB);

            // Assert
            Assert.IsTrue(result);

        }

        [TestMethod]
        public void Compare_DifferentStrings()
        {
            // Arrange
            var stringA = "Hello";
            var stringB = "World";

            var sut = new StringComparer();

            // Act
            var result = sut.Compare(stringA, stringB);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Compare_DifferentCaseInsensitive()
        {
            // Arrange
            var stringA = "Hello";
            var stringB = "hello";

            var flags = new List<ComparerFlags>() { ComparerFlags.CaseInsensitive };

            var sut = new StringComparer(flags);

            // Act
            var result = sut.Compare(stringA, stringB);

            // Assert
            Assert.IsTrue(result);
        }
    }
}
