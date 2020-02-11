using Newtonsoft.Json.Linq;
using PlexstormAPI.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Authentication;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Timers;
using WebSocketSharp;

namespace PlexstormAPI
{
    public static class PlexAPI
    {

        private static string uri = "wss://websocket.plexstorm.com/socket.io/?EIO=3&transport=websocket";
        private static int digitsToCheck = 3;
        public static WebSocket ws;

        public static string channel;
        public static string publicKey;
        public static string clientToken;

        public static bool Initialize(string _channel, string _publicKey, string _clientToken)
        {
            channel = _channel;
            publicKey = _publicKey;
            clientToken = _clientToken;
            Timer pingTimer = new Timer();

            pingTimer = new System.Timers.Timer();
            pingTimer.Interval = 25000;
            pingTimer.AutoReset = true;
            pingTimer.Elapsed += OnPing;
            pingTimer.Start();

            ws = new WebSocket(uri);
            ws.SslConfiguration.EnabledSslProtocols = SslProtocols.Tls12;

            ws.OnMessage += OnMessage;
            ws.OnError += OnError;
            ws.OnOpen += OnOpen;
            ws.Connect();

            return ws.IsAlive;
        }

        private static void OnPing(object sender, System.Timers.ElapsedEventArgs e)
        {
            ws.Send("2");
        }

        private static void OnOpen(object sender, EventArgs e)
        {
            BotAPI.Debug.Log("Connected");
            ws.Send(GetSubString(channel, clientToken));
        }

        private static void OnError(object sender, WebSocketSharp.ErrorEventArgs e)
        {
            BotAPI.Debug.LogError("ERR: " + e.Exception);
        }

        private static void OnMessage(object sender, MessageEventArgs e)
        {
            string jsonstr = e.Data;
            if (jsonstr == "3") {  }
            else if (jsonstr == "40") { /*Index this*/}
            else
            {
                int number = -1;
                string chkString = jsonstr.Substring(0, digitsToCheck);
                string resultString = Regex.Match(chkString, @"\d+").Value;

                if (int.TryParse(resultString, out number))
                {
                    string jsonText = "";

                    if (number > 99)
                    {
                        jsonText = jsonstr.Substring(3, jsonstr.Length - 3);
                    }
                    else if (number > 9)
                    {
                        jsonText = jsonstr.Substring(2, jsonstr.Length - 2);
                    }

                    if (jsonText != string.Empty)
                    {
                        int startIndex = jsonText.IndexOf("content");
                        int endIndex = jsonText.IndexOf("'");

                        if (jsonstr.Contains("MessageCreated"))
                        {
                            EventProcessor.HandleChatMessage(jsonText);
                        }
                    }
                }
                else { BotAPI.Debug.Log("Str is not num"); }
            }
        }

        private static string GetSubString(string channel, string token)
        {
            string baseStr = "42[*subscribe*,{*channel*:*channel.<channelname>*,*auth*:{*headers*:{*Authorization*:*Bearer <token>*}}}]";

            baseStr = baseStr.Replace("<channelname>", channel);
            baseStr = baseStr.Replace("<token>", token);
            baseStr = baseStr.Replace('*', '"');

            return baseStr;
        }

        public static void WriteInChat(string message)
        {
            //string uri = "https://test.wikiop.in/postie.php";
            string uri = "https://api.plexstorm.com/api/channels/" + channel + "/messages";

            string content = "application/json;charset=utf-8";
            string data = "{" + '"' + "message" + '"' + ":" + '"' + message + '"' + "}";

            byte[] dataBytes = Encoding.UTF8.GetBytes(data);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            request.ContentLength = dataBytes.Length;
            request.ContentType = content;
            request.Method = "POST";
            request.Headers[HttpRequestHeader.Authorization] = "Bearer " + clientToken;

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
                    //Console.WriteLine(reader.ReadToEnd());
                }
            }
            catch (Exception e)
            {
                BotAPI.Debug.LogError("HTTP problem");
                BotAPI.Debug.LogError(e.Message);
                //return string.Empty;
            }
        }
    }
}
