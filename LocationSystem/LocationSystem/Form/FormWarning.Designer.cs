namespace LocationSystem
{
    partial class FormWarning
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormWarning));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.操作OToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.クリアCToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.既読クリアToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.customControlListView1 = new LocationSystem.CustomControlListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "new.png");
            this.imageList1.Images.SetKeyName(1, "checked.png");
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.操作OToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(590, 24);
            this.menuStrip1.TabIndex = 7;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 操作OToolStripMenuItem
            // 
            this.操作OToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.クリアCToolStripMenuItem,
            this.既読クリアToolStripMenuItem});
            this.操作OToolStripMenuItem.Name = "操作OToolStripMenuItem";
            this.操作OToolStripMenuItem.Size = new System.Drawing.Size(60, 20);
            this.操作OToolStripMenuItem.Text = "操作(&O)";
            // 
            // クリアCToolStripMenuItem
            // 
            this.クリアCToolStripMenuItem.Name = "クリアCToolStripMenuItem";
            this.クリアCToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.クリアCToolStripMenuItem.Text = "履歴全クリア(&A)";
            this.クリアCToolStripMenuItem.Click += new System.EventHandler(this.クリアCToolStripMenuItem_Click);
            // 
            // 既読クリアToolStripMenuItem
            // 
            this.既読クリアToolStripMenuItem.Name = "既読クリアToolStripMenuItem";
            this.既読クリアToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.既読クリアToolStripMenuItem.Text = "確認行クリア(&C)";
            this.既読クリアToolStripMenuItem.Click += new System.EventHandler(this.既読クリアToolStripMenuItem_Click);
            // 
            // customControlListView1
            // 
            this.customControlListView1.CheckBoxes = true;
            this.customControlListView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.customControlListView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.customControlListView1.Location = new System.Drawing.Point(0, 24);
            this.customControlListView1.Name = "customControlListView1";
            this.customControlListView1.Size = new System.Drawing.Size(590, 652);
            this.customControlListView1.SmallImageList = this.imageList1;
            this.customControlListView1.TabIndex = 6;
            this.customControlListView1.UseCompatibleStateImageBehavior = false;
            this.customControlListView1.View = System.Windows.Forms.View.Details;
            this.customControlListView1.SelectedIndexChanged += new System.EventHandler(this.customControlListView1_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "";
            this.columnHeader1.Width = 1920;
            // 
            // FormWarning
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(590, 676);
            this.Controls.Add(this.customControlListView1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormWarning";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "侵入履歴";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormWarning_FormClosing);
            this.Load += new System.EventHandler(this.FormWarning_Load);
            this.Resize += new System.EventHandler(this.FormWarning_Resize);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ImageList imageList1;
        private CustomControlListView customControlListView1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 操作OToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem クリアCToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 既読クリアToolStripMenuItem;
        private System.Windows.Forms.ColumnHeader columnHeader1;
    }
}