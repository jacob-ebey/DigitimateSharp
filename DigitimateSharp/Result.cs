
using Newtonsoft.Json.Linq;
namespace DigitimateSharp
{
    /// <summary>
    /// Result of DigitimateSharp operations.
    /// </summary>
    public class Result
    {
        /// <summary>
        /// Used to keep the construction of result data internal.
        /// </summary>
        internal Result() { }

        /// <summary>
        /// True if the operation was successful; otherwise false.
        /// </summary>
        public bool OperationSuccessful { get { return ErrorMessage == null; } }

        /// <summary>
        /// If the operatio was successful the mobile number will be avaliable.
        /// </summary>
        public string MobileNumber { get; internal set; }

        /// <summary>
        /// If the operation was unsuccessful the error message will be non-null.
        /// </summary>
        public string ErrorMessage { get; internal set; }

        internal static Result From(JObject o)
        {
            Result result = new Result();

            JToken token = null;
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
