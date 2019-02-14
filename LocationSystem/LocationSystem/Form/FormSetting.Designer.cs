namespace LocationSystem
{
    partial class FormSetting
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSetting));
            this.buttonUser = new System.Windows.Forms.Button();
            this.buttonWrite = new System.Windows.Forms.Button();
            this.dataSet1 = new System.Data.DataSet();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.buttonArea = new System.Windows.Forms.Button();
            this.buttonMachine = new System.Windows.Forms.Button();
            this.customHeaderDataGridView1 = new LocationSystem.CustomHeaderDataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.customHeaderDataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonUser
            // 
            this.buttonUser.Font = new System.Drawing.Font("メイリオ", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.buttonUser.Location = new System.Drawing.Point(3, 3);
            this.buttonUser.Name = "buttonUser";
            this.buttonUser.Size = new System.Drawing.Size(160, 80);
            this.buttonUser.TabIndex = 1;
            this.buttonUser.Text = "作業員";
            this.buttonUser.UseVisualStyleBackColor = true;
            this.buttonUser.Click += new System.EventHandler(this.buttonUser_Click);
            // 
            // buttonWrite
            // 
            this.buttonWrite.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.buttonWrite.Font = new System.Drawing.Font("メイリオ", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.buttonWrite.Location = new System.Drawing.Point(1111, 658);
            this.buttonWrite.Name = "buttonWrite";
            this.buttonWrite.Size = new System.Drawing.Size(160, 80);
            this.buttonWrite.TabIndex = 2;
            this.buttonWrite.Text = "確定";
            this.buttonWrite.UseVisualStyleBackColor = true;
            this.buttonWrite.Click += new System.EventHandler(this.buttonWrite_Click);
            // 
            // dataSet1
            // 
            this.dataSet1.DataSetName = "NewDataSet";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 48.25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 8F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 187F));
            this.tableLayoutPanel1.Controls.Add(this.buttonWrite, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.textBox1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.customHeaderDataGridView1, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 12);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 88.56016F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.43984F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 101F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1274, 749);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tableLayoutPanel1.SetColumnSpan(this.textBox1, 2);
            this.textBox1.Font = new System.Drawing.Font("メイリオ", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.textBox1.Location = new System.Drawing.Point(3, 615);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(733, 29);
            this.textBox1.TabIndex = 3;
            this.textBox1.Text = "C:\\LocationSystem\\Setting.xml";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.buttonUser);
            this.flowLayoutPanel1.Controls.Add(this.buttonArea);
            this.flowLayoutPanel1.Controls.Add(this.buttonMachine);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 650);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(1073, 96);
            this.flowLayoutPanel1.TabIndex = 6;
            // 
            // buttonArea
            // 
            this.buttonArea.Font = new System.Drawing.Font("メイリオ", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.buttonArea.Location = new System.Drawing.Point(169, 3);
            this.buttonArea.Name = "buttonArea";
            this.buttonArea.Size = new System.Drawing.Size(160, 80);
            this.buttonArea.TabIndex = 4;
            this.buttonArea.Text = "エリア";
            this.buttonArea.UseVisualStyleBackColor = true;
            this.buttonArea.Click += new System.EventHandler(this.buttonArea_Click);
            // 
            // buttonMachine
            // 
            this.buttonMachine.Font = new System.Drawing.Font("メイリオ", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.buttonMachine.Location = new System.Drawing.Point(335, 3);
            this.buttonMachine.Name = "buttonMachine";
            this.buttonMachine.Size = new System.Drawing.Size(160, 80);
            this.buttonMachine.TabIndex = 5;
            this.buttonMachine.Text = "重機";
            this.buttonMachine.UseVisualStyleBackColor = true;
            this.buttonMachine.Click += new System.EventHandler(this.buttonMachine_Click);
            // 
            // customHeaderDataGridView1
            // 
            this.customHeaderDataGridView1.ColumnHeaderBorderStyle = LocationSystem.CustomHeaderDataGridView.HeaderCellBorderStyle.SingleLine;
            this.customHeaderDataGridView1.ColumnHeaderRowCount = 1;
            this.customHeaderDataGridView1.ColumnHeaderRowHeight = 17;
            this.customHeaderDataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tableLayoutPanel1.SetColumnSpan(this.customHeaderDataGridView1, 3);
            this.customHeaderDataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.customHeaderDataGridView1.Location = new System.Drawing.Point(3, 3);
            this.customHeaderDataGridView1.Name = "customHeaderDataGridView1";
            this.customHeaderDataGridView1.RowTemplate.Height = 21;
            this.customHeaderDataGridView1.Size = new System.Drawing.Size(1268, 567);
            this.customHeaderDataGridView1.TabIndex = 7;
            this.customHeaderDataGridView1.CellValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.customHeaderDataGridView1_CellValidated);
            this.customHeaderDataGridView1.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.customHeaderDataGridView1_CellValidating);
            this.customHeaderDataGridView1.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.customHeaderDataGridView1_PreviewKeyDown);
            // 
            // FormSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1298, 773);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("MS UI Gothic", 9F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormSetting";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "タグ設定";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormSetting_FormClosing);
            this.Load += new System.EventHandler(this.FormSetting_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.customHeaderDataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button buttonUser;
        private System.Windows.Forms.Button buttonWrite;
        private System.Data.DataSet dataSet1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button buttonArea;
        private System.Windows.Forms.Button buttonMachine;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private CustomHeaderDataGridView customHeaderDataGridView1;
    }
}