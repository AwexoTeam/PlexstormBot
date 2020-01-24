using Newtonsoft.Json.Linq;
using PlexstormAPI.Core;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public static bool Initialize(string _channel)
        {
            channel = _channel;

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
            Console.WriteLine("Connected");
            ws.Send(GetSubString());
        }

        private static void OnError(object sender, ErrorEventArgs e)
        {
            Console.WriteLine("ERR: " + e.Exception);
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
                else { Console.WriteLine("Str is not num"); }
            }
        }

        private static string GetSubString()
        {
            string replaceStr = '"'.ToString();

            string baseStr = "42[{0}subscribe{0},{{0}channel{0}:{0}channel.{1}{0},{0}auth{0}:{{0}headers{0}:{{0}Authorization{0}:null}}}]";
            string rtn = baseStr.Replace("{0}", replaceStr);
            rtn = rtn.Replace("{1}", channel);

            return rtn;
        }

        public static void Log(string str) { Console.WriteLine(str); }
    }
}
