namespace Mandelbrot
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.refreshButton = new System.Windows.Forms.Button();
            this.iterationsNumeric = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.resXTextBox = new System.Windows.Forms.TextBox();
            this.resYTextBox = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.applyResButton = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.zoomFactNumeric = new System.Windows.Forms.NumericUpDown();
            this.resetButton = new System.Windows.Forms.Button();
            this.useOCLCheckBox = new System.Windows.Forms.CheckBox();
            this.prevButton = new System.Windows.Forms.Button();
            this.oclDeviceCombo = new System.Windows.Forms.ComboBox();
            this.SaveButton = new System.Windows.Forms.Button();
            this.PalletButton = new System.Windows.Forms.Button();
            this.smoothingCheckBox = new System.Windows.Forms.CheckBox();
            this.saveSetButton = new System.Windows.Forms.Button();
            this.loadSetButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.centerXTextBox = new System.Windows.Forms.TextBox();
            this.centerYTextBox = new System.Windows.Forms.TextBox();
            this.radiusTextBox = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.colorStepsNumeric = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.iterationsNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.zoomFactNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.colorStepsNumeric)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox
            // 
            this.pictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox.BackColor = System.Drawing.Color.Gray;
            this.pictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox.InitialImage = null;
            this.pictureBox.Location = new System.Drawing.Point(208, 12);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(794, 718);
            this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox.TabIndex = 0;
            this.pictureBox.TabStop = false;
            this.pictureBox.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox_Paint);
            // 
            // refreshButton
            // 
            this.refreshButton.Location = new System.Drawing.Point(50, 587);
            this.refreshButton.Name = "refreshButton";
            this.refreshButton.Size = new System.Drawing.Size(87, 23);
            this.refreshButton.TabIndex = 1;
            this.refreshButton.Text = "Refresh";
            this.refreshButton.UseVisualStyleBackColor = true;
            this.refreshButton.Click += new System.EventHandler(this.refreshButton_Click);
            // 
            // iterationsNumeric
            // 
            this.iterationsNumeric.Location = new System.Drawing.Point(31, 410);
            this.iterationsNumeric.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.iterationsNumeric.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.iterationsNumeric.Name = "iterationsNumeric";
            this.iterationsNumeric.Size = new System.Drawing.Size(120, 23);
            this.iterationsNumeric.TabIndex = 3;
            this.iterationsNumeric.Value = new decimal(new int[] {
            250,
            0,
            0,
            0});
            this.iterationsNumeric.ValueChanged += new System.EventHandler(this.iterationsNumeric_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(31, 392);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 15);
            this.label2.TabIndex = 6;
            this.label2.Text = "Max Iterations";
            // 
            // resXTextBox
            // 
            this.resXTextBox.Location = new System.Drawing.Point(46, 33);
            this.resXTextBox.Name = "resXTextBox";
            this.resXTextBox.Size = new System.Drawing.Size(47, 23);
            this.resXTextBox.TabIndex = 16;
            this.resXTextBox.Text = "500";
            // 
            // resYTextBox
            // 
            this.resYTextBox.Location = new System.Drawing.Point(104, 33);
            this.resYTextBox.Name = "resYTextBox";
            this.resYTextBox.Size = new System.Drawing.Size(47, 23);
            this.resYTextBox.TabIndex = 17;
            this.resYTextBox.Text = "500";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(46, 15);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(91, 15);
            this.label8.TabIndex = 18;
            this.label8.Text = "Resolution (X,Y)";
            // 
            // applyResButton
            // 
            this.applyResButton.Location = new System.Drawing.Point(62, 62);
            this.applyResButton.Name = "applyResButton";
            this.applyResButton.Size = new System.Drawing.Size(75, 23);
            this.applyResButton.TabIndex = 19;
            this.applyResButton.Text = "Apply";
            this.applyResButton.UseVisualStyleBackColor = true;
            this.applyResButton.Click += new System.EventHandler(this.applyResButton_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(31, 436);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(86, 15);
            this.label9.TabIndex = 21;
            this.label9.Text = "Zoom Amount";
            // 
            // zoomFactNumeric
            // 
            this.zoomFactNumeric.DecimalPlaces = 1;
            this.zoomFactNumeric.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.zoomFactNumeric.Location = new System.Drawing.Point(31, 454);
            this.zoomFactNumeric.Maximum = new decimal(new int[] {
            9,
            0,
            0,
            0});
            this.zoomFactNumeric.Name = "zoomFactNumeric";
            this.zoomFactNumeric.Size = new System.Drawing.Size(120, 23);
            this.zoomFactNumeric.TabIndex = 20;
            this.zoomFactNumeric.Value = new decimal(new int[] {
            35,
            0,
            0,
            65536});
            this.zoomFactNumeric.ValueChanged += new System.EventHandler(this.zoomFactNumeric_ValueChanged);
            // 
            // resetButton
            // 
            this.resetButton.Location = new System.Drawing.Point(50, 558);
            this.resetButton.Name = "resetButton";
            this.resetButton.Size = new System.Drawing.Size(87, 23);
            this.resetButton.TabIndex = 22;
            this.resetButton.Text = "Reset";
            this.resetButton.UseVisualStyleBackColor = true;
            this.resetButton.Click += new System.EventHandler(this.resetButton_Click);
            // 
            // useOCLCheckBox
            // 
            this.useOCLCheckBox.AutoSize = true;
            this.useOCLCheckBox.Location = new System.Drawing.Point(57, 634);
            this.useOCLCheckBox.Name = "useOCLCheckBox";
            this.useOCLCheckBox.Size = new System.Drawing.Size(69, 19);
            this.useOCLCheckBox.TabIndex = 23;
            this.useOCLCheckBox.Text = "OpenCL";
            this.useOCLCheckBox.UseVisualStyleBackColor = true;
            this.useOCLCheckBox.CheckedChanged += new System.EventHandler(this.useOCLCheckBox_CheckedChanged);
            // 
            // prevButton
            // 
            this.prevButton.Location = new System.Drawing.Point(57, 237);
            this.prevButton.Name = "prevButton";
            this.prevButton.Size = new System.Drawing.Size(87, 39);
            this.prevButton.TabIndex = 25;
            this.prevButton.Text = "Back";
            this.prevButton.UseVisualStyleBackColor = true;
            this.prevButton.Click += new System.EventHandler(this.prevButton_Click);
            // 
            // oclDeviceCombo
            // 
            this.oclDeviceCombo.FormattingEnabled = true;
            this.oclDeviceCombo.Location = new System.Drawing.Point(13, 659);
            this.oclDeviceCombo.Name = "oclDeviceCombo";
            this.oclDeviceCombo.Size = new System.Drawing.Size(157, 23);
            this.oclDeviceCombo.TabIndex = 26;
            this.oclDeviceCombo.SelectionChangeCommitted += new System.EventHandler(this.oclDeviceCombo_SelectionChangeCommitted);
            // 
            // SaveButton
            // 
            this.SaveButton.Location = new System.Drawing.Point(51, 707);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(75, 23);
            this.SaveButton.TabIndex = 27;
            this.SaveButton.Text = "Save Image";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // PalletButton
            // 
            this.PalletButton.Location = new System.Drawing.Point(53, 292);
            this.PalletButton.Name = "PalletButton";
            this.PalletButton.Size = new System.Drawing.Size(98, 23);
            this.PalletButton.TabIndex = 29;
            this.PalletButton.Text = "Choose Pallet";
            this.PalletButton.UseVisualStyleBackColor = true;
            this.PalletButton.Click += new System.EventHandler(this.PalletButton_Click);
            // 
            // smoothingCheckBox
            // 
            this.smoothingCheckBox.AutoSize = true;
            this.smoothingCheckBox.Location = new System.Drawing.Point(58, 91);
            this.smoothingCheckBox.Name = "smoothingCheckBox";
            this.smoothingCheckBox.Size = new System.Drawing.Size(85, 19);
            this.smoothingCheckBox.TabIndex = 30;
            this.smoothingCheckBox.Text = "Smoothing";
            this.smoothingCheckBox.UseVisualStyleBackColor = true;
            this.smoothingCheckBox.CheckedChanged += new System.EventHandler(this.smoothingCheckBox_CheckedChanged);
            // 
            // saveSetButton
            // 
            this.saveSetButton.Location = new System.Drawing.Point(28, 519);
            this.saveSetButton.Name = "saveSetButton";
            this.saveSetButton.Size = new System.Drawing.Size(65, 23);
            this.saveSetButton.TabIndex = 31;
            this.saveSetButton.Text = "Save Set";
            this.saveSetButton.UseVisualStyleBackColor = true;
            this.saveSetButton.Click += new System.EventHandler(this.saveSetButton_Click);
            // 
            // loadSetButton
            // 
            this.loadSetButton.Location = new System.Drawing.Point(99, 519);
            this.loadSetButton.Name = "loadSetButton";
            this.loadSetButton.Size = new System.Drawing.Size(65, 23);
            this.loadSetButton.TabIndex = 32;
            this.loadSetButton.Text = "Load Set";
            this.loadSetButton.UseVisualStyleBackColor = true;
            this.loadSetButton.Click += new System.EventHandler(this.loadSetButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 131);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 15);
            this.label1.TabIndex = 33;
            this.label1.Text = "CenterX:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(13, 160);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(52, 15);
            this.label10.TabIndex = 34;
            this.label10.Text = "CenterY:";
            // 
            // centerXTextBox
            // 
            this.centerXTextBox.Location = new System.Drawing.Point(71, 128);
            this.centerXTextBox.Name = "centerXTextBox";
            this.centerXTextBox.Size = new System.Drawing.Size(100, 23);
            this.centerXTextBox.TabIndex = 35;
            this.centerXTextBox.Leave += new System.EventHandler(this.centerXTextBox_Leave);
            // 
            // centerYTextBox
            // 
            this.centerYTextBox.Location = new System.Drawing.Point(71, 157);
            this.centerYTextBox.Name = "centerYTextBox";
            this.centerYTextBox.Size = new System.Drawing.Size(100, 23);
            this.centerYTextBox.TabIndex = 36;
            this.centerYTextBox.Leave += new System.EventHandler(this.centerYTextBox_Leave);
            // 
            // radiusTextBox
            // 
            this.radiusTextBox.Location = new System.Drawing.Point(71, 186);
            this.radiusTextBox.Name = "radiusTextBox";
            this.radiusTextBox.Size = new System.Drawing.Size(100, 23);
            this.radiusTextBox.TabIndex = 38;
            this.radiusTextBox.Leave += new System.EventHandler(this.radiusTextBox_Leave);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(13, 189);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(45, 15);
            this.label11.TabIndex = 37;
            this.label11.Text = "Radius:";
            // 
            // colorStepsNumeric
            // 
            this.colorStepsNumeric.DecimalPlaces = 4;
            this.colorStepsNumeric.Increment = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.colorStepsNumeric.Location = new System.Drawing.Point(89, 333);
            this.colorStepsNumeric.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.colorStepsNumeric.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            262144});
            this.colorStepsNumeric.Name = "colorStepsNumeric";
            this.colorStepsNumeric.Size = new System.Drawing.Size(63, 23);
            this.colorStepsNumeric.TabIndex = 39;
            this.colorStepsNumeric.Value = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.colorStepsNumeric.ValueChanged += new System.EventHandler(this.colorStepsNumeric_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 335);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 15);
            this.label3.TabIndex = 40;
            this.label3.Text = "Color Steps:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1014, 742);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.colorStepsNumeric);
            this.Controls.Add(this.radiusTextBox);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.centerYTextBox);
            this.Controls.Add(this.centerXTextBox);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.loadSetButton);
            this.Controls.Add(this.saveSetButton);
            this.Controls.Add(this.smoothingCheckBox);
            this.Controls.Add(this.PalletButton);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.oclDeviceCombo);
            this.Controls.Add(this.prevButton);
            this.Controls.Add(this.useOCLCheckBox);
            this.Controls.Add(this.resetButton);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.zoomFactNumeric);
            this.Controls.Add(this.applyResButton);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.resYTextBox);
            this.Controls.Add(this.resXTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.iterationsNumeric);
            this.Controls.Add(this.refreshButton);
            this.Controls.Add(this.pictureBox);
            this.DoubleBuffered = true;
            this.KeyPreview = true;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Mandelbrot";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.iterationsNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.zoomFactNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.colorStepsNumeric)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private PictureBox pictureBox;
        private Button refreshButton;
        private NumericUpDown iterationsNumeric;
        private Label label2;
        private TextBox resXTextBox;
        private TextBox resYTextBox;
        private Label label8;
        private Button applyResButton;
        private Label label9;
        private NumericUpDown zoomFactNumeric;
        private Button resetButton;
        private CheckBox useOCLCheckBox;
		private Button prevButton;
		private ComboBox oclDeviceCombo;
		private Button SaveButton;
		private Button PalletButton;
		private CheckBox smoothingCheckBox;
		private Button saveSetButton;
		private Button loadSetButton;
		private Label label1;
		private Label label10;
		private TextBox centerXTextBox;
		private TextBox centerYTextBox;
		private TextBox radiusTextBox;
		private Label label11;
		private NumericUpDown colorStepsNumeric;
		private Label label3;
	}
}