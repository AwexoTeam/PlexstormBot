namespace PlexBot
{
    partial class Form1
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
            this.btnConnect = new System.Windows.Forms.Button();
            this.channelTB = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.emailTB = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.passwordTB = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.xResTB = new System.Windows.Forms.TextBox();
            this.yResTB = new System.Windows.Forms.TextBox();
            this.errorLabel = new System.Windows.Forms.Label();
            this.AutoSave = new System.Windows.Forms.Timer(this.components);
            this.testFollowBTN = new System.Windows.Forms.Button();
            this.testSubBtn = new System.Windows.Forms.Button();
            this.testTipBTN = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(89, 32);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(75, 23);
            this.btnConnect.TabIndex = 0;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // channelTB
            // 
            this.channelTB.Location = new System.Drawing.Point(89, 6);
            this.channelTB.Name = "channelTB";
            this.channelTB.Size = new System.Drawing.Size(162, 20);
            this.channelTB.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Channel Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 212);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Email";
            // 
            // emailTB
            // 
            this.emailTB.Location = new System.Drawing.Point(95, 205);
            this.emailTB.Name = "emailTB";
            this.emailTB.Size = new System.Drawing.Size(162, 20);
            this.emailTB.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 234);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Password";
            // 
            // passwordTB
            // 
            this.passwordTB.Location = new System.Drawing.Point(95, 231);
            this.passwordTB.Name = "passwordTB";
            this.passwordTB.Size = new System.Drawing.Size(162, 20);
            this.passwordTB.TabIndex = 6;
            this.passwordTB.UseSystemPasswordChar = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 260);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Resolution";
            // 
            // xResTB
            // 
            this.xResTB.Location = new System.Drawing.Point(95, 257);
            this.xResTB.Name = "xResTB";
            this.xResTB.Size = new System.Drawing.Size(73, 20);
            this.xResTB.TabIndex = 8;
            // 
            // yResTB
            // 
            this.yResTB.Location = new System.Drawing.Point(184, 257);
            this.yResTB.Name = "yResTB";
            this.yResTB.Size = new System.Drawing.Size(73, 20);
            this.yResTB.TabIndex = 10;
            // 
            // errorLabel
            // 
            this.errorLabel.AutoSize = true;
            this.errorLabel.Location = new System.Drawing.Point(3, 150);
            this.errorLabel.Name = "errorLabel";
            this.errorLabel.Size = new System.Drawing.Size(30, 13);
            this.errorLabel.TabIndex = 11;
            this.errorLabel.Text = "ERR";
            // 
            // AutoSave
            // 
            this.AutoSave.Enabled = true;
            this.AutoSave.Interval = 250;
            this.AutoSave.Tick += new System.EventHandler(this.AutoSave_Tick);
            // 
            // testFollowBTN
            // 
            this.testFollowBTN.Location = new System.Drawing.Point(434, 32);
            this.testFollowBTN.Name = "testFollowBTN";
            this.testFollowBTN.Size = new System.Drawing.Size(75, 23);
            this.testFollowBTN.TabIndex = 12;
            this.testFollowBTN.Text = "Test Follow";
            this.testFollowBTN.UseVisualStyleBackColor = true;
            this.testFollowBTN.Click += new System.EventHandler(this.testFollowBTN_Click);
            // 
            // testSubBtn
            // 
            this.testSubBtn.Location = new System.Drawing.Point(8, 32);
            this.testSubBtn.Name = "testSubBtn";
            this.testSubBtn.Size = new System.Drawing.Size(75, 23);
            this.testSubBtn.TabIndex = 13;
            this.testSubBtn.Text = "Test Sub";
            this.testSubBtn.UseVisualStyleBackColor = true;
            this.testSubBtn.Click += new System.EventHandler(this.testSubBtn_Click);
            // 
            // testTipBTN
            // 
            this.testTipBTN.Location = new System.Drawing.Point(176, 32);
            this.testTipBTN.Name = "testTipBTN";
            this.testTipBTN.Size = new System.Drawing.Size(75, 23);
            this.testTipBTN.TabIndex = 14;
            this.testTipBTN.Text = "Test Tip";
            this.testTipBTN.UseVisualStyleBackColor = true;
            this.testTipBTN.Click += new System.EventHandler(this.testTipBTN_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(267, 65);
            this.Controls.Add(this.testTipBTN);
            this.Controls.Add(this.testSubBtn);
            this.Controls.Add(this.testFollowBTN);
            this.Controls.Add(this.errorLabel);
            this.Controls.Add(this.yResTB);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.xResTB);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.passwordTB);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.emailTB);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.channelTB);
            this.Controls.Add(this.btnConnect);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.TextBox channelTB;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox emailTB;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox passwordTB;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox xResTB;
        private System.Windows.Forms.TextBox yResTB;
        private System.Windows.Forms.Label errorLabel;
        private System.Windows.Forms.Timer AutoSave;
        private System.Windows.Forms.Button testFollowBTN;
        private System.Windows.Forms.Button testSubBtn;
        private System.Windows.Forms.Button testTipBTN;
    }
}

