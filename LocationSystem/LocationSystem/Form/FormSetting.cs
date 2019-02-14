using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using Microsoft.VisualBasic;
using System.Text.RegularExpressions;
using System.Collections;

namespace LocationSystem
{
    public partial class FormSetting : Form
    {
        enum USER
        {
            NO = 0,
            USE,
            TAG,
            NAME,
            MAX
        }
        enum AREA
        {
            NO = 0,
            USE,
            TAG,
            NAME,
            PTIP,
            OFFSETUPDOWN,
            OFFSETRIGHTLEFT,
            RADIUS1,
            PTCOLOR1,
            PTPATTERN1,
            PTBUZZER1,
            RADIUS2,
            PTCOLOR2,
            PTPATTERN2,
            PTBUZZER2,
            MAX
        }
        enum MACHINE
        {
            NO = 0,
            USE,
            TAGFL,
            TAGFR,
            TAGBL,
            TAGBR,
            NAME,
            PTIP,
            OFFSETUPDOWNFL,
            OFFSETRIGHTLEFTFL,
            OFFSETUPDOWNFR,
            OFFSETRIGHTLEFTFR,
            OFFSETUPDOWNBL,
            OFFSETRIGHTLEFTBL,
            OFFSETUPDOWNBR,
            OFFSETRIGHTLEFTBR,
            WIDTH,
            HEIGHT,
            RADIUS1,
            PTCOLOR1,
            PTPATTERN1,
            PTBUZZER1,
            RADIUS2,
            PTCOLOR2,
            PTPATTERN2,
            PTBUZZER2,
            MAX
        }

        object[] objBefore;
        object[] objAfter;
        int MAX_OBJ = 1024;

        public FormSetting()
        {
            InitializeComponent();
        }

        private void FormSetting_Load(object sender, EventArgs e)
        {
            // 比較用に十分大きい配列を用意する
            objBefore = new object[MAX_OBJ];
            objAfter = new object[MAX_OBJ];

            // スタイルを設定する
            customHeaderDataGridView1.EnableHeadersVisualStyles = false;
            customHeaderDataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.PaleGreen;

            dataSet1.ReadXml(FILE_PATH);
            customHeaderDataGridView1.DataSource = dataSet1;

            buttonUser.PerformClick();


        }

        public static readonly string FILE_PATH = @"Setting.xml";
        private void buttonUser_Click(object sender, EventArgs e)
        {

            // 初期化する
            for (int i = 0; i < objAfter.Count(); i++)
            {
                objAfter[i] = null;
            }
            // 直前のデータを取得する
            int k = 0;
            for (int i = 0; i < customHeaderDataGridView1.Rows.Count; i++)
            {
                DataGridViewRow r = customHeaderDataGridView1.Rows[i];
                for (int j = 0; j < customHeaderDataGridView1.Columns.Count; j++)
                {
                    objAfter[k] = r.Cells[j].Value;
                    //Console.Write(r.Cells[j].Value);
                    //Console.WriteLine();
                    k = k + 1;
                }
            }
            // 編集の有無を確認する
            if (objBefore != null)
            {
                bool isEqual = ((IStructuralEquatable)objBefore).Equals(objAfter, StructuralComparisons.StructuralEqualityComparer);

                if (!isEqual)
                {
                    // 描画を一時停止する
                    FormMain.FormMainInstance.StopDraw();
                    DialogResult result = MessageBox.Show("編集中の内容は破棄されますが、編集画面を切り替えますか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                    // 描画を再開する
                    FormMain.FormMainInstance.StartDraw();
                    if (result == DialogResult.No)
                    {
                        return;
                    }
                }

            }

            buttonUser.Enabled = false;
            buttonArea.Enabled = true;
            buttonMachine.Enabled = true;

            customHeaderDataGridView1.Columns.Clear();
            customHeaderDataGridView1.HeaderCells.Clear();
            dataSet1.Clear();
            dataSet1.ReadXml(FILE_PATH);
            customHeaderDataGridView1.DataMember = "USER";

            // ヘッダの設定
            var xmlDoc = new XmlDocument();
            xmlDoc.Load(@"Setting.xml");
            //xmlDoc.Load(@"System.xml");
            XmlElement rootElement = xmlDoc.DocumentElement;
            XmlNodeList nodelistNoText = rootElement.GetElementsByTagName("NOTEXT");
            XmlNodeList nodelistUseText = rootElement.GetElementsByTagName("USETEXT");
            XmlNodeList nodelistTagText = rootElement.GetElementsByTagName("TAGTEXT");
            XmlNodeList nodelistNameText = rootElement.GetElementsByTagName("NAMETEXT");

            //ヘッダーセル定義
            this.customHeaderDataGridView1.ColumnHeaderBorderStyle = CustomHeaderDataGridView.HeaderCellBorderStyle.DoubleLine;
            this.customHeaderDataGridView1.ColumnHeaderRowCount = 2;
            this.customHeaderDataGridView1.ColumnHeaderRowHeight = 72;

            var cells = new HeaderCell();
            cells.Column = (int)USER.NO;
            cells.Row = 0;
            cells.ColumnSpan = 1;
            cells.RowSpan = 2;
            cells.SortVisible = true;
            cells.Text = nodelistNoText.Item(0).InnerText;
            cells.TextAlign = DataGridViewContentAlignment.MiddleCenter;
            this.customHeaderDataGridView1.HeaderCells.Add(cells);

            cells = new HeaderCell();
            cells.Column = (int)USER.USE;
            cells.Row = 0;
            cells.ColumnSpan = 1;
            cells.RowSpan = 2;
            cells.SortVisible = true;
            cells.Text = nodelistUseText.Item(0).InnerText;
            cells.TextAlign = DataGridViewContentAlignment.MiddleCenter;
            this.customHeaderDataGridView1.HeaderCells.Add(cells);

            cells = new HeaderCell();
            cells.Column = (int)USER.TAG;
            cells.Row = 0;
            cells.ColumnSpan = 1;
            cells.RowSpan = 2;
            cells.SortVisible = true;
            cells.Text = nodelistTagText.Item(0).InnerText;
            cells.TextAlign = DataGridViewContentAlignment.MiddleCenter;
            this.customHeaderDataGridView1.HeaderCells.Add(cells);

            cells = new HeaderCell();
            cells.Column = (int)USER.NAME;
            cells.Row = 0;
            cells.ColumnSpan = 1;
            cells.RowSpan = 2;
            cells.SortVisible = true;
            cells.Text = nodelistNameText.Item(0).InnerText;
            cells.TextAlign = DataGridViewContentAlignment.MiddleCenter;
            this.customHeaderDataGridView1.HeaderCells.Add(cells);

            this.customHeaderDataGridView1.Font = new System.Drawing.Font("メイリオ", 12F);

            customHeaderDataGridView1.Columns[0].Width = 40;
            customHeaderDataGridView1.Columns[1].Width = 110;
            customHeaderDataGridView1.Columns[2].Width = 160;
            customHeaderDataGridView1.Columns[3].Width = 160;


            //自動的に並び替えられないようにする
            foreach (DataGridViewColumn c in customHeaderDataGridView1.Columns)
                c.SortMode = DataGridViewColumnSortMode.NotSortable;
            customHeaderDataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.PaleGreen;

            //ヘッダーサイズモード
            this.customHeaderDataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;

            this.customHeaderDataGridView1.AllowUserToAddRows = false;
            this.customHeaderDataGridView1.AllowUserToDeleteRows = false;

            buttonUser.BackColor = Color.LightGreen; 
            buttonArea.BackColor = SystemColors.Control;
            buttonMachine.BackColor = SystemColors.Control;

            //DataGridView1の1番目の列を読み取り専用にする
            customHeaderDataGridView1.Columns[0].ReadOnly = true;
            customHeaderDataGridView1.Columns[0].DefaultCellStyle.SelectionForeColor = ForeColor;
            customHeaderDataGridView1.Columns[0].DefaultCellStyle.SelectionBackColor = BackColor;

            // 初期化する
            for (int i = 0; i < objBefore.Count(); i++)
            {
                objBefore[i] = null;
            }
            // 表示時のデータを取得する
            k = 0;
            for (int i = 0; i < customHeaderDataGridView1.Rows.Count; i++)
            {
                DataGridViewRow r = customHeaderDataGridView1.Rows[i];
                for (int j = 0; j < customHeaderDataGridView1.Columns.Count; j++)
                {
                    objBefore[k] = r.Cells[j].Value;
                    //Console.Write(r.Cells[j].Value);
                    //Console.WriteLine();
                    k = k + 1;
                }
            }

        }

        private void buttonArea_Click(object sender, EventArgs e)
        {
            // 初期化する
            for (int i = 0; i < objAfter.Count(); i++)
            {
                objAfter[i] = null;
            }
            // 直前のデータを取得する
            int k = 0;
            for (int i = 0; i < customHeaderDataGridView1.Rows.Count; i++)
            {
                DataGridViewRow r = customHeaderDataGridView1.Rows[i];
                for (int j = 0; j < customHeaderDataGridView1.Columns.Count; j++)
                {
                    objAfter[k] = r.Cells[j].Value;
                    //Console.Write(r.Cells[j].Value);
                    //Console.WriteLine();
                    k = k + 1;
                }
            }
            // 編集の有無を確認する
            if (objBefore != null)
            {
                bool isEqual = ((IStructuralEquatable)objBefore).Equals(objAfter, StructuralComparisons.StructuralEqualityComparer);

                if (!isEqual)
                {
                    // 描画を一時停止する
                    FormMain.FormMainInstance.StopDraw();
                    DialogResult result = MessageBox.Show("編集中の内容は破棄されますが、編集画面を切り替えますか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                    // 描画を再開する
                    FormMain.FormMainInstance.StartDraw();
                    if (result == DialogResult.No)
                    {
                        return;
                    }
                }

            }


            buttonUser.Enabled = true;
            buttonArea.Enabled = false;
            buttonMachine.Enabled = true;

            customHeaderDataGridView1.Columns.Clear();
            customHeaderDataGridView1.HeaderCells.Clear();
            dataSet1.Clear();
            dataSet1.ReadXml(FILE_PATH);
            customHeaderDataGridView1.DataMember = "AREA";
            

            // ヘッダの設定
            var xmlDoc = new XmlDocument();
            xmlDoc.Load(@"Setting.xml");
            XmlElement rootElement = xmlDoc.DocumentElement;
            XmlNodeList nodelistNoText = rootElement.GetElementsByTagName("NOTEXT");
            XmlNodeList nodelistUseText = rootElement.GetElementsByTagName("USETEXT");
            XmlNodeList nodelistTagText = rootElement.GetElementsByTagName("TAGTEXT");
            XmlNodeList nodelistNameText = rootElement.GetElementsByTagName("NAMETEXT");
            XmlNodeList nodelistPtipText = rootElement.GetElementsByTagName("PTIPTEXT");
            XmlNodeList nodelistOffsetUpDownText = rootElement.GetElementsByTagName("OFFSETUPDOWNTEXT");
            XmlNodeList nodelistOffsetRightLeftText = rootElement.GetElementsByTagName("OFFSETRIGHTLEFTTEXT");
            XmlNodeList nodelistZone1Text = rootElement.GetElementsByTagName("ZONE1TEXT");
            XmlNodeList nodelistRadius1Text = rootElement.GetElementsByTagName("RADIUS1TEXT");
            XmlNodeList nodelistPtColor1Text = rootElement.GetElementsByTagName("PTCOLOR1TEXT");
            XmlNodeList nodelistPtPattern1Text = rootElement.GetElementsByTagName("PTPATTERN1TEXT");
            XmlNodeList nodelistPtBuzzer1Text = rootElement.GetElementsByTagName("PTBUZZER1TEXT");
            XmlNodeList nodelistZone2Text = rootElement.GetElementsByTagName("ZONE2TEXT");
            XmlNodeList nodelistRadius2Text = rootElement.GetElementsByTagName("RADIUS2TEXT");
            XmlNodeList nodelistPtColor2Text = rootElement.GetElementsByTagName("PTCOLOR2TEXT");
            XmlNodeList nodelistPtPattern2Text = rootElement.GetElementsByTagName("PTPATTERN2TEXT");
            XmlNodeList nodelistPtBuzzer2Text = rootElement.GetElementsByTagName("PTBUZZER2TEXT");

            //ヘッダーセル定義
            this.customHeaderDataGridView1.ColumnHeaderRowCount = 2;
            this.customHeaderDataGridView1.ColumnHeaderRowHeight = 72;

            var cells = new HeaderCell();
            cells.Column = (int)AREA.NO;
            cells.Row = 0;
            cells.ColumnSpan = 1;
            cells.RowSpan = 2;
            cells.SortVisible = true;
            cells.Text = nodelistNoText.Item(0).InnerText;
            cells.TextAlign = DataGridViewContentAlignment.MiddleCenter;
            this.customHeaderDataGridView1.HeaderCells.Add(cells);

            cells = new HeaderCell();
            cells.Column = (int)AREA.USE;
            cells.Row = 0;
            cells.ColumnSpan = 1;
            cells.RowSpan = 2;
            cells.SortVisible = true;
            cells.Text = nodelistUseText.Item(0).InnerText;
            cells.TextAlign = DataGridViewContentAlignment.MiddleCenter;
            this.customHeaderDataGridView1.HeaderCells.Add(cells);

            cells = new HeaderCell();
            cells.Column = (int)AREA.TAG;
            cells.Row = 0;
            cells.ColumnSpan = 1;
            cells.RowSpan = 2;
            cells.SortVisible = true;
            cells.Text = nodelistTagText.Item(0).InnerText;
            cells.TextAlign = DataGridViewContentAlignment.MiddleCenter;
            this.customHeaderDataGridView1.HeaderCells.Add(cells);

            cells = new HeaderCell();
            cells.Column = (int)AREA.NAME;
            cells.Row = 0;
            cells.ColumnSpan = 1;
            cells.RowSpan = 2;
            cells.SortVisible = true;
            cells.Text = nodelistNameText.Item(0).InnerText;
            cells.TextAlign = DataGridViewContentAlignment.MiddleCenter;
            this.customHeaderDataGridView1.HeaderCells.Add(cells);

            cells = new HeaderCell();
            cells.Column = (int)AREA.PTIP;
            cells.Row = 0;
            cells.ColumnSpan = 1;
            cells.RowSpan = 2;
            cells.SortVisible = true;
            cells.Text = nodelistPtipText.Item(0).InnerText;
            cells.TextAlign = DataGridViewContentAlignment.MiddleCenter;
            this.customHeaderDataGridView1.HeaderCells.Add(cells);

            cells = new HeaderCell();
            cells.Column = (int)AREA.OFFSETUPDOWN;
            cells.Row = 0;
            cells.ColumnSpan = 1;
            cells.RowSpan = 2;
            cells.SortVisible = true;
            cells.Text = nodelistOffsetUpDownText.Item(0).InnerText;
            cells.TextAlign = DataGridViewContentAlignment.MiddleCenter;
            this.customHeaderDataGridView1.HeaderCells.Add(cells);

            cells = new HeaderCell();
            cells.Column = (int)AREA.OFFSETRIGHTLEFT;
            cells.Row = 0;
            cells.ColumnSpan = 1;
            cells.RowSpan = 2;
            cells.SortVisible = true;
            cells.Text = nodelistOffsetRightLeftText.Item(0).InnerText;
            cells.TextAlign = DataGridViewContentAlignment.MiddleCenter;
            this.customHeaderDataGridView1.HeaderCells.Add(cells);

            // 第１ゾーンヘッダー
            cells = new HeaderCell();
            cells.Column = (int)AREA.RADIUS1;
            cells.Row = 0;
            cells.ColumnSpan = 4;
            cells.RowSpan = 1;
            cells.SortVisible = true;
            cells.Text = nodelistZone1Text.Item(0).InnerText;
            cells.TextAlign = DataGridViewContentAlignment.MiddleCenter;
            cells.BackgroundColor = Color.Yellow;
            this.customHeaderDataGridView1.HeaderCells.Add(cells);

            cells = new HeaderCell();
            cells.Column = (int)AREA.RADIUS1;
            cells.Row = 1;
            cells.ColumnSpan = 1;
            cells.RowSpan = 1;
            cells.SortVisible = true;
            cells.Text = nodelistRadius1Text.Item(0).InnerText;
            cells.TextAlign = DataGridViewContentAlignment.MiddleCenter;
            cells.BackgroundColor = Color.Yellow;
            this.customHeaderDataGridView1.HeaderCells.Add(cells);

            cells = new HeaderCell();
            cells.Column = (int)AREA.PTCOLOR1;
            cells.Row = 1;
            cells.ColumnSpan = 1;
            cells.RowSpan = 1;
            cells.SortVisible = true;
            cells.Text = nodelistPtColor1Text.Item(0).InnerText;
            cells.TextAlign = DataGridViewContentAlignment.MiddleCenter;
            cells.BackgroundColor = Color.Yellow;
            this.customHeaderDataGridView1.HeaderCells.Add(cells);

            cells = new HeaderCell();
            cells.Column = (int)AREA.PTPATTERN1;
            cells.Row = 1;
            cells.ColumnSpan = 1;
            cells.RowSpan = 1;
            cells.SortVisible = true;
            cells.Text = nodelistPtPattern1Text.Item(0).InnerText;
            cells.TextAlign = DataGridViewContentAlignment.MiddleCenter;
            cells.BackgroundColor = Color.Yellow;
            this.customHeaderDataGridView1.HeaderCells.Add(cells);

            cells = new HeaderCell();
            cells.Column = (int)AREA.PTBUZZER1;
            cells.Row = 1;
            cells.ColumnSpan = 1;
            cells.RowSpan = 1;
            cells.SortVisible = true;
            cells.Text = nodelistPtBuzzer1Text.Item(0).InnerText;
            cells.TextAlign = DataGridViewContentAlignment.MiddleCenter;
            cells.BackgroundColor = Color.Yellow;
            this.customHeaderDataGridView1.HeaderCells.Add(cells);

            // 第２ゾーンヘッダー
            cells = new HeaderCell();
            cells.Column = (int)AREA.RADIUS2;
            cells.Row = 0;
            cells.ColumnSpan = 4;
            cells.RowSpan = 1;
            cells.SortVisible = true;
            cells.Text = nodelistZone2Text.Item(0).InnerText;
            cells.TextAlign = DataGridViewContentAlignment.MiddleCenter;
            cells.BackgroundColor = Color.Red;
            cells.ForeColor = Color.White;
            this.customHeaderDataGridView1.HeaderCells.Add(cells);

            cells = new HeaderCell();
            cells.Column = (int)AREA.RADIUS2;
            cells.Row = 1;
            cells.ColumnSpan = 1;
            cells.RowSpan = 1;
            cells.SortVisible = true;
            cells.Text = nodelistRadius2Text.Item(0).InnerText;
            cells.TextAlign = DataGridViewContentAlignment.MiddleCenter;
            cells.BackgroundColor = Color.Red;
            cells.ForeColor = Color.White;
            this.customHeaderDataGridView1.HeaderCells.Add(cells);

            cells = new HeaderCell();
            cells.Column = (int)AREA.PTCOLOR2;
            cells.Row = 1;
            cells.ColumnSpan = 1;
            cells.RowSpan = 1;
            cells.SortVisible = true;
            cells.Text = nodelistPtColor2Text.Item(0).InnerText;
            cells.TextAlign = DataGridViewContentAlignment.MiddleCenter;
            cells.BackgroundColor = Color.Red;
            cells.ForeColor = Color.White;
            this.customHeaderDataGridView1.HeaderCells.Add(cells);

            cells = new HeaderCell();
            cells.Column = (int)AREA.PTPATTERN2;
            cells.Row = 1;
            cells.ColumnSpan = 1;
            cells.RowSpan = 1;
            cells.SortVisible = true;
            cells.Text = nodelistPtPattern2Text.Item(0).InnerText;
            cells.TextAlign = DataGridViewContentAlignment.MiddleCenter;
            cells.BackgroundColor = Color.Red;
            cells.ForeColor = Color.White;
            this.customHeaderDataGridView1.HeaderCells.Add(cells);

            cells = new HeaderCell();
            cells.Column = (int)AREA.PTBUZZER2;
            cells.Row = 1;
            cells.ColumnSpan = 1;
            cells.RowSpan = 1;
            cells.SortVisible = true;
            cells.Text = nodelistPtBuzzer2Text.Item(0).InnerText;
            cells.TextAlign = DataGridViewContentAlignment.MiddleCenter;
            cells.BackgroundColor = Color.Red;
            cells.ForeColor = Color.White;
            this.customHeaderDataGridView1.HeaderCells.Add(cells);
            
            this.customHeaderDataGridView1.Font = new Font("メイリオ", 12F);

            customHeaderDataGridView1.Columns[(int)AREA.NO].Width = 40;
            customHeaderDataGridView1.Columns[(int)AREA.USE].Width = 110;
            customHeaderDataGridView1.Columns[(int)AREA.TAG].Width = 160;
            customHeaderDataGridView1.Columns[(int)AREA.NAME].Width = 160;
            customHeaderDataGridView1.Columns[(int)AREA.PTIP].Width = 160;
            customHeaderDataGridView1.Columns[(int)AREA.OFFSETUPDOWN].Width = 110;
            customHeaderDataGridView1.Columns[(int)AREA.OFFSETRIGHTLEFT].Width = 110;
            customHeaderDataGridView1.Columns[(int)AREA.RADIUS1].Width = 80;
            customHeaderDataGridView1.Columns[(int)AREA.PTCOLOR1].Width = 140;
            customHeaderDataGridView1.Columns[(int)AREA.PTPATTERN1].Width = 200;
            customHeaderDataGridView1.Columns[(int)AREA.PTBUZZER1].Width = 110;
            customHeaderDataGridView1.Columns[(int)AREA.RADIUS2].Width = 80;
            customHeaderDataGridView1.Columns[(int)AREA.PTCOLOR2].Width = 140;
            customHeaderDataGridView1.Columns[(int)AREA.PTPATTERN2].Width = 200;
            customHeaderDataGridView1.Columns[(int)AREA.PTBUZZER2].Width = 110;

            //自動的に並び替えられないようにする
            foreach (DataGridViewColumn c in customHeaderDataGridView1.Columns)
                c.SortMode = DataGridViewColumnSortMode.NotSortable;

            //ヘッダーサイズモード
            this.customHeaderDataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;

            this.customHeaderDataGridView1.AllowUserToAddRows = false;
            this.customHeaderDataGridView1.AllowUserToDeleteRows = false;

            buttonUser.BackColor = SystemColors.Control;
            buttonArea.BackColor = Color.LightGreen;
            buttonMachine.BackColor = SystemColors.Control;

            //DataGridView1の1番目の列を読み取り専用にする
            customHeaderDataGridView1.Columns[0].ReadOnly = true;
            customHeaderDataGridView1.Columns[0].DefaultCellStyle.SelectionForeColor = ForeColor;
            customHeaderDataGridView1.Columns[0].DefaultCellStyle.SelectionBackColor = BackColor;

            // 初期化する
            for (int i = 0; i < objBefore.Count(); i++)
            {
                objBefore[i] = null;
            }
            // 表示時のデータを取得する
            k = 0;
            for (int i = 0; i < customHeaderDataGridView1.Rows.Count; i++)
            {
                DataGridViewRow r = customHeaderDataGridView1.Rows[i];
                for (int j = 0; j < customHeaderDataGridView1.Columns.Count; j++)
                {
                    objBefore[k] = r.Cells[j].Value;
                    //Console.Write(r.Cells[j].Value);
                    //Console.WriteLine();
                    k = k + 1;
                }
            }
        }

        private void buttonMachine_Click(object sender, EventArgs e)
        {
            // 初期化する
            for (int i = 0; i < objAfter.Count(); i++)
            {
                objAfter[i] = null;
            }
            // 直前のデータを取得する
            int k = 0;
            for (int i = 0; i < customHeaderDataGridView1.Rows.Count; i++)
            {
                DataGridViewRow r = customHeaderDataGridView1.Rows[i];
                for (int j = 0; j < customHeaderDataGridView1.Columns.Count; j++)
                {
                    objAfter[k] = r.Cells[j].Value;
                    //Console.Write(r.Cells[j].Value);
                    //Console.WriteLine();
                    k = k + 1;
                }
            }
            // 編集の有無を確認する
            if (objBefore != null)
            {
                bool isEqual = ((IStructuralEquatable)objBefore).Equals(objAfter, StructuralComparisons.StructuralEqualityComparer);

                if (!isEqual)
                {
                    // 描画を一時停止する
                    FormMain.FormMainInstance.StopDraw();
                    DialogResult result = MessageBox.Show("編集中の内容は破棄されますが、編集画面を切り替えますか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                    // 描画を再開する
                    FormMain.FormMainInstance.StartDraw();
                    if (result == DialogResult.No)
                    {
                        return;
                    }
                }

            }



            buttonUser.Enabled = true;
            buttonArea.Enabled = true;
            buttonMachine.Enabled = false;

            customHeaderDataGridView1.Columns.Clear();
            customHeaderDataGridView1.HeaderCells.Clear();
            dataSet1.Clear();
            dataSet1.ReadXml(FILE_PATH);

            customHeaderDataGridView1.DataMember = "MACHINE";
            //自動的に並び替えられないようにする
            foreach (DataGridViewColumn c in customHeaderDataGridView1.Columns)
                c.SortMode = DataGridViewColumnSortMode.NotSortable;

            // ヘッダの設定
            var xmlDoc = new XmlDocument();
            xmlDoc.Load(@"Setting.xml");
            XmlElement rootElement = xmlDoc.DocumentElement;
            XmlNodeList nodelistNoText = rootElement.GetElementsByTagName("NOTEXT");
            XmlNodeList nodelistUseText = rootElement.GetElementsByTagName("USETEXT");
            XmlNodeList nodelistTagFlText = rootElement.GetElementsByTagName("TAGFLTEXT");
            XmlNodeList nodelistTagFrText = rootElement.GetElementsByTagName("TAGFRTEXT");
            XmlNodeList nodelistTagBlText = rootElement.GetElementsByTagName("TAGBLTEXT");
            XmlNodeList nodelistTagBrText = rootElement.GetElementsByTagName("TAGBRTEXT");
            XmlNodeList nodelistNameText = rootElement.GetElementsByTagName("NAMETEXT");
            XmlNodeList nodelistPtipText = rootElement.GetElementsByTagName("PTIPTEXT");
            XmlNodeList nodelistOffsetUpDownFLText = rootElement.GetElementsByTagName("OFFSETUPDOWNFLTEXT");
            XmlNodeList nodelistOffsetRightLeftFLText = rootElement.GetElementsByTagName("OFFSETRIGHTLEFTFLTEXT");
            XmlNodeList nodelistOffsetUpDownFRText = rootElement.GetElementsByTagName("OFFSETUPDOWNFRTEXT");
            XmlNodeList nodelistOffsetRightLeftFRText = rootElement.GetElementsByTagName("OFFSETRIGHTLEFTFRTEXT");
            XmlNodeList nodelistOffsetUpDownBLText = rootElement.GetElementsByTagName("OFFSETUPDOWNBLTEXT");
            XmlNodeList nodelistOffsetRightLeftBLText = rootElement.GetElementsByTagName("OFFSETRIGHTLEFTBLTEXT");
            XmlNodeList nodelistOffsetUpDownBRText = rootElement.GetElementsByTagName("OFFSETUPDOWNBRTEXT");
            XmlNodeList nodelistOffsetRightLeftBRText = rootElement.GetElementsByTagName("OFFSETRIGHTLEFTBRTEXT");
            XmlNodeList nodelistWidthText = rootElement.GetElementsByTagName("WIDTHTEXT");
            XmlNodeList nodelistHeightText = rootElement.GetElementsByTagName("HEIGHTTEXT");
            XmlNodeList nodelistZone1Text = rootElement.GetElementsByTagName("ZONE1TEXT");
            XmlNodeList nodelistRadius1Text = rootElement.GetElementsByTagName("RADIUS1TEXT");
            XmlNodeList nodelistPtColor1Text = rootElement.GetElementsByTagName("PTCOLOR1TEXT");
            XmlNodeList nodelistPtPattern1Text = rootElement.GetElementsByTagName("PTPATTERN1TEXT");
            XmlNodeList nodelistPtBuzzer1Text = rootElement.GetElementsByTagName("PTBUZZER1TEXT");
            XmlNodeList nodelistZone2Text = rootElement.GetElementsByTagName("ZONE2TEXT");
            XmlNodeList nodelistRadius2Text = rootElement.GetElementsByTagName("RADIUS2TEXT");
            XmlNodeList nodelistPtColor2Text = rootElement.GetElementsByTagName("PTCOLOR2TEXT");
            XmlNodeList nodelistPtPattern2Text = rootElement.GetElementsByTagName("PTPATTERN2TEXT");
            XmlNodeList nodelistPtBuzzer2Text = rootElement.GetElementsByTagName("PTBUZZER2TEXT");

            //ヘッダーセル定義
            this.customHeaderDataGridView1.ColumnHeaderBorderStyle = CustomHeaderDataGridView.HeaderCellBorderStyle.DoubleLine;
            this.customHeaderDataGridView1.ColumnHeaderRowCount = 2;
            this.customHeaderDataGridView1.ColumnHeaderRowHeight = 72;

            var cells = new HeaderCell();
            cells.Column = (int)MACHINE.NO;
            cells.Row = 0;
            cells.ColumnSpan = 1;
            cells.RowSpan = 2;
            cells.SortVisible = true;
            cells.Text = nodelistNoText.Item(0).InnerText;
            cells.TextAlign = DataGridViewContentAlignment.MiddleCenter;
            this.customHeaderDataGridView1.HeaderCells.Add(cells);

            cells = new HeaderCell();
            cells.Column = (int)MACHINE.USE;
            cells.Row = 0;
            cells.ColumnSpan = 1;
            cells.RowSpan = 2;
            cells.SortVisible = true;
            cells.Text = nodelistUseText.Item(0).InnerText;
            cells.TextAlign = DataGridViewContentAlignment.MiddleCenter;
            this.customHeaderDataGridView1.HeaderCells.Add(cells);

            cells = new HeaderCell();
            cells.Column = (int)MACHINE.TAGFL;
            cells.Row = 0;
            cells.ColumnSpan = 1;
            cells.RowSpan = 2;
            cells.SortVisible = true;
            cells.Text = nodelistTagFlText.Item(0).InnerText;
            cells.TextAlign = DataGridViewContentAlignment.MiddleCenter;
            this.customHeaderDataGridView1.HeaderCells.Add(cells);

            cells = new HeaderCell();
            cells.Column = (int)MACHINE.TAGFR;
            cells.Row = 0;
            cells.ColumnSpan = 1;
            cells.RowSpan = 2;
            cells.SortVisible = true;
            cells.Text = nodelistTagFrText.Item(0).InnerText;
            cells.TextAlign = DataGridViewContentAlignment.MiddleCenter;
            this.customHeaderDataGridView1.HeaderCells.Add(cells);

            cells = new HeaderCell();
            cells.Column = (int)MACHINE.TAGBL;
            cells.Row = 0;
            cells.ColumnSpan = 1;
            cells.RowSpan = 2;
            cells.SortVisible = true;
            cells.Text = nodelistTagBlText.Item(0).InnerText;
            cells.TextAlign = DataGridViewContentAlignment.MiddleCenter;
            this.customHeaderDataGridView1.HeaderCells.Add(cells);

            cells = new HeaderCell();
            cells.Column = (int)MACHINE.TAGBR;
            cells.Row = 0;
            cells.ColumnSpan = 1;
            cells.RowSpan = 2;
            cells.SortVisible = true;
            cells.Text = nodelistTagBrText.Item(0).InnerText;
            cells.TextAlign = DataGridViewContentAlignment.MiddleCenter;
            this.customHeaderDataGridView1.HeaderCells.Add(cells);

            cells = new HeaderCell();
            cells.Column = (int)MACHINE.NAME;
            cells.Row = 0;
            cells.ColumnSpan = 1;
            cells.RowSpan = 2;
            cells.SortVisible = true;
            cells.Text = nodelistNameText.Item(0).InnerText;
            cells.TextAlign = DataGridViewContentAlignment.MiddleCenter;
            this.customHeaderDataGridView1.HeaderCells.Add(cells);

            cells = new HeaderCell();
            cells.Column = (int)MACHINE.PTIP;
            cells.Row = 0;
            cells.ColumnSpan = 1;
            cells.RowSpan = 2;
            cells.SortVisible = true;
            cells.Text = nodelistPtipText.Item(0).InnerText;
            cells.TextAlign = DataGridViewContentAlignment.MiddleCenter;
            this.customHeaderDataGridView1.HeaderCells.Add(cells);

            cells = new HeaderCell();
            cells.Column = (int)MACHINE.OFFSETUPDOWNFL;
            cells.Row = 0;
            cells.ColumnSpan = 1;
            cells.RowSpan = 2;
            cells.SortVisible = true;
            cells.Text = nodelistOffsetUpDownFLText.Item(0).InnerText;
            cells.TextAlign = DataGridViewContentAlignment.MiddleCenter;
            this.customHeaderDataGridView1.HeaderCells.Add(cells);

            cells = new HeaderCell();
            cells.Column = (int)MACHINE.OFFSETRIGHTLEFTFL;
            cells.Row = 0;
            cells.ColumnSpan = 1;
            cells.RowSpan = 2;
            cells.SortVisible = true;
            cells.Text = nodelistOffsetRightLeftFLText.Item(0).InnerText;
            cells.TextAlign = DataGridViewContentAlignment.MiddleCenter;
            this.customHeaderDataGridView1.HeaderCells.Add(cells);

            cells = new HeaderCell();
            cells.Column = (int)MACHINE.OFFSETUPDOWNFR;
            cells.Row = 0;
            cells.ColumnSpan = 1;
            cells.RowSpan = 2;
            cells.SortVisible = true;
            cells.Text = nodelistOffsetUpDownFRText.Item(0).InnerText;
            cells.TextAlign = DataGridViewContentAlignment.MiddleCenter;
            this.customHeaderDataGridView1.HeaderCells.Add(cells);

            cells = new HeaderCell();
            cells.Column = (int)MACHINE.OFFSETRIGHTLEFTFR;
            cells.Row = 0;
            cells.ColumnSpan = 1;
            cells.RowSpan = 2;
            cells.SortVisible = true;
            cells.Text = nodelistOffsetRightLeftFRText.Item(0).InnerText;
            cells.TextAlign = DataGridViewContentAlignment.MiddleCenter;
            this.customHeaderDataGridView1.HeaderCells.Add(cells);

            cells = new HeaderCell();
            cells.Column = (int)MACHINE.OFFSETUPDOWNBL;
            cells.Row = 0;
            cells.ColumnSpan = 1;
            cells.RowSpan = 2;
            cells.SortVisible = true;
            cells.Text = nodelistOffsetUpDownBLText.Item(0).InnerText;
            cells.TextAlign = DataGridViewContentAlignment.MiddleCenter;
            this.customHeaderDataGridView1.HeaderCells.Add(cells);

            cells = new HeaderCell();
            cells.Column = (int)MACHINE.OFFSETRIGHTLEFTBL;
            cells.Row = 0;
            cells.ColumnSpan = 1;
            cells.RowSpan = 2;
            cells.SortVisible = true;
            cells.Text = nodelistOffsetRightLeftBLText.Item(0).InnerText;
            cells.TextAlign = DataGridViewContentAlignment.MiddleCenter;
            this.customHeaderDataGridView1.HeaderCells.Add(cells);

            cells = new HeaderCell();
            cells.Column = (int)MACHINE.OFFSETUPDOWNBR;
            cells.Row = 0;
            cells.ColumnSpan = 1;
            cells.RowSpan = 2;
            cells.SortVisible = true;
            cells.Text = nodelistOffsetUpDownBRText.Item(0).InnerText;
            cells.TextAlign = DataGridViewContentAlignment.MiddleCenter;
            this.customHeaderDataGridView1.HeaderCells.Add(cells);

            cells = new HeaderCell();
            cells.Column = (int)MACHINE.OFFSETRIGHTLEFTBR;
            cells.Row = 0;
            cells.ColumnSpan = 1;
            cells.RowSpan = 2;
            cells.SortVisible = true;
            cells.Text = nodelistOffsetRightLeftBRText.Item(0).InnerText;
            cells.TextAlign = DataGridViewContentAlignment.MiddleCenter;
            this.customHeaderDataGridView1.HeaderCells.Add(cells);

            cells = new HeaderCell();
            cells.Column = (int)MACHINE.WIDTH;
            cells.Row = 0;
            cells.ColumnSpan = 1;
            cells.RowSpan = 2;
            cells.SortVisible = true;
            cells.Text = nodelistWidthText.Item(0).InnerText;
            cells.TextAlign = DataGridViewContentAlignment.MiddleCenter;
            this.customHeaderDataGridView1.HeaderCells.Add(cells);

            cells = new HeaderCell();
            cells.Column = (int)MACHINE.HEIGHT;
            cells.Row = 0;
            cells.ColumnSpan = 1;
            cells.RowSpan = 2;
            cells.SortVisible = true;
            cells.Text = nodelistHeightText.Item(0).InnerText;
            cells.TextAlign = DataGridViewContentAlignment.MiddleCenter;
            this.customHeaderDataGridView1.HeaderCells.Add(cells);

            cells = new HeaderCell();
            cells.Column = (int)MACHINE.RADIUS1;
            cells.Row = 0;
            cells.ColumnSpan = 4;
            cells.RowSpan = 1;
            cells.SortVisible = true;
            cells.Text = nodelistZone1Text.Item(0).InnerText;
            cells.TextAlign = DataGridViewContentAlignment.MiddleCenter;
            cells.BackgroundColor = Color.Yellow;
            this.customHeaderDataGridView1.HeaderCells.Add(cells);

            cells = new HeaderCell();
            cells.Column = (int)MACHINE.RADIUS1;
            cells.Row = 1;
            cells.ColumnSpan = 1;
            cells.RowSpan = 1;
            cells.SortVisible = true;
            cells.Text = nodelistRadius1Text.Item(0).InnerText;
            cells.TextAlign = DataGridViewContentAlignment.MiddleCenter;
            cells.BackgroundColor = Color.Yellow;
            this.customHeaderDataGridView1.HeaderCells.Add(cells);

            cells = new HeaderCell();
            cells.Column = (int)MACHINE.PTCOLOR1;
            cells.Row = 1;
            cells.ColumnSpan = 1;
            cells.RowSpan = 1;
            cells.SortVisible = true;
            cells.Text = nodelistPtColor1Text.Item(0).InnerText;
            cells.TextAlign = DataGridViewContentAlignment.MiddleCenter;
            cells.BackgroundColor = Color.Yellow;
            this.customHeaderDataGridView1.HeaderCells.Add(cells);

            cells = new HeaderCell();
            cells.Column = (int)MACHINE.PTPATTERN1;
            cells.Row = 1;
            cells.ColumnSpan = 1;
            cells.RowSpan = 1;
            cells.SortVisible = true;
            cells.Text = nodelistPtPattern1Text.Item(0).InnerText;
            cells.TextAlign = DataGridViewContentAlignment.MiddleCenter;
            cells.BackgroundColor = Color.Yellow;
            this.customHeaderDataGridView1.HeaderCells.Add(cells);

            cells = new HeaderCell();
            cells.Column = (int)MACHINE.PTBUZZER1;
            cells.Row = 1;
            cells.ColumnSpan = 1;
            cells.RowSpan = 1;
            cells.SortVisible = true;
            cells.Text = nodelistPtBuzzer1Text.Item(0).InnerText;
            cells.TextAlign = DataGridViewContentAlignment.MiddleCenter;
            cells.BackgroundColor = Color.Yellow;
            this.customHeaderDataGridView1.HeaderCells.Add(cells);

            cells = new HeaderCell();
            cells.Column = (int)MACHINE.RADIUS2;
            cells.Row = 0;
            cells.ColumnSpan = 4;
            cells.RowSpan = 1;
            cells.SortVisible = true;
            cells.Text = nodelistZone2Text.Item(0).InnerText;
            cells.TextAlign = DataGridViewContentAlignment.MiddleCenter;
            cells.BackgroundColor = Color.Red;
            cells.ForeColor = Color.White;
            this.customHeaderDataGridView1.HeaderCells.Add(cells);

            cells = new HeaderCell();
            cells.Column = (int)MACHINE.RADIUS2;
            cells.Row = 1;
            cells.ColumnSpan = 1;
            cells.RowSpan = 1;
            cells.SortVisible = true;
            cells.Text = nodelistRadius2Text.Item(0).InnerText;
            cells.TextAlign = DataGridViewContentAlignment.MiddleCenter;
            cells.BackgroundColor = Color.Red;
            cells.ForeColor = Color.White;
            this.customHeaderDataGridView1.HeaderCells.Add(cells);

            cells = new HeaderCell();
            cells.Column = (int)MACHINE.PTCOLOR2;
            cells.Row = 1;
            cells.ColumnSpan = 1;
            cells.RowSpan = 1;
            cells.SortVisible = true;
            cells.Text = nodelistPtColor2Text.Item(0).InnerText;
            cells.TextAlign = DataGridViewContentAlignment.MiddleCenter;
            cells.BackgroundColor = Color.Red;
            cells.ForeColor = Color.White;
            this.customHeaderDataGridView1.HeaderCells.Add(cells);

            cells = new HeaderCell();
            cells.Column = (int)MACHINE.PTPATTERN2;
            cells.Row = 1;
            cells.ColumnSpan = 1;
            cells.RowSpan = 1;
            cells.SortVisible = true;
            cells.Text = nodelistPtPattern2Text.Item(0).InnerText;
            cells.TextAlign = DataGridViewContentAlignment.MiddleCenter;
            cells.BackgroundColor = Color.Red;
            cells.ForeColor = Color.White;
            this.customHeaderDataGridView1.HeaderCells.Add(cells);

            cells = new HeaderCell();
            cells.Column = (int)MACHINE.PTBUZZER2;
            cells.Row = 1;
            cells.ColumnSpan = 1;
            cells.RowSpan = 1;
            cells.SortVisible = true;
            cells.Text = nodelistPtBuzzer2Text.Item(0).InnerText;
            cells.TextAlign = DataGridViewContentAlignment.MiddleCenter;
            cells.BackgroundColor = Color.Red;
            cells.ForeColor = Color.White;
            this.customHeaderDataGridView1.HeaderCells.Add(cells);
            this.customHeaderDataGridView1.Font = new Font(this.Font, FontStyle.Regular);
            
            this.customHeaderDataGridView1.Font = new System.Drawing.Font("メイリオ", 12F);
            customHeaderDataGridView1.Columns[(int)MACHINE.NO].Width = 40;
            customHeaderDataGridView1.Columns[(int)MACHINE.USE].Width = 110;
            customHeaderDataGridView1.Columns[(int)MACHINE.TAGFL].Width = 160;
            customHeaderDataGridView1.Columns[(int)MACHINE.TAGFR].Width = 160;
            customHeaderDataGridView1.Columns[(int)MACHINE.TAGBL].Width = 160;
            customHeaderDataGridView1.Columns[(int)MACHINE.TAGBR].Width = 160;
            customHeaderDataGridView1.Columns[(int)MACHINE.NAME].Width = 160;
            customHeaderDataGridView1.Columns[(int)MACHINE.PTIP].Width = 160;
            customHeaderDataGridView1.Columns[(int)MACHINE.OFFSETUPDOWNFL].Width = 110;
            customHeaderDataGridView1.Columns[(int)MACHINE.OFFSETRIGHTLEFTFL].Width = 110;
            customHeaderDataGridView1.Columns[(int)MACHINE.OFFSETUPDOWNFR].Width = 110;
            customHeaderDataGridView1.Columns[(int)MACHINE.OFFSETRIGHTLEFTFR].Width = 110;
            customHeaderDataGridView1.Columns[(int)MACHINE.OFFSETUPDOWNBL].Width = 110;
            customHeaderDataGridView1.Columns[(int)MACHINE.OFFSETRIGHTLEFTBL].Width = 110;
            customHeaderDataGridView1.Columns[(int)MACHINE.OFFSETUPDOWNBR].Width = 110;
            customHeaderDataGridView1.Columns[(int)MACHINE.OFFSETRIGHTLEFTBR].Width = 110;
            customHeaderDataGridView1.Columns[(int)MACHINE.WIDTH].Width = 110;
            customHeaderDataGridView1.Columns[(int)MACHINE.HEIGHT].Width = 110;
            customHeaderDataGridView1.Columns[(int)MACHINE.RADIUS1].Width = 80;
            customHeaderDataGridView1.Columns[(int)MACHINE.PTCOLOR1].Width = 140;
            customHeaderDataGridView1.Columns[(int)MACHINE.PTPATTERN1].Width = 200;
            customHeaderDataGridView1.Columns[(int)MACHINE.PTBUZZER1].Width = 110;
            customHeaderDataGridView1.Columns[(int)MACHINE.RADIUS2].Width = 80;
            customHeaderDataGridView1.Columns[(int)MACHINE.PTCOLOR2].Width = 140;
            customHeaderDataGridView1.Columns[(int)MACHINE.PTPATTERN2].Width = 200;
            customHeaderDataGridView1.Columns[(int)MACHINE.PTBUZZER2].Width = 110;

            //自動的に並び替えられないようにする
            foreach (DataGridViewColumn c in customHeaderDataGridView1.Columns)
                c.SortMode = DataGridViewColumnSortMode.NotSortable;

            //ヘッダーサイズモード
            this.customHeaderDataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;

            this.customHeaderDataGridView1.AllowUserToAddRows = false;
            this.customHeaderDataGridView1.AllowUserToDeleteRows = false;

            buttonUser.BackColor = SystemColors.Control;
            buttonArea.BackColor = SystemColors.Control;
            buttonMachine.BackColor = Color.LightGreen;

            //DataGridView1の1番目の列を読み取り専用にする
            customHeaderDataGridView1.Columns[0].ReadOnly = true;
            customHeaderDataGridView1.Columns[0].DefaultCellStyle.SelectionForeColor = ForeColor;
            customHeaderDataGridView1.Columns[0].DefaultCellStyle.SelectionBackColor = BackColor;

            // 初期化する
            for (int i = 0; i < objBefore.Count(); i++)
            {
                objBefore[i] = null;
            }
            // 表示時のデータを取得する
            k = 0;
            for (int i = 0; i < customHeaderDataGridView1.Rows.Count; i++)
            {
                DataGridViewRow r = customHeaderDataGridView1.Rows[i];
                for (int j = 0; j < customHeaderDataGridView1.Columns.Count; j++)
                {
                    objBefore[k] = r.Cells[j].Value;
                    //Console.Write(r.Cells[j].Value);
                    //Console.WriteLine();
                    k = k + 1;
                }
            }
        }

        private void buttonWrite_Click(object sender, EventArgs e)
        {
            // 初期化する
            for (int i = 0; i < objAfter.Count(); i++)
            {
                objAfter[i] = null;
            }
            // 直前のデータを取得する
            int k = 0;
            for (int i = 0; i < customHeaderDataGridView1.Rows.Count; i++)
            {
                DataGridViewRow r = customHeaderDataGridView1.Rows[i];
                for (int j = 0; j < customHeaderDataGridView1.Columns.Count; j++)
                {
                    objAfter[k] = r.Cells[j].Value;
                    //Console.Write(r.Cells[j].Value);
                    //Console.WriteLine();
                    k = k + 1;
                }
            }
            // 編集の有無を確認する
            bool isEqual = ((IStructuralEquatable)objBefore).Equals(objAfter, StructuralComparisons.StructuralEqualityComparer);

            if (!isEqual)
            {
                // 描画を一時停止する
                FormMain.FormMainInstance.StopDraw();
                DialogResult result = MessageBox.Show("書き込みますか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                // 描画を再開する
                FormMain.FormMainInstance.StartDraw();
                if (result == DialogResult.No)
                {
                    return;
                }
                else
                {
                    dataSet1.WriteXml(FILE_PATH);
                    for (int i = 0; i < MAX_OBJ; i++)
                    {
                        objBefore[i] = objAfter[i];
                    }
                }
            }
            else
            {
                // 描画を一時停止する
                FormMain.FormMainInstance.StopDraw();
                MessageBox.Show("変更はありません", "確認");
                // 描画を再開する
                FormMain.FormMainInstance.StartDraw();
            }
        }

        private void FormSetting_FormClosing(object sender, FormClosingEventArgs e)
        {
            // 初期化する
            for (int i = 0; i < objAfter.Count(); i++)
            {
                objAfter[i] = null;
            }
            // 直前のデータを取得する
            int k = 0;
            for (int i = 0; i < customHeaderDataGridView1.Rows.Count; i++)
            {
                DataGridViewRow r = customHeaderDataGridView1.Rows[i];
                for (int j = 0; j < customHeaderDataGridView1.Columns.Count; j++)
                {
                    objAfter[k] = r.Cells[j].Value;
                    //Console.Write(r.Cells[j].Value);
                    //Console.WriteLine();
                    k = k + 1;
                }
            }
            // 編集の有無を確認する
            bool isEqual = ((IStructuralEquatable)objBefore).Equals(objAfter, StructuralComparisons.StructuralEqualityComparer);

            if (!isEqual)
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
            }
        }

        #region 入力規制

        //CellValidatingイベントハンドラ
        private void customHeaderDataGridView1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {

            // 半角に変換する
            string InputText = e.FormattedValue.ToString();
            string OutputText = Microsoft.VisualBasic.Strings.StrConv(InputText, VbStrConv.Narrow);

            DataGridView dgv = (DataGridView)sender;

            // 同じ値の場合は検証しない
            if (dgv[e.ColumnIndex, e.RowIndex].Value.ToString() == e.FormattedValue.ToString())
            {
                return;
            }
            
            if (!buttonUser.Enabled)
            {
                // 作業者

                // 入力されていないとき
                if (e.FormattedValue.ToString() == "")
                {
                    //行にエラーテキストを設定
                    dgv.Rows[e.RowIndex].ErrorText = "入力が不正です。";
                    //入力した値をキャンセルして元に戻すには、次のようにする
                    //dgv.CancelEdit();
                    //キャンセルする
                    e.Cancel = true;
                }

                // 使用/未使用
                if (dgv.Columns[e.ColumnIndex].Index == (int)USER.USE)
                {

                    // 0か1以外はキャンセルする
                    if (OutputText != "0" && OutputText != "1")
                    {
                        //行にエラーテキストを設定
                        dgv.Rows[e.RowIndex].ErrorText = "入力が不正です。";
                        //入力した値をキャンセルして元に戻すには、次のようにする
                        //dgv.CancelEdit();
                        //キャンセルする
                        e.Cancel = true;
                    }
                }

                // タグNo
                if (dgv.Columns[e.ColumnIndex].Index == (int)USER.TAG)
                {
                    // ハイフンを統一する
                    OutputText = Regex.Replace(OutputText, @"(\d\d\d)[\p{Pd}\u2212ーｰ](\d\d\d)[\p{Pd}\u2212ーｰ](\d\d\d)[\p{Pd}\u2212ーｰ](\d\d\d)", "$1-$2-$3-$4");

                    // フォーマット(000-000-000-000)を確認する
                    if (!Regex.IsMatch(OutputText, @"\A\d\d\d-\d\d\d-\d\d\d-\d\d\d\z"))
                    {
                        //行にエラーテキストを設定
                        dgv.Rows[e.RowIndex].ErrorText = "入力が不正です。";
                        //入力した値をキャンセルして元に戻すには、次のようにする
                        //dgv.CancelEdit();
                        //キャンセルする
                        e.Cancel = true;
                    }
                }

                // 名称
                if (dgv.Columns[e.ColumnIndex].Index == (int)USER.NAME)
                {
                    // 文字数を確認する
                    int maxLength = 16;
                    if (e.FormattedValue.ToString().Length > maxLength)
                    {
                        //行にエラーテキストを設定
                        dgv.Rows[e.RowIndex].ErrorText = "入力が不正です。";
                        //入力した値をキャンセルして元に戻すには、次のようにする
                        //dgv.CancelEdit();
                        //キャンセルする
                        e.Cancel = true;
                    }
                }

            }
            else if (!buttonArea.Enabled)
            {
                // エリア

                // 使用/未使用
                if (dgv.Columns[e.ColumnIndex].Index == (int)AREA.USE)
                {
                    // 0か1以外はキャンセルする
                    if (OutputText != "0" && OutputText != "1")
                    {
                        //行にエラーテキストを設定
                        dgv.Rows[e.RowIndex].ErrorText = "入力が不正です。";
                        //入力した値をキャンセルして元に戻すには、次のようにする
                        //dgv.CancelEdit();
                        //キャンセルする
                        e.Cancel = true;
                    }
                }

                // タグNo
                if (dgv.Columns[e.ColumnIndex].Index == (int)AREA.TAG)
                {
                    // ハイフンを統一する
                    OutputText = Regex.Replace(OutputText, @"(\d\d\d)[\p{Pd}\u2212ーｰ](\d\d\d)[\p{Pd}\u2212ーｰ](\d\d\d)[\p{Pd}\u2212ーｰ](\d\d\d)", "$1-$2-$3-$4");

                    // フォーマット(000-000-000-000)を確認する
                    if (!Regex.IsMatch(OutputText, @"\A\d\d\d-\d\d\d-\d\d\d-\d\d\d\z"))
                    {
                        //行にエラーテキストを設定
                        dgv.Rows[e.RowIndex].ErrorText = "入力が不正です。";
                        //入力した値をキャンセルして元に戻すには、次のようにする
                        //dgv.CancelEdit();
                        //キャンセルする
                        e.Cancel = true;
                    }
                }

                // 名称
                if (dgv.Columns[e.ColumnIndex].Index == (int)AREA.NAME)
                {
                    // 文字数を確認する
                    int maxLength = 16;
                    if (e.FormattedValue.ToString().Length > maxLength)
                    {
                        //行にエラーテキストを設定
                        dgv.Rows[e.RowIndex].ErrorText = "入力が不正です。";
                        //入力した値をキャンセルして元に戻すには、次のようにする
                        //dgv.CancelEdit();
                        //キャンセルする
                        e.Cancel = true;
                    }
                }

                // 使用パトライトIP
                if (dgv.Columns[e.ColumnIndex].Index == (int)AREA.PTIP)
                {
                    if (!CheckIPAddress(OutputText))
                    {
                        //行にエラーテキストを設定
                        dgv.Rows[e.RowIndex].ErrorText = "入力が不正です。";
                        //入力した値をキャンセルして元に戻すには、次のようにする
                        //dgv.CancelEdit();
                        //キャンセルする
                        e.Cancel = true;
                    }
                }

                // オフセット　上下　左右
                if (dgv.Columns[e.ColumnIndex].Index == (int)AREA.OFFSETUPDOWN || dgv.Columns[e.ColumnIndex].Index == (int)AREA.OFFSETRIGHTLEFT)
                {

                    //数値に変換できるか確かめる
                    double d;
                    if (!double.TryParse(OutputText, out d) )
                    {
                        //行にエラーテキストを設定
                        dgv.Rows[e.RowIndex].ErrorText = "入力が不正です。";
                        //入力した値をキャンセルして元に戻すには、次のようにする
                        //dgv.CancelEdit();
                        //キャンセルする
                        e.Cancel = true;
                    }

                    if(d < -100 || d > 100)
                    {
                        //行にエラーテキストを設定
                        dgv.Rows[e.RowIndex].ErrorText = "入力が不正です。";
                        //入力した値をキャンセルして元に戻すには、次のようにする
                        //dgv.CancelEdit();
                        //キャンセルする
                        e.Cancel = true;
                    }
                }

                // 半径
                if (dgv.Columns[e.ColumnIndex].Index == (int)AREA.RADIUS1 || dgv.Columns[e.ColumnIndex].Index == (int)AREA.RADIUS2)
                {

                    //数値に変換できるか確かめる
                    double d;
                    if (!double.TryParse(OutputText, out d))
                    {
                        //行にエラーテキストを設定
                        dgv.Rows[e.RowIndex].ErrorText = "入力が不正です。";
                        //入力した値をキャンセルして元に戻すには、次のようにする
                        //dgv.CancelEdit();
                        //キャンセルする
                        e.Cancel = true;
                    }

                    if (d < 0 || d > 100)
                    {
                        //行にエラーテキストを設定
                        dgv.Rows[e.RowIndex].ErrorText = "入力が不正です。";
                        //入力した値をキャンセルして元に戻すには、次のようにする
                        //dgv.CancelEdit();
                        //キャンセルする
                        e.Cancel = true;
                    }

                }

                // パトライト点灯色
                if (dgv.Columns[e.ColumnIndex].Index == (int)AREA.PTCOLOR1 || dgv.Columns[e.ColumnIndex].Index == (int)AREA.PTCOLOR2)
                {
                    // 0か1か2以外はキャンセルする
                    if (OutputText != "0" && OutputText != "1" && OutputText != "2")
                    {
                        //行にエラーテキストを設定
                        dgv.Rows[e.RowIndex].ErrorText = "入力が不正です。";
                        //入力した値をキャンセルして元に戻すには、次のようにする
                        //dgv.CancelEdit();
                        //キャンセルする
                        e.Cancel = true;
                    }
                }

                // パトライト動作パターン
                if (dgv.Columns[e.ColumnIndex].Index == (int)AREA.PTPATTERN1 || dgv.Columns[e.ColumnIndex].Index == (int)AREA.PTPATTERN2)
                {
                    // 0か1か2以外はキャンセルする
                    if (OutputText != "0" && OutputText != "1" && OutputText != "2")
                    {
                        //行にエラーテキストを設定
                        dgv.Rows[e.RowIndex].ErrorText = "入力が不正です。";
                        //入力した値をキャンセルして元に戻すには、次のようにする
                        //dgv.CancelEdit();
                        //キャンセルする
                        e.Cancel = true;
                    }
                }

                // ブザー吹鳴
                if (dgv.Columns[e.ColumnIndex].Index == (int)AREA.PTBUZZER1 || dgv.Columns[e.ColumnIndex].Index == (int)AREA.PTBUZZER2)
                {
                    // 0か1以外はキャンセルする
                    if (OutputText != "0" && OutputText != "1")
                    {
                        //行にエラーテキストを設定
                        dgv.Rows[e.RowIndex].ErrorText = "入力が不正です。";
                        //入力した値をキャンセルして元に戻すには、次のようにする
                        //dgv.CancelEdit();
                        //キャンセルする
                        e.Cancel = true;
                    }
                }
            }
            else if(!buttonMachine.Enabled)
            {
                // 重機

                // 使用/未使用
                if (dgv.Columns[e.ColumnIndex].Index == (int)MACHINE.USE)
                {
                    // 0か1以外はキャンセルする
                    if (OutputText != "0" && OutputText != "1")
                    {
                        //行にエラーテキストを設定
                        dgv.Rows[e.RowIndex].ErrorText = "入力が不正です。";
                        //入力した値をキャンセルして元に戻すには、次のようにする
                        //dgv.CancelEdit();
                        //キャンセルする
                        e.Cancel = true;
                    }
                }

                // タグNo
                if (dgv.Columns[e.ColumnIndex].Index == (int)MACHINE.TAGFL || dgv.Columns[e.ColumnIndex].Index == (int)MACHINE.TAGFR || dgv.Columns[e.ColumnIndex].Index == (int)MACHINE.TAGBL || dgv.Columns[e.ColumnIndex].Index == (int)MACHINE.TAGBR)
                {
                    // ハイフンを統一する
                    OutputText = Regex.Replace(OutputText, @"(\d\d\d)[\p{Pd}\u2212ーｰ](\d\d\d)[\p{Pd}\u2212ーｰ](\d\d\d)[\p{Pd}\u2212ーｰ](\d\d\d)", "$1-$2-$3-$4");

                    // フォーマット(000-000-000-000)を確認する
                    if (!Regex.IsMatch(OutputText, @"\A\d\d\d-\d\d\d-\d\d\d-\d\d\d\z"))
                    {
                        //行にエラーテキストを設定
                        dgv.Rows[e.RowIndex].ErrorText = "入力が不正です。";
                        //入力した値をキャンセルして元に戻すには、次のようにする
                        //dgv.CancelEdit();
                        //キャンセルする
                        e.Cancel = true;
                    }
                }

                // 名称
                if (dgv.Columns[e.ColumnIndex].Index == (int)MACHINE.NAME)
                {
                    // 文字数を確認する
                    int maxLength = 16;
                    if (e.FormattedValue.ToString().Length > maxLength)
                    {
                        //行にエラーテキストを設定
                        dgv.Rows[e.RowIndex].ErrorText = "入力が不正です。";
                        //入力した値をキャンセルして元に戻すには、次のようにする
                        //dgv.CancelEdit();
                        //キャンセルする
                        e.Cancel = true;
                    }
                }

                // 使用パトライトIP
                if (dgv.Columns[e.ColumnIndex].Index == (int)MACHINE.PTIP)
                {
                    if (!CheckIPAddress(OutputText))
                    {
                        //行にエラーテキストを設定
                        dgv.Rows[e.RowIndex].ErrorText = "入力が不正です。";
                        //入力した値をキャンセルして元に戻すには、次のようにする
                        //dgv.CancelEdit();
                        //キャンセルする
                        e.Cancel = true;
                    }
                }

                // オフセット　上下　左右
                if (dgv.Columns[e.ColumnIndex].Index == (int)MACHINE.OFFSETUPDOWNFL || dgv.Columns[e.ColumnIndex].Index == (int)MACHINE.OFFSETRIGHTLEFTFL ||
                    dgv.Columns[e.ColumnIndex].Index == (int)MACHINE.OFFSETUPDOWNFR || dgv.Columns[e.ColumnIndex].Index == (int)MACHINE.OFFSETRIGHTLEFTFR ||
                    dgv.Columns[e.ColumnIndex].Index == (int)MACHINE.OFFSETUPDOWNBL || dgv.Columns[e.ColumnIndex].Index == (int)MACHINE.OFFSETRIGHTLEFTBL ||
                    dgv.Columns[e.ColumnIndex].Index == (int)MACHINE.OFFSETUPDOWNBR || dgv.Columns[e.ColumnIndex].Index == (int)MACHINE.OFFSETRIGHTLEFTBR)
                {

                    //数値に変換できるか確かめる
                    double d;
                    if (!double.TryParse(OutputText, out d))
                    {
                        //行にエラーテキストを設定
                        dgv.Rows[e.RowIndex].ErrorText = "入力が不正です。";
                        //入力した値をキャンセルして元に戻すには、次のようにする
                        //dgv.CancelEdit();
                        //キャンセルする
                        e.Cancel = true;
                    }

                    if (d < -100 || d > 100)
                    {
                        //行にエラーテキストを設定
                        dgv.Rows[e.RowIndex].ErrorText = "入力が不正です。";
                        //入力した値をキャンセルして元に戻すには、次のようにする
                        //dgv.CancelEdit();
                        //キャンセルする
                        e.Cancel = true;
                    }
                }

                // 幅 高さ
                if (dgv.Columns[e.ColumnIndex].Index == (int)MACHINE.WIDTH || dgv.Columns[e.ColumnIndex].Index == (int)MACHINE.HEIGHT)
                {

                    //数値に変換できるか確かめる
                    double d;
                    if (!double.TryParse(OutputText, out d))
                    {
                        //行にエラーテキストを設定
                        dgv.Rows[e.RowIndex].ErrorText = "入力が不正です。";
                        //入力した値をキャンセルして元に戻すには、次のようにする
                        //dgv.CancelEdit();
                        //キャンセルする
                        e.Cancel = true;
                    }

                    if (d < 0.001 || d > 50)
                    {
                        //行にエラーテキストを設定
                        dgv.Rows[e.RowIndex].ErrorText = "入力が不正です。";
                        //入力した値をキャンセルして元に戻すには、次のようにする
                        //dgv.CancelEdit();
                        //キャンセルする
                        e.Cancel = true;
                    }
                }                

                // 半径
                if (dgv.Columns[e.ColumnIndex].Index == (int)MACHINE.RADIUS1 || dgv.Columns[e.ColumnIndex].Index == (int)MACHINE.RADIUS2)
                {

                    //数値に変換できるか確かめる
                    double d;
                    if (!double.TryParse(OutputText, out d))
                    {
                        //行にエラーテキストを設定
                        dgv.Rows[e.RowIndex].ErrorText = "入力が不正です。";
                        //入力した値をキャンセルして元に戻すには、次のようにする
                        //dgv.CancelEdit();
                        //キャンセルする
                        e.Cancel = true;
                    }

                    if (d < 0 || d > 100)
                    {
                        //行にエラーテキストを設定
                        dgv.Rows[e.RowIndex].ErrorText = "入力が不正です。";
                        //入力した値をキャンセルして元に戻すには、次のようにする
                        //dgv.CancelEdit();
                        //キャンセルする
                        e.Cancel = true;
                    }
                }

                // パトライト点灯色
                if (dgv.Columns[e.ColumnIndex].Index == (int)MACHINE.PTCOLOR1 || dgv.Columns[e.ColumnIndex].Index == (int)MACHINE.PTCOLOR2)
                {
                    // 0か1か2以外はキャンセルする
                    if (OutputText != "0" && OutputText != "1" && OutputText != "2")
                    {
                        //行にエラーテキストを設定
                        dgv.Rows[e.RowIndex].ErrorText = "入力が不正です。";
                        //入力した値をキャンセルして元に戻すには、次のようにする
                        //dgv.CancelEdit();
                        //キャンセルする
                        e.Cancel = true;
                    }
                }

                // パトライト動作パターン
                if (dgv.Columns[e.ColumnIndex].Index == (int)MACHINE.PTPATTERN1 || dgv.Columns[e.ColumnIndex].Index == (int)MACHINE.PTPATTERN2)
                {
                    // 0か1か2以外はキャンセルする
                    if (OutputText != "0" && OutputText != "1" && OutputText != "2")
                    {
                        //行にエラーテキストを設定
                        dgv.Rows[e.RowIndex].ErrorText = "入力が不正です。";
                        //入力した値をキャンセルして元に戻すには、次のようにする
                        //dgv.CancelEdit();
                        //キャンセルする
                        e.Cancel = true;
                    }
                }
                // ブザー吹鳴
                if (dgv.Columns[e.ColumnIndex].Index == (int)MACHINE.PTBUZZER1 || dgv.Columns[e.ColumnIndex].Index == (int)MACHINE.PTBUZZER2)
                {
                    // 0か1以外はキャンセルする
                    if (OutputText != "0" && OutputText != "1")
                    {
                        //行にエラーテキストを設定
                        dgv.Rows[e.RowIndex].ErrorText = "入力が不正です。";
                        //入力した値をキャンセルして元に戻すには、次のようにする
                        //dgv.CancelEdit();
                        //キャンセルする
                        e.Cancel = true;
                    }
                }
            }
            else
            {
                // DO NOTHING
            }
        }

        //CellValidatedイベントハンドラ
        private void customHeaderDataGridView1_CellValidated(object sender, DataGridViewCellEventArgs e)
        {

            DataGridView dgv = (DataGridView)sender;

            // 半角に変換する
            string InputText = dgv[e.ColumnIndex, e.RowIndex].Value.ToString();
            string OutputText = Microsoft.VisualBasic.Strings.StrConv(InputText, VbStrConv.Narrow);

            //エラーテキストを消す
            dgv.Rows[e.RowIndex].ErrorText = null;

            if (!buttonUser.Enabled)
            {
                // 作業者
                // 使用/未使用
                if (dgv.Columns[e.ColumnIndex].Index == (int)USER.USE)
                {
                    dgv[e.ColumnIndex, e.RowIndex].Value = OutputText;
                }

                // タグNo
                if (dgv.Columns[e.ColumnIndex].Index == (int)USER.TAG)
                {
                    // ハイフンを統一する
                    dgv[e.ColumnIndex, e.RowIndex].Value = Regex.Replace(OutputText, @"(\d\d\d)[\p{Pd}\u2212ーｰ](\d\d\d)[\p{Pd}\u2212ーｰ](\d\d\d)[\p{Pd}\u2212ーｰ](\d\d\d)", "$1-$2-$3-$4");

                }
            }
            else if (!buttonArea.Enabled)
            {
                // エリア
                // 使用/未使用
                if (dgv.Columns[e.ColumnIndex].Index == (int)AREA.USE)
                {
                    dgv[e.ColumnIndex, e.RowIndex].Value = OutputText;
                }

                // タグNo
                if (dgv.Columns[e.ColumnIndex].Index == (int)AREA.TAG)
                {
                    // ハイフンを統一する
                    dgv[e.ColumnIndex, e.RowIndex].Value = Regex.Replace(OutputText, @"(\d\d\d)[\p{Pd}\u2212ーｰ](\d\d\d)[\p{Pd}\u2212ーｰ](\d\d\d)[\p{Pd}\u2212ーｰ](\d\d\d)", "$1-$2-$3-$4");
                }

                // 使用パトライトIP
                if (dgv.Columns[e.ColumnIndex].Index == (int)AREA.PTIP)
                {
                    dgv[e.ColumnIndex, e.RowIndex].Value = OutputText;
                }

                // オフセット 上下 左右
                if (dgv.Columns[e.ColumnIndex].Index == (int)AREA.OFFSETUPDOWN || dgv.Columns[e.ColumnIndex].Index == (int)AREA.OFFSETRIGHTLEFT)
                {
                    double d;
                    if (double.TryParse(OutputText, out d))
                    {
                        // 四捨五入する。小数点第三位まで出力する。
                        double ret = Math.Round(d, 3, MidpointRounding.AwayFromZero);
                        dgv[e.ColumnIndex, e.RowIndex].Value = ret;
                    }
                }

                // 半径
                if (dgv.Columns[e.ColumnIndex].Index == (int)AREA.RADIUS1 || dgv.Columns[e.ColumnIndex].Index == (int)AREA.RADIUS2)
                {
                    double d;
                    if (double.TryParse(OutputText, out d))
                    {
                        // 四捨五入する。小数点第三位まで出力する。
                        double ret = Math.Round(d, 3, MidpointRounding.AwayFromZero);
                        dgv[e.ColumnIndex, e.RowIndex].Value = ret;
                    }
                }

                // パトライト点灯色
                if (dgv.Columns[e.ColumnIndex].Index == (int)AREA.PTCOLOR1 || dgv.Columns[e.ColumnIndex].Index == (int)AREA.PTCOLOR2)
                {
                    dgv[e.ColumnIndex, e.RowIndex].Value = OutputText;
                }

                // ブザー吹鳴
                if (dgv.Columns[e.ColumnIndex].Index == (int)AREA.PTBUZZER1 || dgv.Columns[e.ColumnIndex].Index == (int)AREA.PTBUZZER2)
                {
                    dgv[e.ColumnIndex, e.RowIndex].Value = OutputText;
                }
            }
            else if (!buttonMachine.Enabled)
            {
                // 重機
                // 使用/未使用
                if (dgv.Columns[e.ColumnIndex].Index == (int)MACHINE.USE)
                {
                    dgv[e.ColumnIndex, e.RowIndex].Value = OutputText;
                }

                // タグNo
                if (dgv.Columns[e.ColumnIndex].Index == (int)MACHINE.TAGFL || dgv.Columns[e.ColumnIndex].Index == (int)MACHINE.TAGFR || dgv.Columns[e.ColumnIndex].Index == (int)MACHINE.TAGBL || dgv.Columns[e.ColumnIndex].Index == (int)MACHINE.TAGBR)
                {
                    // ハイフンを統一する
                    dgv[e.ColumnIndex, e.RowIndex].Value = Regex.Replace(OutputText, @"(\d\d\d)[\p{Pd}\u2212ーｰ](\d\d\d)[\p{Pd}\u2212ーｰ](\d\d\d)[\p{Pd}\u2212ーｰ](\d\d\d)", "$1-$2-$3-$4");
                }

                // 使用パトライトIP
                if (dgv.Columns[e.ColumnIndex].Index == (int)MACHINE.PTIP)
                {
                    dgv[e.ColumnIndex, e.RowIndex].Value = OutputText;
                }

                // オフセット 上下 左右
                if (dgv.Columns[e.ColumnIndex].Index == (int)MACHINE.OFFSETUPDOWNFL || dgv.Columns[e.ColumnIndex].Index == (int)MACHINE.OFFSETRIGHTLEFTFL ||
                    dgv.Columns[e.ColumnIndex].Index == (int)MACHINE.OFFSETUPDOWNFR || dgv.Columns[e.ColumnIndex].Index == (int)MACHINE.OFFSETRIGHTLEFTFR ||
                    dgv.Columns[e.ColumnIndex].Index == (int)MACHINE.OFFSETUPDOWNBL || dgv.Columns[e.ColumnIndex].Index == (int)MACHINE.OFFSETRIGHTLEFTBL ||
                    dgv.Columns[e.ColumnIndex].Index == (int)MACHINE.OFFSETUPDOWNBR || dgv.Columns[e.ColumnIndex].Index == (int)MACHINE.OFFSETRIGHTLEFTBR)
                {
                    double d;
                    if (double.TryParse(OutputText, out d))
                    {
                        // 四捨五入する。小数点第三位まで出力する。
                        double ret = Math.Round(d, 3, MidpointRounding.AwayFromZero);
                        dgv[e.ColumnIndex, e.RowIndex].Value = ret;
                    }
                }

                // 幅　高さ
                if (dgv.Columns[e.ColumnIndex].Index == (int)MACHINE.WIDTH || dgv.Columns[e.ColumnIndex].Index == (int)MACHINE.HEIGHT)
                {
                    double d;
                    if (double.TryParse(OutputText, out d))
                    {
                        // 四捨五入する。小数点第三位まで出力する。
                        double ret = Math.Round(d, 3, MidpointRounding.AwayFromZero);
                        dgv[e.ColumnIndex, e.RowIndex].Value = ret;
                    }
                }

                // 半径
                if (dgv.Columns[e.ColumnIndex].Index == (int)MACHINE.RADIUS1 || dgv.Columns[e.ColumnIndex].Index == (int)MACHINE.RADIUS2)
                {
                    double d;
                    if (double.TryParse(OutputText, out d))
                    {
                        // 四捨五入する。小数点第三位まで出力する。
                        double ret = Math.Round(d, 3, MidpointRounding.AwayFromZero);
                        dgv[e.ColumnIndex, e.RowIndex].Value = ret;
                    }
                }

                // パトライト点灯色
                if (dgv.Columns[e.ColumnIndex].Index == (int)MACHINE.PTCOLOR1 || dgv.Columns[e.ColumnIndex].Index == (int)MACHINE.PTCOLOR2)
                {
                    dgv[e.ColumnIndex, e.RowIndex].Value = OutputText;
                }

                // ブザー吹鳴
                if (dgv.Columns[e.ColumnIndex].Index == (int)MACHINE.PTBUZZER1 || dgv.Columns[e.ColumnIndex].Index == (int)MACHINE.PTBUZZER2)
                {
                    dgv[e.ColumnIndex, e.RowIndex].Value = OutputText;
                }
            }
            else
            {

            }
        }
        //// DataGridViewでセルを選択して全角を入力時、２重に入力されてしまう仕様の対策
        //protected override void OnPreviewKeyDown(PreviewKeyDownEventArgs e)
        //{
        //    // IMEの仮想キーだった場合グリッドを編集状態にする
        //    if (e.KeyValue == (int)ConsoleKey.Process)
        //    {
        //        BeginEdit();
        //    }

        //    base.OnPreviewKeyDown(e);
        //}
        /// <summary>
        /// IPアドレスの形式になっているかチェック
        /// </summary>
        /// <param name="ip">IPアドレス</param>
        /// <returns>t/f</returns>
        public bool CheckIPAddress(string ip)
        {

            //「.」の数を数える
            int count = ip.Length - ip.Replace(".".ToString(), "").Length;

            //「.」の数をチェック
            if (count != 3)
            {
                //「.」が3つではない場合
                return false;
            }

            // 「.」で分割して配列に格納する
            string[] ipSplitData = ip.Split('.');

            // データを確認する
            foreach (string data in ipSplitData)
            {
                // 数字以外が入力されていないかチェック
                try
                {
                    // キャストして数字かチェック
                    int.Parse(data);
                }
                catch
                {
                    // 空白や文字列などが入力されていた場合
                    return false;
                }

                // 0～255の範囲内かチェック
                if (int.Parse(data) < 0 || int.Parse(data) > 255)
                {
                    // 0～255ではない場合
                    return false;
                }
            }

            // IPアドレス形式の場合
            return true;
        }

        private void customHeaderDataGridView1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;

            // IMEの仮想キーだった場合グリッドを編集状態にする
            if (e.KeyValue == (int)ConsoleKey.Process)
            {
                dgv.BeginEdit(false);
            }

            base.OnPreviewKeyDown(e);
        }
        #endregion 入力規制
    }
}
