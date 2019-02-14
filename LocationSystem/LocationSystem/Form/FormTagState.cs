using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace LocationSystem
{
    public partial class FormTagState : Form
    {
        enum TAG_STATE
        {
            NO = 0,
            TAG,
            NAME,
            STDERR,
            BATTERY,
            X,
            Y,
            MAX
        }

        public string TagId { get; set; }
        public string StdErr { get; set; }
        public string Battery { get; set; }
        public string X { get; set; }
        public string Y { get; set; }

        public bool Runnninng { get; set; }
        

        int maxTag = 65; // 作業員タグと重機タグとエリアタグの合計

        decimal stdErrThreshold; // 誤差色付けしきい値(stdErr以上の場合は赤色とする) Setting.xmlの<STDERR>から取得する

        Ubisense.UBatteryMonitor.Status.Schema batSchema;
        Ubisense.ULocationEngine.TagStatus.Schema statusSchema;

        public FormTagState()
        {
            InitializeComponent();
            
            batSchema = new Ubisense.UBatteryMonitor.Status.Schema(false);
            batSchema.ConnectAsClient();

            statusSchema = new Ubisense.ULocationEngine.TagStatus.Schema(false);
            statusSchema.ConnectAsClient();
        }

        //~FormTagState()
        //{
        //    statusSchema.Disconnect();
        //    if (statusSchema != null)
        //    {
        //        statusSchema.Dispose();
        //    }

        //    batSchema.Disconnect();
        //    if (batSchema != null)
        //    {
        //        batSchema.Dispose();
        //    }
        //}

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
        private void FormTagState_Load(object sender, EventArgs e)
        {
            // ダブルバッファを設定する
            Type dgvtype = typeof(DataGridView);
            System.Reflection.PropertyInfo dgvPropertyInfo = 
                dgvtype.GetProperty(
                    "DoubleBuffered", System.Reflection.BindingFlags.Instance | 
                    System.Reflection.BindingFlags.NonPublic);
            dgvPropertyInfo.SetValue(dataGridView1, true, null);


            // 読み取り専用
            dataGridView1.ReadOnly = true;

            // 追加・削除を無効にする
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;

            // スタイルを設定する
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.PaleGreen;

            //列を追加する
            for (int i = 0; i < (int)TAG_STATE.MAX; i++)
            {
                DataGridViewTextBoxColumn textColumn = new DataGridViewTextBoxColumn();
                dataGridView1.Columns.Add(textColumn);
                textColumn.Dispose();
            }

            //自動的に並び替えられないようにする
            foreach (DataGridViewColumn c in dataGridView1.Columns)
                c.SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.PaleGreen;

            //自動調整する
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            // ヘッダの設定
            var xmlDoc = new XmlDocument();
            xmlDoc.Load(@"Setting.xml");
            XmlElement rootElement = xmlDoc.DocumentElement;
            XmlNodeList nodelistNoText = rootElement.GetElementsByTagName("NOTEXT");
            XmlNodeList nodelistTagNoText = rootElement.GetElementsByTagName("TAGNOTEXT");
            XmlNodeList nodelistNameText = rootElement.GetElementsByTagName("NAMETEXT");
            XmlNodeList nodelistStdErrText = rootElement.GetElementsByTagName("STDERRTEXT");
            XmlNodeList nodelistBatteryText = rootElement.GetElementsByTagName("BATTERYTEXT");
            XmlNodeList nodelistXText = rootElement.GetElementsByTagName("XTEXT");
            XmlNodeList nodelistYText = rootElement.GetElementsByTagName("YTEXT");
            dataGridView1.Columns[(int)TAG_STATE.NO].HeaderText = nodelistNoText.Item(0).InnerText;
            dataGridView1.Columns[(int)TAG_STATE.TAG].HeaderText = nodelistTagNoText.Item(0).InnerText;
            dataGridView1.Columns[(int)TAG_STATE.NAME].HeaderText = nodelistNameText.Item(0).InnerText;
            dataGridView1.Columns[(int)TAG_STATE.STDERR].HeaderText = nodelistStdErrText.Item(0).InnerText;
            dataGridView1.Columns[(int)TAG_STATE.BATTERY].HeaderText = nodelistBatteryText.Item(0).InnerText;
            dataGridView1.Columns[(int)TAG_STATE.X].HeaderText = nodelistXText.Item(0).InnerText;
            dataGridView1.Columns[(int)TAG_STATE.Y].HeaderText = nodelistYText.Item(0).InnerText;

            // 列幅を固定する
            // 数値を右寄せにする
            for (int i = 0; i < dataGridView1.Columns.Count; i++)
            {
                dataGridView1.Columns[i].Frozen = true;
                dataGridView1.Columns[(int)TAG_STATE.STDERR].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dataGridView1.Columns[(int)TAG_STATE.X].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dataGridView1.Columns[(int)TAG_STATE.Y].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }

            // タグと名称を取得する
            string[] tagName = new string[maxTag];
            string[] name = new string[maxTag];

            // 作業員のタグと名称を取得する
            XmlNodeList nodeList = rootElement.SelectNodes("USER");
            int index = 0;
            foreach (XmlNode nd in nodeList)
            {
                tagName[index] = nd.SelectSingleNode("TAG").InnerText;
                name[index] = nd.SelectSingleNode("NAME").InnerText;
                index = index + 1;
            }

            // エリアのタグと名称を取得する
            nodeList = rootElement.SelectNodes("AREA");
            foreach (XmlNode nd in nodeList)
            {
                tagName[index] = nd.SelectSingleNode("TAG").InnerText;
                name[index] = nd.SelectSingleNode("NAME").InnerText;
                index = index + 1;
            }

            // 重機のタグと名称を取得する
            nodeList = rootElement.SelectNodes("MACHINE");
            foreach (XmlNode nd in nodeList)
            {
                tagName[index] = nd.SelectSingleNode("TAGFL").InnerText;
                name[index] = nd.SelectSingleNode("NAME").InnerText + "(左前)";
                index = index + 1;
                tagName[index] = nd.SelectSingleNode("TAGFR").InnerText;
                name[index] = nd.SelectSingleNode("NAME").InnerText + "(右前)";
                index = index + 1;
                tagName[index] = nd.SelectSingleNode("TAGBL").InnerText;
                name[index] = nd.SelectSingleNode("NAME").InnerText +"(左後)";
                index = index + 1;
                tagName[index] = nd.SelectSingleNode("TAGBR").InnerText;
                name[index] = nd.SelectSingleNode("NAME").InnerText + "(右後)";
                index = index + 1;
            }

            int no = 0;
            for (int i = 0; i < maxTag; i++)
            {
                dataGridView1.Rows.Add();
                no = no + 1;
                dataGridView1[(int)TAG_STATE.NO, i].Value = no;
                dataGridView1[(int)TAG_STATE.TAG, i].Value = tagName[i];
                dataGridView1[(int)TAG_STATE.NAME, i].Value = name[i];
                dataGridView1[(int)TAG_STATE.STDERR, i].Value = " 0.000";
                dataGridView1[(int)TAG_STATE.X, i].Value = "  0.00";
                dataGridView1[(int)TAG_STATE.Y, i].Value = "  0.00";
            }

            XmlNodeList nodelistStdErrValue = rootElement.GetElementsByTagName("STDERR");
            decimal.TryParse(nodelistStdErrValue.Item(0).InnerText, out stdErrThreshold);

            this.dataGridView1.Font = new System.Drawing.Font("メイリオ", 12F);

            // データの設定
            Runnninng = true;
            ServerProcess();

            // バッテリー情報更新
            更新ToolStripMenuItem.PerformClick();

            // 色付け
            decimal d;
            string str;
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                Decimal.TryParse(dataGridView1[(int)TAG_STATE.STDERR, i].Value.ToString(), out d);
                if (d >= stdErrThreshold)
                {
                    dataGridView1[(int)TAG_STATE.STDERR, i].Style.BackColor = Color.Red;
                    dataGridView1[(int)TAG_STATE.STDERR, i].Style.ForeColor = Color.White;
                }
                else
                {
                    dataGridView1[(int)TAG_STATE.STDERR, i].Style.BackColor = Color.White;
                    dataGridView1[(int)TAG_STATE.STDERR, i].Style.ForeColor = Color.Black;
                }
                if (dataGridView1[(int)TAG_STATE.BATTERY, i].Value == null)
                {
                    return;
                }
                str = dataGridView1[(int)TAG_STATE.BATTERY, i].Value.ToString();
                switch (str)
                {
                    case "OK":
                        dataGridView1[(int)TAG_STATE.BATTERY, i].Style.BackColor = Color.LightGreen;
                        dataGridView1[(int)TAG_STATE.BATTERY, i].Style.ForeColor = Color.Black;
                        break;
                    case "Warning":
                        dataGridView1[(int)TAG_STATE.BATTERY, i].Style.BackColor = Color.Yellow;
                        dataGridView1[(int)TAG_STATE.BATTERY, i].Style.ForeColor = Color.Black;
                        break;
                    case "Failing":
                        dataGridView1[(int)TAG_STATE.BATTERY, i].Style.BackColor = Color.Red;
                        dataGridView1[(int)TAG_STATE.BATTERY, i].Style.ForeColor = Color.White;
                        break;
                    default:
                        break;
                }
            }
        }

        private void noToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            ToolStripMenuItem item = (ToolStripMenuItem)sender;
            if (item.Checked)
            {
                dataGridView1.Columns[(int)TAG_STATE.NO].Visible = true;
            }
            else
            {
                dataGridView1.Columns[(int)TAG_STATE.NO].Visible = false;
            }
        }

        private void タグNoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = (ToolStripMenuItem)sender;
            if (item.Checked)
            {
                dataGridView1.Columns[(int)TAG_STATE.TAG].Visible = true;
            }
            else
            {
                dataGridView1.Columns[(int)TAG_STATE.TAG].Visible = false;
            }
        }

        private void 名称ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = (ToolStripMenuItem)sender;
            if (item.Checked)
            {
                dataGridView1.Columns[(int)TAG_STATE.NAME].Visible = true;
            }
            else
            {
                dataGridView1.Columns[(int)TAG_STATE.NAME].Visible = false;
            }
        }

        private void 誤差ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = (ToolStripMenuItem)sender;
            if (item.Checked)
            {
                dataGridView1.Columns[(int)TAG_STATE.STDERR].Visible = true;
            }
            else
            {
                dataGridView1.Columns[(int)TAG_STATE.STDERR].Visible = false;
            }
        }

        private void バッテリー状態ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = (ToolStripMenuItem)sender;
            if (item.Checked)
            {
                dataGridView1.Columns[(int)TAG_STATE.BATTERY].Visible = true;
            }
            else
            {
                dataGridView1.Columns[(int)TAG_STATE.BATTERY].Visible = false;
            }
        }

        private void xToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = (ToolStripMenuItem)sender;
            if (item.Checked)
            {
                dataGridView1.Columns[(int)TAG_STATE.X].Visible = true;
            }
            else
            {
                dataGridView1.Columns[(int)TAG_STATE.X].Visible = false;
            }
        }

        private void yToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = (ToolStripMenuItem)sender;
            if (item.Checked)
            {
                dataGridView1.Columns[(int)TAG_STATE.Y].Visible = true;
            }
            else
            {
                dataGridView1.Columns[(int)TAG_STATE.Y].Visible = false;
            }
        }

        #region スレッド
        void SetLocationAction()
        {
            try
            {
                // 色付け
                decimal d;

                // 値を設定するときは自動サイズ設定しない
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
                dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

                for (int i = 0; i < maxTag; i++)
                {
                    // - を削除する
                    string tag = dataGridView1[(int)TAG_STATE.TAG, i].Value.ToString();
                    var removeChars = new char[] { '-' };
                    tag = removeChars.Aggregate(tag, (s, c) => s.Replace(c.ToString(), ""));

                    if (TagId != "000000000000000" + tag)
                    {
                        continue;
                    }
                    // 色付け
                    string stdErr = StdErr;
                    Decimal.TryParse(stdErr, out d);
                    if (d >= stdErrThreshold)
                    {
                        dataGridView1[(int)TAG_STATE.STDERR, i].Style.BackColor = Color.Red;
                        dataGridView1[(int)TAG_STATE.STDERR, i].Style.ForeColor = Color.White;
                    }
                    else
                    {
                        dataGridView1[(int)TAG_STATE.STDERR, i].Style.BackColor = Color.White;
                        dataGridView1[(int)TAG_STATE.STDERR, i].Style.ForeColor = Color.Black;
                    }
                    dataGridView1[(int)TAG_STATE.STDERR, i].Value = stdErr;
                    dataGridView1[(int)TAG_STATE.X, i].Value = X;
                    dataGridView1[(int)TAG_STATE.Y, i].Value = Y;
                    //dataGridView1.Refresh();
                }

                //// セルに値を設定したあとに自動でサイズを設定する
                //dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                //dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                //dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;

                dataGridView1.Refresh();
            }
            catch (Exception e)
            {
                Console.Write("err" + e.ToString());
            }
        }
        async void ServerProcess()
        {
            //Runnninng = true;
            await Task.Run(() => {
                while (Runnninng)
                {
                    // Invoke を通す
                    Invoke(new Action(() => {
                        SetLocationAction();
                    }));
                    Task.Delay(50).Wait();
                }
            });
        }
        #endregion スレッド

        private void FormTagState_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        private void 更新ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                dataGridView1[(int)TAG_STATE.STDERR, i].Value = " 0.000";
                dataGridView1[(int)TAG_STATE.STDERR, i].Style.BackColor = Color.White;
                dataGridView1[(int)TAG_STATE.STDERR, i].Style.ForeColor = Color.Black;
                dataGridView1[(int)TAG_STATE.BATTERY, i].Value = string.Empty;
                dataGridView1[(int)TAG_STATE.BATTERY, i].Style.BackColor = Color.White;
                dataGridView1[(int)TAG_STATE.BATTERY, i].Style.ForeColor = Color.Black;
                dataGridView1[(int)TAG_STATE.X, i].Value = "  0.00";
                dataGridView1[(int)TAG_STATE.Y, i].Value = "  0.00";
            }

                using (Ubisense.UBatteryMonitor.Status.ReadTransaction _xact = batSchema.ReadTransaction())
            {
                // LECのTag Battery Voltage画面で表示されるタグステータスのデータを取得
                foreach (Ubisense.UBatteryMonitor.Status.TagHasBatteryStatus.RowType row in Ubisense.UBatteryMonitor.Status.TagHasBatteryStatus.tag_(_xact))
                {
                    string tagId = Ubisense.ULocationIntegration.Tag.ConvertIdToString(row.tag_.Id, '-');

                    string[] data = { tagId,								//タグID
                                      row.status_.ToString()};				//電池残量のステータス(OK, Warning, Failing)

                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        if (data[0] == dataGridView1[(int)TAG_STATE.TAG, i].Value.ToString())
                        {
                            dataGridView1[(int)TAG_STATE.BATTERY, i].Value = data[1];
                            // 色付け
                            string str = dataGridView1[(int)TAG_STATE.BATTERY, i].Value.ToString();
                            switch (str)
                            {
                                case "OK":
                                    dataGridView1[(int)TAG_STATE.BATTERY, i].Style.BackColor = Color.LightGreen;
                                    dataGridView1[(int)TAG_STATE.BATTERY, i].Style.ForeColor = Color.Black;
                                    break;
                                case "Warning":
                                    dataGridView1[(int)TAG_STATE.BATTERY, i].Style.BackColor = Color.Yellow;
                                    dataGridView1[(int)TAG_STATE.BATTERY, i].Style.ForeColor = Color.Black;
                                    break;
                                case "Failing":
                                    dataGridView1[(int)TAG_STATE.BATTERY, i].Style.BackColor = Color.Red;
                                    dataGridView1[(int)TAG_STATE.BATTERY, i].Style.ForeColor = Color.White;
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                }
            }
        }


        //private void timer1_Tick(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        timer1.Stop(); // もしくは timer.Enabled = false;

        //        if (backgroundWorker1.IsBusy)
        //        {
        //            Console.WriteLine("busy");
        //            timer1.Start(); // もしくは timer.Enabled = true;
        //            return;
        //        }

        //        backgroundWorker1.WorkerReportsProgress = true;

        //        backgroundWorker1.RunWorkerAsync(0);
        //    }
        //    finally
        //    {
        //        timer1.Start(); // もしくは timer.Enabled = true;
        //    }
        //}

        //private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        //{
        //    BackgroundWorker bgWorker = (BackgroundWorker)sender;

        //    bgWorker.ReportProgress(0);

        //}

        //private void backgroundWorker1_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        //{
        //    SetLocationAction();
        //}

        public void UpdateTagNo()
        {
            var doc = new XmlDocument();
            doc.Load(@"Setting.xml");

            XmlNodeList nodeList;
            XmlNode rootElement = doc.DocumentElement;

            // 作業員のタグと名称を設定する
            nodeList = rootElement.SelectNodes("USER");
            int index = 0;
            foreach (XmlNode nd in nodeList)
            {
                dataGridView1[(int)TAG_STATE.TAG, index].Value = nd.SelectSingleNode("TAG").InnerText;
                dataGridView1[(int)TAG_STATE.NAME, index].Value = nd.SelectSingleNode("NAME").InnerText;
                index = index + 1;
            }

            // エリアのタグと名称を設定する
            nodeList = rootElement.SelectNodes("AREA");
            foreach (XmlNode nd in nodeList)
            {
                dataGridView1[(int)TAG_STATE.TAG, index].Value = nd.SelectSingleNode("TAG").InnerText;
                dataGridView1[(int)TAG_STATE.NAME, index].Value = nd.SelectSingleNode("NAME").InnerText;
                index = index + 1;
            }

            // 重機のタグと名称を取得する
            nodeList = rootElement.SelectNodes("MACHINE");
            foreach (XmlNode nd in nodeList)
            {
                dataGridView1[(int)TAG_STATE.TAG, index].Value = nd.SelectSingleNode("TAGFL").InnerText;
                dataGridView1[(int)TAG_STATE.NAME, index].Value = nd.SelectSingleNode("NAME").InnerText + "(左前)";
                index = index + 1;
                dataGridView1[(int)TAG_STATE.TAG, index].Value = nd.SelectSingleNode("TAGFR").InnerText;
                dataGridView1[(int)TAG_STATE.NAME, index].Value = nd.SelectSingleNode("NAME").InnerText + "(右前)";
                index = index + 1;
                dataGridView1[(int)TAG_STATE.TAG, index].Value = nd.SelectSingleNode("TAGBL").InnerText;
                dataGridView1[(int)TAG_STATE.NAME, index].Value = nd.SelectSingleNode("NAME").InnerText + "(左後)";
                index = index + 1;
                dataGridView1[(int)TAG_STATE.TAG, index].Value = nd.SelectSingleNode("TAGBR").InnerText;
                dataGridView1[(int)TAG_STATE.NAME, index].Value = nd.SelectSingleNode("NAME").InnerText + "(右後)";
                index = index + 1;
            }
        }

        private void FormTagState_Activated(object sender, EventArgs e)
        {
            // バッテリー情報更新
            更新ToolStripMenuItem.PerformClick();
        }
    }
}
