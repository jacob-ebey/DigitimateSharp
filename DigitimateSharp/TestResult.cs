using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace DigitimateSharp
{
    /// <summary>
    /// Test result of DigitimateSharp's SendCodeAsync operation.
    /// </summary>
    public class TestResult : Result
    {
        /// <summary>
        /// Used to keep the construction of result data internal.
        /// </summary>
        internal TestResult() { }

        /// <summary>
        /// The code to validate against. This is usually sent directly 
        /// to the mobile number and is only avaliable for test runs.
        /// </summary>
        public string Code { get; internal set; }

        internal static new TestResult From(JObject o)
        {
            TestResult result = new TestResult();

            Result tmp = Result.From(o);

            result.MobileNumber = tmp.MobileNumber;
            result.ErrorMessage = tmp.ErrorMessage;

            JToken token = null;
            if (o.TryGetValue("__testing__", out token))
            {
                JObject sub = token as JObject;
                if (sub != null)
                {
                    if (sub.TryGetValue("code", out token))
                    {
                        result.Code = token.Value<string>();
                    }
                }
            }

            return result;
        }
    }
}
