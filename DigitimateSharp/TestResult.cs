using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace DigitimateSharp
{
    public class TestResult : Result
    {
        /// <summary>
        /// Used to keep the construction of result data internal.
        /// </summary>
        internal TestResult() { }

        public string Code { get; internal set; }

        internal static TestResult From(JObject o)
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
