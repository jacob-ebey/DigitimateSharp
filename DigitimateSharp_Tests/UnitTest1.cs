using System;
using System.Threading.Tasks;
using DigitimateSharp;
using DigitimateSharp_Tests.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DigitimateSharp_Tests
{
    [TestClass]
    public class DigitimateSharpTest
    {
        [TestMethod]
        public async Task SendAndValidate()
        {
            TestValidator validator = new TestValidator("jacob.ebey@live.com");

            TestResult result = (await validator.SendCodeAsync("8888888888")) as TestResult;

            Assert.IsNotNull(result, "Result send was null.");
            Assert.AreEqual(true, result.OperationSuccessful, "The send operation on the server was unsuccessful.");
            Assert.IsNotNull(result.Code, "The sent code to validate against was null.");

            CheckCodeResult checkResult = await validator.CheckCodeAsync("8888888888", result.Code);

            Assert.IsNotNull(checkResult, "The check result was null.");
            Assert.AreEqual(true, checkResult.OperationSuccessful, "The check operation on the server was unsuccessful");
            Assert.AreEqual(true, checkResult.ValidCode, "The check result did not pass on the server.");
        }
    }
}
