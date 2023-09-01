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
            this.cValueNumeric = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.xtMinNumeric = new System.Windows.Forms.NumericUpDown();
            this.xtMaxNumeric = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.ytMaxNumeric = new System.Windows.Forms.NumericUpDown();
            this.ytMinNumeric = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
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
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.iterationsNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cValueNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtMinNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtMaxNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ytMaxNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ytMinNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.zoomFactNumeric)).BeginInit();
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
            this.pictureBox.Size = new System.Drawing.Size(794, 609);
            this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox.TabIndex = 0;
            this.pictureBox.TabStop = false;
            this.pictureBox.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox_Paint);
            // 
            // refreshButton
            // 
            this.refreshButton.Location = new System.Drawing.Point(50, 505);
            this.refreshButton.Name = "refreshButton";
            this.refreshButton.Size = new System.Drawing.Size(87, 29);
            this.refreshButton.TabIndex = 1;
            this.refreshButton.Text = "Refresh";
            this.refreshButton.UseVisualStyleBackColor = true;
            this.refreshButton.Click += new System.EventHandler(this.refreshButton_Click);
            // 
            // iterationsNumeric
            // 
            this.iterationsNumeric.Location = new System.Drawing.Point(46, 336);
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
            // cValueNumeric
            // 
            this.cValueNumeric.DecimalPlaces = 1;
            this.cValueNumeric.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.cValueNumeric.Location = new System.Drawing.Point(46, 380);
            this.cValueNumeric.Maximum = new decimal(new int[] {
            3000,
            0,
            0,
            0});
            this.cValueNumeric.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.cValueNumeric.Name = "cValueNumeric";
            this.cValueNumeric.Size = new System.Drawing.Size(120, 23);
            this.cValueNumeric.TabIndex = 4;
            this.cValueNumeric.Value = new decimal(new int[] {
            20,
            0,
            0,
            65536});
            this.cValueNumeric.ValueChanged += new System.EventHandler(this.cValueNumeric_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(46, 318);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 15);
            this.label2.TabIndex = 6;
            this.label2.Text = "Max Iterations";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(46, 362);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 15);
            this.label3.TabIndex = 7;
            this.label3.Text = "C Value?";
            // 
            // xtMinNumeric
            // 
            this.xtMinNumeric.DecimalPlaces = 2;
            this.xtMinNumeric.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.xtMinNumeric.Location = new System.Drawing.Point(45, 111);
            this.xtMinNumeric.Maximum = new decimal(new int[] {
            100,
            0,
            0,
            65536});
            this.xtMinNumeric.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147418112});
            this.xtMinNumeric.Name = "xtMinNumeric";
            this.xtMinNumeric.Size = new System.Drawing.Size(47, 23);
            this.xtMinNumeric.TabIndex = 8;
            this.xtMinNumeric.ValueChanged += new System.EventHandler(this.xtMinNumeric_ValueChanged);
            // 
            // xtMaxNumeric
            // 
            this.xtMaxNumeric.DecimalPlaces = 2;
            this.xtMaxNumeric.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.xtMaxNumeric.Location = new System.Drawing.Point(105, 111);
            this.xtMaxNumeric.Maximum = new decimal(new int[] {
            100,
            0,
            0,
            65536});
            this.xtMaxNumeric.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147418112});
            this.xtMaxNumeric.Name = "xtMaxNumeric";
            this.xtMaxNumeric.Size = new System.Drawing.Size(47, 23);
            this.xtMaxNumeric.TabIndex = 9;
            this.xtMaxNumeric.ValueChanged += new System.EventHandler(this.xtMaxNumeric_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(46, 93);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(33, 15);
            this.label4.TabIndex = 10;
            this.label4.Text = "Tmin";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(93, 93);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 15);
            this.label5.TabIndex = 11;
            this.label5.Text = "Tmax";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(22, 113);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(17, 15);
            this.label6.TabIndex = 12;
            this.label6.Text = "X:";
            // 
            // ytMaxNumeric
            // 
            this.ytMaxNumeric.DecimalPlaces = 2;
            this.ytMaxNumeric.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.ytMaxNumeric.Location = new System.Drawing.Point(105, 140);
            this.ytMaxNumeric.Maximum = new decimal(new int[] {
            100,
            0,
            0,
            65536});
            this.ytMaxNumeric.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147418112});
            this.ytMaxNumeric.Name = "ytMaxNumeric";
            this.ytMaxNumeric.Size = new System.Drawing.Size(46, 23);
            this.ytMaxNumeric.TabIndex = 14;
            this.ytMaxNumeric.ValueChanged += new System.EventHandler(this.ytMaxNumeric_ValueChanged);
            // 
            // ytMinNumeric
            // 
            this.ytMinNumeric.DecimalPlaces = 2;
            this.ytMinNumeric.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.ytMinNumeric.Location = new System.Drawing.Point(46, 140);
            this.ytMinNumeric.Maximum = new decimal(new int[] {
            100,
            0,
            0,
            65536});
            this.ytMinNumeric.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147418112});
            this.ytMinNumeric.Name = "ytMinNumeric";
            this.ytMinNumeric.Size = new System.Drawing.Size(47, 23);
            this.ytMinNumeric.TabIndex = 13;
            this.ytMinNumeric.ValueChanged += new System.EventHandler(this.ytMinNumeric_ValueChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(22, 142);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(17, 15);
            this.label7.TabIndex = 15;
            this.label7.Text = "Y:";
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
            this.label9.Location = new System.Drawing.Point(46, 406);
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
            this.zoomFactNumeric.Location = new System.Drawing.Point(46, 424);
            this.zoomFactNumeric.Maximum = new decimal(new int[] {
            9,
            0,
            0,
            0});
            this.zoomFactNumeric.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.zoomFactNumeric.Name = "zoomFactNumeric";
            this.zoomFactNumeric.Size = new System.Drawing.Size(120, 23);
            this.zoomFactNumeric.TabIndex = 20;
            this.zoomFactNumeric.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.zoomFactNumeric.ValueChanged += new System.EventHandler(this.zoomFactNumeric_ValueChanged);
            // 
            // resetButton
            // 
            this.resetButton.Location = new System.Drawing.Point(50, 457);
            this.resetButton.Name = "resetButton";
            this.resetButton.Size = new System.Drawing.Size(87, 29);
            this.resetButton.TabIndex = 22;
            this.resetButton.Text = "Reset";
            this.resetButton.UseVisualStyleBackColor = true;
            this.resetButton.Click += new System.EventHandler(this.resetButton_Click);
            // 
            // useOCLCheckBox
            // 
            this.useOCLCheckBox.AutoSize = true;
            this.useOCLCheckBox.Location = new System.Drawing.Point(58, 554);
            this.useOCLCheckBox.Name = "useOCLCheckBox";
            this.useOCLCheckBox.Size = new System.Drawing.Size(69, 19);
            this.useOCLCheckBox.TabIndex = 23;
            this.useOCLCheckBox.Text = "OpenCL";
            this.useOCLCheckBox.UseVisualStyleBackColor = true;
            this.useOCLCheckBox.CheckedChanged += new System.EventHandler(this.useOCLCheckBox_CheckedChanged);
            // 
            // prevButton
            // 
            this.prevButton.Location = new System.Drawing.Point(50, 208);
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
            this.oclDeviceCombo.Location = new System.Drawing.Point(14, 579);
            this.oclDeviceCombo.Name = "oclDeviceCombo";
            this.oclDeviceCombo.Size = new System.Drawing.Size(157, 23);
            this.oclDeviceCombo.TabIndex = 26;
            this.oclDeviceCombo.SelectionChangeCommitted += new System.EventHandler(this.oclDeviceCombo_SelectionChangeCommitted);
            // 
            // SaveButton
            // 
            this.SaveButton.Location = new System.Drawing.Point(53, 277);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(75, 23);
            this.SaveButton.TabIndex = 27;
            this.SaveButton.Text = "Save Image";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1014, 633);
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
            this.Controls.Add(this.label7);
            this.Controls.Add(this.ytMaxNumeric);
            this.Controls.Add(this.ytMinNumeric);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.xtMaxNumeric);
            this.Controls.Add(this.xtMinNumeric);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cValueNumeric);
            this.Controls.Add(this.iterationsNumeric);
            this.Controls.Add(this.refreshButton);
            this.Controls.Add(this.pictureBox);
            this.DoubleBuffered = true;
            this.KeyPreview = true;
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.iterationsNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cValueNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtMinNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtMaxNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ytMaxNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ytMinNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.zoomFactNumeric)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private PictureBox pictureBox;
        private Button refreshButton;
        private NumericUpDown iterationsNumeric;
        private NumericUpDown cValueNumeric;
        private Label label2;
        private Label label3;
        private NumericUpDown xtMinNumeric;
        private NumericUpDown xtMaxNumeric;
        private Label label4;
        private Label label5;
        private Label label6;
        private NumericUpDown ytMaxNumeric;
        private NumericUpDown ytMinNumeric;
        private Label label7;
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
	}
}