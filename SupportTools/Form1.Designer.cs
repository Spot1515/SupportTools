namespace SupportTools
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
            this.DailyTempDirectoryUpdateButton = new System.Windows.Forms.Button();
            this.DailyTempDirectoryTextBox = new System.Windows.Forms.TextBox();
            this.ConsolTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.DailyTempArchiveDirectoryTextBox = new System.Windows.Forms.TextBox();
            this.DailyTempArchiveDirectoryUpdateButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.DailyNotesDirectoryTextBox = new System.Windows.Forms.TextBox();
            this.DailyNotesDirectoryUpdateButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // DailyTempDirectoryUpdateButton
            // 
            this.DailyTempDirectoryUpdateButton.Location = new System.Drawing.Point(557, 7);
            this.DailyTempDirectoryUpdateButton.Name = "DailyTempDirectoryUpdateButton";
            this.DailyTempDirectoryUpdateButton.Size = new System.Drawing.Size(75, 23);
            this.DailyTempDirectoryUpdateButton.TabIndex = 0;
            this.DailyTempDirectoryUpdateButton.Text = "Update";
            this.DailyTempDirectoryUpdateButton.UseVisualStyleBackColor = true;
            // 
            // DailyTempDirectoryTextBox
            // 
            this.DailyTempDirectoryTextBox.Location = new System.Drawing.Point(158, 9);
            this.DailyTempDirectoryTextBox.Name = "DailyTempDirectoryTextBox";
            this.DailyTempDirectoryTextBox.Size = new System.Drawing.Size(393, 20);
            this.DailyTempDirectoryTextBox.TabIndex = 1;
            // 
            // ConsolTextBox
            // 
            this.ConsolTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ConsolTextBox.Location = new System.Drawing.Point(12, 260);
            this.ConsolTextBox.Multiline = true;
            this.ConsolTextBox.Name = "ConsolTextBox";
            this.ConsolTextBox.Size = new System.Drawing.Size(776, 178);
            this.ConsolTextBox.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(47, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Daily Temp Directory";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(144, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Daily Temp Archive Directory";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // DailyTempArchiveDirectoryTextBox
            // 
            this.DailyTempArchiveDirectoryTextBox.Location = new System.Drawing.Point(158, 35);
            this.DailyTempArchiveDirectoryTextBox.Name = "DailyTempArchiveDirectoryTextBox";
            this.DailyTempArchiveDirectoryTextBox.Size = new System.Drawing.Size(393, 20);
            this.DailyTempArchiveDirectoryTextBox.TabIndex = 5;
            // 
            // DailyTempArchiveDirectoryUpdateButton
            // 
            this.DailyTempArchiveDirectoryUpdateButton.Location = new System.Drawing.Point(557, 33);
            this.DailyTempArchiveDirectoryUpdateButton.Name = "DailyTempArchiveDirectoryUpdateButton";
            this.DailyTempArchiveDirectoryUpdateButton.Size = new System.Drawing.Size(75, 23);
            this.DailyTempArchiveDirectoryUpdateButton.TabIndex = 4;
            this.DailyTempArchiveDirectoryUpdateButton.Text = "Update";
            this.DailyTempArchiveDirectoryUpdateButton.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(47, 64);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(106, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Daily Notes Directory";
            // 
            // DailyNotesDirectoryTextBox
            // 
            this.DailyNotesDirectoryTextBox.Location = new System.Drawing.Point(158, 61);
            this.DailyNotesDirectoryTextBox.Name = "DailyNotesDirectoryTextBox";
            this.DailyNotesDirectoryTextBox.Size = new System.Drawing.Size(393, 20);
            this.DailyNotesDirectoryTextBox.TabIndex = 8;
            // 
            // DailyNotesDirectoryUpdateButton
            // 
            this.DailyNotesDirectoryUpdateButton.Location = new System.Drawing.Point(557, 59);
            this.DailyNotesDirectoryUpdateButton.Name = "DailyNotesDirectoryUpdateButton";
            this.DailyNotesDirectoryUpdateButton.Size = new System.Drawing.Size(75, 23);
            this.DailyNotesDirectoryUpdateButton.TabIndex = 7;
            this.DailyNotesDirectoryUpdateButton.Text = "Update";
            this.DailyNotesDirectoryUpdateButton.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.DailyNotesDirectoryTextBox);
            this.Controls.Add(this.DailyNotesDirectoryUpdateButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.DailyTempArchiveDirectoryTextBox);
            this.Controls.Add(this.DailyTempArchiveDirectoryUpdateButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ConsolTextBox);
            this.Controls.Add(this.DailyTempDirectoryTextBox);
            this.Controls.Add(this.DailyTempDirectoryUpdateButton);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button DailyTempDirectoryUpdateButton;
        private System.Windows.Forms.TextBox DailyTempDirectoryTextBox;
        private System.Windows.Forms.TextBox ConsolTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox DailyTempArchiveDirectoryTextBox;
        private System.Windows.Forms.Button DailyTempArchiveDirectoryUpdateButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox DailyNotesDirectoryTextBox;
        private System.Windows.Forms.Button DailyNotesDirectoryUpdateButton;
    }
}

