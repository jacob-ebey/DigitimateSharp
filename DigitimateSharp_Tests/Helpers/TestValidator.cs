using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DigitimateSharp;

namespace DigitimateSharp_Tests.Helpers
{
    class TestValidator : Validator
    {
        public TestValidator(string developerEmail, int numberOfDigits = 6, string message = null)
            : base(developerEmail, numberOfDigits, message) 
        {
            IsForTesting = true;
        }
    }
}
