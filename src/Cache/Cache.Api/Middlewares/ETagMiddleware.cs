//using Cache.Api.EntityTagsGenerators;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.WebUtilities;
//using Microsoft.Net.Http.Headers;
//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Net;
//using System.Net.Http;
//using System.Security.Cryptography;
//using System.Text.Json;
//using System.Threading.Tasks;

//namespace Cache.Api.Middlewares
//{
//    public class ETagMiddleware
//    {
//        private readonly RequestDelegate _next;
//        //private readonly IEntityTaggerGenerator _entityTaggerGenerator;

//        public ETagMiddleware(RequestDelegate next) //, IEntityTaggerGenerator entityTaggerGenerator)
//        {
//            _next = next;
//            //_entityTaggerGenerator = entityTaggerGenerator;
//        }

//        public async Task InvokeAsync(HttpContext context)
//        {
//            if (context.Request.Method == HttpMethod.Get.Method)
//            {
//                await _next(context);
//                if (context.Response.StatusCode == (int)HttpStatusCode.OK)
//                {
//                    var checksum = CalculateChecksum(context.Response.Body);
//                    context.Response.Headers[HeaderNames.ETag] = checksum;
//                    return;
//                }
//            }
//            await _next(context);
//        }

//        private static string CalculateChecksum(Stream stream)
//        {
//            string token;
//            using (var ms = new MemoryStream())
//            {
//                using (var writer = new BsonWriter(ms))
//                {
//                    var serializer = new JsonSerializer();
//                    serializer.Serialize(writer, result);
//                    token = GetToken(ms.ToArray());
//                }
//            }
//        }

//        private static string GetToken(byte[] bytes) { 
//            using (var hashAlg = MD5.Create()) { 
//                var checksum = hashAlg.ComputeHash(bytes); 
//                return Convert.ToBase64String(checksum, 0, checksum.Length);
//            } 
//        }

//    }
//}
