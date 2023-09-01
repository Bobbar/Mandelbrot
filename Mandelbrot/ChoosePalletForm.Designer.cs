namespace Mandelbrot
{
	partial class ChoosePalletForm
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
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.moveDownButton = new System.Windows.Forms.Button();
			this.colorView = new System.Windows.Forms.ListView();
			this.moveUpButton = new System.Windows.Forms.Button();
			this.OKButton = new System.Windows.Forms.Button();
			this.CancelButton = new System.Windows.Forms.Button();
			this.defaultButton = new System.Windows.Forms.Button();
			this.addButton = new System.Windows.Forms.Button();
			this.deleteButton = new System.Windows.Forms.Button();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.moveDownButton);
			this.groupBox1.Controls.Add(this.colorView);
			this.groupBox1.Controls.Add(this.moveUpButton);
			this.groupBox1.Location = new System.Drawing.Point(36, 21);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(264, 363);
			this.groupBox1.TabIndex = 1;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Colors";
			// 
			// moveDownButton
			// 
			this.moveDownButton.Location = new System.Drawing.Point(239, 170);
			this.moveDownButton.Name = "moveDownButton";
			this.moveDownButton.Size = new System.Drawing.Size(21, 23);
			this.moveDownButton.TabIndex = 9;
			this.moveDownButton.Text = "↓";
			this.moveDownButton.UseVisualStyleBackColor = true;
			this.moveDownButton.Click += new System.EventHandler(this.moveDownButton_Click);
			// 
			// colorView
			// 
			this.colorView.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
			this.colorView.HideSelection = true;
			this.colorView.Location = new System.Drawing.Point(6, 22);
			this.colorView.Name = "colorView";
			this.colorView.Size = new System.Drawing.Size(227, 328);
			this.colorView.TabIndex = 7;
			this.colorView.UseCompatibleStateImageBehavior = false;
			this.colorView.View = System.Windows.Forms.View.List;
			this.colorView.ItemActivate += new System.EventHandler(this.colorView_ItemActivate);
			this.colorView.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.colorView_ItemSelectionChanged);
			// 
			// moveUpButton
			// 
			this.moveUpButton.Location = new System.Drawing.Point(239, 141);
			this.moveUpButton.Name = "moveUpButton";
			this.moveUpButton.Size = new System.Drawing.Size(21, 23);
			this.moveUpButton.TabIndex = 8;
			this.moveUpButton.Text = "↑";
			this.moveUpButton.UseVisualStyleBackColor = true;
			this.moveUpButton.Click += new System.EventHandler(this.moveUpButton_Click);
			// 
			// OKButton
			// 
			this.OKButton.Location = new System.Drawing.Point(238, 435);
			this.OKButton.Name = "OKButton";
			this.OKButton.Size = new System.Drawing.Size(75, 23);
			this.OKButton.TabIndex = 2;
			this.OKButton.Text = "OK";
			this.OKButton.UseVisualStyleBackColor = true;
			this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
			// 
			// CancelButton
			// 
			this.CancelButton.Location = new System.Drawing.Point(26, 435);
			this.CancelButton.Name = "CancelButton";
			this.CancelButton.Size = new System.Drawing.Size(75, 23);
			this.CancelButton.TabIndex = 3;
			this.CancelButton.Text = "Cancel";
			this.CancelButton.UseVisualStyleBackColor = true;
			this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
			// 
			// defaultButton
			// 
			this.defaultButton.Location = new System.Drawing.Point(221, 390);
			this.defaultButton.Name = "defaultButton";
			this.defaultButton.Size = new System.Drawing.Size(75, 23);
			this.defaultButton.TabIndex = 4;
			this.defaultButton.Text = "Default";
			this.defaultButton.UseVisualStyleBackColor = true;
			this.defaultButton.Click += new System.EventHandler(this.defaultButton_Click);
			// 
			// addButton
			// 
			this.addButton.Location = new System.Drawing.Point(36, 387);
			this.addButton.Name = "addButton";
			this.addButton.Size = new System.Drawing.Size(52, 23);
			this.addButton.TabIndex = 6;
			this.addButton.Text = "Add";
			this.addButton.UseVisualStyleBackColor = true;
			this.addButton.Click += new System.EventHandler(this.addButton_Click);
			// 
			// deleteButton
			// 
			this.deleteButton.Location = new System.Drawing.Point(116, 387);
			this.deleteButton.Name = "deleteButton";
			this.deleteButton.Size = new System.Drawing.Size(75, 23);
			this.deleteButton.TabIndex = 7;
			this.deleteButton.Text = "Delete";
			this.deleteButton.UseVisualStyleBackColor = true;
			this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
			// 
			// ChoosePalletForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(338, 470);
			this.Controls.Add(this.deleteButton);
			this.Controls.Add(this.addButton);
			this.Controls.Add(this.defaultButton);
			this.Controls.Add(this.CancelButton);
			this.Controls.Add(this.OKButton);
			this.Controls.Add(this.groupBox1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "ChoosePalletForm";
			this.Text = "ChoosePalletForm";
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion
		private GroupBox groupBox1;
		private Button OKButton;
		private Button CancelButton;
		private Button defaultButton;
		private Button addButton;
		private ListView colorView;
		private Button moveUpButton;
		private Button moveDownButton;
		private Button deleteButton;
	}
}