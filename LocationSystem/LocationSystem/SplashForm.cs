using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LocationSystem
{
    public partial class SplashForm : Form
    {
        public SplashForm()
        {
            InitializeComponent();
        }

        private void SplashForm_Load(object sender, EventArgs e)
        {
            //透明にする色
            Color transColor = Color.White;
            //背景色を指定する
            this.BackColor = transColor;
            //透明を指定する
            this.TransparencyKey = transColor;


            var bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            using (var g = Graphics.FromImage(bitmap))
            using (var font = new Font("游明朝 Demibold", 24F, FontStyle.Bold))
            {
                string drawString = "Location System Loading...";

                //線形グラデーション（横に白から黒）を作成
                LinearGradientBrush brush = new LinearGradientBrush(
                                            pictureBox1.ClientRectangle,
                                            Color.Blue,
                                            Color.Black,
                                            LinearGradientMode.Horizontal);
                //StringFormatを作成
                StringFormat sf = new StringFormat();
                //文字を真ん中に表示
                sf.Alignment = StringAlignment.Center;
                sf.LineAlignment = StringAlignment.Center;
                //文字を書く
                g.DrawString(drawString, font, brush, pictureBox1.ClientRectangle, sf);

            }
            pictureBox1.Image = bitmap;
        }
    }
}
