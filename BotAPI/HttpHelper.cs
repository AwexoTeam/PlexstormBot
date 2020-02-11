using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BotAPI
{
    public static class HttpHelper
    {
        public static string Get(string uri)
        {
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
                request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
            catch (Exception e)
            {
                Debug.LogError("FAILED ON URI: " + uri);
                Debug.LogError(e.Message);
                throw;
            }
        }

        public static string Post(string uri, string data, string contentType, string header, string method = "POST")
        {
            byte[] dataBytes = Encoding.UTF8.GetBytes(data);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            request.ContentLength = dataBytes.Length;
            request.ContentType = contentType;
            request.Method = method;

            if (header.ToLower().Contains("accept"))
            {
                request.Accept = header;
                
                using (Stream requestBody = request.GetRequestStream())
                {
                    requestBody.Write(dataBytes, 0, dataBytes.Length);
                }

                try
                {
                    using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                    using (Stream stream = response.GetResponseStream())
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        return reader.ReadToEnd();
                    }
                }
                catch (Exception e)
                {
                    Debug.LogError("HTTP problem");
                    Debug.LogError(e.Message);
                    return string.Empty;
                }
            }
            else { return string.Empty; }
        }
    }
}
