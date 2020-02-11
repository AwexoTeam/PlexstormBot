using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
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
        public string clientKey;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            string url = "https://api.plexstorm.com/oauth/token";
            string content = "application/json;charset=utf-8";
            string headers = "Accept: application/json, text/plain, */*";

            string dataBase = "{*username*:*<email address>*,*password*:*<password>*,*grant_type*:*password*,*client_id*:*1*,*client_secret*:*<token>*}";

            dataBase = dataBase.Replace("<email address>", emailTB.Text);
            dataBase = dataBase.Replace("<password>", passwordTB.Text);
            dataBase = dataBase.Replace("<token>", settings.publicKey);

            string data = dataBase.Replace('*', '"');

            string json = HttpHelper.Post(url, data, content, headers);
            JObject rss = JObject.Parse(json);
            clientKey = (string)rss["access_token"];

            if (clientKey != string.Empty)
            {
                Debug.Log("Starting up.");
                if (PlexAPI.Initialize(channelTB.Text, settings.publicKey, clientKey))
                {
                    Initialize();
                }
                else { Debug.LogError("Problems with connection. "); }
            }
        }

        private void Initialize()
        {
            bool canLogin = TryLogin();

            EventProcessor.OnMessageRecv += OnMessageRecv;

            for (int i = 0; i < PluginManager.inits.Count; i++)
            {
                IInitializable init = PluginManager.inits[i];
                if (init.Initialize())
                {
                    Debug.Log("Loaded " + init.initHandle + " successfully");
                }
                else
                {
                    Debug.LogError("Loaded " + init.initHandle + " unsuccessfully");
                }
            }

        }

        private bool TryLogin()
        {
            //Something here?
            return true;
        }

        private void OnMessageRecv(ChatMessage msg)
        {
            if (msg.content[0] == settings.prefix)
            {
                string commandContext = msg.content.Substring(1, msg.content.Length - 1);

                ChatContext context = new ChatContext
                {
                    id = msg.id,
                    hexColor = msg.hexColor,
                    avatar = msg.avatar,
                    name = msg.name,
                    slug = msg.slug,
                    gender = msg.gender,
                    isTrans = msg.isTrans,
                    level = msg.level,
                    isPremium = msg.isPremium,
                    credit = msg.credit,
                    content = msg.content,
                    type = msg.type,
                    isTip = msg.isTip
                };

                CommandHandler.ProcessCommand(context, commandContext);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            instance = this;

            runningPath = Directory.GetCurrentDirectory();
            PluginManager.runningPath = runningPath + "\\Plugins";

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

            string pluginPath = runningPath + "\\Plugins\\DLLs";

            if (!Directory.Exists(pluginPath))
                Directory.CreateDirectory(pluginPath);

            
            CommandHandler.Initialize(channelTB.Text, runningPath);



            foreach (string file in Directory.GetFiles(pluginPath))
            {
                if (Path.GetExtension(file) == ".dll")
                {
                    Assembly dll = Assembly.LoadFile(file);
                    string name = Path.GetFileName(file);

                    PluginManager.LoadPlugins(dll);
                    PluginManager.LoadCommands(dll, name);
                    PluginManager.LoadInjections(dll, name);
                }
            }

            for (int i = 0; i < PluginManager.injects.Count; i++)
            {
                IInjectable inject = PluginManager.injects[i];
                if (inject.Initialize())
                {
                    Debug.Log("Injected " + inject.initHandle + " successfully");
                }
                else
                {
                    Debug.LogError("Injected " + inject.initHandle + " unsuccessfully");
                }
            }

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
            if(settings.lastChannel != string.Empty)
                settings.lastChannel = channelTB.Text;

            if(settings.lastEmail != string.Empty)
                settings.lastEmail = emailTB.Text;

            if(settings.tickDelay != -1)
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
