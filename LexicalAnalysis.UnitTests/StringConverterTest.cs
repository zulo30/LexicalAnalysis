using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;


namespace LexicalAnalysis.UnitTests
{
    [TestClass]
    public class StringConverterTest
    {
        [TestMethod]
        public void InfixToPrefix_ReturnsTheExpresionInPrefixFormat()
        {
            // Arrange
            List<string> tokens = new List<string>() { "A","*","B","+","C","/","D"};
            var s = new Sentence(tokens, 1);
            // Act
            var actual = StringConverter.InfixToPrefix(s);
            var expected = "+*AB/CD";

            // Assert
            Assert.AreEqual(expected,actual);

        }


        [TestMethod]
        public void InfixToPostfix_ReturnsTheExpresionInPostfixFormat()
        {
            //arrange
            List<string> tokens = new List<string>() { "(", "52", "+", "5688", ")", "/", "672", "+", "279", "-", "282" };
            var s = new Sentence(tokens, 1);
            // Act
            var actual = StringConverter.InfixToPostfix(s);
            var expected = "525688+672/279+282-";

            // Assert
            Assert.AreEqual(expected, actual);

        }

    }
}
