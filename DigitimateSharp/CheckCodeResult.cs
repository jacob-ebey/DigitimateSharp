
namespace DigitimateSharp
{
    /// <summary>
    /// Result of the check code operation.
    /// </summary>
    public class CheckCodeResult : Result
    {
        /// <summary>
        /// True if the code was valid; otherwise false.
        /// </summary>
        public bool ValidCode { get; internal set; }
    }
}
