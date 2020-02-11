using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlexstormAPI.Core
{
     public enum ResponseType
    {
        Pong,
        SubscribeResponse,
        ChatMessage,
    }

    public struct ChatMessage
    {
        public int id;
        public string hexColor;
        public string avatar;
        public string name;
        public string slug;
        public string gender;
        public bool isTrans;
        public int level;
        public bool isPremium;
        public int credit;
        public string content;
        public string type;
        public bool isTip;
        public ChatMessage(JObject rss)
        {
            id = (int)rss["data"]["message"]["id"];
            content = (string)rss["data"]["message"]["content"];
            type = (string)rss["data"]["message"]["type"];
            if (type != "system")
            {
                name = (string)rss["data"]["message"]["user"]["name"];

                if (name != string.Empty && name != "null" && name != "system")
                {
                    hexColor = (string)rss["data"]["message"]["user"]["color"];
                    avatar = (string)rss["data"]["message"]["user"]["avatar"];

                    slug = (string)rss["data"]["message"]["user"]["slug"];
                    gender = (string)rss["data"]["message"]["user"]["gender"];
                    isTrans = (bool)rss["data"]["message"]["user"]["is_trans"];
                    level = (int)rss["data"]["message"]["user"]["level"];
                    isPremium = (bool)rss["data"]["message"]["user"]["is_premium"];

                }
                else
                {
                    hexColor = "#FFCCFF";
                    avatar = "";
                    slug = "";
                    gender = "non-defined";
                    isTrans = false;
                    level = -1;
                    isPremium = false;
                    isTip = false;
                    credit = 0;
                }

                string creditStr = (string)rss["data"]["message"]["credits"];
                if (!int.TryParse(creditStr, out credit))
                {
                    credit = 0;
                    isTip = false;
                }
                else
                {
                    isTip = credit > 0;
                }
            }
        }
    }

    public struct TipMessage
    {
        public string name;
        public int amount;
        public string content;
        public bool isGeneralTip;

        public TipMessage(string name, int amount, string content)
        {
            this.content = content;
            this.name = name;
            this.amount = amount;

            isGeneralTip = content == string.Empty;
        }
    }
}
