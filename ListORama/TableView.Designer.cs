namespace ListORama
{
	partial class TableView
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
			this.dataGridView1 = new System.Windows.Forms.DataGridView();
			this.SavedIndex = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Type = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Unitverse = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Network = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Channel = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Color = new System.Windows.Forms.DataGridViewTextBoxColumn();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
			this.SuspendLayout();
			// 
			// dataGridView1
			// 
			this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SavedIndex,
            this.Name,
            this.Type,
            this.Unitverse,
            this.Network,
            this.Channel,
            this.Color});
			this.dataGridView1.Location = new System.Drawing.Point(59, 75);
			this.dataGridView1.Name = "dataGridView1";
			this.dataGridView1.Size = new System.Drawing.Size(997, 309);
			this.dataGridView1.TabIndex = 0;
			// 
			// SavedIndex
			// 
			this.SavedIndex.HeaderText = "SavedIndex";
			this.SavedIndex.Name = "SavedIndex";
			// 
			// Name
			// 
			this.Name.HeaderText = "Name";
			this.Name.Name = "Name";
			// 
			// Type
			// 
			this.Type.HeaderText = "Type";
			this.Type.Name = "Type";
			// 
			// Unitverse
			// 
			this.Unitverse.HeaderText = "Unit-verse";
			this.Unitverse.Name = "Unitverse";
			// 
			// Network
			// 
			this.Network.HeaderText = "Network";
			this.Network.Name = "Network";
			// 
			// Channel
			// 
			this.Channel.HeaderText = "Channel";
			this.Channel.Name = "Channel";
			// 
			// Color
			// 
			this.Color.HeaderText = "Color";
			this.Color.Name = "Color";
			// 
			// TableView
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1297, 450);
			this.Controls.Add(this.dataGridView1);
			//this.Name = "TableView";
			this.Text = "TableView";
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.DataGridView dataGridView1;
		private System.Windows.Forms.DataGridViewTextBoxColumn SavedIndex;
		private System.Windows.Forms.DataGridViewTextBoxColumn Name;
		private System.Windows.Forms.DataGridViewTextBoxColumn Type;
		private System.Windows.Forms.DataGridViewTextBoxColumn Unitverse;
		private System.Windows.Forms.DataGridViewTextBoxColumn Network;
		private System.Windows.Forms.DataGridViewTextBoxColumn Channel;
		private System.Windows.Forms.DataGridViewTextBoxColumn Color;
	}
}