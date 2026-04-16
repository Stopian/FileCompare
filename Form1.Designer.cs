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
            pnlLeftBottom = new Panel();
            lvwLeftDir = new ListView();
            columnHeader1 = new ColumnHeader();
            columnHeader2 = new ColumnHeader();
            columnHeader3 = new ColumnHeader();
            columnHeader7 = new ColumnHeader();
            pnlLeftMiddle = new Panel();
            btnLeftDir = new Button();
            txtLeftDir = new TextBox();
            pnlLeftTop = new Panel();
            lblAppName = new Label();
            btnCopyFromLeft = new Button();
            pnlRightBottom = new Panel();
            lvwRightDir = new ListView();
            columnHeader4 = new ColumnHeader();
            columnHeader5 = new ColumnHeader();
            columnHeader6 = new ColumnHeader();
            columnHeader8 = new ColumnHeader();
            pnlRightMiddle = new Panel();
            btnRightDir = new Button();
            txtRightDir = new TextBox();
            pnlRightTop = new Panel();
            btnCopyFromRight = new Button();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            pnlLeftBottom.SuspendLayout();
            pnlLeftMiddle.SuspendLayout();
            pnlLeftTop.SuspendLayout();
            pnlRightBottom.SuspendLayout();
            pnlRightMiddle.SuspendLayout();
            pnlRightTop.SuspendLayout();
            SuspendLayout();
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(30, 30);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(pnlLeftBottom);
            splitContainer1.Panel1.Controls.Add(pnlLeftMiddle);
            splitContainer1.Panel1.Controls.Add(pnlLeftTop);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(pnlRightBottom);
            splitContainer1.Panel2.Controls.Add(pnlRightMiddle);
            splitContainer1.Panel2.Controls.Add(pnlRightTop);
            splitContainer1.Size = new Size(1124, 663);
            splitContainer1.SplitterDistance = 551;
            splitContainer1.TabIndex = 0;
            // 
            // pnlLeftBottom
            // 
            pnlLeftBottom.Controls.Add(lvwLeftDir);
            pnlLeftBottom.Dock = DockStyle.Fill;
            pnlLeftBottom.Location = new Point(0, 111);
            pnlLeftBottom.Name = "pnlLeftBottom";
            pnlLeftBottom.Size = new Size(551, 552);
            pnlLeftBottom.TabIndex = 7;
            // 
            // lvwLeftDir
            // 
            lvwLeftDir.Columns.AddRange(new ColumnHeader[] { columnHeader1, columnHeader2, columnHeader3, columnHeader7 });
            lvwLeftDir.Dock = DockStyle.Fill;
            lvwLeftDir.FullRowSelect = true;
            lvwLeftDir.GridLines = true;
            lvwLeftDir.Location = new Point(0, 0);
            lvwLeftDir.Name = "lvwLeftDir";
            lvwLeftDir.Size = new Size(551, 552);
            lvwLeftDir.TabIndex = 5;
            lvwLeftDir.UseCompatibleStateImageBehavior = false;
            lvwLeftDir.View = View.Details;
            // 
            // columnHeader1
            // 
            columnHeader1.Text = "이름";
            columnHeader1.Width = 300;
            // 
            // columnHeader2
            // 
            columnHeader2.Text = "수정일";
            columnHeader2.Width = 180;
            // 
            // columnHeader3
            // 
            columnHeader3.Text = "크기";
            columnHeader3.Width = 100;
            // 
            // columnHeader7
            // 
            columnHeader7.Text = "상태";
            columnHeader7.Width = 120;
            // 
            // pnlLeftMiddle
            // 
            pnlLeftMiddle.Controls.Add(btnLeftDir);
            pnlLeftMiddle.Controls.Add(txtLeftDir);
            pnlLeftMiddle.Dock = DockStyle.Top;
            pnlLeftMiddle.Location = new Point(0, 81);
            pnlLeftMiddle.Name = "pnlLeftMiddle";
            pnlLeftMiddle.Padding = new Padding(3);
            pnlLeftMiddle.Size = new Size(551, 30);
            pnlLeftMiddle.TabIndex = 6;
            // 
            // btnLeftDir
            // 
            btnLeftDir.Dock = DockStyle.Right;
            btnLeftDir.Location = new Point(464, 3);
            btnLeftDir.Name = "btnLeftDir";
            btnLeftDir.Size = new Size(84, 24);
            btnLeftDir.TabIndex = 4;
            btnLeftDir.Text = "폴더선택";
            btnLeftDir.UseVisualStyleBackColor = true;
            btnLeftDir.Click += btnLeftDir_Click;
            // 
            // txtLeftDir
            // 
            txtLeftDir.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtLeftDir.Location = new Point(21, 3);
            txtLeftDir.Name = "txtLeftDir";
            txtLeftDir.Size = new Size(437, 23);
            txtLeftDir.TabIndex = 3;
            // 
            // pnlLeftTop
            // 
            pnlLeftTop.Controls.Add(lblAppName);
            pnlLeftTop.Controls.Add(btnCopyFromLeft);
            pnlLeftTop.Dock = DockStyle.Top;
            pnlLeftTop.Location = new Point(0, 0);
            pnlLeftTop.Name = "pnlLeftTop";
            pnlLeftTop.Padding = new Padding(8);
            pnlLeftTop.Size = new Size(551, 81);
            pnlLeftTop.TabIndex = 5;
            // 
            // lblAppName
            // 
            lblAppName.AutoSize = true;
            lblAppName.Dock = DockStyle.Top;
            lblAppName.Font = new Font("맑은 고딕", 21.75F, FontStyle.Bold, GraphicsUnit.Point, 129);
            lblAppName.Location = new Point(8, 8);
            lblAppName.Name = "lblAppName";
            lblAppName.Size = new Size(199, 40);
            lblAppName.TabIndex = 0;
            lblAppName.Text = "File Compare";
            // 
            // btnCopyFromLeft
            // 
            btnCopyFromLeft.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnCopyFromLeft.Location = new Point(456, 29);
            btnCopyFromLeft.Name = "btnCopyFromLeft";
            btnCopyFromLeft.Size = new Size(84, 40);
            btnCopyFromLeft.TabIndex = 3;
            btnCopyFromLeft.Text = ">>>";
            btnCopyFromLeft.UseVisualStyleBackColor = true;
            // 
            // pnlRightBottom
            // 
            pnlRightBottom.Controls.Add(lvwRightDir);
            pnlRightBottom.Dock = DockStyle.Fill;
            pnlRightBottom.Location = new Point(0, 115);
            pnlRightBottom.Name = "pnlRightBottom";
            pnlRightBottom.Size = new Size(569, 548);
            pnlRightBottom.TabIndex = 8;
            // 
            // lvwRightDir
            // 
            lvwRightDir.Columns.AddRange(new ColumnHeader[] { columnHeader4, columnHeader5, columnHeader6, columnHeader8 });
            lvwRightDir.Dock = DockStyle.Fill;
            lvwRightDir.FullRowSelect = true;
            lvwRightDir.GridLines = true;
            lvwRightDir.Location = new Point(0, 0);
            lvwRightDir.Name = "lvwRightDir";
            lvwRightDir.Size = new Size(569, 548);
            lvwRightDir.TabIndex = 5;
            lvwRightDir.UseCompatibleStateImageBehavior = false;
            lvwRightDir.View = View.Details;
            // 
            // columnHeader4
            // 
            columnHeader4.Text = "이름";
            columnHeader4.Width = 300;
            // 
            // columnHeader5
            // 
            columnHeader5.Text = "수정일";
            columnHeader5.Width = 180;
            // 
            // columnHeader6
            // 
            columnHeader6.Text = "크기";
            columnHeader6.Width = 100;
            // 
            // columnHeader8
            // 
            columnHeader8.Text = "상태";
            columnHeader8.Width = 120;
            // 
            // pnlRightMiddle
            // 
            pnlRightMiddle.Controls.Add(btnRightDir);
            pnlRightMiddle.Controls.Add(txtRightDir);
            pnlRightMiddle.Dock = DockStyle.Top;
            pnlRightMiddle.Location = new Point(0, 81);
            pnlRightMiddle.Name = "pnlRightMiddle";
            pnlRightMiddle.Padding = new Padding(3);
            pnlRightMiddle.Size = new Size(569, 34);
            pnlRightMiddle.TabIndex = 7;
            // 
            // btnRightDir
            // 
            btnRightDir.Dock = DockStyle.Right;
            btnRightDir.Location = new Point(482, 3);
            btnRightDir.Name = "btnRightDir";
            btnRightDir.Size = new Size(84, 28);
            btnRightDir.TabIndex = 5;
            btnRightDir.Text = "폴더선택";
            btnRightDir.UseVisualStyleBackColor = true;
            btnRightDir.Click += btnRightDir_Click;
            // 
            // txtRightDir
            // 
            txtRightDir.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtRightDir.Location = new Point(3, 3);
            txtRightDir.Name = "txtRightDir";
            txtRightDir.Size = new Size(471, 23);
            txtRightDir.TabIndex = 4;
            // 
            // pnlRightTop
            // 
            pnlRightTop.Controls.Add(btnCopyFromRight);
            pnlRightTop.Dock = DockStyle.Top;
            pnlRightTop.Location = new Point(0, 0);
            pnlRightTop.Name = "pnlRightTop";
            pnlRightTop.Padding = new Padding(8);
            pnlRightTop.Size = new Size(569, 81);
            pnlRightTop.TabIndex = 6;
            // 
            // btnCopyFromRight
            // 
            btnCopyFromRight.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnCopyFromRight.Location = new Point(11, 29);
            btnCopyFromRight.Name = "btnCopyFromRight";
            btnCopyFromRight.Padding = new Padding(8);
            btnCopyFromRight.Size = new Size(84, 40);
            btnCopyFromRight.TabIndex = 4;
            btnCopyFromRight.Text = "<<<";
            btnCopyFromRight.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1184, 723);
            Controls.Add(splitContainer1);
            Name = "Form1";
            Padding = new Padding(30);
            Text = "File Compare v1.0";
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            pnlLeftBottom.ResumeLayout(false);
            pnlLeftMiddle.ResumeLayout(false);
            pnlLeftMiddle.PerformLayout();
            pnlLeftTop.ResumeLayout(false);
            pnlLeftTop.PerformLayout();
            pnlRightBottom.ResumeLayout(false);
            pnlRightMiddle.ResumeLayout(false);
            pnlRightMiddle.PerformLayout();
            pnlRightTop.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private SplitContainer splitContainer1;
        private Label lblAppName;
        private Button btnCopyFromLeft;
        private Button btnCopyFromRight;
        private Panel pnlLeftTop;
        private Panel pnlLeftMiddle;
        private Button btnLeftDir;
        private TextBox txtLeftDir;
        private Panel pnlRightMiddle;
        private Button btnRightDir;
        private TextBox txtRightDir;
        private Panel pnlRightTop;
        private Panel pnlLeftBottom;
        private ListView lvwLeftDir;
        private Panel pnlRightBottom;
        private ListView lvwRightDir;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private ColumnHeader columnHeader3;
        private ColumnHeader columnHeader7;
        private ColumnHeader columnHeader8;
        private ColumnHeader columnHeader4;
        private ColumnHeader columnHeader5;
        private ColumnHeader columnHeader6;
    }
}
