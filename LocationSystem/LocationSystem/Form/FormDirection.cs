using System;
using System.Drawing;
using System.Windows.Forms;

namespace LocationSystem
{
    public partial class FormDirection : Form
    {
        public string UpText { get; set; }
        public string DownText { get; set; }
        public string RightText { get; set; }
        public string LeftText { get; set; }

        public string CaissonImagePath { get; set; }
        
        Color grayText = Color.Gray;

        public FormDirection()
        {
            InitializeComponent();
        }

        private void FormDirection_Load(object sender, EventArgs e)
        {
            textBoxUp.Text = UpText;
            textBoxDown.Text = DownText;
            textBoxRight.Text = RightText;
            textBoxLeft.Text = LeftText;

            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.ImageLocation = CaissonImagePath;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void textBoxUp_TextChanged(object sender, EventArgs e)
        {
            UpText = textBoxUp.Text;
        }

        private void textBoxDown_TextChanged(object sender, EventArgs e)
        {
            DownText = textBoxDown.Text;
        }

        private void textBoxRight_TextChanged(object sender, EventArgs e)
        {
            RightText = textBoxRight.Text;
        }

        private void textBoxLeft_TextChanged(object sender, EventArgs e)
        {
            LeftText = textBoxLeft.Text;
        }
    }
}
