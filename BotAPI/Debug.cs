using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotAPI
{
    public static class Debug
    {
        public static void Log(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Green;

            string dateStamp = DateTime.Now.ToString("hh:mm tt");
            Console.Write("[" + dateStamp + "]");
            
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(" " + msg);
            Console.Write(Environment.NewLine);
        }

        public static void LogError(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            StackTrace trace = new StackTrace();

            string method = trace.GetFrame(1).GetMethod().Name;
            Console.Write("[" + method + "]");
            Console.Write(" " + msg);
            Console.Write(Environment.NewLine);
        }

        public static void LogWarning(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            StackTrace trace = new StackTrace();

            string method = trace.GetFrame(1).GetMethod().Name;
            Console.Write("[" + method + "]");
            Console.Write(" " + msg);
            Console.Write(Environment.NewLine);
        }
    }
}
