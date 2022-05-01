using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WordToNumberTests
{
    using WordToNumber;

    [TestClass]
    public class StringToNumberConverterTest
    {
        [TestMethod]
        public void TestStringIsaValidSingleDigitNumber()
        {
            var sut = new StringToNumberConverter();
            Assert.IsTrue(sut.IsValidNumber("one"));
            Assert.IsTrue(sut.IsValidNumber("two"));
            Assert.IsTrue(sut.IsValidNumber("three"));
            Assert.IsTrue(sut.IsValidNumber("four"));
            Assert.IsTrue(sut.IsValidNumber("five"));
            Assert.IsTrue(sut.IsValidNumber("six"));
            Assert.IsTrue(sut.IsValidNumber("seven"));
            Assert.IsTrue(sut.IsValidNumber("eight"));
            Assert.IsTrue(sut.IsValidNumber("nine"));
            Assert.IsTrue(sut.IsValidNumber("zero"));
            Assert.IsFalse(sut.IsValidNumber("xyz"));
            Assert.IsTrue(sut.IsValidNumber("ten"));
            Assert.IsTrue(sut.IsValidNumber("eleven"));
            Assert.IsTrue(sut.IsValidNumber("twelve"));
            Assert.IsTrue(sut.IsValidNumber("thirteen"));
            Assert.IsTrue(sut.IsValidNumber("fourteen"));
            Assert.IsTrue(sut.IsValidNumber("fifteen"));
            Assert.IsTrue(sut.IsValidNumber("sixteen"));
            Assert.IsTrue(sut.IsValidNumber("seventeen"));
            Assert.IsTrue(sut.IsValidNumber("eighteen"));
            Assert.IsTrue(sut.IsValidNumber("nineteen"));

            // two digit numbers
            Assert.IsTrue(sut.IsValidNumber("twenty one"));
            Assert.IsTrue(sut.IsValidNumber("eighty six"));
            Assert.IsTrue(sut.IsValidNumber("ninety five"));

            // Three digit numbers
            Assert.IsTrue(sut.IsValidNumber("one hundred ninety five"));
            Assert.IsTrue(sut.IsValidNumber("six hundred fifty seven"));

            // four digit numbers Bad string with space 
            Assert.IsTrue(sut.IsValidNumber(" two thousand one hundred ninety five"));
            
            // Bad string with spaces in between four digit number
            Assert.IsTrue(sut.IsValidNumber(" eight    thousand six hundred ninety two"));

            // Six digit number
            Assert.IsTrue(sut.IsValidNumber(" eight million five hundred  thousand six hundred ninety two"));
        }

        /// <summary>
        /// The test valid string is converted to number.
        /// </summary>
        [TestMethod]
        public void TestValidStringIsConvertedToNumber()
        {
            var sut = new StringToNumberConverter();
            Assert.IsTrue(sut.ConvertTo("zero") == 0, "Failed to convert 0");
            Assert.IsTrue(sut.ConvertTo("ten") == 10, "Failed to convert 10");
            Assert.IsTrue(sut.ConvertTo("Fifty one") == 51, "Failed to convert 51");
            Assert.IsTrue(sut.ConvertTo("eighty nine") == 89, "Failed to convert 89");
            Assert.IsTrue(sut.ConvertTo("three hundred Fifty one") == 351, "Failed to convert 351");
            Assert.IsTrue(sut.ConvertTo("four thousand three hundred Fifty one") == 4351, "Failed to convert 4351");
            Assert.IsTrue(sut.ConvertTo("fifty three thousand") == 53000, "Failed to convert 53000");


            Assert.IsTrue(sut.ConvertTo("fifty three thousand three hundred fifty seven") == 53357, "Failed to convert 53357");
            Assert.IsTrue(sut.ConvertTo("sixty five thousand three hundred") == 65300, "Failed to convert 65300");
            Assert.IsTrue(sut.ConvertTo("sixty  thousand six hundred sixty six") == 60666, "Failed to convert 60666");
            Assert.IsTrue(sut.ConvertTo("Three hundred thousand") == 300000, "Failed to convert 300000");
            Assert.IsTrue(sut.ConvertTo("Six thousand eight hundred twenty one") == 6821, "Failed to convert 6821");

            Assert.IsTrue(sut.ConvertTo("Seven hundred ten thousand nine hundred sixty three") == 710963, "Failed to convert 710963");
            Assert.IsTrue(sut.ConvertTo("Three million five hundred twelve thousand three hundred") == 3512300, "Failed to convert 3512300");
            Assert.IsTrue(sut.ConvertTo("two hundred sixty three million seven hundred fifty nine thousand twenty four") == 263759024, "Failed to convert 2,63,759,024 ");

        }
        
        /// <summary>
        /// Convert string to number.
        /// </summary>
        [TestMethod]
        public void TestConvertStringToNumber()
        {
            var sut = new StringToNumberConverter();
            Assert.IsTrue(sut.ConvertTo(51) == "fifty one", "Failed to convert 51");
            Assert.IsTrue(sut.ConvertTo(89) == "eighty nine", "Failed to convert 89");
            Assert.IsTrue(sut.ConvertTo(53357) == "fifty three thousand three hundred fifty seven", "Failed to convert 53357");
            Assert.IsTrue(sut.ConvertTo(710963) == "seven hundred ten thousand nine hundred sixty three", "Failed to convert 710963");

            Assert.IsTrue(sut.ConvertTo(3512300).Trim() == "three million five hundred twelve thousand three hundred", "Failed to convert 3512300");
        }
    }
}
