namespace LocationSystem
{
    partial class FormDirection
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormDirection));
            this.button1 = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.textBoxLeft = new System.Windows.Forms.TextBox();
            this.textBoxRight = new System.Windows.Forms.TextBox();
            this.textBoxDown = new System.Windows.Forms.TextBox();
            this.textBoxUp = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("メイリオ", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button1.Location = new System.Drawing.Point(914, 579);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(160, 80);
            this.button1.TabIndex = 7;
            this.button1.Text = "確定";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(341, 103);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(400, 400);
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            // 
            // textBoxLeft
            // 
            this.textBoxLeft.Font = new System.Drawing.Font("メイリオ", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.textBoxLeft.Location = new System.Drawing.Point(10, 285);
            this.textBoxLeft.MaxLength = 16;
            this.textBoxLeft.Name = "textBoxLeft";
            this.textBoxLeft.Size = new System.Drawing.Size(330, 36);
            this.textBoxLeft.TabIndex = 43;
            this.textBoxLeft.Text = "あいうえおかきくけこさしすせそた";
            this.textBoxLeft.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBoxLeft.TextChanged += new System.EventHandler(this.textBoxLeft_TextChanged);
            // 
            // textBoxRight
            // 
            this.textBoxRight.Font = new System.Drawing.Font("メイリオ", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.textBoxRight.Location = new System.Drawing.Point(744, 285);
            this.textBoxRight.MaxLength = 16;
            this.textBoxRight.Name = "textBoxRight";
            this.textBoxRight.Size = new System.Drawing.Size(330, 36);
            this.textBoxRight.TabIndex = 42;
            this.textBoxRight.Text = "あいうえおかきくけこさしすせそた";
            this.textBoxRight.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBoxRight.TextChanged += new System.EventHandler(this.textBoxRight_TextChanged);
            // 
            // textBoxDown
            // 
            this.textBoxDown.Font = new System.Drawing.Font("メイリオ", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.textBoxDown.Location = new System.Drawing.Point(376, 505);
            this.textBoxDown.MaxLength = 16;
            this.textBoxDown.Name = "textBoxDown";
            this.textBoxDown.Size = new System.Drawing.Size(330, 36);
            this.textBoxDown.TabIndex = 41;
            this.textBoxDown.Text = "あいうえおかきくけこさしすせそた";
            this.textBoxDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBoxDown.TextChanged += new System.EventHandler(this.textBoxDown_TextChanged);
            // 
            // textBoxUp
            // 
            this.textBoxUp.Font = new System.Drawing.Font("メイリオ", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.textBoxUp.Location = new System.Drawing.Point(376, 65);
            this.textBoxUp.MaxLength = 16;
            this.textBoxUp.Name = "textBoxUp";
            this.textBoxUp.Size = new System.Drawing.Size(330, 36);
            this.textBoxUp.TabIndex = 40;
            this.textBoxUp.Text = "あいうえおかきくけこさしすせそた";
            this.textBoxUp.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBoxUp.TextChanged += new System.EventHandler(this.textBoxUp_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("メイリオ", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(5, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(435, 28);
            this.label1.TabIndex = 44;
            this.label1.Text = "表示する文字列を16文字以内で入力して下さい。";
            this.label1.Visible = false;
            // 
            // FormDirection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1083, 671);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxLeft);
            this.Controls.Add(this.textBoxRight);
            this.Controls.Add(this.textBoxDown);
            this.Controls.Add(this.textBoxUp);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.pictureBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormDirection";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "表示文字列設定";
            this.Load += new System.EventHandler(this.FormDirection_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox textBoxLeft;
        private System.Windows.Forms.TextBox textBoxRight;
        private System.Windows.Forms.TextBox textBoxDown;
        private System.Windows.Forms.TextBox textBoxUp;
        private System.Windows.Forms.Label label1;
    }
}