namespace LocationSystem
{
    partial class MonitorImageForm
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
            if (disposing && (areaBeforePen != null))
            {
                areaBeforePen.Dispose();
            }
            if (disposing && (areaPen != null))
            {
                areaPen.Dispose();
            }
            if (disposing && (sb != null))
            {
                sb.Dispose();
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
            this.components = new System.ComponentModel.Container();
            this.labelTitle = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.numericUpDownOriginY = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownOriginX = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.numericUpDownViewAreaWidth = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownViewAreaHeight = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonMachine = new System.Windows.Forms.Button();
            this.buttonDirection = new System.Windows.Forms.Button();
            this.buttonUser = new System.Windows.Forms.Button();
            this.buttonOrigin = new System.Windows.Forms.Button();
            this.buttonCaisson = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownOriginY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownOriginX)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownViewAreaWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownViewAreaHeight)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelTitle
            // 
            this.labelTitle.AutoSize = true;
            this.labelTitle.BackColor = System.Drawing.Color.Transparent;
            this.labelTitle.Font = new System.Drawing.Font("メイリオ", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.labelTitle.ForeColor = System.Drawing.Color.White;
            this.labelTitle.Location = new System.Drawing.Point(12, 54);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(259, 28);
            this.labelTitle.TabIndex = 9;
            this.labelTitle.Text = "ボタンを押して設定します。";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(12, 90);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1677, 939);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            this.pictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseUp);
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.White;
            this.textBox1.Font = new System.Drawing.Font("メイリオ", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.textBox1.ForeColor = System.Drawing.Color.Black;
            this.textBox1.Location = new System.Drawing.Point(914, 46);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(1008, 36);
            this.textBox1.TabIndex = 11;
            this.textBox1.Visible = false;
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.Transparent;
            this.groupBox3.Controls.Add(this.numericUpDownOriginY);
            this.groupBox3.Controls.Add(this.numericUpDownOriginX);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Location = new System.Drawing.Point(1695, 862);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(257, 162);
            this.groupBox3.TabIndex = 12;
            this.groupBox3.TabStop = false;
            // 
            // numericUpDownOriginY
            // 
            this.numericUpDownOriginY.Font = new System.Drawing.Font("メイリオ", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.numericUpDownOriginY.Location = new System.Drawing.Point(114, 105);
            this.numericUpDownOriginY.Maximum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.numericUpDownOriginY.Minimum = new decimal(new int[] {
            1080,
            0,
            0,
            -2147483648});
            this.numericUpDownOriginY.Name = "numericUpDownOriginY";
            this.numericUpDownOriginY.Size = new System.Drawing.Size(120, 36);
            this.numericUpDownOriginY.TabIndex = 6;
            this.numericUpDownOriginY.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDownOriginY.ValueChanged += new System.EventHandler(this.numericUpDownOriginY_ValueChanged);
            // 
            // numericUpDownOriginX
            // 
            this.numericUpDownOriginX.Font = new System.Drawing.Font("メイリオ", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.numericUpDownOriginX.Location = new System.Drawing.Point(114, 51);
            this.numericUpDownOriginX.Maximum = new decimal(new int[] {
            1920,
            0,
            0,
            0});
            this.numericUpDownOriginX.Name = "numericUpDownOriginX";
            this.numericUpDownOriginX.Size = new System.Drawing.Size(120, 36);
            this.numericUpDownOriginX.TabIndex = 5;
            this.numericUpDownOriginX.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDownOriginX.ValueChanged += new System.EventHandler(this.numericUpDownOriginX_ValueChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("メイリオ", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(38, 107);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(24, 28);
            this.label7.TabIndex = 2;
            this.label7.Text = "Y";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("メイリオ", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(38, 53);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(25, 28);
            this.label8.TabIndex = 1;
            this.label8.Text = "X";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("メイリオ", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(7, 17);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(88, 28);
            this.label9.TabIndex = 0;
            this.label9.Text = "原点座標";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.numericUpDownViewAreaWidth);
            this.groupBox1.Controls.Add(this.numericUpDownViewAreaHeight);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(1695, 677);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(257, 173);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            // 
            // numericUpDownViewAreaWidth
            // 
            this.numericUpDownViewAreaWidth.DecimalPlaces = 3;
            this.numericUpDownViewAreaWidth.Font = new System.Drawing.Font("メイリオ", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.numericUpDownViewAreaWidth.Increment = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.numericUpDownViewAreaWidth.Location = new System.Drawing.Point(114, 109);
            this.numericUpDownViewAreaWidth.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.numericUpDownViewAreaWidth.Name = "numericUpDownViewAreaWidth";
            this.numericUpDownViewAreaWidth.Size = new System.Drawing.Size(122, 36);
            this.numericUpDownViewAreaWidth.TabIndex = 4;
            this.numericUpDownViewAreaWidth.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDownViewAreaWidth.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownViewAreaWidth.ValueChanged += new System.EventHandler(this.numericUpDownViewAreaWidth_ValueChanged);
            // 
            // numericUpDownViewAreaHeight
            // 
            this.numericUpDownViewAreaHeight.DecimalPlaces = 3;
            this.numericUpDownViewAreaHeight.Font = new System.Drawing.Font("メイリオ", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.numericUpDownViewAreaHeight.Increment = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.numericUpDownViewAreaHeight.Location = new System.Drawing.Point(114, 56);
            this.numericUpDownViewAreaHeight.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.numericUpDownViewAreaHeight.Name = "numericUpDownViewAreaHeight";
            this.numericUpDownViewAreaHeight.Size = new System.Drawing.Size(122, 36);
            this.numericUpDownViewAreaHeight.TabIndex = 3;
            this.numericUpDownViewAreaHeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDownViewAreaHeight.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownViewAreaHeight.ValueChanged += new System.EventHandler(this.numericUpDownViewAreaHeight_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("メイリオ", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(7, 111);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(84, 28);
            this.label3.TabIndex = 2;
            this.label3.Text = "横(m)：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("メイリオ", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(7, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 28);
            this.label2.TabIndex = 1;
            this.label2.Text = "縦(m)：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("メイリオ", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(37, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 28);
            this.label1.TabIndex = 0;
            this.label1.Text = "表示エリア";
            // 
            // buttonMachine
            // 
            this.buttonMachine.Font = new System.Drawing.Font("メイリオ", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.buttonMachine.Location = new System.Drawing.Point(74, 327);
            this.buttonMachine.Name = "buttonMachine";
            this.buttonMachine.Size = new System.Drawing.Size(160, 80);
            this.buttonMachine.TabIndex = 8;
            this.buttonMachine.Text = "重機登録";
            this.buttonMachine.UseVisualStyleBackColor = true;
            this.buttonMachine.Click += new System.EventHandler(this.buttonMachine_Click);
            // 
            // buttonDirection
            // 
            this.buttonDirection.BackColor = System.Drawing.Color.White;
            this.buttonDirection.Font = new System.Drawing.Font("メイリオ", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.buttonDirection.ForeColor = System.Drawing.SystemColors.ControlText;
            this.buttonDirection.Location = new System.Drawing.Point(74, 435);
            this.buttonDirection.Name = "buttonDirection";
            this.tableLayoutPanel1.SetRowSpan(this.buttonDirection, 2);
            this.buttonDirection.Size = new System.Drawing.Size(160, 80);
            this.buttonDirection.TabIndex = 14;
            this.buttonDirection.Text = "方位文字\r\n登録";
            this.buttonDirection.UseVisualStyleBackColor = true;
            this.buttonDirection.Click += new System.EventHandler(this.buttonDirection_Click);
            // 
            // buttonUser
            // 
            this.buttonUser.Font = new System.Drawing.Font("メイリオ", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.buttonUser.Location = new System.Drawing.Point(74, 219);
            this.buttonUser.Name = "buttonUser";
            this.buttonUser.Size = new System.Drawing.Size(160, 80);
            this.buttonUser.TabIndex = 7;
            this.buttonUser.Text = "作業員登録";
            this.buttonUser.UseVisualStyleBackColor = true;
            this.buttonUser.Click += new System.EventHandler(this.buttonUser_Click);
            // 
            // buttonOrigin
            // 
            this.buttonOrigin.Font = new System.Drawing.Font("メイリオ", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.buttonOrigin.Location = new System.Drawing.Point(74, 111);
            this.buttonOrigin.Name = "buttonOrigin";
            this.tableLayoutPanel1.SetRowSpan(this.buttonOrigin, 2);
            this.buttonOrigin.Size = new System.Drawing.Size(160, 80);
            this.buttonOrigin.TabIndex = 10;
            this.buttonOrigin.Text = "原点登録";
            this.buttonOrigin.UseVisualStyleBackColor = true;
            this.buttonOrigin.Click += new System.EventHandler(this.buttonOrigin_Click);
            // 
            // buttonCaisson
            // 
            this.buttonCaisson.Font = new System.Drawing.Font("メイリオ", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.buttonCaisson.Location = new System.Drawing.Point(74, 3);
            this.buttonCaisson.Name = "buttonCaisson";
            this.buttonCaisson.Size = new System.Drawing.Size(160, 80);
            this.buttonCaisson.TabIndex = 9;
            this.buttonCaisson.Text = "背景登録";
            this.buttonCaisson.UseVisualStyleBackColor = true;
            this.buttonCaisson.Click += new System.EventHandler(this.buttonCaisson_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 29.21811F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70.78189F));
            this.tableLayoutPanel1.Controls.Add(this.buttonOrigin, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.buttonMachine, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.buttonUser, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.buttonCaisson, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.buttonDirection, 1, 5);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(1709, 92);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 7;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 64.53488F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 35.46511F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 49F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 108F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 108F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 57F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 49F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(243, 539);
            this.tableLayoutPanel1.TabIndex = 22;
            // 
            // MonitorImageForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(151)))), ((int)(((byte)(243)))));
            this.ClientSize = new System.Drawing.Size(1924, 1041);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.labelTitle);
            this.Controls.Add(this.pictureBox1);
            this.Font = new System.Drawing.Font("Symbol", 8.25F);
            this.Location = new System.Drawing.Point(1366, -305);
            this.Name = "MonitorImageForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Load += new System.EventHandler(this.MonitorImageForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownOriginY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownOriginX)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownViewAreaWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownViewAreaHeight)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NumericUpDown numericUpDownViewAreaWidth;
        private System.Windows.Forms.NumericUpDown numericUpDownViewAreaHeight;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonMachine;
        private System.Windows.Forms.Button buttonDirection;
        private System.Windows.Forms.Button buttonUser;
        private System.Windows.Forms.Button buttonOrigin;
        private System.Windows.Forms.Button buttonCaisson;
        private System.Windows.Forms.NumericUpDown numericUpDownOriginY;
        private System.Windows.Forms.NumericUpDown numericUpDownOriginX;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}

