namespace FileCompare
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
            splitContainer1 = new SplitContainer();
            lblAppName = new Label();
            txtLeftDir = new TextBox();
            txtRightDir = new TextBox();
            btnLeftDir = new Button();
            btnRightDir = new Button();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            SuspendLayout();
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(0, 0);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(btnLeftDir);
            splitContainer1.Panel1.Controls.Add(txtLeftDir);
            splitContainer1.Panel1.Controls.Add(lblAppName);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(btnRightDir);
            splitContainer1.Panel2.Controls.Add(txtRightDir);
            splitContainer1.Size = new Size(983, 499);
            splitContainer1.SplitterDistance = 485;
            splitContainer1.TabIndex = 0;
            // 
            // lblAppName
            // 
            lblAppName.AutoSize = true;
            lblAppName.Font = new Font("맑은 고딕", 21.75F, FontStyle.Bold, GraphicsUnit.Point, 129);
            lblAppName.Location = new Point(12, 20);
            lblAppName.Name = "lblAppName";
            lblAppName.Size = new Size(199, 40);
            lblAppName.TabIndex = 0;
            lblAppName.Text = "File Compare";
            // 
            // txtLeftDir
            // 
            txtLeftDir.Location = new Point(12, 102);
            txtLeftDir.Name = "txtLeftDir";
            txtLeftDir.Size = new Size(322, 23);
            txtLeftDir.TabIndex = 1;
            // 
            // txtRightDir
            // 
            txtRightDir.Location = new Point(16, 102);
            txtRightDir.Name = "txtRightDir";
            txtRightDir.Size = new Size(322, 23);
            txtRightDir.TabIndex = 1;
            // 
            // btnLeftDir
            // 
            btnLeftDir.Location = new Point(370, 101);
            btnLeftDir.Name = "btnLeftDir";
            btnLeftDir.Size = new Size(84, 23);
            btnLeftDir.TabIndex = 2;
            btnLeftDir.Text = "폴더선택";
            btnLeftDir.UseVisualStyleBackColor = true;
            // 
            // btnRightDir
            // 
            btnRightDir.Location = new Point(376, 101);
            btnRightDir.Name = "btnRightDir";
            btnRightDir.Size = new Size(84, 23);
            btnRightDir.TabIndex = 3;
            btnRightDir.Text = "폴더선택";
            btnRightDir.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(983, 499);
            Controls.Add(splitContainer1);
            Name = "Form1";
            Text = "File Compare v1.0";
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel1.PerformLayout();
            splitContainer1.Panel2.ResumeLayout(false);
            splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private SplitContainer splitContainer1;
        private Button btnLeftDir;
        private TextBox txtLeftDir;
        private Label lblAppName;
        private TextBox txtRightDir;
        private Button btnRightDir;
    }
}
