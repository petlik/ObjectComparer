using Microsoft.VisualStudio.TestTools.UnitTesting;
using ObjectComparer;
using System.Collections.Generic;

namespace ObjectComparerTests.Comparers
{
    [TestClass]
    public class CharacterComparerTests
    {
        [TestMethod]
        public void Compare_SameCharacter()
        {
            // Arrange
            var charA = "H";
            var charB = "H";

            var sut = new CharacterComparer();

            // Act
            var result = sut.Compare(charA, charB);

            // Assert
            Assert.IsTrue(result);

        }

        [TestMethod]
        public void Compare_DifferentCharacters()
        {
            // Arrange
            var charA = "H";
            var charB = "W";

            var sut = new CharacterComparer();

            // Act
            var result = sut.Compare(charA, charB);

            // Assert
            Assert.IsFalse(result);
        }
        
        [TestMethod]
        public void Compare_DifferentCaseInsensitive()
        {
            // Arrange
            var charA = "H";
            var charB = "h";

            var flags = new List<ComparerFlags>() { ComparerFlags.CaseInsensitive };

            var sut = new CharacterComparer(flags);

            // Act
            var result = sut.Compare(charA, charB);

            // Assert
            Assert.IsTrue(result);
        }
    }
}
