using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using PlexstormAPI.Core;

namespace PlexstormAPI.Core
{
    public static class EventProcessor
    {
        public delegate void _OnMessageRecv(ChatMessage msg);
        public delegate void _OnTipRecieved(TipMessage msg);
        public delegate void _OnMilestoneReached(string msg);
        public delegate void _OnSubscribtion(string msg);
        public delegate void _OnFollow(string msg);
        public delegate void _OnBotTick();

        public static event _OnMessageRecv OnMessageRecv;
        public static event _OnTipRecieved OnTipRecieved;
        public static event _OnMilestoneReached OnMilestoneReached;
        public static event _OnSubscribtion OnSubscribtion;
        public static event _OnFollow OnFollow;
        public static event _OnBotTick OnBotTick;

        public static void HandleChatMessage(string jsonStr)
        {
            //TODO: deal with json.

            int index = jsonStr.IndexOf('{');

            string json = jsonStr.Substring(index, jsonStr.Length - index - 1);

            JObject rss = JObject.Parse(json);

            string rssTitle = (string)rss["data"]["message"]["content"];

            ChatMessage msg = new ChatMessage(rss);
            if(OnMessageRecv != null)
            {
                OnMessageRecv.Invoke(msg);
            }

            if(msg.type == "tip")
            {
                TipMessage tipMsg = new TipMessage(msg.name, msg.credit, msg.content);
                OnTipRecieved?.Invoke(tipMsg);

            }
            else if(msg.type == "subscription") { OnSubscribtion?.Invoke(msg.name); }
            else if(msg.type == "milestone") { OnMilestoneReached?.Invoke(msg.content); }
        }

        public static void MilestoneHandler(string jsonStr)
        {
            OnMilestoneReached?.Invoke(jsonStr);
        }

        public static void InvokeTick() { OnBotTick?.Invoke(); }

        public static void InvokeFolowTest(){ OnFollow?.Invoke("Carlson"); }
        public static void InvokeSubTest() { OnSubscribtion?.Invoke("Benny"); }
        public static void InvokeTipTest() { OnTipRecieved?.Invoke(new TipMessage("Tim",100, "")); }
        
    }
}
