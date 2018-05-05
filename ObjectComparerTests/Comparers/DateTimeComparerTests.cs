using Microsoft.VisualStudio.TestTools.UnitTesting;
using ObjectComparer;
using System;
using System.Collections.Generic;

namespace ObjectComparerTests.Comparers
{
    [TestClass]
    public class DateTimeComparerTests
    {
        [TestMethod]
        public void Compare_SameDate()
        {
            // Arrange
            var dateA = new DateTime(1969, 7, 20, 20, 18, 00);
            var dateB = new DateTime(1969, 7, 20, 20, 18, 00);

            var sut = new DateTimeComparer();

            // Act
            var result = sut.Compare(dateA, dateB);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Compare_DifferentDate()
        {
            // Arrange
            var dateA = new DateTime(1969, 7, 16, 13, 32, 00);
            var dateB = new DateTime(1969, 7, 24, 16, 50, 00);

            var sut = new DateTimeComparer();

            // Act
            var result = sut.Compare(dateA, dateB);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Compare_IgnoreTime()
        {
            // Arrange
            var dateA = new DateTime(1969, 7, 20, 20, 18, 00);
            var dateB = new DateTime(1969, 7, 20, 20, 20, 00);

            var flags = new List<ComparerFlags>() { ComparerFlags.IgnoreTime };

            var sut = new DateTimeComparer(flags);

            // Act
            var result = sut.Compare(dateA, dateB);

            // Assert
            Assert.IsTrue(result);
        }
    }
}
