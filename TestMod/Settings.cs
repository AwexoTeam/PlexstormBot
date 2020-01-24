using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMod
{
    public class Settings
    {
        public string TipImagine { get; set; }
        public string SubImagine { get; set; }
        public string FollowImagine { get; set; }

        public string TipMessage { get; set; }
        public string TipMenuMessage { get; set; }
        public string SubMessage { get; set; }
        public string FollowMessage { get; set; }

        public string SubSound { get; set; }
        public string TipSound { get; set; }
        public string FollowSound { get; set; }

        public static Settings DefaultSettings()
        {
            return new Settings
            {
                TipImagine = "tip.gif",
                SubImagine = "sub.gif",
                FollowImagine = "follow.gif",

                TipMessage = "{user} just tipped you {amount}!",
                TipMenuMessage = "{user} just tipped for {tipItem} worth {amount}!",
                SubMessage = "{user} just Subscribed!",
                FollowMessage = "{user} just followed!",
                SubSound = "sub.wav",
                TipSound = "tip.wav",
                FollowSound = "follow.wav"
            };
        }
    }
}
