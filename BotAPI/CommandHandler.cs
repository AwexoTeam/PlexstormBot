
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace BotAPI
{
    public static class CommandHandler
    {
        public delegate void _CommandActivated(string command, string[] args);
        public static event _CommandActivated OnCommandActivated;
        public static Dictionary<string, ICommandable> commands;
        public static List<string> moderators;
        private static string adminChannel;
        public static void Initialize(string channel, string basePath)
        {
            Debug.Log("channel: " + channel);

            adminChannel = channel;
            moderators = new List<string>();
            
            commands = new Dictionary<string, ICommandable>();
            Timer tick = new Timer();
            tick.Interval = 1000;
            tick.AutoReset = true;
            tick.Elapsed += CommandHandler_Save;
            tick.Start();
        }

        private static void CommandHandler_Save(object sender, ElapsedEventArgs e)
        {
            if (moderators.Count > 0)
            {
                string output = "";
                string moderatorPath = Directory.GetCurrentDirectory() + "\\moderators.txt";
                for (int i = 0; i < moderators.Count; i++)
                {
                    output += moderators[i] + Environment.NewLine;
                }

                File.WriteAllText(moderatorPath, output);
            }
        }

        public static void ProcessCommand(ChatContext context, string commandContext)
        {
            string cmdHandle = "";
            string[] args = new string[] { "NONE" };
            bool haveArgs = false;
            string user = context.name;

            if(commandContext.Contains(" "))
            {
                List<string> split = commandContext.Split(' ').ToList();
                cmdHandle = split[0];

                split.RemoveAt(0);
                args = split.ToArray();
                haveArgs = true;
            }
            else
            {
                cmdHandle = commandContext;
                haveArgs = false;
            }

            if (commands.ContainsKey(cmdHandle))
            {
                ICommandable cmd = commands[cmdHandle];
                AuthLevel authLevel = GetUserAuthLevel(user);
                if (authLevel >= cmd.authLevel)
                {


                    if (haveArgs) { cmd.InvokeCommand(context, user, args); }
                    else { cmd.InvokeCommand(context, user); }

                    OnCommandActivated?.Invoke(cmdHandle, args);
                }
                else{ Debug.LogError(user + " tried to use " + cmd.handle + " as a(n) " + authLevel); }
            }
            else { Debug.LogError("Couldnt find command."); }
        }

        public static bool AddCommand(ICommandable cmd)
        {
            bool rtn = false;
            if (commands.ContainsKey(cmd.handle))
            {
                Debug.LogWarning("There is already a defination for: " + cmd.handle);
                rtn = false;
            }
            else
            {
                commands.Add(cmd.handle, cmd);
                rtn = true;
            }

            return rtn;
        }

        public static AuthLevel GetUserAuthLevel(string user)
        {
            if(user == adminChannel) { return AuthLevel.Admin; }
            else if (moderators.Contains(user)) { return AuthLevel.Moderator; }
            else { return AuthLevel.User; }
        }
    }
}
