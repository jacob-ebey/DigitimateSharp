﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace DigitimateSharp
{
    /// <summary>
    /// A phone validator using Digitmate.
    /// </summary>
    public class Validator
    {
        /// <summary>
        /// The base url on which to build
        /// </summary>
        const string BASE_URL = "http://digitimate.com/";

        /// <summary>
        /// Initialize a new instance of <see cref="DigitimateSharp.Validator"/>.
        /// </summary>
        /// <param name="developerEmail">Your company or product e-mail address so we can contact you.</param>
        /// <param name="numberOfDigits">The number of digits in the code to send.</param>
        /// <param name="message">A custom message format for the SMS sent to the user. This defaults to "Code: ".</param>
        public Validator(string developerEmail, int numberOfDigits = 6, string message = null)
        {
            DeveloperEmail = developerEmail;
            NumberOfDigits = numberOfDigits;
            Message = message;
        }

        /// <summary>
        /// Gets the e-mail address that you have provided to be reached at.
        /// </summary>
        public string DeveloperEmail { get; private set; }

        /// <summary>
        /// Gets the number of digits in the code to send.
        /// </summary>
        public int NumberOfDigits { get; private set; }

        /// <summary>
        /// Gets the custom message format of the sms to be sent to the user. If null, it defaults to "Code: ".
        /// </summary>
        public string Message { get; private set; }

        /// <summary>
        /// Send a code to the user that they will use to validate.
        /// </summary>
        /// <param name="mobileNumber">The user's mobile number.</param>
        /// <returns></returns>
        public async Task<Result> SendCodeAsync(string mobileNumber)
        {
            Dictionary<string, object> options = new Dictionary<string, object>
            {
                { "developerEmail", DeveloperEmail },
                { "userMobileNumber", mobileNumber },
                { "message", Message },
                { "numberOfDigits", (NumberOfDigits == 6 ? null : (object)NumberOfDigits) }
            };

            string url = BuildUrl("sendCode", options);

            HttpClient client = new HttpClient();
            string jsonResult = await client.GetStringAsync(url);

            JObject o = JObject.Parse(jsonResult);

            Result result = new Result { Successful = false };

            JToken token = null;
            if (o.TryGetValue("success", out token))
            {
                result.Successful = token.Value<bool>();
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

        public async Task<CheckCodeResult> CheckCodeAsync(string mobileNumber, string code)
        {
            Dictionary<string, object> options = new Dictionary<string, object>
            {
                { "developerEmail", DeveloperEmail },
                { "userMobileNumber", mobileNumber },
                { "code", code }
            };

            string url = BuildUrl("checkCode", options);

            HttpClient client = new HttpClient();
            string jsonResult = await client.GetStringAsync(url);

            JObject o = JObject.Parse(jsonResult);

            CheckCodeResult result = new CheckCodeResult { Successful = false, ValidCode = false };

            JToken token = null;
            if (o.TryGetValue("success", out token))
            {
                result.Successful = token.Value<bool>();
            }
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

        /// <summary>
        /// Build a url from the base url with the action and the options specified.
        /// </summary>
        /// <param name="options">
        /// A dictionary of option name and the value. The <see cref="System.Object.ToString"/> 
        /// result is what is used as the value.
        /// </param>
        /// <returns></returns>
        protected string BuildUrl(string action, Dictionary<string, object> options)
        {
            string url = BASE_URL + action;

            string delim = "?";
            foreach (KeyValuePair<string, object> option in options)
            {
                if (option.Value != null)
                {
                    url += string.Format("{0}{1}={2}", delim, Uri.EscapeDataString(option.Key), Uri.EscapeDataString(option.Value.ToString()));
                    delim = "&";
                }
                
            }

            return url;
        }
    }
}
