using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace LexicalAnalysis.UnitTests
{
    [TestClass]
    public class StringConverterTest
    {
        [TestMethod]
        public void InfixToPrefix_ReturnsTheExpresionInPrefixFormat()
        {
            
            // Act
            var actual = StringConverter.InfixToPrefix("A*B+C/D");
            var expected = "+*AB/CD";

            // Assert
            Assert.AreEqual(expected,actual);

        }


        [TestMethod]
        public void InfixToPostfix_ReturnsTheExpresionInPostfixFormat()
        {

            // Act
            var actual = StringConverter.InfixToPostfix("A+(B*C-(D/E-F)*G)*H");
            var expected = "ABC*DE/F-G*-H*+";

            // Assert
            Assert.AreEqual(expected, actual);

        }

    }
}
