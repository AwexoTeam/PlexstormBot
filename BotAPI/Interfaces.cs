using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotAPI
{
    public interface IInitializable
    {
        int priority { get; }
        string initHandle { get; }
        string basePath { set; get; }
        bool Initialize();
    }

    public interface IInjectable
    {
        int priority { get; }
        string initHandle { get; }
        string basePath { set; get; }
        bool Initialize();
    }

    public enum AuthLevel
    {
        User,
        Moderator,
        Admin
    }

    public interface ICommandable
    {
        string handle { get; }
        AuthLevel authLevel { get; }
        void InvokeCommand(ChatContext msg, string invoker, string[] args);
        void InvokeCommand(ChatContext msg, string invoker);
    }

    public struct ChatContext
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
       
    }
}
