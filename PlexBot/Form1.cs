using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using BotAPI;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PlexBot.Definations;
using PlexstormAPI;
using PlexstormAPI.Core;

namespace PlexBot
{
    public partial class Form1 : Form
    {
        public static Form1 instance;
        public string runningPath;
        public Settings settings;
        public Form1()
        {
            InitializeComponent();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Starting up.");
            if (PlexAPI.Initialize(channelTB.Text))
            {
                Initialize();
            }
            else { Console.WriteLine("Problems with connection. "); }
        }

        private void Initialize()
        {
            CommandHandler.Initialize(channelTB.Text);
            string pluginPath = runningPath + "\\Plugins\\DLLs";

            bool canLogin = TryLogin();

            EventProcessor.OnMessageRecv += OnMessageRecv;

            if (!Directory.Exists(pluginPath))
                Directory.CreateDirectory(pluginPath);
            
            foreach (string file in Directory.GetFiles(pluginPath))
            {
                if (Path.GetExtension(file) == ".dll")
                {
                    Assembly dll = Assembly.LoadFile(file);
                    InitializePlugin(dll, Path.GetFileName(file));
                }
            }
        }

        private bool TryLogin()
        {
            string getURI = "https://www.plexstorm.com/_nuxt/9f23ce14305d78437a2f.js";
            string json = HttpHelper.Get(getURI);
            
            return true;
        }

        private void OnMessageRecv(ChatMessage msg)
        {
            if(msg.content[0] == settings.prefix)
            {
                string commandContext = msg.content.Substring(1, msg.content.Length - 1);
                CommandHandler.ProcessCommand(msg.name, commandContext);
            }
        }

        private void InitializePlugin(Assembly assembly, string fileName)
        {
            var initType = typeof(IInitializable);

            List<Type> initializables = assembly.GetTypes().Where(x => initType.IsAssignableFrom(x)).ToList();
            foreach (Type iType in initializables)
            {
                IInitializable init = (IInitializable)Activator.CreateInstance(iType);
                string pluginPath = runningPath + "\\Plugins\\" + init.initHandle + "\\";

                if (!Directory.Exists(pluginPath))
                    Directory.CreateDirectory(pluginPath);

                init.basePath = pluginPath;

                bool isInit = init.Initialize();
                if (isInit)
                {
                    Console.WriteLine("{0} Initialized success", init.initHandle);
                }
                else { Console.WriteLine("{0} Initialized failed", init.initHandle); }
            }

            var commandType = typeof(ICommandable);
            List<Type> commands = assembly.GetTypes().Where(x => commandType.IsAssignableFrom(x)).ToList();
            foreach (Type cType in commands)
            {
                ICommandable cmd = (ICommandable)Activator.CreateInstance(cType);
                bool canAdd = CommandHandler.AddCommand(cmd);

                string output = "";
                if (canAdd)
                {
                    output = string.Format("Command {0} from {1} added successfully", cmd.handle, fileName);
                }
                else
                {
                    output = string.Format("Command {0} from {1} failed", cmd.handle, fileName);
                }

                Console.WriteLine(output);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            instance = this;
            
            runningPath = Directory.GetCurrentDirectory();

            string settingFilePath = runningPath + "\\Settings.txt";

            if (!File.Exists(settingFilePath))
            {
                SaveSettings(new Settings(), settingFilePath);
            }

            string json = File.ReadAllText(settingFilePath);
            settings = JsonConvert.DeserializeObject<Settings>(json);

            xResTB.Text = settings.xRes.ToString();
            yResTB.Text = settings.yRes.ToString();

            channelTB.Text = settings.lastChannel;
            emailTB.Text = settings.lastEmail;
            AutoSave.Interval = settings.tickDelay;
        }

        private void SaveSettings(Settings setting, string path)
        {
            using (StreamWriter file = File.CreateText(path))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Formatting = Formatting.Indented;
                serializer.Serialize(file, setting);
            }
        }

        private void SaveSettings()
        {
            string settingFilePath = runningPath + "\\Settings.txt";
            SaveSettings(settings, settingFilePath);
        }

        private void AutoSave_Tick(object sender, EventArgs e)
        {
            EventProcessor.InvokeTick();
            settings.lastChannel = channelTB.Text;
            settings.lastEmail = emailTB.Text;
            settings.tickDelay = AutoSave.Interval;

            SaveSettings();
            //TODO: parse resolution and make sure they real ints.
        }

        private void testFollowBTN_Click(object sender, EventArgs e)
        {
            EventProcessor.InvokeFolowTest();
        }

        private void testSubBtn_Click(object sender, EventArgs e)
        {
            EventProcessor.InvokeSubTest();
        }

        private void testTipBTN_Click(object sender, EventArgs e)
        {
            EventProcessor.InvokeTipTest();
        }
    }
}
