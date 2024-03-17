namespace ImageDownsizer
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
            btnSelect = new Button();
            txtBox = new TextBox();
            lblWritingPercent = new Label();
            lblSelected = new Label();
            btnSingleThread = new Button();
            btnMultiThread = new Button();
            SuspendLayout();
            // 
            // btnSelect
            // 
            btnSelect.Location = new Point(59, 109);
            btnSelect.Name = "btnSelect";
            btnSelect.Size = new Size(122, 23);
            btnSelect.TabIndex = 0;
            btnSelect.Text = "Select image";
            btnSelect.UseVisualStyleBackColor = true;
            btnSelect.Click += btnSelect_Click;
            // 
            // txtBox
            // 
            txtBox.Location = new Point(140, 49);
            txtBox.Name = "txtBox";
            txtBox.Size = new Size(105, 23);
            txtBox.TabIndex = 1;
            txtBox.Text = "100";
            // 
            // lblWritingPercent
            // 
            lblWritingPercent.AutoSize = true;
            lblWritingPercent.Location = new Point(12, 52);
            lblWritingPercent.Name = "lblWritingPercent";
            lblWritingPercent.Size = new Size(117, 15);
            lblWritingPercent.TabIndex = 2;
            lblWritingPercent.Text = "Enter value up to 100";
            // 
            // lblSelected
            // 
            lblSelected.AutoSize = true;
            lblSelected.Location = new Point(58, 82);
            lblSelected.Name = "lblSelected";
            lblSelected.Size = new Size(0, 15);
            lblSelected.TabIndex = 3;
            // 
            // btnSingleThread
            // 
            btnSingleThread.Enabled = false;
            btnSingleThread.Location = new Point(26, 160);
            btnSingleThread.Name = "btnSingleThread";
            btnSingleThread.Size = new Size(167, 23);
            btnSingleThread.TabIndex = 4;
            btnSingleThread.Text = "Start Single Thread";
            btnSingleThread.UseVisualStyleBackColor = true;
            btnSingleThread.Click += btnSingleThread_Click;
            // 
            // btnMultiThread
            // 
            btnMultiThread.Enabled = false;
            btnMultiThread.Location = new Point(24, 201);
            btnMultiThread.Name = "btnMultiThread";
            btnMultiThread.Size = new Size(169, 23);
            btnMultiThread.TabIndex = 5;
            btnMultiThread.Text = "Start Multiple Threads";
            btnMultiThread.UseVisualStyleBackColor = true;
            btnMultiThread.Click += btnMultiThread_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnMultiThread);
            Controls.Add(btnSingleThread);
            Controls.Add(lblSelected);
            Controls.Add(lblWritingPercent);
            Controls.Add(txtBox);
            Controls.Add(btnSelect);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnSelect;
        private TextBox txtBox;
        private Label lblWritingPercent;
        private Label lblSelected;
        private Button btnSingleThread;
        private Button btnMultiThread;
    }
}
