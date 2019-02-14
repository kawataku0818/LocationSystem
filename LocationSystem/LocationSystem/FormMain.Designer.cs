namespace LocationSystem
{
    partial class FormMain
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (bitmap != null)
                {
                    bitmap.Dispose();
                }
                if (bitmapBackImage != null)
                {
                    bitmapBackImage.Dispose();
                }
                if (bitmapMachineImage != null)
                {
                    bitmapMachineImage.Dispose();
                }
                if (bitmapUserImage != null)
                {
                    bitmapUserImage.Dispose();
                }
                if (canvas != null)
                {
                    canvas.Dispose();
                }
                if (redBrush != null)
                {
                    redBrush.Dispose();
                }
                if (yellowBrush != null)
                {
                    yellowBrush.Dispose();
                }
                if (font != null)
                {
                    font.Dispose();
                }
                if (namingSchema != null)
                {
                    //namingSchema.Dispose();
                }
                if (multiCell != null)
                {
                    //multiCell.Dispose();
                }
                if (blackPen != null)
                {
                    blackPen.Dispose();
                }
                if (redPen != null)
                {
                    redPen.Dispose();
                }
                if (yellowPen != null)
                {
                    yellowPen.Dispose();
                }
            }

            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.buttonStart = new System.Windows.Forms.Button();
            this.buttonStop = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.labelTitle = new System.Windows.Forms.Label();
            this.checkBoxCoordinate = new System.Windows.Forms.CheckBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.システム設定ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.開始ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.停止ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.システム終了ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.タグ設定ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.システムToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.タグToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.表示ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.侵入履歴ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.タグ状態ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.ヘルプToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.バージョンToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.labelUp = new System.Windows.Forms.Label();
            this.labelDown = new System.Windows.Forms.Label();
            this.labelRight = new System.Windows.Forms.Label();
            this.labelLeft = new System.Windows.Forms.Label();
            this.dataSet1 = new System.Data.DataSet();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.checkBoxAreaLock5 = new System.Windows.Forms.CheckBox();
            this.checkBoxAreaLock1 = new System.Windows.Forms.CheckBox();
            this.checkBoxAreaLock4 = new System.Windows.Forms.CheckBox();
            this.checkBoxAreaLock2 = new System.Windows.Forms.CheckBox();
            this.checkBoxAreaLock3 = new System.Windows.Forms.CheckBox();
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.checkBoxVisibleMachineZone = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).BeginInit();
            this.toolStripContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonStart
            // 
            this.buttonStart.Font = new System.Drawing.Font("メイリオ", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.buttonStart.Location = new System.Drawing.Point(1228, 92);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(158, 80);
            this.buttonStart.TabIndex = 6;
            this.buttonStart.Text = "開始";
            this.buttonStart.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // buttonStop
            // 
            this.buttonStop.Font = new System.Drawing.Font("メイリオ", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.buttonStop.Location = new System.Drawing.Point(1228, 178);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(158, 80);
            this.buttonStop.TabIndex = 7;
            this.buttonStop.Text = "停止";
            this.buttonStop.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonStop.UseVisualStyleBackColor = true;
            this.buttonStop.Click += new System.EventHandler(this.buttonStop_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(5, 92);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1143, 685);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
            // 
            // labelTitle
            // 
            this.labelTitle.AutoSize = true;
            this.labelTitle.BackColor = System.Drawing.Color.Transparent;
            this.labelTitle.Font = new System.Drawing.Font("メイリオ", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.labelTitle.ForeColor = System.Drawing.Color.White;
            this.labelTitle.Location = new System.Drawing.Point(10, 48);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(234, 41);
            this.labelTitle.TabIndex = 9;
            this.labelTitle.Text = "屋内測位システム";
            // 
            // checkBoxCoordinate
            // 
            this.checkBoxCoordinate.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBoxCoordinate.AutoSize = true;
            this.checkBoxCoordinate.Font = new System.Drawing.Font("メイリオ", 11.25F);
            this.checkBoxCoordinate.Location = new System.Drawing.Point(1650, 790);
            this.checkBoxCoordinate.Name = "checkBoxCoordinate";
            this.checkBoxCoordinate.Size = new System.Drawing.Size(158, 33);
            this.checkBoxCoordinate.TabIndex = 18;
            this.checkBoxCoordinate.Text = " ☐ 座標表示           ";
            this.checkBoxCoordinate.UseVisualStyleBackColor = true;
            this.checkBoxCoordinate.CheckedChanged += new System.EventHandler(this.checkBoxCoordinate_CheckedChanged);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.menuStrip1.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.システム設定ToolStripMenuItem,
            this.タグ設定ToolStripMenuItem,
            this.表示ToolStripMenuItem,
            this.ヘルプToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(3, 3);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(294, 26);
            this.menuStrip1.TabIndex = 19;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // システム設定ToolStripMenuItem
            // 
            this.システム設定ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.開始ToolStripMenuItem,
            this.停止ToolStripMenuItem,
            this.システム終了ToolStripMenuItem});
            this.システム設定ToolStripMenuItem.Name = "システム設定ToolStripMenuItem";
            this.システム設定ToolStripMenuItem.Size = new System.Drawing.Size(85, 22);
            this.システム設定ToolStripMenuItem.Text = "ファイル(&F)";
            // 
            // 開始ToolStripMenuItem
            // 
            this.開始ToolStripMenuItem.Name = "開始ToolStripMenuItem";
            this.開始ToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.開始ToolStripMenuItem.Text = "開始(&O)";
            this.開始ToolStripMenuItem.Click += new System.EventHandler(this.開始ToolStripMenuItem_Click);
            // 
            // 停止ToolStripMenuItem
            // 
            this.停止ToolStripMenuItem.Name = "停止ToolStripMenuItem";
            this.停止ToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.停止ToolStripMenuItem.Text = "停止(&C)";
            this.停止ToolStripMenuItem.Click += new System.EventHandler(this.停止ToolStripMenuItem_Click);
            // 
            // システム終了ToolStripMenuItem
            // 
            this.システム終了ToolStripMenuItem.Name = "システム終了ToolStripMenuItem";
            this.システム終了ToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.システム終了ToolStripMenuItem.Text = "システム終了(&X)";
            this.システム終了ToolStripMenuItem.Click += new System.EventHandler(this.システム終了ToolStripMenuItem_Click);
            // 
            // タグ設定ToolStripMenuItem
            // 
            this.タグ設定ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.システムToolStripMenuItem,
            this.タグToolStripMenuItem});
            this.タグ設定ToolStripMenuItem.Name = "タグ設定ToolStripMenuItem";
            this.タグ設定ToolStripMenuItem.Size = new System.Drawing.Size(63, 22);
            this.タグ設定ToolStripMenuItem.Text = "設定(&O)";
            // 
            // システムToolStripMenuItem
            // 
            this.システムToolStripMenuItem.Name = "システムToolStripMenuItem";
            this.システムToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.システムToolStripMenuItem.Text = "システム設定(&S)";
            this.システムToolStripMenuItem.Click += new System.EventHandler(this.システム設定ToolStripMenuItem_Click);
            // 
            // タグToolStripMenuItem
            // 
            this.タグToolStripMenuItem.Name = "タグToolStripMenuItem";
            this.タグToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.タグToolStripMenuItem.Text = "タグ設定(&T)";
            this.タグToolStripMenuItem.Click += new System.EventHandler(this.タグ設定ToolStripMenuItem_Click);
            // 
            // 表示ToolStripMenuItem
            // 
            this.表示ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.侵入履歴ToolStripMenuItem1,
            this.タグ状態ToolStripMenuItem1});
            this.表示ToolStripMenuItem.Name = "表示ToolStripMenuItem";
            this.表示ToolStripMenuItem.Size = new System.Drawing.Size(62, 22);
            this.表示ToolStripMenuItem.Text = "表示(&V)";
            // 
            // 侵入履歴ToolStripMenuItem1
            // 
            this.侵入履歴ToolStripMenuItem1.Name = "侵入履歴ToolStripMenuItem1";
            this.侵入履歴ToolStripMenuItem1.Size = new System.Drawing.Size(143, 22);
            this.侵入履歴ToolStripMenuItem1.Text = "侵入履歴(&H)";
            this.侵入履歴ToolStripMenuItem1.Click += new System.EventHandler(this.侵入履歴ToolStripMenuItem_Click);
            // 
            // タグ状態ToolStripMenuItem1
            // 
            this.タグ状態ToolStripMenuItem1.Name = "タグ状態ToolStripMenuItem1";
            this.タグ状態ToolStripMenuItem1.Size = new System.Drawing.Size(143, 22);
            this.タグ状態ToolStripMenuItem1.Text = "タグ状態(&S)";
            this.タグ状態ToolStripMenuItem1.Click += new System.EventHandler(this.タグ状態ToolStripMenuItem_Click);
            // 
            // ヘルプToolStripMenuItem
            // 
            this.ヘルプToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.バージョンToolStripMenuItem});
            this.ヘルプToolStripMenuItem.Name = "ヘルプToolStripMenuItem";
            this.ヘルプToolStripMenuItem.Size = new System.Drawing.Size(75, 22);
            this.ヘルプToolStripMenuItem.Text = "ヘルプ(&H)";
            // 
            // バージョンToolStripMenuItem
            // 
            this.バージョンToolStripMenuItem.Name = "バージョンToolStripMenuItem";
            this.バージョンToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.バージョンToolStripMenuItem.Text = "バージョン(&A)";
            this.バージョンToolStripMenuItem.Click += new System.EventHandler(this.バージョンToolStripMenuItem_Click);
            // 
            // labelUp
            // 
            this.labelUp.AutoSize = true;
            this.labelUp.BackColor = System.Drawing.Color.White;
            this.labelUp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelUp.Font = new System.Drawing.Font("メイリオ", 14.25F);
            this.labelUp.Location = new System.Drawing.Point(783, 41);
            this.labelUp.Name = "labelUp";
            this.labelUp.Size = new System.Drawing.Size(33, 30);
            this.labelUp.TabIndex = 28;
            this.labelUp.Text = "上";
            // 
            // labelDown
            // 
            this.labelDown.AutoSize = true;
            this.labelDown.BackColor = System.Drawing.Color.White;
            this.labelDown.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelDown.Font = new System.Drawing.Font("メイリオ", 14.25F);
            this.labelDown.Location = new System.Drawing.Point(783, 133);
            this.labelDown.Name = "labelDown";
            this.labelDown.Size = new System.Drawing.Size(33, 30);
            this.labelDown.TabIndex = 29;
            this.labelDown.Text = "下";
            // 
            // labelRight
            // 
            this.labelRight.AutoSize = true;
            this.labelRight.BackColor = System.Drawing.Color.White;
            this.labelRight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelRight.Font = new System.Drawing.Font("メイリオ", 14.25F);
            this.labelRight.Location = new System.Drawing.Point(899, 82);
            this.labelRight.Name = "labelRight";
            this.labelRight.Size = new System.Drawing.Size(33, 30);
            this.labelRight.TabIndex = 30;
            this.labelRight.Text = "右";
            // 
            // labelLeft
            // 
            this.labelLeft.AutoSize = true;
            this.labelLeft.BackColor = System.Drawing.Color.White;
            this.labelLeft.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelLeft.Font = new System.Drawing.Font("メイリオ", 14.25F);
            this.labelLeft.Location = new System.Drawing.Point(665, 82);
            this.labelLeft.Name = "labelLeft";
            this.labelLeft.Size = new System.Drawing.Size(33, 30);
            this.labelLeft.TabIndex = 31;
            this.labelLeft.Text = "左";
            // 
            // dataSet1
            // 
            this.dataSet1.DataSetName = "NewDataSet";
            // 
            // checkBoxAreaLock5
            // 
            this.checkBoxAreaLock5.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBoxAreaLock5.AutoSize = true;
            this.checkBoxAreaLock5.Font = new System.Drawing.Font("メイリオ", 11.25F);
            this.checkBoxAreaLock5.ForeColor = System.Drawing.SystemColors.ControlText;
            this.checkBoxAreaLock5.Location = new System.Drawing.Point(1650, 751);
            this.checkBoxAreaLock5.Name = "checkBoxAreaLock5";
            this.checkBoxAreaLock5.Size = new System.Drawing.Size(158, 33);
            this.checkBoxAreaLock5.TabIndex = 46;
            this.checkBoxAreaLock5.Text = " ☐ No５エリア固定 ";
            this.checkBoxAreaLock5.UseVisualStyleBackColor = true;
            this.checkBoxAreaLock5.CheckedChanged += new System.EventHandler(this.checkBoxAreaLock5_CheckedChanged);
            // 
            // checkBoxAreaLock1
            // 
            this.checkBoxAreaLock1.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBoxAreaLock1.AutoSize = true;
            this.checkBoxAreaLock1.Font = new System.Drawing.Font("メイリオ", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.checkBoxAreaLock1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.checkBoxAreaLock1.Location = new System.Drawing.Point(1650, 595);
            this.checkBoxAreaLock1.Name = "checkBoxAreaLock1";
            this.checkBoxAreaLock1.Size = new System.Drawing.Size(158, 33);
            this.checkBoxAreaLock1.TabIndex = 42;
            this.checkBoxAreaLock1.Text = " ☐ No１エリア固定 ";
            this.checkBoxAreaLock1.UseVisualStyleBackColor = true;
            this.checkBoxAreaLock1.CheckedChanged += new System.EventHandler(this.checkBoxAreaLock1_CheckedChanged);
            // 
            // checkBoxAreaLock4
            // 
            this.checkBoxAreaLock4.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBoxAreaLock4.AutoSize = true;
            this.checkBoxAreaLock4.Font = new System.Drawing.Font("メイリオ", 11.25F);
            this.checkBoxAreaLock4.ForeColor = System.Drawing.SystemColors.ControlText;
            this.checkBoxAreaLock4.Location = new System.Drawing.Point(1650, 712);
            this.checkBoxAreaLock4.Name = "checkBoxAreaLock4";
            this.checkBoxAreaLock4.Size = new System.Drawing.Size(158, 33);
            this.checkBoxAreaLock4.TabIndex = 45;
            this.checkBoxAreaLock4.Text = " ☐ No４エリア固定 ";
            this.checkBoxAreaLock4.UseVisualStyleBackColor = true;
            this.checkBoxAreaLock4.CheckedChanged += new System.EventHandler(this.checkBoxAreaLock4_CheckedChanged);
            // 
            // checkBoxAreaLock2
            // 
            this.checkBoxAreaLock2.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBoxAreaLock2.AutoSize = true;
            this.checkBoxAreaLock2.Font = new System.Drawing.Font("メイリオ", 11.25F);
            this.checkBoxAreaLock2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.checkBoxAreaLock2.Location = new System.Drawing.Point(1650, 634);
            this.checkBoxAreaLock2.Name = "checkBoxAreaLock2";
            this.checkBoxAreaLock2.Size = new System.Drawing.Size(158, 33);
            this.checkBoxAreaLock2.TabIndex = 43;
            this.checkBoxAreaLock2.Text = " ☐ No２エリア固定 ";
            this.checkBoxAreaLock2.UseVisualStyleBackColor = true;
            this.checkBoxAreaLock2.CheckedChanged += new System.EventHandler(this.checkBoxAreaLock2_CheckedChanged);
            // 
            // checkBoxAreaLock3
            // 
            this.checkBoxAreaLock3.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBoxAreaLock3.AutoSize = true;
            this.checkBoxAreaLock3.Font = new System.Drawing.Font("メイリオ", 11.25F);
            this.checkBoxAreaLock3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.checkBoxAreaLock3.Location = new System.Drawing.Point(1650, 673);
            this.checkBoxAreaLock3.Name = "checkBoxAreaLock3";
            this.checkBoxAreaLock3.Size = new System.Drawing.Size(158, 33);
            this.checkBoxAreaLock3.TabIndex = 44;
            this.checkBoxAreaLock3.Text = " ☐ No３エリア固定 ";
            this.checkBoxAreaLock3.UseVisualStyleBackColor = true;
            this.checkBoxAreaLock3.CheckedChanged += new System.EventHandler(this.checkBoxAreaLock3_CheckedChanged);
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(1920, 7);
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.Size = new System.Drawing.Size(1920, 32);
            this.toolStripContainer1.TabIndex = 47;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // checkBoxVisibleMachineZone
            // 
            this.checkBoxVisibleMachineZone.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBoxVisibleMachineZone.AutoSize = true;
            this.checkBoxVisibleMachineZone.Font = new System.Drawing.Font("メイリオ", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.checkBoxVisibleMachineZone.ForeColor = System.Drawing.SystemColors.ControlText;
            this.checkBoxVisibleMachineZone.Location = new System.Drawing.Point(1650, 556);
            this.checkBoxVisibleMachineZone.Name = "checkBoxVisibleMachineZone";
            this.checkBoxVisibleMachineZone.Size = new System.Drawing.Size(158, 33);
            this.checkBoxVisibleMachineZone.TabIndex = 48;
            this.checkBoxVisibleMachineZone.Text = "☐ 重機ゾーン非表示";
            this.checkBoxVisibleMachineZone.UseVisualStyleBackColor = true;
            this.checkBoxVisibleMachineZone.CheckedChanged += new System.EventHandler(this.checkBoxVisibleMachineZone_CheckedChanged);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1825, 934);
            this.Controls.Add(this.checkBoxVisibleMachineZone);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.toolStripContainer1);
            this.Controls.Add(this.checkBoxAreaLock5);
            this.Controls.Add(this.checkBoxAreaLock1);
            this.Controls.Add(this.checkBoxAreaLock4);
            this.Controls.Add(this.checkBoxAreaLock2);
            this.Controls.Add(this.checkBoxAreaLock3);
            this.Controls.Add(this.labelLeft);
            this.Controls.Add(this.labelRight);
            this.Controls.Add(this.labelDown);
            this.Controls.Add(this.labelUp);
            this.Controls.Add(this.checkBoxCoordinate);
            this.Controls.Add(this.buttonStop);
            this.Controls.Add(this.buttonStart);
            this.Controls.Add(this.labelTitle);
            this.Controls.Add(this.pictureBox1);
            this.Font = new System.Drawing.Font("Symbol", 8.25F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "屋内測位システム";
            this.Load += new System.EventHandler(this.FormMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).EndInit();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.Button buttonStop;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.CheckBox checkBoxCoordinate;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem システム設定ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem タグ設定ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ヘルプToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 開始ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 停止ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem システム終了ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem システムToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem タグToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem バージョンToolStripMenuItem;
        private System.Windows.Forms.Label labelUp;
        private System.Windows.Forms.Label labelDown;
        private System.Windows.Forms.Label labelRight;
        private System.Windows.Forms.Label labelLeft;
        private System.Data.DataSet dataSet1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.ToolStripMenuItem 表示ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 侵入履歴ToolStripMenuItem1;
        private System.Windows.Forms.CheckBox checkBoxAreaLock5;
        private System.Windows.Forms.CheckBox checkBoxAreaLock1;
        private System.Windows.Forms.CheckBox checkBoxAreaLock4;
        private System.Windows.Forms.CheckBox checkBoxAreaLock2;
        private System.Windows.Forms.CheckBox checkBoxAreaLock3;
        private System.Windows.Forms.ToolStripMenuItem タグ状態ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.CheckBox checkBoxVisibleMachineZone;
    }
}

