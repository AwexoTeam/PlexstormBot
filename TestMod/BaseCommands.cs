using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotAPI.BaseCommands
{
    public class AddModerator : ICommandable
    {
        public string handle => "op";

        public AuthLevel authLevel => AuthLevel.Admin;

        public void InvokeCommand(string[] args)
        {
            for (int i = 0; i < args.Length; i++)
            {
                Console.WriteLine("Adding " + args[i] + " to mod.");
                CommandHandler.moderators.Add(args[i]);
            }
        }

        public void InvokeCommand()
        {
            //TODO: callback into chat
            Console.WriteLine("Please specificy an user!");
        }
    }
    public class RemoveModerator : ICommandable
    {
        public string handle => "deop";

        public AuthLevel authLevel => AuthLevel.Admin;

        public void InvokeCommand(string[] args)
        {
            for (int i = 0; i < args.Length; i++)
            {
                if (CommandHandler.moderators.Contains(args[i]))
                {
                    CommandHandler.moderators.Remove(args[i]);
                }
            }
        }

        public void InvokeCommand()
        {
            //TODO: callback into chat
            Console.WriteLine("Please specificy an user!");
        }
    }

}
