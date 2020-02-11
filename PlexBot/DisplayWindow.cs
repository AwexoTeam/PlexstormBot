using PlexBot.Definations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PlexBot
{
    public partial class DisplayWindow : Form
    {
        public delegate void _OnWindowTick();
        public event _OnWindowTick OnWindowTick;

        public Queue<Control> controlQueue;
        public Queue<RenderRequest> displayQueue;
        public PictureBox PictureBox;
        public Label TextLabel;
        public bool needCleaning = false;

        public bool isRendering = false;
        public DisplayWindow()
        {
            InitializeComponent();
        }

        public void AddControl(Control ctrl)
        {
            controlQueue.Enqueue(ctrl);
        }

        public void Initialize()
        {
            displayQueue = new Queue<RenderRequest>();
            controlQueue = new Queue<Control>();
        }

        public void DisplayDone()
        {
            isRendering = false;
            needCleaning = true;
        }

        private void UnqueueRender()
        {
            if (displayQueue.Count > 0 && !isRendering)
            {
                RenderRequest request = displayQueue.Dequeue();
                request.Render(this);
                isRendering = true;
            }
        }

        public void EnqueueRenders(RenderRequest request)
        {
            displayQueue.Enqueue(request);
        }

        private void widnowTick_Tick(object sender, EventArgs e)
        {
            OnWindowTick?.Invoke();
            if (needCleaning)
            {
                //TODO: do proper clean up.
                Controls.Clear();
                needCleaning = false;
            }
            else { UnqueueRender(); }
            if(controlQueue.Count > 0)
            {
                Control ctrl = controlQueue.Dequeue();
                Controls.Add(ctrl);
                ctrl.Show();
                ctrl.Refresh();
            }
        }

        private void DisplayWindow_Load(object sender, EventArgs e)
        {

        }
    }
}
