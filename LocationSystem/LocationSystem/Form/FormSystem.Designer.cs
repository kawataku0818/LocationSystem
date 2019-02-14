namespace LocationSystem
{
    partial class FormSystem
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSystem));
            this.buttonSetting = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.textBoxLocationPath = new System.Windows.Forms.TextBox();
            this.numericUpDownSaveDay = new System.Windows.Forms.NumericUpDown();
            this.labelSaveDay = new System.Windows.Forms.Label();
            this.labelSaveFolder = new System.Windows.Forms.Label();
            this.labelLocationFileSetting = new System.Windows.Forms.Label();
            this.buttonWrite = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSaveDay)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonSetting
            // 
            this.buttonSetting.Font = new System.Drawing.Font("メイリオ", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.buttonSetting.Location = new System.Drawing.Point(12, 23);
            this.buttonSetting.Name = "buttonSetting";
            this.buttonSetting.Size = new System.Drawing.Size(160, 100);
            this.buttonSetting.TabIndex = 0;
            this.buttonSetting.Text = "各種画像・原点\r\n登録 ";
            this.buttonSetting.UseVisualStyleBackColor = true;
            this.buttonSetting.Click += new System.EventHandler(this.buttonSetting_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.textBoxLocationPath);
            this.groupBox2.Controls.Add(this.numericUpDownSaveDay);
            this.groupBox2.Controls.Add(this.labelSaveDay);
            this.groupBox2.Controls.Add(this.labelSaveFolder);
            this.groupBox2.Controls.Add(this.labelLocationFileSetting);
            this.groupBox2.Location = new System.Drawing.Point(12, 140);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(804, 206);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            // 
            // textBoxLocationPath
            // 
            this.textBoxLocationPath.Font = new System.Drawing.Font("メイリオ", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.textBoxLocationPath.Location = new System.Drawing.Point(135, 79);
            this.textBoxLocationPath.Name = "textBoxLocationPath";
            this.textBoxLocationPath.Size = new System.Drawing.Size(654, 36);
            this.textBoxLocationPath.TabIndex = 5;
            this.textBoxLocationPath.TextChanged += new System.EventHandler(this.textBoxLocationPath_TextChanged);
            this.textBoxLocationPath.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxLocationPath_Validating);
            this.textBoxLocationPath.Validated += new System.EventHandler(this.textBoxLocationPath_Validated);
            // 
            // numericUpDownSaveDay
            // 
            this.numericUpDownSaveDay.Font = new System.Drawing.Font("メイリオ", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.numericUpDownSaveDay.Location = new System.Drawing.Point(135, 138);
            this.numericUpDownSaveDay.Maximum = new decimal(new int[] {
            3650,
            0,
            0,
            0});
            this.numericUpDownSaveDay.Name = "numericUpDownSaveDay";
            this.numericUpDownSaveDay.Size = new System.Drawing.Size(90, 36);
            this.numericUpDownSaveDay.TabIndex = 4;
            this.numericUpDownSaveDay.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDownSaveDay.Validated += new System.EventHandler(this.numericUpDownSaveDay_Validated);
            // 
            // labelSaveDay
            // 
            this.labelSaveDay.AutoSize = true;
            this.labelSaveDay.Font = new System.Drawing.Font("メイリオ", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.labelSaveDay.Location = new System.Drawing.Point(3, 140);
            this.labelSaveDay.Name = "labelSaveDay";
            this.labelSaveDay.Size = new System.Drawing.Size(123, 28);
            this.labelSaveDay.TabIndex = 2;
            this.labelSaveDay.Text = "保存期間(日)";
            // 
            // labelSaveFolder
            // 
            this.labelSaveFolder.AutoSize = true;
            this.labelSaveFolder.Font = new System.Drawing.Font("メイリオ", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.labelSaveFolder.Location = new System.Drawing.Point(3, 82);
            this.labelSaveFolder.Name = "labelSaveFolder";
            this.labelSaveFolder.Size = new System.Drawing.Size(126, 28);
            this.labelSaveFolder.TabIndex = 1;
            this.labelSaveFolder.Text = "格納フォルダ";
            // 
            // labelLocationFileSetting
            // 
            this.labelLocationFileSetting.AutoSize = true;
            this.labelLocationFileSetting.Font = new System.Drawing.Font("メイリオ", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.labelLocationFileSetting.Location = new System.Drawing.Point(3, 15);
            this.labelLocationFileSetting.Name = "labelLocationFileSetting";
            this.labelLocationFileSetting.Size = new System.Drawing.Size(164, 28);
            this.labelLocationFileSetting.TabIndex = 0;
            this.labelLocationFileSetting.Text = "ログファイル設定";
            // 
            // buttonWrite
            // 
            this.buttonWrite.Font = new System.Drawing.Font("メイリオ", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.buttonWrite.Location = new System.Drawing.Point(656, 367);
            this.buttonWrite.Name = "buttonWrite";
            this.buttonWrite.Size = new System.Drawing.Size(160, 80);
            this.buttonWrite.TabIndex = 7;
            this.buttonWrite.Text = "確定";
            this.buttonWrite.UseVisualStyleBackColor = true;
            this.buttonWrite.Click += new System.EventHandler(this.buttonWrite_Click);
            // 
            // FormSystem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(828, 469);
            this.Controls.Add(this.buttonWrite);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.buttonSetting);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormSystem";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "システム設定";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormSystem_FormClosing);
            this.Load += new System.EventHandler(this.FormSystem_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSaveDay)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonSetting;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox textBoxLocationPath;
        private System.Windows.Forms.NumericUpDown numericUpDownSaveDay;
        private System.Windows.Forms.Label labelSaveDay;
        private System.Windows.Forms.Label labelSaveFolder;
        private System.Windows.Forms.Label labelLocationFileSetting;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button buttonWrite;
    }
}