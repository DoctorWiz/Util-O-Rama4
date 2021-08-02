namespace loraxe
{
    partial class lorForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(lorForm));
            this.fileTextBox = new System.Windows.Forms.TextBox();
            this.BrowseButton = new System.Windows.Forms.Button();
            this.fileLabel = new System.Windows.Forms.Label();
            this.channelsLabel = new System.Windows.Forms.Label();
            this.channelsListBox = new System.Windows.Forms.ListBox();
            this.deleteChannelButton = new System.Windows.Forms.Button();
            this.addChannelTextBox = new System.Windows.Forms.TextBox();
            this.addChannelButton = new System.Windows.Forms.Button();
            this.lorilizeButton = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // fileTextBox
            // 
            this.fileTextBox.Enabled = false;
            this.fileTextBox.Location = new System.Drawing.Point(18, 192);
            this.fileTextBox.Name = "fileTextBox";
            this.fileTextBox.Size = new System.Drawing.Size(223, 20);
            this.fileTextBox.TabIndex = 0;
            // 
            // BrowseButton
            // 
            this.BrowseButton.Location = new System.Drawing.Point(245, 192);
            this.BrowseButton.Name = "BrowseButton";
            this.BrowseButton.Size = new System.Drawing.Size(26, 20);
            this.BrowseButton.TabIndex = 1;
            this.BrowseButton.Text = "...";
            this.BrowseButton.UseVisualStyleBackColor = true;
            this.BrowseButton.Click += new System.EventHandler(this.BrowseButton_Click);
            // 
            // fileLabel
            // 
            this.fileLabel.AutoSize = true;
            this.fileLabel.Location = new System.Drawing.Point(19, 176);
            this.fileLabel.Name = "fileLabel";
            this.fileLabel.Size = new System.Drawing.Size(26, 13);
            this.fileLabel.TabIndex = 2;
            this.fileLabel.Text = "File:";
            // 
            // channelsLabel
            // 
            this.channelsLabel.AutoSize = true;
            this.channelsLabel.Location = new System.Drawing.Point(15, 18);
            this.channelsLabel.Name = "channelsLabel";
            this.channelsLabel.Size = new System.Drawing.Size(84, 13);
            this.channelsLabel.TabIndex = 3;
            this.channelsLabel.Text = "Channels to Axe";
            // 
            // channelsListBox
            // 
            this.channelsListBox.FormattingEnabled = true;
            this.channelsListBox.Location = new System.Drawing.Point(16, 37);
            this.channelsListBox.Name = "channelsListBox";
            this.channelsListBox.Size = new System.Drawing.Size(165, 95);
            this.channelsListBox.TabIndex = 4;
            this.channelsListBox.SelectedIndexChanged += new System.EventHandler(this.channelsListBox_SelectedIndexChanged);
            // 
            // deleteChannelButton
            // 
            this.deleteChannelButton.Enabled = false;
            this.deleteChannelButton.Location = new System.Drawing.Point(191, 45);
            this.deleteChannelButton.Name = "deleteChannelButton";
            this.deleteChannelButton.Size = new System.Drawing.Size(28, 22);
            this.deleteChannelButton.TabIndex = 5;
            this.deleteChannelButton.Text = "X";
            this.deleteChannelButton.UseVisualStyleBackColor = true;
            this.deleteChannelButton.Click += new System.EventHandler(this.deleteChannelButton_Click);
            // 
            // addChannelTextBox
            // 
            this.addChannelTextBox.Location = new System.Drawing.Point(17, 138);
            this.addChannelTextBox.Name = "addChannelTextBox";
            this.addChannelTextBox.Size = new System.Drawing.Size(62, 20);
            this.addChannelTextBox.TabIndex = 6;
            this.addChannelTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.addChannelTextBox_KeyPress);
            // 
            // addChannelButton
            // 
            this.addChannelButton.Location = new System.Drawing.Point(86, 139);
            this.addChannelButton.Name = "addChannelButton";
            this.addChannelButton.Size = new System.Drawing.Size(42, 19);
            this.addChannelButton.TabIndex = 7;
            this.addChannelButton.Text = "Add";
            this.addChannelButton.UseVisualStyleBackColor = true;
            this.addChannelButton.TextChanged += new System.EventHandler(this.addChannelButton_TextChanged);
            this.addChannelButton.Click += new System.EventHandler(this.addChannelButton_Click);
            // 
            // lorilizeButton
            // 
            this.lorilizeButton.Location = new System.Drawing.Point(75, 218);
            this.lorilizeButton.Name = "lorilizeButton";
            this.lorilizeButton.Size = new System.Drawing.Size(139, 36);
            this.lorilizeButton.TabIndex = 8;
            this.lorilizeButton.Text = "Axe!";
            this.lorilizeButton.UseVisualStyleBackColor = true;
            this.lorilizeButton.Click += new System.EventHandler(this.lorilizeButton_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.DefaultExt = "lms";
            this.openFileDialog.Filter = "Light-O-Rama Sequences (*.lms)|*.lms|Light-O-Rama Animations (*.las)|*.las|All Fi" +
    "les (*.*)|*.*";
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 262);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(288, 22);
            this.statusStrip.TabIndex = 9;
            this.statusStrip.Text = "statusStrip1";
            // 
            // statusLabel
            // 
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(139, 17);
            this.statusLabel.Text = "Select file, then click Axe!";
            // 
            // lorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(288, 284);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.lorilizeButton);
            this.Controls.Add(this.addChannelButton);
            this.Controls.Add(this.addChannelTextBox);
            this.Controls.Add(this.deleteChannelButton);
            this.Controls.Add(this.channelsListBox);
            this.Controls.Add(this.channelsLabel);
            this.Controls.Add(this.fileLabel);
            this.Controls.Add(this.BrowseButton);
            this.Controls.Add(this.fileTextBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "lorForm";
            this.Text = "The Lor-Axe";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.lorForm_FormClosed);
            this.Load += new System.EventHandler(this.lorForm_Load);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox fileTextBox;
        private System.Windows.Forms.Button BrowseButton;
        private System.Windows.Forms.Label fileLabel;
        private System.Windows.Forms.Label channelsLabel;
        private System.Windows.Forms.ListBox channelsListBox;
        private System.Windows.Forms.Button deleteChannelButton;
        private System.Windows.Forms.TextBox addChannelTextBox;
        private System.Windows.Forms.Button addChannelButton;
        private System.Windows.Forms.Button lorilizeButton;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel;
    }
}

