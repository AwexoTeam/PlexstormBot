using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace PlexBot.Definations
{
    public partial class Settings
    {
        //TODO: make a way to initialize other setting's config
        public Settings()
        {
            xRes = 800;
            yRes = 600;
            lastEmail = "";
            lastChannel = "";
            tickDelay = 1500;
            prefix = '!';
            publicKey = "tJ1Ng4AoDwnecJ1zknyX0cDd3BaxWAKR10s7YKnw";
        }
        public string publicKey { get; set; }
        public int tickDelay { get; set; }
        public int xRes { get; set; }
        public int yRes { get; set; }
        public string lastEmail { get; set; }
        public string lastChannel { get; set; }
        public char prefix { get; set; }

    }

    public interface IRenderable
    {
        void Render(DisplayWindow window);
    }

    public struct RenderRequest
    {
        public int displayTime;

        private List<IRenderable> _renders;
        public IRenderable[] renders { get { return _renders.ToArray(); } }

        private DisplayWindow targetWindow;

        public RenderRequest(int displayTime, params IRenderable[] _irenderables)
        {
            targetWindow = null;
            this.displayTime = displayTime;

            _renders = new List<IRenderable>();
            _renders.AddRange(_irenderables);
        }

        public void AddRenders(params IRenderable[] _renders)
        {
            this._renders.AddRange(_renders);
        }

        public void Render(DisplayWindow window)
        {
            targetWindow = window;

            System.Timers.Timer displayTimer = new System.Timers.Timer();
            displayTimer.Interval = displayTime;
            displayTimer.Elapsed += OnTimesUp;
            displayTimer.Start();

            if(_renders.Count > 0 && targetWindow != null)
            {
                for (int i = 0; i < _renders.Count; i++)
                {
                    _renders[i].Render(targetWindow);
                }
            }
        }

        private void OnTimesUp(object sender, ElapsedEventArgs e)
        {
            targetWindow.DisplayDone();
        }
    }
    public class ImagineRequest : IRenderable
    {
        public int x;
        public int y;
        public int width;
        public int height;

        public Image image;

        public int displayTime { get; set; }

        private DisplayWindow targetWindow;
        private PictureBox pictureBox;
        public void Render(DisplayWindow window)
        {
            targetWindow = window;

            pictureBox = new PictureBox();
            pictureBox.Location = new Point(x, y);
            pictureBox.Width = width;
            pictureBox.Height = height;
            pictureBox.Image = image;
            pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;

            targetWindow.AddControl(pictureBox);
        }

        public void SetFromFile(string file)
        {
            if (File.Exists(file))
                image = Image.FromFile(file);
            else
                BotAPI.Debug.LogError("Couldnt find file: " + file);
        }
    }

    public class TextRequest : IRenderable
    {
        public int x;
        public int y;
        public int width;
        public int height;
        public string text;

        public bool autoResize = true;

        private DisplayWindow targetWindow;
        private Label textLabel;
        private Color color;
        public void SetColor(int r, int g, int b)
        {
            color = Color.FromArgb(r, g, b);
        }

        public void Render(DisplayWindow window)
        {
            targetWindow = window;

            textLabel = new Label();
            textLabel.Location = new Point(x, y);
            textLabel.Width = width;
            textLabel.Height = height;
            textLabel.Text = text;
            textLabel.Font = new Font("Ariel", 10);
            textLabel.ForeColor = color; 

            if (autoResize)
                SizeLabelFont(textLabel);

            targetWindow.AddControl(textLabel);
        }

        private void SizeLabelFont(Label lbl)
        {
            // Only bother if there's text.
            string txt = lbl.Text;
            if (txt.Length > 0)
            {
                int best_size = 100;

                // See how much room we have, allowing a bit
                // for the Label's internal margin.
                int wid = lbl.DisplayRectangle.Width - 3;
                int hgt = lbl.DisplayRectangle.Height - 3;

                // Make a Graphics object to measure the text.
                using (Graphics gr = lbl.CreateGraphics())
                {
                    for (int i = 1; i <= 100; i++)
                    {
                        using (Font test_font =
                            new Font(lbl.Font.FontFamily, i))
                        {
                            // See how much space the text would
                            // need, specifying a maximum width.
                            SizeF text_size =
                                gr.MeasureString(txt, test_font);
                            if ((text_size.Width > wid) ||
                                (text_size.Height > hgt))
                            {
                                best_size = i - 1;
                                break;
                            }
                        }
                    }
                }

                // Use that font size.
                lbl.Font = new Font(lbl.Font.FontFamily, best_size);
            }
        }
    }

    public class SoundRequest : IRenderable
    {
        public string file;
        public int soundLength;


        public void Render(DisplayWindow window)
        {
            if (file != string.Empty)
            {
                if (File.Exists(file))
                {
                    SoundPlayer notificationSound = new SoundPlayer(file);
                    notificationSound.Play();
                }
                else
                {
                    BotAPI.Debug.LogError("Couldnt find file: " + file);
                }
            }
        }
    }
}
