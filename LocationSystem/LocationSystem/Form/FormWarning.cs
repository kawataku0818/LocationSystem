using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using SysCommon;
using System.IO;

namespace LocationSystem
{
    public partial class FormWarning : Form
    {
        enum ICON
        {
            NEW = 0,
            CHECKED,
            MAX
        }
        
        public string WarningText { get; set; }
        public string LocationDirectoryPath { get; set; }

        public WarningCsvFile wcf;

        private string warningTextBefore;

        public FormWarning()
        {            
            InitializeComponent();
        }

        private void FormWarning_Load(object sender, EventArgs e)
        {
            // 位置を設定する
            int x = FormMain.FormMainInstance.Location.X + FormMain.FormMainInstance.Width - this.Width;
            int y = FormMain.FormMainInstance.Location.Y + FormMain.FormMainInstance.Height - this.Height + 100;
            this.Location = new Point(x, y);


            this.customControlListView1.HeaderStyle = ColumnHeaderStyle.None;

            AddListView();

            // CSV
            if (String.IsNullOrEmpty(LocationDirectoryPath) == true) 
            {
                LocationDirectoryPath = System.Environment.CurrentDirectory;
            }
            wcf = new WarningCsvFile(LocationDirectoryPath);
            this.wcf.Open();
        }

        private void customControlListView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (customControlListView1.SelectedItems.Count > 0)
            {
                //customControlListView1.SelectedItems[0].ImageIndex = (int)ICON.CHECKED;
                //customControlListView1.SelectedItems[0].ForeColor = Color.Blue;
                //customControlListView1.SelectedItems[0].Font = new Font("游ゴシック", 12F, FontStyle.Bold);
            }
        }

        private void FormWarning_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.wcf.Close();
            e.Cancel = true;
            this.Hide();
        }

        private void FormWarning_Resize(object sender, EventArgs e)
        {
            //this.columnHeader1.Width = this.Width;
        }
        void AddListViewAction()
        {

            if (warningTextBefore == WarningText)
            {
                return;
            }
            try
            {
                warningTextBefore = WarningText;
                ListViewItem lv_item = new ListViewItem(WarningText);
                this.customControlListView1.Items.Insert(0,lv_item);
                //this.customControlListView1.Items[0].ImageIndex = 0;
                this.customControlListView1.Items[0].ForeColor = Color.Blue;
                this.customControlListView1.Items[0].Font = new Font("游ゴシック", 12F, FontStyle.Bold);
                this.customControlListView1.Items[0].ToolTipText = WarningText;
                if (WarningText.IndexOf("第２ゾーン") >= 0)
                {
                    this.customControlListView1.Items[0].ForeColor = Color.White;
                    this.customControlListView1.Items[0].BackColor = Color.Red;
                }
                else if(WarningText.IndexOf("第１ゾーン") >= 0)
                {
                    this.customControlListView1.Items[0].ForeColor = Color.Black;
                    this.customControlListView1.Items[0].BackColor = Color.Yellow;
                }
                else
                {
                    this.customControlListView1.Items[0].ForeColor = Color.Black;
                    this.customControlListView1.Items[0].BackColor = Color.White;
                }
                //最初の行を表示する
                this.customControlListView1.Items[0].EnsureVisible();
                //warningTextBefore = WarningText;
                //ListViewItem lv_item = new ListViewItem(WarningText);
                //this.customControlListView1.Items.Add(lv_item);
                //this.customControlListView1.Items[this.customControlListView1.Items.Count - 1].ImageIndex = 0;
                //this.customControlListView1.Items[this.customControlListView1.Items.Count - 1].ForeColor = Color.FromArgb(255, 128, 128);
                //this.customControlListView1.Items[this.customControlListView1.Items.Count - 1].Font = new Font("游ゴシック", 12F, FontStyle.Bold);
                //this.customControlListView1.Items[this.customControlListView1.Items.Count - 1].ToolTipText = WarningText;
                ////最後の行を表示する
                //this.customControlListView1.Items[this.customControlListView1.Items.Count - 1].EnsureVisible();


                if (wcf != null)
                {
                    this.wcf.Write(WarningText);
                }

            }
            catch (Exception e)
            {
                Console.Write("err" + e.ToString());
            }
        }

        private bool running;//スレッド実行フラグ
        async void AddListView()
        {
            running = true;//これ重要

            await Task.Run(() => {
                while (running)
                {
                    // listBox1 にテキストを追加するのに Invoke を通す
                    Invoke(new Action(() => {
                        AddListViewAction();
                    }));
                    //Task.Delay(100).Wait();
                }
            });
        }

        private void クリアCToolStripMenuItem_Click(object sender, EventArgs e)
        {

            // 描画を一時停止する
            FormMain.FormMainInstance.StopDraw();
            DialogResult result = MessageBox.Show("全ての履歴メッセージをクリアしますか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
            // 描画を再開する
            FormMain.FormMainInstance.StartDraw();
            if (result == DialogResult.No)
            {
                return;
            }
            else
            {
                this.customControlListView1.Items.Clear();
            }

            
        }

        private void 既読クリアToolStripMenuItem_Click(object sender, EventArgs e)
        {

            // 描画を一時停止する
            FormMain.FormMainInstance.StopDraw();
            DialogResult result = MessageBox.Show("確認済みのメッセージをクリアしますか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
            // 描画を再開する
            FormMain.FormMainInstance.StartDraw();
            if (result == DialogResult.No)
            {
                return;
            }
            else
            {
                foreach (ListViewItem itemx in customControlListView1.CheckedItems)
                {
                    customControlListView1.Items.Remove(itemx);
                }
            }
            
        }

        public void ReOpen(string path)
        {
            // ファイルが開いていたら閉じる
            FileStream stream = null;
            try
            {
                stream = new FileStream(LocationDirectoryPath, FileMode.Open, FileAccess.ReadWrite, FileShare.None);
            }
            catch (Exception)
            {
                this.wcf.Close();
            }
            finally
            {
                if (stream != null)
                {
                    stream.Close();
                }
            }

            // パスを更新して再度ファイルを開く
            LocationDirectoryPath = path;

            if (wcf != null)
            {
                wcf = null;
            }

            wcf = new WarningCsvFile(LocationDirectoryPath);
            this.wcf.Open();
        }

    }
}
