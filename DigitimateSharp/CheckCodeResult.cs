
using Newtonsoft.Json.Linq;
namespace DigitimateSharp
{
    /// <summary>
    /// Result of the check code operation.
    /// </summary>
    public class CheckCodeResult : Result
    {
        /// <summary>
        /// Used to keep the construction of result data internal.
        /// </summary>
        internal CheckCodeResult() { }

        /// <summary>
        /// True if the code was valid; otherwise false.
        /// </summary>
        public bool ValidCode { get; internal set; }

        new internal static CheckCodeResult From(JObject o)
        {
            CheckCodeResult result = new CheckCodeResult { ValidCode = false };

            JToken token = null;
            if (o.TryGetValue("validCode", out token))
            {
                result.ValidCode = token.Value<bool>();
            }
            if (o.TryGetValue("userMobileNumber", out token))
            {
                result.MobileNumber = token.Value<string>();
            }
            if (o.TryGetValue("err", out token))
            {
                result.ErrorMessage = token.Value<string>();
            }

            return result;
        }
    }
}
