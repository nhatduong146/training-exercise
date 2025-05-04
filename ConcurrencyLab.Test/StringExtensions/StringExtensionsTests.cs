using ConcurrencyLab.Extensions;
using ConcurrencyLab.Infrastructure.Helper.Validator;
using Moq;

namespace ConcurrencyLab.Test.StringExtensions
{
    [TestClass]
    public sealed class StringExtensionsTests
    {
        [TestMethod]
        public void Test_IsValidEmail()
        {
            var emailValidatorMock = new Mock<IEmailValidator>();

            // Mock the validation method
            emailValidatorMock
                .Setup(v => v.Validate(It.IsAny<string>()))
                .Returns<string>(email => email.Contains("@")); // Just simple verify

            string email1 = "duong@enlab.com";
            string email2 = "invalid.email";

            bool result1 = email1.IsValidEmail(emailValidatorMock.Object);
            bool result2 = email2.IsValidEmail(emailValidatorMock.Object);

            // Assertions
            Assert.IsTrue(result1);  // duong@enlab.com is valid
            Assert.IsFalse(result2); // invalid.email is not valid
        }
    }
}
