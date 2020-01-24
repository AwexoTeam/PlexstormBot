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
        void InvokeCommand(string[] args);
        void InvokeCommand();
    }
}
