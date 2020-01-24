using BotAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMod
{
    public class CommandTest : ICommandable
    {
        public string handle => "ping";
        public AuthLevel authLevel => AuthLevel.User;

        public void InvokeCommand(string[] args)
        {
            string output = "Pong ";

            for (int i = 0; i < args.Length; i++)
            {
                output += args[i] + ", ";
            }

            Console.WriteLine(output + "to you too!");
        }

        public void InvokeCommand()
        {
            Console.WriteLine("Pong!");
        }
    }
}
