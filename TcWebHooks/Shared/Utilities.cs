using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using TcWebHooks.Models;
using System.Web.Http;

namespace TcWebHooks.Shared
{
    public static class Utilities
    {
        public static string WriteToDebugLogFile(this string message, HttpRequestMessage request)
        {
            string requestBody = string.Empty;

            using (var stream = new MemoryStream())
            {
                var context = (HttpContextBase)request.Properties["MS_HttpContext"];
                context.Request.InputStream.Seek(0, SeekOrigin.Begin);
                context.Request.InputStream.CopyTo(stream);
                requestBody = Encoding.UTF8.GetString(stream.ToArray());
            }

            string docPath = @"C:\Users\matthews\Documents";

            using (StreamWriter outputFile = new StreamWriter(docPath + @"\MyAppsLogs\WriteLines.txt", true))
            {
                outputFile.WriteLine("{0} : {1}", "Body", requestBody);
            }

            return message;
        }
    }
}