using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace LocationSystem
{
    public partial class MonitorImageForm : Form
    {
        enum MODE
        {
            NONE = 0,
            AREA,
            ORIGIN,
            MAX
        }
        public string CaissonImagePath { get; set; }
        public string UserImagePath { get; set; }
        public string MachineImagePath { get; set; }

        public string UpText { get; set; }
        public string DownText { get; set; }
        public string RightText { get; set; }
        public string LeftText { get; set; }

        public int StartX { get; set; }
        public int StartY { get; set; }
        public int EndX { get; set; }
        public int EndY { get; set; }

        public int OriginX { get; set; }
        public int OriginY { get; set; }

        public float MachineWidth { get; set; }
        public float MachineHeight { get; set; }

        public decimal ViewAreaWidth { get; set; }
        public decimal ViewAreaHeight { get; set; }

        public string CassionImagePath { get; set; }

        public decimal ScaleX { get; set; }
        public decimal ScaleY { get; set; }

        int mode = (int)MODE.NONE;

        //string NoText;


        private decimal x;
        private decimal y;
        public decimal returnX;
        public decimal returnY;
        SolidBrush sb = new SolidBrush(Color.FromArgb(63, 255, 0, 0));
        Pen areaPen = new Pen(Color.LightGreen, 3);
        Pen areaBeforePen = new Pen(Color.Gray, 3);

        bool flg = false;
        bool regist = false;
        
        public MonitorImageForm()
        {
            InitializeComponent();
        }

        public MonitorImageForm(decimal x, decimal y)
        {
            this.x = x;
            this.y = y;

            InitializeComponent();
        }

        private void MonitorImageForm_Load(object sender, EventArgs e)
        {
            // 原点位置の数値入力を無効にする
            numericUpDownOriginX.Enabled = false;
            numericUpDownOriginY.Enabled = false;

            toolTip1.SetToolTip(textBox1, textBox1.Text);

            Init();

            this.numericUpDownViewAreaHeight.Minimum = 0.001M;
            this.numericUpDownViewAreaHeight.Maximum = 100000; 
            this.numericUpDownViewAreaWidth.Minimum = 0.001M;
            this.numericUpDownViewAreaWidth.Maximum = 100000;

            this.numericUpDownViewAreaWidth.Value = this.ViewAreaWidth;
            this.numericUpDownViewAreaHeight.Value = this.ViewAreaHeight;

            this.numericUpDownOriginX.Minimum = 0;
            this.numericUpDownOriginX.Maximum = pictureBox1.Width;
            this.numericUpDownOriginY.Minimum = -(pictureBox1.Height);
            this.numericUpDownOriginY.Maximum = 0;

            this.numericUpDownOriginX.Value = OriginX;
            this.numericUpDownOriginY.Value = OriginY;
        }

        //static public void ShowMiniForm(decimal x, decimal y, out decimal returnX, out decimal returnY)
        //{
        //    using (var f = new MonitorImageForm(x, y))
        //    {
        //        f.ShowDialog();
        //        decimal receiveValue = f.x;

        //        returnX = f.returnX;
        //        returnY = f.returnY;
        //    }

        //}

        void Init()
        {

            InitMonitorImageForm();

            InitButton();
            
        }

        void InitMonitorImageForm()
        {
            //フォームが存在しているディスプレイの作業領域の高さと幅を取得
            int w = System.Windows.Forms.Screen.GetWorkingArea(this).Width;
            int h = System.Windows.Forms.Screen.GetWorkingArea(this).Height;

            // フォームが存在しているディスプレイの作業領域の高さと幅を設定する
            this.Width = w;
            this.Height = h;

            // 現在フォームが存在しているディスプレイを取得する
            Screen s = Screen.FromControl(this);

            // ディスプレイの位置を取得する
            int xp = s.Bounds.X;
            int yp = s.Bounds.Y;
            
            // 位置を設定する
            this.Location = new Point(xp, yp);

            // 背景設定
            pictureBox1.Location = new Point(12, 90);
            pictureBox1.Width = w - 350 - pictureBox1.Location.X;
            pictureBox1.Height = h - 50 - pictureBox1.Location.Y;

            // 画像を表示する
            var canvas = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            var g = Graphics.FromImage(canvas);
            try
            {
                var bitmap = new Bitmap(this.CaissonImagePath);
                float width = bitmap.Width;
                float height = bitmap.Height;
                float x = (float)pictureBox1.Width / (float)2 - (float)width / (float)2;
                float y = (float)pictureBox1.Height / (float)2 - (float)height / (float)2;

                if (pictureBox1.Width < bitmap.Width || pictureBox1.Height < bitmap.Height)
                {
                    double widthRate = (double)bitmap.Width / (double)pictureBox1.Width;
                    double heightRate = (double)bitmap.Height / (double)pictureBox1.Height;
                    if (widthRate < heightRate)
                    {
                        height = pictureBox1.Height;
                        width = height * bitmap.Width / bitmap.Height;
                        x = pictureBox1.Width / 2 - width / 2;
                        y = 0;
                    }
                    else
                    {
                        width = pictureBox1.Width;
                        height = width * bitmap.Height / bitmap.Width;
                        x = 0;
                        y = pictureBox1.Height / 2 - height / 2;
                    }
                }
                g.DrawImage(bitmap, x, y, width, height);

                // 図面
                pictureBox1.Image = canvas;
                pictureBox1.BackColor = Color.White;

            }
            catch (Exception e)
            {
                FormMain.FormMainInstance.WriteLog(e.Message + " " + e.StackTrace);
            }
        }

        void InitButton()
        {
            textBox1.Location = new Point(this.Width - textBox1.Width - 20, textBox1.Location.Y);
            textBox1.Visible = false;

            // ボタン位置設定
            tableLayoutPanel1.Location = new Point(this.Width - tableLayoutPanel1.Width - 20, tableLayoutPanel1.Location.Y);

            groupBox1.Location = new Point(this.Width - groupBox1.Width - 20, tableLayoutPanel1.Location.Y + tableLayoutPanel1.Height + 20);
            groupBox3.Location = new Point(this.Width - groupBox3.Width - 20, pictureBox1.Location.Y + pictureBox1.Height - groupBox3.Height);

            // 背景設定
            this.BackgroundImageLayout = ImageLayout.Stretch;
            this.BackgroundImage = Image.FromFile(@"BMP\monitor_back.jpg");

        }


        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            switch (mode)
            {
                case (int)MODE.NONE:
                    break;
                case (int)MODE.AREA:
                    Graphics gArea = e.Graphics;
                    gArea = e.Graphics;
                    gArea.DrawLine(Pens.Yellow, StartX - 5, StartY - 5, StartX + 5, StartY + 5);
                    gArea.DrawLine(Pens.Yellow, StartX - 5, StartY + 5, StartX + 5, StartY - 5);
                    gArea.DrawLine(Pens.Yellow, StartX - 5, StartY - 5, StartX + 5, StartY + 5);
                    gArea.DrawLine(Pens.Yellow, StartX - 5, StartY + 5, StartX + 5, StartY - 5);
                    gArea.DrawRectangle(Pens.Red, StartX, StartY, EndX - StartX, EndY - StartY);
                    if (flg)
                    {
                        gArea.FillRectangle(sb, StartX, StartY, (EndX - StartX), (EndY - StartY));
                    }
                    if (regist)
                    {
                        gArea.DrawRectangle(areaPen, StartX, StartY, EndX - StartX, EndY - StartY);
                        regist = false;
                        flg = false;
                    }
                    break;
                case (int)MODE.ORIGIN:
                    break;
                default:
                    break;
            }
        }

        #region マウス操作

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            switch (mode)
            {
                case (int)MODE.NONE:
                    break;
                case (int)MODE.AREA: // 表示エリア登録

                    flg = false;
                    StartX = e.X;
                    StartY = e.Y;

                    EndX = e.X;
                    EndY = e.Y;

                    if (e.Button != MouseButtons.Left)
                    {
                        return;
                    }
                    pictureBox1.Invalidate();

                    break;
                case (int)MODE.ORIGIN: // 原点登録

                    pictureBox1.Image.Dispose();
                    // 画像を表示する
                    var canvas = new Bitmap(pictureBox1.Width, pictureBox1.Height);
                    using (var g = Graphics.FromImage(canvas))
                    using (var bitmap = new Bitmap(this.CaissonImagePath))
                    using (var fnt = new Font("メイリオ", 15.75F))
                    {
                        float width = bitmap.Width;
                        float height = bitmap.Height;
                        float x = (float)pictureBox1.Width / (float)2 - (float)width / (float)2;
                        float y = (float)pictureBox1.Height / (float)2 - (float)height / (float)2;

                        if (pictureBox1.Width < bitmap.Width || pictureBox1.Height < bitmap.Height)
                        {
                            double widthRate = (double)bitmap.Width / (double)pictureBox1.Width;
                            double heightRate = (double)bitmap.Height / (double)pictureBox1.Height;
                            if (widthRate < heightRate)
                            {
                                height = pictureBox1.Height;
                                width = height * bitmap.Width / bitmap.Height;
                                x = pictureBox1.Width / 2 - width / 2;
                                y = 0;
                            }
                            else
                            {
                                width = pictureBox1.Width;
                                height = width * bitmap.Height / bitmap.Width;
                                x = 0;
                                y = pictureBox1.Height / 2 - height / 2;
                            }
                        }
                        g.DrawImage(bitmap, x, y, width, height);

                        // エリアを表示する
                        g.DrawRectangle(areaPen, StartX, StartY, EndX - StartX, EndY - StartY);

                        // クリック位置を取得する
                        Point sp = Cursor.Position;
                        Point cp = pictureBox1.PointToClient(sp);
                        int xp = cp.X;
                        int yp = cp.Y;

                        // クリック位置を表示する
                        g.FillEllipse(Brushes.Blue, xp - 10 / 2, yp - 10 / 2, 10, 10);

                        g.DrawString("(" + xp.ToString() + "," + (-yp).ToString() + ")", fnt, Brushes.Blue, xp, yp);

                        pictureBox1.Image = canvas;

                        // 数値を更新する
                        numericUpDownOriginX.Value = xp;
                        numericUpDownOriginY.Value = -yp;

                        // 原点を登録する
                        OriginX = (int)numericUpDownOriginX.Value;
                        OriginY = (int)numericUpDownOriginY.Value;
                    }
                    break;
                default:
                    break;
            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            switch (mode)
            {
                case (int)MODE.NONE:
                    break;
                case (int)MODE.AREA: // 表示エリア登録
                    if (e.Button != MouseButtons.Left)
                    {
                        return;
                    }

                    EndX = e.X;
                    EndY = e.Y;
                    pictureBox1.Invalidate();

                    break;
                case (int)MODE.ORIGIN: // 原点登録
                    break;
                default:
                    break;
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            switch (mode)
            {
                case (int)MODE.NONE:
                    break;
                case (int)MODE.AREA: // 表示エリア登録
                    flg = true;
                    EndX = e.X;
                    EndY = e.Y;
                    ScaleX = (EndX - StartX) / ViewAreaWidth;
                    ScaleY = (EndY - StartY) / ViewAreaHeight;
                    pictureBox1.Invalidate();
                    if ((EndX - StartX) < 0 || (EndY - StartY) < 0)
                    {
                        return;
                    }

                    break;
                case (int)MODE.ORIGIN: // 原点登録
                    break;
                default:
                    break;
            }

        }
        #endregion マウス操作
        
        private void buttonCaisson_Click(object sender, EventArgs e)
        {
            // 原点位置の数値入力を無効にする
            numericUpDownOriginX.Enabled = false;
            numericUpDownOriginY.Enabled = false;

            // テキストを表示する
            labelTitle.Text = "背景の画像を登録して下さい。";

            // 画像パスを表示する
            textBox1.Visible = true;
            textBox1.Text = this.CaissonImagePath;

            // ボタン色付けをする
            buttonOrigin.UseVisualStyleBackColor = true;
            buttonCaisson.BackColor = Color.LightGreen;
            buttonUser.UseVisualStyleBackColor = true;
            buttonMachine.UseVisualStyleBackColor = true;
            buttonDirection.UseVisualStyleBackColor = true;

            var canvas = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            try
            {
                if (pictureBox1.Image != null)
                {
                    pictureBox1.Image.Dispose();
                }
                // 画像を表示する
                using (var g = Graphics.FromImage(canvas))
                using (var bitmap = new Bitmap(this.CaissonImagePath))
                {
                    float width = bitmap.Width;
                    float height = bitmap.Height;
                    float x = pictureBox1.Width / 2 - width / 2;
                    float y = pictureBox1.Height / 2 - height / 2;

                    if (pictureBox1.Width < bitmap.Width || pictureBox1.Height < bitmap.Height)
                    {
                        double widthRate = (double)bitmap.Width / (double)pictureBox1.Width;
                        double heightRate = (double)bitmap.Height / (double)pictureBox1.Height;
                        if (widthRate < heightRate)
                        {
                            height = pictureBox1.Height;
                            width = height * bitmap.Width / bitmap.Height;
                            x = pictureBox1.Width / 2 - width / 2;
                            y = 0;
                        }
                        else
                        {
                            width = pictureBox1.Width;
                            height = width * bitmap.Height / bitmap.Width;
                            x = 0;
                            y = pictureBox1.Height / 2 - height / 2;
                        }
                    }
                    g.DrawImage(bitmap, x, y, width, height);
                    pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;

                    // エリアを表示する
                    g.DrawRectangle(areaPen, StartX, StartY, EndX - StartX, EndY - StartY);

                    // 原点を表示する
                    g.FillEllipse(Brushes.Blue, OriginX - (10 / 2), -OriginY - (10 / 2), 10, 10);
                    using (var fnt = new Font("メイリオ", 15.75F))
                    {
                        g.DrawString("(" + OriginX.ToString() + "," + OriginY.ToString() + ")", fnt, Brushes.Blue, OriginX, -OriginY);
                    }

                    pictureBox1.Image = canvas;
                }
            }
            catch (Exception eCaissonImagePath)
            {
                FormMain.FormMainInstance.WriteLog(eCaissonImagePath.Message + " " + eCaissonImagePath.StackTrace);
            }

            // 画像選択ダイアログを開く
            using (var ofd = new OpenFileDialog())
            {
                ofd.RestoreDirectory = true;
                ofd.Filter = "*.bmp; *.tif; *.tiff; *.png; *.jpg; *.jpeg; *.gif; *.emf | *.bmp; *.tif; *.tiff; *.png; *.jpg; *.jpeg; *.gif; *.emf";
                if (ofd.ShowDialog() == DialogResult.OK)
                {


                    // 画像を更新する
                    CaissonImagePath = ofd.FileName;

                    // 画像ファイルパス表示を更新する
                    textBox1.Visible = true;
                    textBox1.Text = ofd.FileName;

                    // ツールチップを更新する
                    toolTip1.SetToolTip(textBox1, textBox1.Text);

                    if (canvas != null)
                    {
                        canvas.Dispose();
                    }
                    if (pictureBox1.Image != null)
                    {
                        pictureBox1.Image.Dispose();
                    }
                    // 画像を表示する
                    canvas = new Bitmap(pictureBox1.Width, pictureBox1.Height);

                    using (var gAfter = Graphics.FromImage(canvas))
                    using (var bitmapAfter = new Bitmap(this.CaissonImagePath))
                    {
                        // ピクチャーボックスをクリアする
                        //gAfter.Clear(pictureBox1.BackColor);

                        float width = bitmapAfter.Width;
                        float height = bitmapAfter.Height;
                        float x = pictureBox1.Width / 2 - width / 2;
                        float y = pictureBox1.Height / 2 - height / 2;

                        if (pictureBox1.Width < bitmapAfter.Width || pictureBox1.Height < bitmapAfter.Height)
                        {
                            double widthRate = (double)bitmapAfter.Width / (double)pictureBox1.Width;
                            double heightRate = (double)bitmapAfter.Height / (double)pictureBox1.Height;
                            if (widthRate < heightRate)
                            {
                                height = pictureBox1.Height;
                                width = height * bitmapAfter.Width / bitmapAfter.Height;
                                x = pictureBox1.Width / 2 - width / 2;
                                y = 0;
                            }
                            else
                            {
                                width = pictureBox1.Width;
                                height = width * bitmapAfter.Height / bitmapAfter.Width;
                                x = 0;
                                y = pictureBox1.Height / 2 - height / 2;
                            }
                        }
                        gAfter.DrawImage(bitmapAfter, x, y, width, height);

                        // 原点を表示する
                        gAfter.FillEllipse(Brushes.Blue, OriginX - (10 / 2), -OriginY - (10 / 2), 10, 10);
                        using (var fnt = new Font("メイリオ", 15.75F))
                        {
                            gAfter.DrawString("(" + OriginX.ToString() + "," + OriginY.ToString() + ")", fnt, Brushes.Blue, OriginX, -OriginY);
                        }

                        pictureBox1.Image = canvas;

                    }
                }
            }

            // テキストを表示する
            labelTitle.Text = "表示エリアをドラッグして下さい。";

            // モードを変更する
            mode = (int)MODE.AREA; // 表示エリア登録モード
        }

        private void buttonOrigin_Click(object sender, EventArgs e)
        {
            // 原点位置の数値入力を有効にする
            numericUpDownOriginX.Enabled = true;
            numericUpDownOriginY.Enabled = true;

            // モードを変更する
            mode = (int)MODE.ORIGIN; // 原点登録モード

            // テキストを表示する
            labelTitle.Text = "原点をクリックして下さい";

            // 画像パスを表示する
            textBox1.Visible = false;
            textBox1.Text = "";

            // ボタン色付けをする
            buttonOrigin.BackColor = Color.LightGreen;
            buttonCaisson.UseVisualStyleBackColor = true;
            buttonUser.UseVisualStyleBackColor = true;
            buttonMachine.UseVisualStyleBackColor = true;
            buttonDirection.UseVisualStyleBackColor = true;

            try
            {
                pictureBox1.Image.Dispose();

                // 画像を表示する
                var canvas = new Bitmap(pictureBox1.Width, pictureBox1.Height);
                using (var g = Graphics.FromImage(canvas))
                using (var bitmap = new Bitmap(this.CaissonImagePath))
                using (var fnt = new Font("メイリオ", 15.75F))
                {
                    float width = bitmap.Width;
                    float height = bitmap.Height;
                    float x = pictureBox1.Width / 2 - width / 2;
                    float y = pictureBox1.Height / 2 - height / 2;

                    if (pictureBox1.Width < bitmap.Width || pictureBox1.Height < bitmap.Height)
                    {
                        double widthRate = (double)bitmap.Width / (double)pictureBox1.Width;
                        double heightRate = (double)bitmap.Height / (double)pictureBox1.Height;
                        if (widthRate < heightRate)
                        {
                            height = pictureBox1.Height;
                            width = height * bitmap.Width / bitmap.Height;
                            x = pictureBox1.Width / 2 - width / 2;
                            y = 0;
                        }
                        else
                        {
                            width = pictureBox1.Width;
                            height = width * bitmap.Height / bitmap.Width;
                            x = 0;
                            y = pictureBox1.Height / 2 - height / 2;
                        }
                    }
                    g.DrawImage(bitmap, x, y, width, height);

                    // エリアを表示する
                    g.DrawRectangle(areaPen, StartX, StartY, EndX - StartX, EndY - StartY);

                    // 原点を表示する
                    g.FillEllipse(Brushes.Blue, OriginX - (10 / 2), -OriginY - (10 / 2), 10, 10);
                    g.DrawString("(" + OriginX.ToString() + "," + OriginY.ToString() + ")", fnt, Brushes.Blue, OriginX, -OriginY);

                    pictureBox1.Image = canvas;
                }
            }
            catch (Exception eCaissonImagePath)
            {
                FormMain.FormMainInstance.WriteLog(eCaissonImagePath.Message + " " + eCaissonImagePath.StackTrace);
            }
        }

        private void buttonUser_Click(object sender, EventArgs e)
        {
            // 原点位置の数値入力を無効にする
            numericUpDownOriginX.Enabled = false;
            numericUpDownOriginY.Enabled = false;

            // モードを変更する
            mode = (int)MODE.NONE;

            // テキストを表示する
            labelTitle.Text = "作業員の画像を登録して下さい。";

            // 画像パスを表示する
            textBox1.Visible = true;
            textBox1.Text = this.UserImagePath;

            // ボタン色付けをする
            buttonOrigin.UseVisualStyleBackColor = true;
            buttonCaisson.UseVisualStyleBackColor = true;
            buttonUser.BackColor = Color.LightGreen;
            buttonMachine.UseVisualStyleBackColor = true;
            buttonDirection.UseVisualStyleBackColor = true;

            try
            {
                if(pictureBox1.Image != null)
                {
                    pictureBox1.Image.Dispose();
                }
                // 画像を表示する
                var canvas = new Bitmap(pictureBox1.Width, pictureBox1.Height);
                using (var g = Graphics.FromImage(canvas))
                using (var bitmap = new Bitmap(this.UserImagePath))
                {

                    float width = bitmap.Width;
                    float height = bitmap.Height;
                    float x = pictureBox1.Width / 2 - width / 2;
                    float y = pictureBox1.Height / 2 - height / 2;

                    if (pictureBox1.Width < bitmap.Width || pictureBox1.Height < bitmap.Height)
                    {
                        double widthRate = (double)bitmap.Width / (double)pictureBox1.Width;
                        double heightRate = (double)bitmap.Height / (double)pictureBox1.Height;
                        if (widthRate < heightRate)
                        {
                            height = pictureBox1.Height;
                            width = height * bitmap.Width / bitmap.Height;
                            x = pictureBox1.Width / 2 - width / 2;
                            y = 0;
                        }
                        else
                        {
                            width = pictureBox1.Width;
                            height = width * bitmap.Height / bitmap.Width;
                            x = 0;
                            y = pictureBox1.Height / 2 - height / 2;
                        }
                    }
                    g.DrawImage(bitmap, x, y, width, height);
                    pictureBox1.Image = canvas;

                }

                // 画像選択ダイアログを開く
                using (var ofd = new OpenFileDialog())
                {
                    ofd.RestoreDirectory = true;
                    ofd.Filter = "*.bmp; *.tif; *.tiff; *.png; *.jpg; *.jpeg; *.gif; *.emf | *.bmp; *.tif; *.tiff; *.png; *.jpg; *.jpeg; *.gif; *.emf";
                    if (ofd.ShowDialog() == DialogResult.OK)
                    {
                        // 画像を更新する
                        UserImagePath = ofd.FileName;

                        // 画像ファイルパス表示を更新する
                        textBox1.Text = ofd.FileName;

                        // ツールチップを更新する
                        toolTip1.SetToolTip(textBox1, textBox1.Text);

                        canvas.Dispose();
                        pictureBox1.Image.Dispose();
                        // 画像を表示する
                        canvas = new Bitmap(pictureBox1.Width, pictureBox1.Height);
                        using (var gAfter = Graphics.FromImage(canvas))
                        using (var bitmapAfter = new Bitmap(this.UserImagePath))
                        {
                            float width = bitmapAfter.Width;
                            float height = bitmapAfter.Height;
                            float x = pictureBox1.Width / 2 - width / 2;
                            float y = pictureBox1.Height / 2 - height / 2;

                            if (pictureBox1.Width < bitmapAfter.Width || pictureBox1.Height < bitmapAfter.Height)
                            {
                                double widthRate = (double)bitmapAfter.Width / (double)pictureBox1.Width;
                                double heightRate = (double)bitmapAfter.Height / (double)pictureBox1.Height;
                                if (widthRate < heightRate)
                                {
                                    height = pictureBox1.Height;
                                    width = height * bitmapAfter.Width / bitmapAfter.Height;
                                    x = pictureBox1.Width / 2 - width / 2;
                                    y = 0;
                                }
                                else
                                {
                                    width = pictureBox1.Width;
                                    height = width * bitmapAfter.Height / bitmapAfter.Width;
                                    x = 0;
                                    y = pictureBox1.Height / 2 - height / 2;
                                }
                            }
                            gAfter.DrawImage(bitmapAfter, x, y, width, height);
                            pictureBox1.Image = canvas;
                        }
                    }
                }

            }
            catch (Exception eUserImagePath)
            {
                FormMain.FormMainInstance.WriteLog(eUserImagePath.Message + " " + eUserImagePath.StackTrace);
            }
        }

        private void buttonMachine_Click(object sender, EventArgs e)
        {
            // 原点位置の数値入力を無効にする
            numericUpDownOriginX.Enabled = false;
            numericUpDownOriginY.Enabled = false;

            // モードを変更する
            mode = (int)MODE.NONE;

            // テキストを表示する
            labelTitle.Text = "重機の画像を登録して下さい。";

            // 画像パスを表示する
            textBox1.Visible = true;
            textBox1.Text = this.MachineImagePath;

            // ボタン色付けをする
            buttonOrigin.UseVisualStyleBackColor = true;
            buttonCaisson.UseVisualStyleBackColor = true;
            buttonUser.UseVisualStyleBackColor = true;
            buttonMachine.BackColor = Color.LightGreen;
            buttonDirection.UseVisualStyleBackColor = true;

            try
            {

                if (pictureBox1.Image != null)
                {
                    pictureBox1.Image.Dispose();
                }

                // 画像を表示する
                var canvas = new Bitmap(pictureBox1.Width, pictureBox1.Height);
                using (var g = Graphics.FromImage(canvas))
                using (var bitmap = new Bitmap(this.MachineImagePath))
                {

                    float width = bitmap.Width;
                    float height = bitmap.Height;
                    float x = pictureBox1.Width / 2 - width / 2;
                    float y = pictureBox1.Height / 2 - height / 2;

                    if (pictureBox1.Width < bitmap.Width || pictureBox1.Height < bitmap.Height)
                    {
                        double widthRate = (double)bitmap.Width / (double)pictureBox1.Width;
                        double heightRate = (double)bitmap.Height / (double)pictureBox1.Height;
                        if (widthRate < heightRate)
                        {
                            height = pictureBox1.Height;
                            width = height * bitmap.Width / bitmap.Height;
                            x = pictureBox1.Width / 2 - width / 2;
                            y = 0;
                        }
                        else
                        {
                            width = pictureBox1.Width;
                            height = width * bitmap.Height / bitmap.Width;
                            x = 0;
                            y = pictureBox1.Height / 2 - height / 2;
                        }
                    }
                    g.DrawImage(bitmap, x, y, width, height);
                    pictureBox1.Image = canvas;

                }

                // 画像選択ダイアログを開く
                using (var ofd = new OpenFileDialog())
                {
                    ofd.RestoreDirectory = true;
                    ofd.Filter = "*.bmp; *.tif; *.tiff; *.png; *.jpg; *.jpeg; *.gif; *.emf | *.bmp; *.tif; *.tiff; *.png; *.jpg; *.jpeg; *.gif; *.emf";
                    if (ofd.ShowDialog() == DialogResult.OK)
                    {
                        // 画像を更新する
                        MachineImagePath = ofd.FileName;

                        // 画像ファイルパス表示を更新する
                        textBox1.Text = ofd.FileName;

                        // ツールチップを更新する
                        toolTip1.SetToolTip(textBox1, textBox1.Text);

                        canvas.Dispose();
                        pictureBox1.Image.Dispose();
                        // 画像を表示する
                        canvas = new Bitmap(pictureBox1.Width, pictureBox1.Height);
                        using (var gAfter = Graphics.FromImage(canvas))
                        using (var bitmapAfter = new Bitmap(this.MachineImagePath))
                        {
                            float width = bitmapAfter.Width;
                            float height = bitmapAfter.Height;
                            float x = pictureBox1.Width / 2 - width / 2;
                            float y = pictureBox1.Height / 2 - height / 2;

                            if (pictureBox1.Width < bitmapAfter.Width || pictureBox1.Height < bitmapAfter.Height)
                            {
                                double widthRate = (double)bitmapAfter.Width / (double)pictureBox1.Width;
                                double heightRate = (double)bitmapAfter.Height / (double)pictureBox1.Height;
                                if (widthRate < heightRate)
                                {
                                    height = pictureBox1.Height;
                                    width = height * bitmapAfter.Width / bitmapAfter.Height;
                                    x = pictureBox1.Width / 2 - width / 2;
                                    y = 0;
                                }
                                else
                                {
                                    width = pictureBox1.Width;
                                    height = width * bitmapAfter.Height / bitmapAfter.Width;
                                    x = 0;
                                    y = pictureBox1.Height / 2 - height / 2;
                                }
                            }
                            gAfter.DrawImage(bitmapAfter, x, y, width, height);
                            pictureBox1.Image = canvas;
                        }
                    }
                }
            }
            catch (Exception eMachineImagePath)
            {
                FormMain.FormMainInstance.WriteLog(eMachineImagePath.Message + " " + eMachineImagePath.StackTrace);
            }
            
        }

        private void buttonDirection_Click(object sender, EventArgs e)
        {
            // 原点位置の数値入力を無効にする
            numericUpDownOriginX.Enabled = false;
            numericUpDownOriginY.Enabled = false;

            // モードを変更する
            mode = (int)MODE.NONE;

            // テキストを表示する
            labelTitle.Text = "表示する文字列を16文字以内で入力して下さい。";

            // 画像パスを表示する
            textBox1.Visible = true;
            textBox1.Text = this.MachineImagePath;

            // ボタン色付けをする
            buttonOrigin.UseVisualStyleBackColor = true;
            buttonCaisson.UseVisualStyleBackColor = true;
            buttonUser.UseVisualStyleBackColor = true;
            buttonMachine.UseVisualStyleBackColor = true;
            buttonDirection.BackColor = Color.LightGreen;

            // 上下右左のテキストを設定する
            using (var formDirection = new FormDirection())
            {
                formDirection.CaissonImagePath = CaissonImagePath;
                formDirection.UpText = UpText;
                formDirection.DownText = DownText;
                formDirection.RightText = RightText;
                formDirection.LeftText = LeftText;
                formDirection.ShowDialog();

                this.UpText = formDirection.UpText;
                this.DownText = formDirection.DownText;
                this.RightText = formDirection.RightText;
                this.LeftText = formDirection.LeftText;

            }
        }

        [UIPermission(SecurityAction.Demand, Window = UIPermissionWindow.AllWindows)]
        protected override bool ProcessDialogKey(Keys keyData)
        {
            switch (mode)
            {
                case (int)MODE.NONE: // なし
                    break;
                case (int)MODE.AREA: // エリア登録モード

                    break;
                case (int)MODE.ORIGIN: // 原点登録モード
                    int xp, yp;
                    xp = (int)numericUpDownOriginX.Value;
                    yp = (int)numericUpDownOriginY.Value;

                    if ((keyData & Keys.KeyCode) == Keys.Left)
                    {
                        if (numericUpDownOriginX.Minimum < xp )
                        {
                            xp = xp - 1;
                        }
                    }
                    else if ((keyData & Keys.KeyCode) == Keys.Right)
                    {
                        if (xp < numericUpDownOriginX.Maximum)
                        {
                            xp = xp + 1;
                        }
                    }
                    else if ((keyData & Keys.KeyCode) == Keys.Up)
                    {
                        if (yp < numericUpDownOriginY.Maximum )
                        {
                            yp = yp + 1;
                        }
                    }
                    else if ((keyData & Keys.KeyCode) == Keys.Down)
                    {
                        if (numericUpDownOriginY.Minimum < yp)
                        {
                            yp = yp - 1;
                        }
                    }
                    else
                    {
                        return base.ProcessDialogKey(keyData);
                    }

                    pictureBox1.Image.Dispose();
                    // 画像を表示する
                    var canvas = new Bitmap(pictureBox1.Width, pictureBox1.Height);
                    using (var g = Graphics.FromImage(canvas))
                    using (var bitmap = new Bitmap(this.CaissonImagePath))
                    using (var fnt = new Font("メイリオ", 15.75F))
                    {
                        float width = bitmap.Width;
                        float height = bitmap.Height;
                        float x = (float)pictureBox1.Width / (float)2 - (float)width / (float)2;
                        float y = (float)pictureBox1.Height / (float)2 - (float)height / (float)2;

                        if (pictureBox1.Width < bitmap.Width || pictureBox1.Height < bitmap.Height)
                        {
                            double widthRate = (double)bitmap.Width / (double)pictureBox1.Width;
                            double heightRate = (double)bitmap.Height / (double)pictureBox1.Height;
                            if (widthRate < heightRate)
                            {
                                height = pictureBox1.Height;
                                width = height * bitmap.Width / bitmap.Height;
                                x = pictureBox1.Width / 2 - width / 2;
                                y = 0;
                            }
                            else
                            {
                                width = pictureBox1.Width;
                                height = width * bitmap.Height / bitmap.Width;
                                x = 0;
                                y = pictureBox1.Height / 2 - height / 2;
                            }
                        }
                        g.DrawImage(bitmap, x, y, width, height);

                        // エリアを表示する
                        g.DrawRectangle(areaPen, StartX, StartY, EndX - StartX, EndY - StartY);

                        // 原点位置を表示する
                        g.FillEllipse(Brushes.Blue, xp - 10 / 2, -yp - 10 / 2, 10, 10);
                        g.DrawString("(" + xp.ToString() + "," + yp.ToString() + ")", fnt, Brushes.Blue, xp, -yp);

                        pictureBox1.Image = canvas;

                        // 数値を更新する
                        numericUpDownOriginX.Value = xp;
                        numericUpDownOriginY.Value = yp;

                        //本来の処理(コントロールにフォーカスを移す)をさせたくないときはtrueを返す
                        return true;

                    }
                default:
                    break;
            }
                return base.ProcessDialogKey(keyData);
        }

        private void numericUpDownViewAreaHeight_ValueChanged(object sender, EventArgs e)
        {
            this.ViewAreaHeight = numericUpDownViewAreaHeight.Value;
            ScaleY = (EndY - StartY) / ViewAreaHeight;
        }

        private void numericUpDownViewAreaWidth_ValueChanged(object sender, EventArgs e)
        {
            this.ViewAreaWidth = numericUpDownViewAreaWidth.Value;
            ScaleX = (EndX - StartX) / ViewAreaWidth;
        }

        private void numericUpDownOriginX_ValueChanged(object sender, EventArgs e)
        {

            if (mode != (int)MODE.ORIGIN)
            {
                return;
            }

            // 原点を登録する
            OriginX = (int)numericUpDownOriginX.Value;
            OriginY = (int)numericUpDownOriginY.Value;

            pictureBox1.Image.Dispose();
            // 画像を表示する
            var canvas = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            using (var g = Graphics.FromImage(canvas))
            using (var bitmap = new Bitmap(this.CaissonImagePath))
            using (var fnt = new Font("メイリオ", 15.75F))
            {
                float width = bitmap.Width;
                float height = bitmap.Height;
                float x = (float)pictureBox1.Width / (float)2 - (float)width / (float)2;
                float y = (float)pictureBox1.Height / (float)2 - (float)height / (float)2;

                if (pictureBox1.Width < bitmap.Width || pictureBox1.Height < bitmap.Height)
                {
                    double widthRate = (double)bitmap.Width / (double)pictureBox1.Width;
                    double heightRate = (double)bitmap.Height / (double)pictureBox1.Height;
                    if (widthRate < heightRate)
                    {
                        height = pictureBox1.Height;
                        width = height * bitmap.Width / bitmap.Height;
                        x = pictureBox1.Width / 2 - width / 2;
                        y = 0;
                    }
                    else
                    {
                        width = pictureBox1.Width;
                        height = width * bitmap.Height / bitmap.Width;
                        x = 0;
                        y = pictureBox1.Height / 2 - height / 2;
                    }
                }
                g.DrawImage(bitmap, x, y, width, height);

                // エリアを表示する
                g.DrawRectangle(areaPen, StartX, StartY, EndX - StartX, EndY - StartY);

                // 原点位置を表示する
                g.FillEllipse(Brushes.Blue, OriginX - 10 / 2, -OriginY - 10 / 2, 10, 10);
                
                g.DrawString("(" + OriginX.ToString() + "," + OriginY.ToString() + ")", fnt, Brushes.Blue, OriginX, -OriginY);

                pictureBox1.Image = canvas;

            }
        }

        private void numericUpDownOriginY_ValueChanged(object sender, EventArgs e)
        {

            if (mode != (int)MODE.ORIGIN)
            {
                return;
            }

            // 原点を登録する
            OriginX = (int)numericUpDownOriginX.Value;
            OriginY = (int)numericUpDownOriginY.Value;

            pictureBox1.Image.Dispose();
            // 画像を表示する
            var canvas = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            using (var g = Graphics.FromImage(canvas))
            using (var bitmap = new Bitmap(this.CaissonImagePath))
            using (var fnt = new Font("メイリオ", 15.75F))
            {
                float width = bitmap.Width;
                float height = bitmap.Height;
                float x = (float)pictureBox1.Width / (float)2 - (float)width / (float)2;
                float y = (float)pictureBox1.Height / (float)2 - (float)height / (float)2;

                if (pictureBox1.Width < bitmap.Width || pictureBox1.Height < bitmap.Height)
                {
                    double widthRate = (double)bitmap.Width / (double)pictureBox1.Width;
                    double heightRate = (double)bitmap.Height / (double)pictureBox1.Height;
                    if (widthRate < heightRate)
                    {
                        height = pictureBox1.Height;
                        width = height * bitmap.Width / bitmap.Height;
                        x = pictureBox1.Width / 2 - width / 2;
                        y = 0;
                    }
                    else
                    {
                        width = pictureBox1.Width;
                        height = width * bitmap.Height / bitmap.Width;
                        x = 0;
                        y = pictureBox1.Height / 2 - height / 2;
                    }
                }
                g.DrawImage(bitmap, x, y, width, height);

                // エリアを表示する
                g.DrawRectangle(areaPen, StartX, StartY, EndX - StartX, EndY - StartY);

                // 原点位置を表示する
                g.FillEllipse(Brushes.Blue, OriginX - 10 / 2, -OriginY - 10 / 2, 10, 10);

                g.DrawString("(" + OriginX.ToString() + "," + OriginY.ToString() + ")", fnt, Brushes.Blue, OriginX, -OriginY);

                pictureBox1.Image = canvas;
            }
        }

    }// class MonitorImageForm end
}
