using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using System.Net.Http;
using System.Net;
using Microsoft.Net.Http.Headers;
using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Security.Cryptography;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace Cache.Api.Filters
{
    // prevents the action filter methods to be invoked twice
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false)]
    public class ETagFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context) // OnActionExecuted(ActionExecutedContext context)
        {

            var request = context.HttpContext.Request;
            var response = context.HttpContext.Response;

            if (request.Method == HttpMethod.Get.Method &&
             response.StatusCode == (int)HttpStatusCode.OK)
            {
                var res = JsonConvert.SerializeObject(context.Result);

                var etag = GenerateETag(res);

                response.GetTypedHeaders().ETag = new EntityTagHeaderValue($"\"{etag}\"");
            }
            base.OnActionExecuted(context);
        }
        private string GenerateETag(string response)
        {
            // mechanism to generate ETag string
            // for the response content
            // can be any mechanism chosen by the developer
            return SHA1HashStringForUTF8String(response);
        }

        /// <summary>
        /// Compute hash for string encoded as UTF8
        /// </summary>
        /// <param name="s">String to be hashed</param>
        /// <returns>40-character hex string</returns>
        public static string SHA1HashStringForUTF8String(string s)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(s);

            var sha1 = SHA1.Create();
            byte[] hashBytes = sha1.ComputeHash(bytes);

            return HexStringFromBytes(hashBytes);
        }

        /// <summary>
        /// Convert an array of bytes to a string of hex digits
        /// </summary>
        /// <param name="bytes">array of bytes</param>
        /// <returns>String of hex digits</returns>
        private static string HexStringFromBytes(byte[] bytes)
        {
            var sb = new StringBuilder();
            foreach (byte b in bytes)
            {
                var hex = b.ToString("x2");
                sb.Append(hex);
            }
            return sb.ToString();
        }
    }
}