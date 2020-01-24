namespace PlexBot
{
    partial class DisplayWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.widnowTick = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // widnowTick
            // 
            this.widnowTick.Enabled = true;
            this.widnowTick.Interval = 250;
            this.widnowTick.Tick += new System.EventHandler(this.widnowTick_Tick);
            // 
            // DisplayWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Lime;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Name = "DisplayWindow";
            this.Text = "DisplayWindow";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Timer widnowTick;
    }
}