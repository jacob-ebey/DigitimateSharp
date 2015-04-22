
namespace DigitimateSharp
{
    /// <summary>
    /// Result of DigitimateSharp operations.
    /// </summary>
    public class Result
    {
        /// <summary>
        /// True if the operation was successful; otherwise false.
        /// </summary>
        public bool Successful { get; internal set; }

        /// <summary>
        /// If the operatio was successful the mobile number will be avaliable.
        /// </summary>
        public string MobileNumber { get; internal set; }

        /// <summary>
        /// If the operation was unsuccessful the error message will be non-null.
        /// </summary>
        public string ErrorMessage { get; internal set; }
    }
}
