namespace LocationSystem
{
    partial class FormTagState
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
            if (disposing && (batSchema != null))
            {
                batSchema.Dispose();
            }
            if (disposing && (statusSchema != null))
            {
                statusSchema.Dispose();
            }

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormTagState));
            this.dataSet1 = new System.Data.DataSet();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.オプションToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.noToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.タグNoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.名称ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.誤差ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.バッテリー状態ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.xToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.yToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.バッテリーToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.更新ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataSet1
            // 
            this.dataSet1.DataSetName = "NewDataSet";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.オプションToolStripMenuItem,
            this.バッテリーToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1116, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // オプションToolStripMenuItem
            // 
            this.オプションToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.noToolStripMenuItem,
            this.タグNoToolStripMenuItem,
            this.名称ToolStripMenuItem,
            this.誤差ToolStripMenuItem,
            this.バッテリー状態ToolStripMenuItem,
            this.xToolStripMenuItem,
            this.yToolStripMenuItem});
            this.オプションToolStripMenuItem.Name = "オプションToolStripMenuItem";
            this.オプションToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.オプションToolStripMenuItem.Text = "表示";
            // 
            // noToolStripMenuItem
            // 
            this.noToolStripMenuItem.Checked = true;
            this.noToolStripMenuItem.CheckOnClick = true;
            this.noToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.noToolStripMenuItem.Name = "noToolStripMenuItem";
            this.noToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.noToolStripMenuItem.Text = "No";
            this.noToolStripMenuItem.CheckedChanged += new System.EventHandler(this.noToolStripMenuItem_CheckedChanged);
            // 
            // タグNoToolStripMenuItem
            // 
            this.タグNoToolStripMenuItem.Checked = true;
            this.タグNoToolStripMenuItem.CheckOnClick = true;
            this.タグNoToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.タグNoToolStripMenuItem.Name = "タグNoToolStripMenuItem";
            this.タグNoToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.タグNoToolStripMenuItem.Text = "タグNo";
            this.タグNoToolStripMenuItem.Click += new System.EventHandler(this.タグNoToolStripMenuItem_Click);
            // 
            // 名称ToolStripMenuItem
            // 
            this.名称ToolStripMenuItem.Checked = true;
            this.名称ToolStripMenuItem.CheckOnClick = true;
            this.名称ToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.名称ToolStripMenuItem.Name = "名称ToolStripMenuItem";
            this.名称ToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.名称ToolStripMenuItem.Text = "名称";
            this.名称ToolStripMenuItem.Click += new System.EventHandler(this.名称ToolStripMenuItem_Click);
            // 
            // 誤差ToolStripMenuItem
            // 
            this.誤差ToolStripMenuItem.Checked = true;
            this.誤差ToolStripMenuItem.CheckOnClick = true;
            this.誤差ToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.誤差ToolStripMenuItem.Name = "誤差ToolStripMenuItem";
            this.誤差ToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.誤差ToolStripMenuItem.Text = "誤差";
            this.誤差ToolStripMenuItem.Click += new System.EventHandler(this.誤差ToolStripMenuItem_Click);
            // 
            // バッテリー状態ToolStripMenuItem
            // 
            this.バッテリー状態ToolStripMenuItem.Checked = true;
            this.バッテリー状態ToolStripMenuItem.CheckOnClick = true;
            this.バッテリー状態ToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.バッテリー状態ToolStripMenuItem.Name = "バッテリー状態ToolStripMenuItem";
            this.バッテリー状態ToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.バッテリー状態ToolStripMenuItem.Text = "バッテリー状態";
            this.バッテリー状態ToolStripMenuItem.Click += new System.EventHandler(this.バッテリー状態ToolStripMenuItem_Click);
            // 
            // xToolStripMenuItem
            // 
            this.xToolStripMenuItem.Checked = true;
            this.xToolStripMenuItem.CheckOnClick = true;
            this.xToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.xToolStripMenuItem.Name = "xToolStripMenuItem";
            this.xToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.xToolStripMenuItem.Text = "X";
            this.xToolStripMenuItem.Click += new System.EventHandler(this.xToolStripMenuItem_Click);
            // 
            // yToolStripMenuItem
            // 
            this.yToolStripMenuItem.Checked = true;
            this.yToolStripMenuItem.CheckOnClick = true;
            this.yToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.yToolStripMenuItem.Name = "yToolStripMenuItem";
            this.yToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.yToolStripMenuItem.Text = "Y";
            this.yToolStripMenuItem.Click += new System.EventHandler(this.yToolStripMenuItem_Click);
            // 
            // バッテリーToolStripMenuItem
            // 
            this.バッテリーToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.更新ToolStripMenuItem});
            this.バッテリーToolStripMenuItem.Name = "バッテリーToolStripMenuItem";
            this.バッテリーToolStripMenuItem.Size = new System.Drawing.Size(86, 20);
            this.バッテリーToolStripMenuItem.Text = "バッテリー状態";
            // 
            // 更新ToolStripMenuItem
            // 
            this.更新ToolStripMenuItem.Name = "更新ToolStripMenuItem";
            this.更新ToolStripMenuItem.Size = new System.Drawing.Size(98, 22);
            this.更新ToolStripMenuItem.Text = "更新";
            this.更新ToolStripMenuItem.Click += new System.EventHandler(this.更新ToolStripMenuItem_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(3, 3);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 21;
            this.dataGridView1.Size = new System.Drawing.Size(1110, 623);
            this.dataGridView1.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.dataGridView1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 24);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 8F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1116, 637);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // FormTagState
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1116, 661);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.menuStrip1);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormTagState";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "タグ状態";
            this.Activated += new System.EventHandler(this.FormTagState_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormTagState_FormClosing);
            this.Load += new System.EventHandler(this.FormTagState_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Data.DataSet dataSet1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem オプションToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem noToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem タグNoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 名称ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 誤差ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem バッテリー状態ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem xToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem yToolStripMenuItem;
        //private System.Windows.Forms.ToolStripMenuItem zToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ToolStripMenuItem バッテリーToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 更新ToolStripMenuItem;
    }
}