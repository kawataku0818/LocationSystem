using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace LocationSystem
{
    public partial class FormSystem : Form
    {
        enum SYSTEM_SETTING
        {
            CAISSONIMAGEPATH = 0,
            USERIMAGEPATH,
            MACHINEIMAGEPATH,
            UPTEXT,
            DOWNTEXT,
            RIGHTTEXT,
            LEFTTEXT,
            STARTX,
            STARTY,
            ENDX,
            ENDY,
            ORIGINX,
            ORIGINY,
            VIEWAREAWIDTH,
            VIEWAREAHEIGHT,
            SCALEX,
            SCALEY,
            LOCATIONDIRECTORYPATH,
            LOCATIONFILESAVEDAY,
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

        public decimal ViewAreaWidth { get; set; }
        public decimal ViewAreaHeight { get; set; }

        public decimal ScaleX { get; set; }
        public decimal ScaleY { get; set; }


        public string LocationDirectoryPath { get; set; }
        public int LocationFileSaveDay { get; set; }
 
        public static readonly string FILE_PATH = @"Setting.xml";

        object[] objBefore;
        
        public FormSystem()
        {
            InitializeComponent();
        }


        private void FormSystem_Load(object sender, EventArgs e)
        {
            
            objBefore = new object[(int)SYSTEM_SETTING.MAX];

            objBefore[(int)SYSTEM_SETTING.CAISSONIMAGEPATH] = CaissonImagePath;
            objBefore[(int)SYSTEM_SETTING.USERIMAGEPATH] = UserImagePath;
            objBefore[(int)SYSTEM_SETTING.MACHINEIMAGEPATH] = MachineImagePath;
            objBefore[(int)SYSTEM_SETTING.UPTEXT] = UpText;
            objBefore[(int)SYSTEM_SETTING.DOWNTEXT] = DownText;
            objBefore[(int)SYSTEM_SETTING.RIGHTTEXT] = RightText;
            objBefore[(int)SYSTEM_SETTING.LEFTTEXT] = LeftText;
            objBefore[(int)SYSTEM_SETTING.STARTX] = StartX;
            objBefore[(int)SYSTEM_SETTING.STARTY] = StartY;
            objBefore[(int)SYSTEM_SETTING.ENDX] = EndX;
            objBefore[(int)SYSTEM_SETTING.ENDY] = EndY;
            objBefore[(int)SYSTEM_SETTING.ORIGINX] = OriginX;
            objBefore[(int)SYSTEM_SETTING.ORIGINY] = OriginY;
            objBefore[(int)SYSTEM_SETTING.VIEWAREAWIDTH] = ViewAreaWidth;
            objBefore[(int)SYSTEM_SETTING.VIEWAREAHEIGHT] = ViewAreaHeight;
            objBefore[(int)SYSTEM_SETTING.SCALEX] = ScaleX;
            objBefore[(int)SYSTEM_SETTING.SCALEY] = ScaleY;
            objBefore[(int)SYSTEM_SETTING.LOCATIONDIRECTORYPATH] = LocationDirectoryPath;
            objBefore[(int)SYSTEM_SETTING.LOCATIONFILESAVEDAY] = LocationFileSaveDay;

            // ファイルパスを確認するためのツールチップ
            toolTip1.SetToolTip(textBoxLocationPath, textBoxLocationPath.Text);

            textBoxLocationPath.Text = LocationDirectoryPath;
            numericUpDownSaveDay.Value = LocationFileSaveDay;

        }

        private void textBoxLocationPath_TextChanged(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(textBoxLocationPath, textBoxLocationPath.Text);
            
        }

        private void buttonSetting_Click(object sender, EventArgs e)
        {
            var frm = new MonitorImageForm();
            frm.CaissonImagePath = this.CaissonImagePath;
            frm.UserImagePath = this.UserImagePath;
            frm.MachineImagePath = this.MachineImagePath;
            frm.UpText = this.UpText;
            frm.DownText = this.DownText;
            frm.RightText = this.RightText;
            frm.LeftText = this.LeftText;
            frm.StartX = StartX;
            frm.StartY = StartY;
            frm.EndX = EndX;
            frm.EndY = EndY;
            frm.OriginX = OriginX;
            frm.OriginY = OriginY;
            frm.ViewAreaWidth = this.ViewAreaWidth;
            frm.ViewAreaHeight = this.ViewAreaHeight;
            frm.ScaleX = this.ScaleX;
            frm.ScaleY = this.ScaleY;
            frm.ShowDialog();

            this.CaissonImagePath = frm.CaissonImagePath;
            this.UserImagePath = frm.UserImagePath;
            this.MachineImagePath = frm.MachineImagePath;
            this.UpText = frm.UpText;
            this.DownText = frm.DownText;
            this.RightText = frm.RightText;
            this.LeftText = frm.LeftText;
            this.StartX = frm.StartX;
            this.StartY = frm.StartY;
            this.EndX = frm.EndX;
            this.EndY = frm.EndY;
            this.OriginX = frm.OriginX;
            this.OriginY = frm.OriginY;
            this.ViewAreaWidth = frm.ViewAreaWidth;
            this.ViewAreaHeight = frm.ViewAreaHeight;
            this.ScaleX = frm.ScaleX;
            this.ScaleY = frm.ScaleY;

        }

        private void buttonWrite_Click(object sender, EventArgs e)
        {

            if (!checkEquals())
            {
                // 描画を一時停止する
                FormMain.FormMainInstance.StopDraw();
                DialogResult r = MessageBox.Show("システム設定を書き込みますか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                // 描画を再開する
                FormMain.FormMainInstance.StartDraw();

                if (r == DialogResult.Yes)
                {
                    // ファイルパスチェック
                    if (!Directory.Exists(textBoxLocationPath.Text))
                    {
                        // 描画を一時停止する
                        FormMain.FormMainInstance.StopDraw();
                        MessageBox.Show("'" + textBoxLocationPath.Text + "'は存在しません。","確認");
                        // 描画を再開する
                        FormMain.FormMainInstance.StartDraw();
                        return;
                    }

                    // XML書き込み
                    using (var dataSet = new DataSet())
                    {
                        dataSet.ReadXml(FILE_PATH);

                        dataSet.Tables["MONITOR"].Rows[0]["CAISSONIMAGEPATH"] = this.CaissonImagePath;
                        dataSet.Tables["MONITOR"].Rows[0]["USERIMAGEPATH"] = this.UserImagePath;
                        dataSet.Tables["MONITOR"].Rows[0]["MACHINEIMAGEPATH"] = this.MachineImagePath;
                        dataSet.Tables["DIRECTION"].Rows[0]["UP"] = this.UpText;
                        dataSet.Tables["DIRECTION"].Rows[0]["DOWN"] = this.DownText;
                        dataSet.Tables["DIRECTION"].Rows[0]["RIGHT"] = this.RightText;
                        dataSet.Tables["DIRECTION"].Rows[0]["LEFT"] = this.LeftText;
                        dataSet.Tables["VIEWAREA"].Rows[0]["STARTX"] = this.StartX;
                        dataSet.Tables["VIEWAREA"].Rows[0]["STARTY"] = this.StartY;
                        dataSet.Tables["VIEWAREA"].Rows[0]["ENDX"] = this.EndX;
                        dataSet.Tables["VIEWAREA"].Rows[0]["ENDY"] = this.EndY;
                        dataSet.Tables["VIEWAREA"].Rows[0]["WIDTH"] = this.ViewAreaWidth;
                        dataSet.Tables["VIEWAREA"].Rows[0]["HEIGHT"] = this.ViewAreaHeight;
                        dataSet.Tables["ORIGIN"].Rows[0]["X"] = this.OriginX;
                        dataSet.Tables["ORIGIN"].Rows[0]["Y"] = this.OriginY;

                        dataSet.Tables["LOCATIONFILE"].Rows[0]["PATH"] = this.LocationDirectoryPath;
                        dataSet.Tables["LOCATIONFILE"].Rows[0]["SAVEDAY"] = this.LocationFileSaveDay;
                        dataSet.WriteXml(FILE_PATH);
                    }

                    // 描画を一時停止する
                    FormMain.FormMainInstance.StopDraw();
                    MessageBox.Show("書き込みが完了しました","確認");
                    // 描画を再開する
                    FormMain.FormMainInstance.StartDraw();

                    objBefore[(int)SYSTEM_SETTING.CAISSONIMAGEPATH] = CaissonImagePath;
                    objBefore[(int)SYSTEM_SETTING.USERIMAGEPATH] = UserImagePath;
                    objBefore[(int)SYSTEM_SETTING.MACHINEIMAGEPATH] = MachineImagePath;
                    objBefore[(int)SYSTEM_SETTING.UPTEXT] = UpText;
                    objBefore[(int)SYSTEM_SETTING.DOWNTEXT] = DownText;
                    objBefore[(int)SYSTEM_SETTING.RIGHTTEXT] = RightText;
                    objBefore[(int)SYSTEM_SETTING.LEFTTEXT] = LeftText;
                    objBefore[(int)SYSTEM_SETTING.STARTX] = StartX;
                    objBefore[(int)SYSTEM_SETTING.STARTY] = StartY;
                    objBefore[(int)SYSTEM_SETTING.ENDX] = EndX;
                    objBefore[(int)SYSTEM_SETTING.ENDY] = EndY;
                    objBefore[(int)SYSTEM_SETTING.ORIGINX] = OriginX;
                    objBefore[(int)SYSTEM_SETTING.ORIGINY] = OriginY;
                    objBefore[(int)SYSTEM_SETTING.VIEWAREAWIDTH] = ViewAreaWidth;
                    objBefore[(int)SYSTEM_SETTING.VIEWAREAHEIGHT] = ViewAreaHeight;
                    objBefore[(int)SYSTEM_SETTING.SCALEX] = ScaleX;
                    objBefore[(int)SYSTEM_SETTING.SCALEY] = ScaleY;
                    objBefore[(int)SYSTEM_SETTING.LOCATIONDIRECTORYPATH] = LocationDirectoryPath;
                    objBefore[(int)SYSTEM_SETTING.LOCATIONFILESAVEDAY] = LocationFileSaveDay;
                }
            }
            else
            {
                // 描画を一時停止する
                FormMain.FormMainInstance.StopDraw();
                MessageBox.Show("変更はありません","確認");
                // 描画を再開する
                FormMain.FormMainInstance.StartDraw();
            }
        }

        private void FormSystem_FormClosing(object sender, FormClosingEventArgs e)
        {

            // ファイルパスチェック
            if (!Directory.Exists(textBoxLocationPath.Text))
            {
                // 描画を一時停止する
                FormMain.FormMainInstance.StopDraw();
                MessageBox.Show("'" + textBoxLocationPath.Text + "'は存在しません。","確認");
                // 描画を再開する
                FormMain.FormMainInstance.StartDraw();
                e.Cancel = true;
                return;
            }

            LocationDirectoryPath = textBoxLocationPath.Text;

            LocationFileSaveDay = (int)numericUpDownSaveDay.Value;

            if (!checkEquals())
            {
                // 描画を一時停止する
                FormMain.FormMainInstance.StopDraw();
                DialogResult result = MessageBox.Show("編集中の内容は破棄されますが、画面を閉じますか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                // 描画を再開する
                FormMain.FormMainInstance.StartDraw();
                if (result == DialogResult.No)
                {
                    // キャンセルする
                    e.Cancel = true;
                }
                else
                {
                    // 編集内容を破棄して画面を閉じる
                    CaissonImagePath = (string)objBefore[(int)SYSTEM_SETTING.CAISSONIMAGEPATH];
                    UserImagePath = (string)objBefore[(int)SYSTEM_SETTING.USERIMAGEPATH];
                    MachineImagePath = (string)objBefore[(int)SYSTEM_SETTING.MACHINEIMAGEPATH];
                    UpText = (string)objBefore[(int)SYSTEM_SETTING.UPTEXT];
                    DownText = (string)objBefore[(int)SYSTEM_SETTING.DOWNTEXT];
                    RightText = (string)objBefore[(int)SYSTEM_SETTING.RIGHTTEXT];
                    LeftText = (string)objBefore[(int)SYSTEM_SETTING.LEFTTEXT];
                    StartX = (int)objBefore[(int)SYSTEM_SETTING.STARTX];
                    StartY = (int)objBefore[(int)SYSTEM_SETTING.STARTY];
                    EndX = (int)objBefore[(int)SYSTEM_SETTING.ENDX];
                    EndY = (int)objBefore[(int)SYSTEM_SETTING.ENDY];
                    OriginX = (int)objBefore[(int)SYSTEM_SETTING.ORIGINX];
                    OriginY = (int)objBefore[(int)SYSTEM_SETTING.ORIGINY];
                    ViewAreaWidth = (decimal)objBefore[(int)SYSTEM_SETTING.VIEWAREAWIDTH];
                    ViewAreaHeight = (decimal)objBefore[(int)SYSTEM_SETTING.VIEWAREAHEIGHT];
                    ScaleX = (decimal)objBefore[(int)SYSTEM_SETTING.SCALEX];
                    ScaleY = (decimal)objBefore[(int)SYSTEM_SETTING.SCALEY];
                    LocationDirectoryPath = (string)objBefore[(int)SYSTEM_SETTING.LOCATIONDIRECTORYPATH];
                    LocationFileSaveDay = (int)objBefore[(int)SYSTEM_SETTING.LOCATIONFILESAVEDAY];
                }
            }
        }

        private void textBoxLocationPath_Validating(object sender, CancelEventArgs e)
        {
            // ファイルパスチェック
            if (!Directory.Exists(textBoxLocationPath.Text))
            {
                // 描画を一時停止する
                FormMain.FormMainInstance.StopDraw();
                MessageBox.Show("'" + textBoxLocationPath.Text + "'は存在しません。","確認");
                // 描画を再開する
                FormMain.FormMainInstance.StartDraw();
                e.Cancel = true;
            }
        }
        private void textBoxLocationPath_Validated(object sender, EventArgs e)
        {
            LocationDirectoryPath = textBoxLocationPath.Text;
        }

        private void numericUpDownSaveDay_Validated(object sender, EventArgs e)
        {
            LocationFileSaveDay = (int)numericUpDownSaveDay.Value;
        }

        /// <summary>
        /// 一致する場合はtrueを返す
        /// </summary>
        /// <returns></returns>
        private bool checkEquals()
        {
            if ( (string)objBefore[(int)SYSTEM_SETTING.CAISSONIMAGEPATH] != CaissonImagePath ||
                 (string)objBefore[(int)SYSTEM_SETTING.USERIMAGEPATH] != UserImagePath ||
                 (string)objBefore[(int)SYSTEM_SETTING.MACHINEIMAGEPATH] != MachineImagePath ||
                 (string)objBefore[(int)SYSTEM_SETTING.UPTEXT] != UpText ||
                 (string)objBefore[(int)SYSTEM_SETTING.DOWNTEXT] != DownText ||
                 (string)objBefore[(int)SYSTEM_SETTING.RIGHTTEXT] != RightText ||
                 (string)objBefore[(int)SYSTEM_SETTING.LEFTTEXT] != LeftText ||
                 (int)objBefore[(int)SYSTEM_SETTING.STARTX] != StartX ||
                 (int)objBefore[(int)SYSTEM_SETTING.STARTY] != StartY ||
                 (int)objBefore[(int)SYSTEM_SETTING.ENDX] != EndX ||
                 (int)objBefore[(int)SYSTEM_SETTING.ENDY] != EndY ||
                 (int)objBefore[(int)SYSTEM_SETTING.ORIGINX] != OriginX ||
                 (int)objBefore[(int)SYSTEM_SETTING.ORIGINY] != OriginY ||
                 (decimal)objBefore[(int)SYSTEM_SETTING.VIEWAREAWIDTH] != ViewAreaWidth ||
                 (decimal)objBefore[(int)SYSTEM_SETTING.VIEWAREAHEIGHT] != ViewAreaHeight ||
                 (decimal)objBefore[(int)SYSTEM_SETTING.SCALEX] != ScaleX ||
                 (decimal)objBefore[(int)SYSTEM_SETTING.SCALEY] != ScaleY ||
                 (string)objBefore[(int)SYSTEM_SETTING.LOCATIONDIRECTORYPATH] != LocationDirectoryPath ||
                 (int)objBefore[(int)SYSTEM_SETTING.LOCATIONFILESAVEDAY] != LocationFileSaveDay)
            {
                return false;
            }
            return true;
        }

        //#region 画面更新一時停止

        //[DllImport("user32.dll")]
        //public static extern IntPtr SendMessage(
        //    HandleRef hWnd, int msg, IntPtr wParam, IntPtr lParam);
        //private const int WM_SETREDRAW = 0x000B;

        ///// <summary>
        ///// コントロールの再描画を停止させる
        ///// </summary>
        ///// <param name="control">対象のコントロール</param>
        //public static void BeginControlUpdate(Control control)
        //{
        //    SendMessage(new HandleRef(control, control.Handle),
        //        WM_SETREDRAW, IntPtr.Zero, IntPtr.Zero);
        //}

        ///// <summary>
        ///// コントロールの再描画を再開させる
        ///// </summary>
        ///// <param name="control">対象のコントロール</param>
        //public static void EndControlUpdate(Control control)
        //{
        //    SendMessage(new HandleRef(control, control.Handle),
        //        WM_SETREDRAW, new IntPtr(1), IntPtr.Zero);
        //    control.Invalidate();
        //}
        //#endregion 画面更新一時停止
    }
}
