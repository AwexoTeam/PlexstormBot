using BotAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlexstormAPI;
using PlexstormAPI.Core;
using PlexBot;
using PlexBot.Definations;
using System.IO;
using Newtonsoft.Json;
using System.Media;

namespace TestMod
{
    public class TestModEntry : IInitializable
    {
        public int priority => 1;

        public string initHandle => "Test Mod";

        public string basePath { get; set; }
        public string settingsFile { get { return basePath + "\\Settings.json"; } }
        public DisplayWindow displayWindow;
        public Settings settings;
        public bool Initialize()
        {
            displayWindow = new DisplayWindow();
            displayWindow.Width = 400;
            displayWindow.Height = 400;
            displayWindow.Initialize();
            displayWindow.Show();

            EventProcessor.OnSubscribtion += EventProcessor_OnSubscribtion;
            EventProcessor.OnTipRecieved += EventProcessor_OnTipRecieved;
            EventProcessor.OnFollow += EventProcessor_OnFollow;

            if(!File.Exists(settingsFile))
            {
                Settings defaultSettings = Settings.DefaultSettings();
                string json = JsonConvert.SerializeObject(defaultSettings, Formatting.Indented);
                File.WriteAllText(settingsFile, json);
            }

            string fileJson = File.ReadAllText(settingsFile);
            settings = JsonConvert.DeserializeObject<Settings>(fileJson);

            return true;
        }

        private void EventProcessor_OnFollow(string msg)
        {
            string followGif = basePath + settings.FollowImagine;
            string followContent = settings.FollowMessage;
            followContent = followContent.Replace("{user}", msg);

            string soundFile = string.Empty;
            if (settings.FollowSound != "")
                soundFile = basePath + settings.FollowSound;

            GifNotification(displayWindow, 8000, followGif, soundFile, 50, followContent);
        }

        private void EventProcessor_OnTipRecieved(TipMessage msg)
        {
            string tippedGif = basePath + settings.TipImagine;
            string tippedContent = "";

            tippedContent = msg.isGeneralTip ? settings.TipMessage : settings.TipMenuMessage;
            tippedContent = tippedContent.Replace("{user}", msg.name);
            tippedContent = tippedContent.Replace("{amount}", msg.amount.ToString());
            tippedContent = tippedContent.Replace("{tipItem}", msg.content);

            string soundFile = string.Empty;
            if (settings.TipSound != "")
                soundFile = basePath + settings.TipSound;
            
            GifNotification(displayWindow, 8000, tippedGif, soundFile, 50, tippedContent);
        }

        private void EventProcessor_OnSubscribtion(string msg)
        {
            string subGif = basePath + settings.SubImagine;
            string subContent = settings.SubMessage;
            subContent = subContent.Replace("{user}", msg);

            string soundFile = string.Empty;
            if (settings.SubSound != "")
                soundFile = basePath + settings.SubSound;

            GifNotification(displayWindow, 8000, subGif, soundFile, 50, subContent);
        }

        public static void GifNotification(DisplayWindow window, int displayTime, string imagineFile, string soundFile, int textheight, string content)
        {
            int oWidth = window.Width;
            int oHeight = window.Height;

            ImagineRequest imagineRender = new ImagineRequest()
            {
                x = 0,
                y = 0,
                width = oWidth,
                height = oHeight - (textheight*2),
            };

            imagineRender.SetFromFile(imagineFile);

            TextRequest textRender = new TextRequest()
            {
                x = 5,
                y = oHeight - (textheight * 2),
                width = oWidth - 5,
                height = textheight,
                autoResize = true,
                text = content,
            };

            SoundRequest soundRequest = new SoundRequest() { file = soundFile };
            
            RenderRequest request = new RenderRequest(displayTime, soundRequest, imagineRender, textRender);
            window.EnqueueRenders(request);
        }


    }
}
