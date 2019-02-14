using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Security.Permissions;
using SysCommon;
using System.IO;
using System.Xml;
using System.Linq;
using System.Runtime.InteropServices;
using System.Collections;
using System.Data;
using LocationSystem.Class;
using System.Net;

namespace LocationSystem
{

    public partial class FormMain : Form
    {
        enum LOCATION
        {
            TAG = 0,
            STD_ERR,
            X,
            Y,
            Z,
            MAX
        }

        #region public メンバ変数
        //
        // パブリックメンバ変数
        //
        public static FormMain FormMainInstance { get; set; }
        
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

        public string WarningText {get;set;}

        public bool ReportPositionFlg { get; set; }

        public bool[] FlgPt { get; set; }

        public LocationCsvFile lcf;

        public int Number;

        public string Message;

        string LocationDirectoryPath { get; set; }

        int LocationFileSaveDay { get; set; }

        public static readonly string FILE_PATH = @"Setting.xml";

        public SysLogFile sysLogFile;

        // 2018/04/05 Rev1.10 XMLファイルを読み込む start
        Root deserializedObect;
        // 2018/04/05 Rev1.10 XMLファイルを読み込む end


        #endregion public メンバ変数

        #region private メンバ変数
        //
        // プライベートメンバ変数
        //

        // 最大タグ数
        int USER_MAX = 20;
        int AREA_MAX = 5;
        int MACHINE_MAX = 10;

        bool[] userUse;           // 使用/未使用
        string[] userTagNo;       // 作業員タグNo
        string[] userName;    　　// 作業員名称
        double[] userPositionX;
        double[] userPositionY;
        double[] userPositionZ;
        // 2018/04/06 Rev1.10 加重移動平均対応 start
        List<double> userPositionXBuffer;
        List<double> userPositionYBuffer;
        List<double> userPositionZBuffer;
        // 2018/04/06 Rev1.10 加重移動平均対応 end
        double[] viewUserPositionX;
        double[] viewUserPositionY;
        double[] viewUserPositionZ;
        //double[] userStdErr;
        //double[] userPositionXbefore;
        //double[] userPositionYbefore;

        bool[] areaUse;           // 使用/未使用
        string[] areaTagNo;   　　// エリアタグNo
        string[] areaName;    　　// エリア名称
        string[] areaPtIp; 　　　 // エリアパトライトIP
        double[] areaOffsetUpDown;
        double[] areaOffsetRightLeft;
        double[] areaYellowZone;
        int[] areaPtColor1;
        int[] areaPtPattern1;
        bool[] areaPtBuzzer1;
        double[] areaRedZone;
        int[] areaPtColor2;
        int[] areaPtPattern2;
        bool[] areaPtBuzzer2;

        double[] areaPositionX;
        double[] areaPositionY;
        double[] areaPositionZ;
        double[] viewAreaPositionX;
        double[] viewAreaPositionY;

        double[] areaPositionOffsetX;
        double[] areaPositionOffsetY;
        double[] viewAreaPositionOffsetX;
        double[] viewAreaPositionOffsetY;


        bool[] machineUse;          // 使用/未使用
        string[] machineTagNoFL;   // 重機タグNo
        string[] machineTagNoFR;   // 重機タグNo
        string[] machineTagNoBL;   // 重機タグNo
        string[] machineTagNoBR;   // 重機タグNo

        string[] machineName; // 重機名称

        string[] machinePtIp; // 重機パトライトIP

        double[] machineOffsetUpDownFL;
        double[] machineOffsetRightLeftFL;
        double[] machineOffsetUpDownFR;
        double[] machineOffsetRightLeftFR;
        double[] machineOffsetUpDownBL;
        double[] machineOffsetRightLeftBL;
        double[] machineOffsetUpDownBR;
        double[] machineOffsetRightLeftBR;

        double[] machineWidth;
        double[] machineHeight;

        double[] machineYellowZone;
        int[] machinePtColor1;
        int[] machinePtPattern1;
        bool[] machinePtBuzzer1;

        double[] machineRedZone;
        int[] machinePtColor2;
        int[] machinePtPattern2;
        bool[] machinePtBuzzer2;

        double[] machinePositionFLX;
        double[] machinePositionFLY;
        double[] machinePositionFLZ;
        double[] machinePositionFRX;
        double[] machinePositionFRY;
        double[] machinePositionFRZ;
        double[] machinePositionBLX;
        double[] machinePositionBLY;
        double[] machinePositionBLZ;
        double[] machinePositionBRX;
        double[] machinePositionBRY;
        double[] machinePositionBRZ;

        // 2018/04/06 Rev1.10 加重移動平均対応 start
        List<double> machinePositionFLXBuffer;
        List<double> machinePositionFLYBuffer;
        List<double> machinePositionFLZBuffer;
        List<double> machinePositionFRXBuffer;
        List<double> machinePositionFRYBuffer;
        List<double> machinePositionFRZBuffer;
        List<double> machinePositionBLXBuffer;
        List<double> machinePositionBLYBuffer;
        List<double> machinePositionBLZBuffer;
        List<double> machinePositionBRXBuffer;
        List<double> machinePositionBRYBuffer;
        List<double> machinePositionBRZBuffer;
        // 2018/04/06 Rev1.10 加重移動平均対応 end

        double[] viewMachinePositionFLX;
        double[] viewMachinePositionFLY;
        double[] viewMachinePositionFRX;
        double[] viewMachinePositionFRY;
        double[] viewMachinePositionBLX;
        double[] viewMachinePositionBLY;
        double[] viewMachinePositionBRX;
        double[] viewMachinePositionBRY;

        double[] machinePositionOffsetFLX;
        double[] machinePositionOffsetFLY;
        double[] machinePositionOffsetFRX;
        double[] machinePositionOffsetFRY;
        double[] machinePositionOffsetBLX;
        double[] machinePositionOffsetBLY;
        double[] machinePositionOffsetBRX;
        double[] machinePositionOffsetBRY;

        double[] viewMachinePositionOffsetFLX;
        double[] viewMachinePositionOffsetFLY;
        double[] viewMachinePositionOffsetFRX;
        double[] viewMachinePositionOffsetFRY;
        double[] viewMachinePositionOffsetBLX;
        double[] viewMachinePositionOffsetBLY;
        double[] viewMachinePositionOffsetBRX;
        double[] viewMachinePositionOffsetBRY;

        double[] machinePositionCx;
        double[] machinePositionCy;

        double[] viewMachinePositionCx;
        double[] viewMachinePositionCy;

        float[] viewMachineWidth;
        float[] viewMachineHeight;

        double[] stdErrFL;
        double[] stdErrFR;
        double[] stdErrBL;
        double[] stdErrBR;

        //double[] machineYaw;

        //double[] machinePositionFLXbefore;
        //double[] machinePositionFLYbefore;
        //double[] machinePositionFRXbefore;
        //double[] machinePositionFRYbefore;
        //double[] machinePositionBLXbefore;
        //double[] machinePositionBLYbefore;
        //double[] machinePositionBRXbefore;
        //double[] machinePositionBRYbefore;

        // 位置情報CSVファイル
        DateTime LocationFileCheckTime;

        // エリア固定
        bool[] checkBoxAreaLocks;

        // タグ状態画面
        FormTagState formTagState;

        // 侵入履歴画面
        FormWarning formWarning;

        // 誤差の少ない重機タグ算出用
        double tmpStdErr;
        double tmpStdErr2;

        // タグ状態画面更新用フラグ
        bool flgUpdateTagState = true;

        //static bool ptResult = false;

        // 位置イベントから取得する位置データ
        ArrayList listLocation;
        string[] locationString;

        // 描画用
        Bitmap canvas;
        Bitmap bitmap;
        Bitmap bitmapBackImage;
        Bitmap bitmapMachineImage;
        Bitmap bitmapUserImage;

        Graphics g;
        Pen yellowPen;
        Pen redPen;
        Pen blackPen;
        Brush yellowBrush;
        Brush redBrush;
        Font font;

        // 侵入検知用フラグ
        // 立ち入り禁止エリアに作業員が入っているか判断フラグ
        bool[,] flgAreaInZone1User;
        bool[,] flgAreaInZone2User;
        // 立ち入り禁止エリアに重機が入っているか判断フラグ
        bool[,] flgAreaInZone1Machine;
        bool[,] flgAreaInZone2Machine;

        // 重機に作業員が入っているか判断フラグ
        bool[,] flgMachineInZone1User;
        bool[,] flgMachineInZone2User;
        // 重機に重機が入っているか判断フラグ
        bool[,] flgMachineInZone1Machine;
        bool[,] flgMachineInZone2Machine;
        //int[] flgPtMachine;

        //bool[] writeHistoryFlg0;
        //bool[] writeHistoryFlg1;
        //bool[] writeHistoryFlg2;

        // 立ち入り禁止エリアに作業員が入った時のメッセージ判断フラグ
        bool[,] flgAreaInMessage0User;
        bool[,] flgAreaInMessage1User;
        bool[,] flgAreaInMessage2User;
        // 立ち入り禁止エリアに重機が入った時のメッセージ判断フラグ
        bool[,] flgAreaInMessage0Machine;
        bool[,] flgAreaInMessage1Machine;
        bool[,] flgAreaInMessage2Machine;
        // 立ち入り禁止エリアパトライト点灯フラグ
        bool[] flgOnPt0Area;
        bool[] flgOnPt1Area;
        bool[] flgOnPt2Area;


        // 重機に作業員が入った時のメッセージ判断フラグ
        bool[,] flgMachineInMessage0User;
        bool[,] flgMachineInMessage1User;
        bool[,] flgMachineInMessage2User;
        // 重機に重機が入った時のメッセージ判断フラグ
        bool[,] flgMachineInMessage0Machine;
        bool[,] flgMachineInMessage1Machine;
        bool[,] flgMachineInMessage2Machine;
        // 重機パトライト点灯フラグ
        bool[] flgOnPt0Machine;
        bool[] flgOnPt1Machine;
        bool[] flgOnPt2Machine;

        int[] areaInZoneState;

        int[] machineInZoneState;

        // 重機対角線
        double[] machineDiagonalLength;
        float[] viewmachineDiagonalLength;
        double[] machineDiagonalLengthHalf;
        float[] viewmachineDiagonalLengthHalf;
        double[] machineDiagonalAngle;

        // 重機中心点算出用
        double angle, angle2, angle3;




        // 2018/04/05 Rev1.10 加重移動平均バッファ数 start
        // XMLファイルで設定する
        int WEIGHTED_MOVING_AVERAGE_NUM_BUFFER_USER = 10;
        int WEIGHTED_MOVING_AVERAGE_NUM_BUFFER_MACHINE = 10;
        // 2018/04/05 Rev1.10 加重移動平均バッファ数 end

        //private delegate void delegatePaintDelegate();

        #endregion private メンバ変数

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public FormMain()
        {
            InitializeComponent();
        }

        /// <summary>
        /// デストラクタ
        /// </summary>
        //~FormMain()
        //{
        //    canvas.Dispose();
        //    bitmap.Dispose();
        //    bitmapBackImage.Dispose();
        //    bitmapMachineImage.Dispose();
        //    bitmapUserImage.Dispose();
        //    g.Dispose();
        //    yellowPen.Dispose();
        //    redPen.Dispose();
        //    blackPen.Dispose();
        //    yellowBrush.Dispose();
        //    redBrush.Dispose();
        //    font.Dispose();

        //    formWarning.Dispose();
        //    formTagState.Dispose();
        //    namingSchema.Dispose();
        //    multiCell.Dispose();
        //}

        /// <summary>
        /// メイン画面ロード
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormMain_Load(object sender, EventArgs e)
        {
            listLocation = new ArrayList();
            listLocation.Add("");
            listLocation.Add(0.000);
            listLocation.Add(0.000);
            listLocation.Add(0.000);
            listLocation.Add(0.000);
            listLocation[0] = "";

            locationString = new string[] { "TAG", "ERR", "X", "Y", "Z" };

            
            this.userUse = new bool[USER_MAX];
            this.userTagNo = new string[USER_MAX];
            this.userName = new string[USER_MAX];

            this.userPositionX = new double[USER_MAX];
            this.userPositionY = new double[USER_MAX];
            this.userPositionZ = new double[USER_MAX];


            // 2018/04/06 Rev1.10 加重移動平均対応 start
            this.userPositionXBuffer = new List<double>();
            this.userPositionYBuffer = new List<double>();
            this.userPositionZBuffer = new List<double>();
            // 2018/04/06 Rev1.10 加重移動平均対応 end

            this.viewUserPositionX = new double[USER_MAX];
            this.viewUserPositionY = new double[USER_MAX];
            this.viewUserPositionZ = new double[USER_MAX];

            //this.userStdErr = new double[USER_MAX];

            //this.userPositionXbefore = new double[USER_MAX];
            //this.userPositionYbefore = new double[USER_MAX];


            this.areaUse = new bool[AREA_MAX];
            this.areaTagNo = new string[AREA_MAX];
            this.areaName = new string[AREA_MAX];

            this.areaPtIp = new string[AREA_MAX];

            this.areaOffsetUpDown = new double[AREA_MAX];
            this.areaOffsetRightLeft = new double[AREA_MAX];

            this.areaYellowZone = new double[AREA_MAX];
            this.areaPtColor1 = new int[AREA_MAX];
            this.areaPtPattern1 = new int[AREA_MAX];
            this.areaPtBuzzer1 = new bool[AREA_MAX];

            this.areaRedZone = new double[AREA_MAX];
            this.areaPtColor2 = new int[AREA_MAX];
            this.areaPtPattern2 = new int[AREA_MAX];
            this.areaPtBuzzer2 = new bool[AREA_MAX];

            this.areaPositionX = new double[AREA_MAX];
            this.areaPositionY = new double[AREA_MAX];
            this.areaPositionZ = new double[AREA_MAX];

            this.viewAreaPositionX = new double[AREA_MAX];
            this.viewAreaPositionY = new double[AREA_MAX];

            this.areaPositionOffsetX = new double[AREA_MAX];
            this.areaPositionOffsetY = new double[AREA_MAX];

            this.viewAreaPositionOffsetX = new double[AREA_MAX];
            this.viewAreaPositionOffsetY = new double[AREA_MAX];

            this.machineUse = new bool[MACHINE_MAX];
            this.machineTagNoFL = new string[MACHINE_MAX];
            this.machineTagNoFR = new string[MACHINE_MAX];
            this.machineTagNoBL = new string[MACHINE_MAX];
            this.machineTagNoBR = new string[MACHINE_MAX];
            this.machineName = new string[MACHINE_MAX];

            this.machinePtIp = new string[MACHINE_MAX];

            this.machineOffsetUpDownFL = new double[MACHINE_MAX];
            this.machineOffsetRightLeftFL = new double[MACHINE_MAX];
            this.machineOffsetUpDownFR = new double[MACHINE_MAX];
            this.machineOffsetRightLeftFR = new double[MACHINE_MAX];
            this.machineOffsetUpDownBL = new double[MACHINE_MAX];
            this.machineOffsetRightLeftBL = new double[MACHINE_MAX];
            this.machineOffsetUpDownBR = new double[MACHINE_MAX];
            this.machineOffsetRightLeftBR = new double[MACHINE_MAX];

            this.machineWidth = new double[MACHINE_MAX];
            this.machineHeight = new double[MACHINE_MAX];

            this.viewMachineWidth = new float[MACHINE_MAX];
            this.viewMachineHeight = new float[MACHINE_MAX];

            this.machineYellowZone = new double[MACHINE_MAX];
            this.machinePtColor1 = new int[MACHINE_MAX];
            this.machinePtPattern1 = new int[MACHINE_MAX];
            this.machinePtBuzzer1 = new bool[MACHINE_MAX];

            this.machineRedZone = new double[MACHINE_MAX];
            this.machinePtColor2 = new int[MACHINE_MAX];
            this.machinePtPattern2 = new int[MACHINE_MAX];
            this.machinePtBuzzer2 = new bool[MACHINE_MAX];

            //this.machineYaw = new double[MACHINE_MAX];


            this.machinePositionFLX = new double[MACHINE_MAX];
            this.machinePositionFLY = new double[MACHINE_MAX];
            this.machinePositionFLZ = new double[MACHINE_MAX];
            this.machinePositionFRX = new double[MACHINE_MAX];
            this.machinePositionFRY = new double[MACHINE_MAX];
            this.machinePositionFRZ = new double[MACHINE_MAX];
            this.machinePositionBLX = new double[MACHINE_MAX];
            this.machinePositionBLY = new double[MACHINE_MAX];
            this.machinePositionBLZ = new double[MACHINE_MAX];
            this.machinePositionBRX = new double[MACHINE_MAX];
            this.machinePositionBRY = new double[MACHINE_MAX];
            this.machinePositionBRZ = new double[MACHINE_MAX];

            // 2018/04/06 Rev1.10 加重移動平均対応 start
            this.machinePositionFLXBuffer = new List<double>();
            this.machinePositionFLYBuffer = new List<double>();
            this.machinePositionFLZBuffer = new List<double>();
            this.machinePositionFRXBuffer = new List<double>();
            this.machinePositionFRYBuffer = new List<double>();
            this.machinePositionFRZBuffer = new List<double>();
            this.machinePositionBLXBuffer = new List<double>();
            this.machinePositionBLYBuffer = new List<double>();
            this.machinePositionBLZBuffer = new List<double>();
            this.machinePositionBRXBuffer = new List<double>();
            this.machinePositionBRYBuffer = new List<double>();
            this.machinePositionBRZBuffer = new List<double>();
            // 2018/04/06 Rev1.10 加重移動平均対応 end

            this.viewMachinePositionFLX = new double[MACHINE_MAX];
            this.viewMachinePositionFLY = new double[MACHINE_MAX];
            this.viewMachinePositionFRX = new double[MACHINE_MAX];
            this.viewMachinePositionFRY = new double[MACHINE_MAX];
            this.viewMachinePositionBLX = new double[MACHINE_MAX];
            this.viewMachinePositionBLY = new double[MACHINE_MAX];
            this.viewMachinePositionBRX = new double[MACHINE_MAX];
            this.viewMachinePositionBRY = new double[MACHINE_MAX];

            this.machinePositionOffsetFLX = new double[MACHINE_MAX];
            this.machinePositionOffsetFLY = new double[MACHINE_MAX];
            this.machinePositionOffsetFRX = new double[MACHINE_MAX];
            this.machinePositionOffsetFRY = new double[MACHINE_MAX];
            this.machinePositionOffsetBLX = new double[MACHINE_MAX];
            this.machinePositionOffsetBLY = new double[MACHINE_MAX];
            this.machinePositionOffsetBRX = new double[MACHINE_MAX];
            this.machinePositionOffsetBRY = new double[MACHINE_MAX];

            this.viewMachinePositionOffsetFLX = new double[MACHINE_MAX];
            this.viewMachinePositionOffsetFLY = new double[MACHINE_MAX];
            this.viewMachinePositionOffsetFRX = new double[MACHINE_MAX];
            this.viewMachinePositionOffsetFRY = new double[MACHINE_MAX];
            this.viewMachinePositionOffsetBLX = new double[MACHINE_MAX];
            this.viewMachinePositionOffsetBLY = new double[MACHINE_MAX];
            this.viewMachinePositionOffsetBRX = new double[MACHINE_MAX];
            this.viewMachinePositionOffsetBRY = new double[MACHINE_MAX];

            this.machinePositionCx = new double[MACHINE_MAX];
            this.machinePositionCy = new double[MACHINE_MAX];

            this.viewMachinePositionCx = new double[MACHINE_MAX];
            this.viewMachinePositionCy = new double[MACHINE_MAX];

            machineDiagonalLength = new double[MACHINE_MAX];
            viewmachineDiagonalLength = new float[MACHINE_MAX];
            machineDiagonalLengthHalf = new double[MACHINE_MAX];
            viewmachineDiagonalLengthHalf = new float[MACHINE_MAX];
            machineDiagonalAngle = new double[MACHINE_MAX];

            this.stdErrFL = new double[MACHINE_MAX];
            this.stdErrFR = new double[MACHINE_MAX];
            this.stdErrBL = new double[MACHINE_MAX];
            this.stdErrBR = new double[MACHINE_MAX];
            // エラー値はdoubleの最大値を初期値とする
            for (int i = 0; i < MACHINE_MAX; i++)
            {
                stdErrFL[i] = double.MaxValue;
                stdErrFR[i] = double.MaxValue;
                stdErrBL[i] = double.MaxValue;
                stdErrBR[i] = double.MaxValue;
            }
            //this.machinePositionFLXbefore = new double[MACHINE_MAX];
            //this.machinePositionFLYbefore = new double[MACHINE_MAX];
            //this.machinePositionFRXbefore = new double[MACHINE_MAX];
            //this.machinePositionFRYbefore = new double[MACHINE_MAX];
            //this.machinePositionBLXbefore = new double[MACHINE_MAX];
            //this.machinePositionBLYbefore = new double[MACHINE_MAX];
            //this.machinePositionBRXbefore = new double[MACHINE_MAX];
            //this.machinePositionBRYbefore = new double[MACHINE_MAX];

            checkBoxAreaLocks = new bool[AREA_MAX];

            // 
            flgAreaInZone1User = new bool[AREA_MAX, USER_MAX];
            flgAreaInZone2User = new bool[AREA_MAX, USER_MAX];
            flgAreaInZone1Machine = new bool[AREA_MAX, MACHINE_MAX];
            flgAreaInZone2Machine = new bool[AREA_MAX, MACHINE_MAX];

            flgAreaInMessage0User = new bool[AREA_MAX, USER_MAX];
            flgAreaInMessage1User = new bool[AREA_MAX, USER_MAX];
            flgAreaInMessage2User = new bool[AREA_MAX, USER_MAX];
            flgAreaInMessage0Machine = new bool[AREA_MAX, MACHINE_MAX];
            flgAreaInMessage1Machine = new bool[AREA_MAX, MACHINE_MAX];
            flgAreaInMessage2Machine = new bool[AREA_MAX, MACHINE_MAX];

            flgOnPt0Area = new bool[AREA_MAX];
            flgOnPt1Area = new bool[AREA_MAX];
            flgOnPt2Area = new bool[AREA_MAX];

            areaInZoneState = new int[AREA_MAX];

            //flgPtMachine = new int[MACHINE_MAX];

            //writeHistoryFlg0 = new bool[MACHINE_MAX];
            //writeHistoryFlg1 = new bool[MACHINE_MAX];
            //writeHistoryFlg2 = new bool[MACHINE_MAX];

            flgMachineInZone1User = new bool[MACHINE_MAX, USER_MAX];
            flgMachineInZone2User = new bool[MACHINE_MAX, USER_MAX];
            flgMachineInZone1Machine = new bool[MACHINE_MAX, MACHINE_MAX];
            flgMachineInZone2Machine = new bool[MACHINE_MAX, MACHINE_MAX];

            flgMachineInMessage0User = new bool[MACHINE_MAX, USER_MAX];
            flgMachineInMessage1User = new bool[MACHINE_MAX, USER_MAX];
            flgMachineInMessage2User = new bool[MACHINE_MAX, USER_MAX];
            flgMachineInMessage0Machine = new bool[MACHINE_MAX, MACHINE_MAX];
            flgMachineInMessage1Machine = new bool[MACHINE_MAX, MACHINE_MAX];
            flgMachineInMessage2Machine = new bool[MACHINE_MAX, MACHINE_MAX];

            flgOnPt0Machine = new bool[MACHINE_MAX];
            flgOnPt1Machine = new bool[MACHINE_MAX];
            flgOnPt2Machine = new bool[MACHINE_MAX];

            machineInZoneState = new int[MACHINE_MAX];

            // XMLを読み込む
            ReadXML();

            try
            {
                // 格納フォルダがなければ生成する
                if (!Directory.Exists(LocationDirectoryPath))
                {
                    DirectoryInfo di = Directory.CreateDirectory(LocationDirectoryPath);
                }
            }
            catch (Exception eLocationDirectoryPath)
            {
                MessageBox.Show(eLocationDirectoryPath.Message + " " + eLocationDirectoryPath.StackTrace);
            }
            sysLogFile = new SysLogFile(LocationDirectoryPath, "Log.txt", DateTime.Now);
            sysLogFile.Open();
            sysLogFile.Write("Location Systemを起動します。");

            try
            {
                // 格納フォルダがなければ生成する
                if (!Directory.Exists(LocationDirectoryPath))
                {
                    DirectoryInfo di = Directory.CreateDirectory(LocationDirectoryPath);
                }

                // 格納フォルダ保存期間確認
                string[] files = Directory.GetFiles(LocationDirectoryPath);
                List<string> list = new List<string>();
                foreach (var file in files)
                {
                    DateTime dtFile = File.GetLastWriteTime(file);
                    DateTime dtLimit = DateTime.Now.AddDays(-LocationFileSaveDay);
                    if (dtFile < dtLimit)
                    {
                        // 保存期間を過ぎた場合はファイルを削除する
                        File.Delete(file);
                    }
                }
            }
            catch (Exception eLocationDirectoryPath)
            {
                sysLogFile.Write(eLocationDirectoryPath.Message + " " + eLocationDirectoryPath.StackTrace);
            }

            // 時間を保存する
            LocationFileCheckTime = DateTime.Now;


            // 起動時は停止状態とする
            buttonStop.BackColor = Color.Coral;
            buttonStop.Enabled = false;
            this.停止ToolStripMenuItem.Enabled = false;

            checkBoxVisibleMachineZone.Enabled = true;
            checkBoxAreaLock1.Enabled = true;
            checkBoxAreaLock2.Enabled = true;
            checkBoxAreaLock3.Enabled = true;
            checkBoxAreaLock4.Enabled = true;
            checkBoxAreaLock5.Enabled = true;

            checkBoxCoordinate.Checked = false;

            //
            // ユビセンスの初期化をする
            //

            namingSchema = new Ubisense.UName.Naming.Schema(false);
            multiCell = new Ubisense.ULocation.MultiCell();

            foreach (KeyValuePair<string, Ubisense.ULocation.Cell> cell in multiCell.GetAvailableCells())
            {
                multiCell.LoadCell(cell.Value, true);
            }

            namingSchema.ConnectAsClient();

            Ubisense.ULocation.CellData.Location.AddInsertHandler(multiCell.Schema, OnInsert);
            Ubisense.ULocation.CellData.Location.AddUpdateHandler(multiCell.Schema, OnUpdate);
            Ubisense.ULocation.CellData.Location.AddDeleteHandler(multiCell.Schema, OnDelete);

            // インスタンスを設定する
            FormMain.FormMainInstance = this;

            // 警告画面（侵入履歴画面）を生成する
            formWarning = new FormWarning();
            formWarning.LocationDirectoryPath = LocationDirectoryPath;

            // タグ状態画面を生成する
            formTagState = new FormTagState();
            // 時間がかかるため先にロードする
            formTagState.Show();
            formTagState.Hide();

            // 初期化する
            Init();

            // 描画用オブジェクトを用意しておく
            canvas = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            try
            {
                bitmapBackImage = new Bitmap(this.CaissonImagePath);
                bitmapMachineImage = new Bitmap(this.MachineImagePath);
                bitmapUserImage = new Bitmap(this.UserImagePath);
            }
            catch (Exception eImage)
            {
                sysLogFile.Write(eImage.Message + " " + eImage.StackTrace);

            }
            g = Graphics.FromImage(canvas);
            yellowPen = new Pen(Color.Yellow, 5);
            yellowPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            redPen = new Pen(Color.Red, 5);
            redPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            blackPen = new Pen(Color.Black, 2);
            blackPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;

            yellowBrush = new SolidBrush(Color.Yellow);
            redBrush = new SolidBrush(Color.Red);
            font = new Font("メイリオ", 12F);
          
            // 
            formWarning.Show();
            formWarning.Hide();


            // 描画処理をする（固定エリアのため）
            paintMonitor();

            // 2018/04/05 Rev1.10 XMLファイルを読み込む start
            // XMLファイルを読み取る
            deserializedObect = MyXmlSerializer.Deserialize<Root>(@"Setting_1.10.xml");
            WEIGHTED_MOVING_AVERAGE_NUM_BUFFER_USER = deserializedObect.WeightedMovingAverageList.WeightedMovingAverages[0].CountUserGroup;
            // 0以下は1に書き換える
            if (WEIGHTED_MOVING_AVERAGE_NUM_BUFFER_USER <= 0)
            {
                WEIGHTED_MOVING_AVERAGE_NUM_BUFFER_USER = 1;
            }

            // 100以上は100に書き換える
            if (WEIGHTED_MOVING_AVERAGE_NUM_BUFFER_USER >= 100)
            {
                WEIGHTED_MOVING_AVERAGE_NUM_BUFFER_USER = 100;
            }
            WEIGHTED_MOVING_AVERAGE_NUM_BUFFER_MACHINE = deserializedObect.WeightedMovingAverageList.WeightedMovingAverages[0].CountMachineGroup;
            // 0以下は1に書き換える
            if (WEIGHTED_MOVING_AVERAGE_NUM_BUFFER_MACHINE <= 0)
            {
                WEIGHTED_MOVING_AVERAGE_NUM_BUFFER_MACHINE = 1;
            }

            // 100以上は100に書き換える
            if (WEIGHTED_MOVING_AVERAGE_NUM_BUFFER_MACHINE >= 100)
            {
                WEIGHTED_MOVING_AVERAGE_NUM_BUFFER_MACHINE = 100;
            }
            // 2018/04/05 Rev1.10 XMLファイルを読み込む end
        }

        void Init()
        {
            InitFormMain();

            InitButton();
        }
        void InitFormMain()
        {
            //フォームが存在しているディスプレイの作業領域の高さと幅を取得
            int h = System.Windows.Forms.Screen.GetWorkingArea(this).Height;
            int w = System.Windows.Forms.Screen.GetWorkingArea(this).Width;

            // フォームが存在しているディスプレイの作業領域の高さと幅を設定する
            this.Height = h;
            this.Width = w;

            // 現在フォームが存在しているディスプレイを取得する
            Screen s = Screen.FromControl(this);

            // ディスプレイの位置を取得する
            int x = s.Bounds.X;
            int y = s.Bounds.Y;
            // 位置を設定する
            this.Location = new Point(x, y);

            // 背景設定
            pictureBox1.Location = new Point(12, 90);
            pictureBox1.Width = w - 350 - pictureBox1.Location.X;
            pictureBox1.Height = h - 50 - pictureBox1.Location.Y;


            // 上下右左設定
            labelUp.Text = UpText;
            labelDown.Text = DownText;

            // 左右は縦書きとする
            String tmp = "";

            for (int i = 0; i < RightText.Length; i++)
            {
                tmp = tmp + RightText[i];
                tmp = tmp + '\n';
            }
            labelRight.Text = tmp;

            tmp = "";

            for (int i = 0; i < LeftText.Length; i++)
            {
                tmp = tmp + LeftText[i];
                tmp = tmp + '\n';
            }

            labelLeft.Text = tmp;
            labelUp.Location = new Point(pictureBox1.Location.X + pictureBox1.Width / 2 - labelUp.Width / 2, pictureBox1.Location.Y + 1);
            labelDown.Location = new Point(pictureBox1.Location.X + pictureBox1.Width / 2 - labelDown.Width / 2, pictureBox1.Location.Y + pictureBox1.Height - labelDown.Height - 1);
            labelRight.Location = new Point(pictureBox1.Location.X + pictureBox1.Width - labelRight.Width - 1, pictureBox1.Location.Y + pictureBox1.Height / 2 - labelRight.Height / 2);
            labelLeft.Location = new Point(pictureBox1.Location.X + 1, pictureBox1.Location.Y + pictureBox1.Height / 2 - labelLeft.Height / 2);

            labelUp.Visible = (labelUp.Text == "") ? false : true;
            labelDown.Visible = (labelDown.Text == "") ? false : true;
            labelRight.Visible = (labelRight.Text == "") ? false : true;
            labelLeft.Visible = (labelLeft.Text == "") ? false : true;
        }

        void InitButton()
        {
            // ボタンアイコンを設定する
            buttonStart.Image = Image.FromFile(@".\BMP\Start.ico");
            buttonStart.TextImageRelation = TextImageRelation.ImageBeforeText;
            buttonStop.Image = Image.FromFile(@".\BMP\End.ico");
            buttonStop.TextImageRelation = TextImageRelation.ImageBeforeText;

            // ボタン位置を設定する
            buttonStart.Location = new Point(this.Width - buttonStart.Width - 20, buttonStart.Location.Y);
            buttonStop.Location = new Point(this.Width - buttonStop.Width - 20, buttonStop.Location.Y);

            // チェックボックスを設定する
            checkBoxCoordinate.Location = new Point(buttonStart.Location.X, pictureBox1.Location.Y + pictureBox1.Height - checkBoxCoordinate.Height - 20);
            int margin = 10;
            checkBoxAreaLock5.Location = new Point(checkBoxCoordinate.Location.X, checkBoxCoordinate.Location.Y - margin - checkBoxAreaLock4.Height - 20);
            checkBoxAreaLock4.Location = new Point(checkBoxAreaLock5.Location.X, checkBoxAreaLock5.Location.Y - margin - checkBoxAreaLock4.Height);
            checkBoxAreaLock3.Location = new Point(checkBoxAreaLock4.Location.X, checkBoxAreaLock4.Location.Y - margin - checkBoxAreaLock3.Height);
            checkBoxAreaLock2.Location = new Point(checkBoxAreaLock3.Location.X, checkBoxAreaLock3.Location.Y - margin - checkBoxAreaLock2.Height);
            checkBoxAreaLock1.Location = new Point(checkBoxAreaLock2.Location.X, checkBoxAreaLock2.Location.Y - margin - checkBoxAreaLock1.Height);
            checkBoxVisibleMachineZone.Location = new Point(checkBoxAreaLock1.Location.X, checkBoxAreaLock1.Location.Y - margin - checkBoxVisibleMachineZone.Height - 20);

            // 背景画像を設定する
            this.BackgroundImageLayout = ImageLayout.Stretch;
            this.BackgroundImage = Image.FromFile(@"BMP\monitor_back.jpg");
        }
        
        /// <summary>
        /// 重機の重心を算出する　：重機の長さが正しく設定してあること
        /// </summary>
        void getMachineCenterPoint()
        {
            for (int i = 0; i < MACHINE_MAX; i++)
            {
                if (!machineUse[i])
                {
                    continue;
                }
                // 精度の高い2点から中心点を求める

                // 精度の高い点を取得する
                // 左前、右前、右後、左後の順番に比較して、「より」エラー値が小さい方の値を取得する。
                // （値が等しい場合は左前、右前、右後、左後、の順番で優先される）
                //
                // (左後)・--------◎(左前)
                //       ｜        ｜
                //       ｜        ｜
                // (右後)・--------〇(右前)
                //
                // ・：タグ
                // ◎：一番目に精度が高いタグ
                // 〇：二番目に精度が高いタグ


                // 対角線の長さと角度を求める
                // ・----・
                // ｜  ／｜
                // ｜／angle
                // ・----・
                //double diagonalLength = Math.Sqrt(MachineWidth * MachineWidth + MachineHeight * MachineHeight);
                //double diagonalAngle = Math.Atan2(MachineHeight, MachineWidth);

                // 計算に使用する変数を宣言する
                //double angleDegree; // for debug

                //// for debug
                //stdErrFL[i] = 4;
                //stdErrFR[i] = 3;
                //stdErrBL[i] = 1;
                //stdErrBR[i] = 2;

                // 精度が１番高いタグを取得する
                tmpStdErr = double.MaxValue;
                //
                // （左前）の精度が１番高い
                //
                if (tmpStdErr > this.stdErrFL[i])
                {
                    tmpStdErr = this.stdErrFL[i];

                    //次に精度の高い点を取得する

                    tmpStdErr2 = double.MaxValue;

                    //（左前）-（右前）
                    //　・----◎
                    //  ｜  ／｜
                    //  ｜／  ｜angle
                    //　・----〇-- -
                    if (tmpStdErr2 > this.stdErrFR[i])
                    {
                        tmpStdErr2 = this.stdErrFR[i];

                        // 中心点を求める
                        angle = Math.Atan2(machinePositionOffsetFLY[i] - machinePositionOffsetFRY[i], machinePositionOffsetFLX[i] - machinePositionOffsetFRX[i]);
                        angle2 = angle - Math.PI / 2.0;
                        angle3 = machineDiagonalAngle[i] + angle2;
                        double angleDegree = (double)(angle * 180 / Math.PI);
                        //Console.WriteLine(angleDegree);

                        //
                        // 重機の向きを考慮してオフセットを設定する
                        //
                        // オフセットの長さを取得する
                        double lengthOffsetFL = Math.Sqrt((machineOffsetRightLeftFL[i] * machineOffsetRightLeftFL[i]) + (machineOffsetUpDownFL[i] * machineOffsetUpDownFL[i]));
                        // FLとオフセットFLの角度を取得する
                        double angleOffsetFL = Math.Atan2(machineOffsetRightLeftFL[i], machineOffsetUpDownFL[i]);
                        angleDegree = (double)(angleOffsetFL * 180 / Math.PI);
                        //Console.WriteLine(angleDegree);
                        machinePositionOffsetFLX[i] = machinePositionFLX[i] + lengthOffsetFL * Math.Cos(angle2 + angleOffsetFL);
                        machinePositionOffsetFLY[i] = machinePositionFLY[i] + lengthOffsetFL * Math.Sin(angle2 + angleOffsetFL);

                        // 
                        double lengthOffsetFR = Math.Sqrt((machineOffsetRightLeftFR[i] * machineOffsetRightLeftFR[i]) + (machineOffsetUpDownFR[i] * machineOffsetUpDownFR[i]));
                        // FRとオフセットFRの角度を取得する
                        double angleOffsetFR = Math.Atan2(machineOffsetRightLeftFR[i], machineOffsetUpDownFR[i]);
                        angleDegree = (double)(angleOffsetFR * 180 / Math.PI);
                        machinePositionOffsetFRX[i] = machinePositionFRX[i] + lengthOffsetFR * Math.Cos(angle2 + angleOffsetFR);
                        machinePositionOffsetFRY[i] = machinePositionFRY[i] + lengthOffsetFR * Math.Sin(angle2 + angleOffsetFR);

                        // 
                        double lengthOffsetBL = Math.Sqrt((machineOffsetRightLeftBL[i] * machineOffsetRightLeftBL[i]) + (machineOffsetUpDownBL[i] * machineOffsetUpDownBL[i]));
                        // BLとオフセットBLの角度を取得する
                        double angleOffsetBL = Math.Atan2(machineOffsetRightLeftBL[i], machineOffsetUpDownBL[i]);
                        angleDegree = (double)(angleOffsetBL * 180 / Math.PI);
                        machinePositionOffsetBLX[i] = machinePositionBLX[i] + lengthOffsetBL * Math.Cos(angle2 + angleOffsetBL);
                        machinePositionOffsetBLY[i] = machinePositionBLY[i] + lengthOffsetBL * Math.Sin(angle2 + angleOffsetBL);

                        // 
                        double lengthOffsetBR = Math.Sqrt((machineOffsetRightLeftBR[i] * machineOffsetRightLeftBR[i]) + (machineOffsetUpDownBR[i] * machineOffsetUpDownBR[i]));
                        // BRとオフセットBRの角度を取得する
                        double angleOffsetBR = Math.Atan2(machineOffsetRightLeftBR[i], machineOffsetUpDownBR[i]);
                        angleDegree = (double)(angleOffsetBR * 180 / Math.PI);
                        machinePositionOffsetBRX[i] = machinePositionBRX[i] + lengthOffsetBR * Math.Cos(angle2 + angleOffsetBR);
                        machinePositionOffsetBRY[i] = machinePositionBRY[i] + lengthOffsetBR * Math.Sin(angle2 + angleOffsetBR);

                        machinePositionCx[i] = machinePositionOffsetFLX[i] - Math.Cos(angle3) * machineDiagonalLength[i] / 2.0;
                        machinePositionCy[i] = machinePositionOffsetFLY[i] - Math.Sin(angle3) * machineDiagonalLength[i] / 2.0;
                        //machinePositionCx[i] = machinePositionOffsetFRX[i] - Math.Cos(angle3) * machineDiagonalLength[i] / 2.0;
                        //machinePositionCy[i] = machinePositionOffsetFRY[i] - Math.Sin(angle3) * machineDiagonalLength[i] / 2.0;

                        // 表示用位置（オフセット含）も更新する
                        viewMachinePositionOffsetFLX[i] = OriginX + machinePositionOffsetFLX[i] * (double)ScaleX;
                        viewMachinePositionOffsetFLY[i] = -(OriginY) - machinePositionOffsetFLY[i] * (double)ScaleY;
                        viewMachinePositionOffsetFRX[i] = OriginX + machinePositionOffsetFRX[i] * (double)ScaleX;
                        viewMachinePositionOffsetFRY[i] = -(OriginY) - machinePositionOffsetFRY[i] * (double)ScaleY;
                        viewMachinePositionOffsetBLX[i] = OriginX + machinePositionOffsetBLX[i] * (double)ScaleX;
                        viewMachinePositionOffsetBLY[i] = -(OriginY) - machinePositionOffsetBLY[i] * (double)ScaleY;
                        viewMachinePositionOffsetBRX[i] = OriginX + machinePositionOffsetBRX[i] * (double)ScaleX;
                        viewMachinePositionOffsetBRY[i] = -(OriginY) - machinePositionOffsetBRY[i] * (double)ScaleY;
                    }

                    // （左前）-（右後）
                    //   ・----◎
                    //   ｜  ／｜
                    //   ｜／  ｜
                    //   〇----・
                    if (tmpStdErr2 > this.stdErrBR[i])
                    {
                        tmpStdErr2 = this.stdErrBR[i];

                        //中心点を求める
                        angle = Math.Atan2(machinePositionOffsetFLY[i] - machinePositionOffsetBRY[i], machinePositionOffsetFLX[i] - machinePositionOffsetBRX[i]);
                        angle2 = angle - machineDiagonalAngle[i];

                        //
                        // 重機の向きを考慮してオフセットを設定する
                        //
                        // オフセットの長さを取得する
                        double lengthOffsetFL = Math.Sqrt((machineOffsetRightLeftFL[i] * machineOffsetRightLeftFL[i]) + (machineOffsetUpDownFL[i] * machineOffsetUpDownFL[i]));
                        // FLとオフセットFLの角度を取得する
                        double angleOffsetFL = Math.Atan2(machineOffsetRightLeftFL[i], machineOffsetUpDownFL[i]);
                        double angleDegree = (double)(angleOffsetFL * 180 / Math.PI);
                        //Console.WriteLine(angleDegree);
                        machinePositionOffsetFLX[i] = machinePositionFLX[i] + lengthOffsetFL * Math.Cos(angle2 + angleOffsetFL);
                        machinePositionOffsetFLY[i] = machinePositionFLY[i] + lengthOffsetFL * Math.Sin(angle2 + angleOffsetFL);

                        // 
                        double lengthOffsetFR = Math.Sqrt((machineOffsetRightLeftFR[i] * machineOffsetRightLeftFR[i]) + (machineOffsetUpDownFR[i] * machineOffsetUpDownFR[i]));
                        // FRとオフセットFRの角度を取得する
                        double angleOffsetFR = Math.Atan2(machineOffsetRightLeftFR[i], machineOffsetUpDownFR[i]);
                        angleDegree = (double)(angleOffsetFR * 180 / Math.PI);
                        machinePositionOffsetFRX[i] = machinePositionFRX[i] + lengthOffsetFR * Math.Cos(angle2 + angleOffsetFR);
                        machinePositionOffsetFRY[i] = machinePositionFRY[i] + lengthOffsetFR * Math.Sin(angle2 + angleOffsetFR);

                        // 
                        double lengthOffsetBL = Math.Sqrt((machineOffsetRightLeftBL[i] * machineOffsetRightLeftBL[i]) + (machineOffsetUpDownBL[i] * machineOffsetUpDownBL[i]));
                        // BLとオフセットBLの角度を取得する
                        double angleOffsetBL = Math.Atan2(machineOffsetRightLeftBL[i], machineOffsetUpDownBL[i]);
                        angleDegree = (double)(angleOffsetBL * 180 / Math.PI);
                        machinePositionOffsetBLX[i] = machinePositionBLX[i] + lengthOffsetBL * Math.Cos(angle2 + angleOffsetBL);
                        machinePositionOffsetBLY[i] = machinePositionBLY[i] + lengthOffsetBL * Math.Sin(angle2 + angleOffsetBL);

                        // 
                        double lengthOffsetBR = Math.Sqrt((machineOffsetRightLeftBR[i] * machineOffsetRightLeftBR[i]) + (machineOffsetUpDownBR[i] * machineOffsetUpDownBR[i]));
                        // BRとオフセットBRの角度を取得する
                        double angleOffsetBR = Math.Atan2(machineOffsetRightLeftBR[i], machineOffsetUpDownBR[i]);
                        angleDegree = (double)(angleOffsetBR * 180 / Math.PI);
                        machinePositionOffsetBRX[i] = machinePositionBRX[i] + lengthOffsetBR * Math.Cos(angle2 + angleOffsetBR);
                        machinePositionOffsetBRY[i] = machinePositionBRY[i] + lengthOffsetBR * Math.Sin(angle2 + angleOffsetBR);

                        machinePositionCx[i] = machinePositionOffsetBRX[i] + Math.Cos(angle) * machineDiagonalLength[i] / 2.0;
                        machinePositionCy[i] = machinePositionOffsetBRY[i] + Math.Sin(angle) * machineDiagonalLength[i] / 2.0;
                        //machinePositionCx[i] = machinePositionOffsetFRX[i] - Math.Cos(angle) * machineDiagonalLength[i] / 2.0;
                        //machinePositionCy[i] = machinePositionOffsetFRY[i] - Math.Sin(angle) * machineDiagonalLength[i] / 2.0;

                        // 表示用位置（オフセット含）も更新する
                        viewMachinePositionOffsetFLX[i] = OriginX + machinePositionOffsetFLX[i] * (double)ScaleX;
                        viewMachinePositionOffsetFLY[i] = -(OriginY) - machinePositionOffsetFLY[i] * (double)ScaleY;
                        viewMachinePositionOffsetFRX[i] = OriginX + machinePositionOffsetFRX[i] * (double)ScaleX;
                        viewMachinePositionOffsetFRY[i] = -(OriginY) - machinePositionOffsetFRY[i] * (double)ScaleY;
                        viewMachinePositionOffsetBLX[i] = OriginX + machinePositionOffsetBLX[i] * (double)ScaleX;
                        viewMachinePositionOffsetBLY[i] = -(OriginY) - machinePositionOffsetBLY[i] * (double)ScaleY;
                        viewMachinePositionOffsetBRX[i] = OriginX + machinePositionOffsetBRX[i] * (double)ScaleX;
                        viewMachinePositionOffsetBRY[i] = -(OriginY) - machinePositionOffsetBRY[i] * (double)ScaleY;
                    }

                    // （左前）-（左後）
                    //   〇----◎
                    //   ｜  ／｜
                    //   ｜／  ｜
                    //   ・----・
                    if (tmpStdErr2 > this.stdErrBL[i])
                    {
                        tmpStdErr2 = this.stdErrBL[i];

                        // 中心点を求める
                        angle = Math.Atan2(machinePositionOffsetFLY[i] - machinePositionOffsetBLY[i], machinePositionOffsetFLX[i] - machinePositionOffsetBLX[i]);
                        angle2 = machineDiagonalAngle[i] + angle;

                        //
                        // 重機の向きを考慮してオフセットを設定する
                        //
                        // オフセットの長さを取得する
                        double lengthOffsetFL = Math.Sqrt((machineOffsetRightLeftFL[i] * machineOffsetRightLeftFL[i]) + (machineOffsetUpDownFL[i] * machineOffsetUpDownFL[i]));
                        // FLとオフセットFLの角度を取得する
                        double angleOffsetFL = Math.Atan2(machineOffsetRightLeftFL[i], machineOffsetUpDownFL[i]);
                        double angleDegree = (double)(angleOffsetFL * 180 / Math.PI);
                        //Console.WriteLine(angleDegree);
                        machinePositionOffsetFLX[i] = machinePositionFLX[i] + lengthOffsetFL * Math.Cos(angle + angleOffsetFL);
                        machinePositionOffsetFLY[i] = machinePositionFLY[i] + lengthOffsetFL * Math.Sin(angle + angleOffsetFL);

                        // 
                        double lengthOffsetFR = Math.Sqrt((machineOffsetRightLeftFR[i] * machineOffsetRightLeftFR[i]) + (machineOffsetUpDownFR[i] * machineOffsetUpDownFR[i]));
                        // FRとオフセットFRの角度を取得する
                        double angleOffsetFR = Math.Atan2(machineOffsetRightLeftFR[i], machineOffsetUpDownFR[i]);
                        angleDegree = (double)(angleOffsetFR * 180 / Math.PI);
                        machinePositionOffsetFRX[i] = machinePositionFRX[i] + lengthOffsetFR * Math.Cos(angle + angleOffsetFR);
                        machinePositionOffsetFRY[i] = machinePositionFRY[i] + lengthOffsetFR * Math.Sin(angle + angleOffsetFR);

                        // 
                        double lengthOffsetBL = Math.Sqrt((machineOffsetRightLeftBL[i] * machineOffsetRightLeftBL[i]) + (machineOffsetUpDownBL[i] * machineOffsetUpDownBL[i]));
                        // BLとオフセットBLの角度を取得する
                        double angleOffsetBL = Math.Atan2(machineOffsetRightLeftBL[i], machineOffsetUpDownBL[i]);
                        angleDegree = (double)(angleOffsetBL * 180 / Math.PI);
                        machinePositionOffsetBLX[i] = machinePositionBLX[i] + lengthOffsetBL * Math.Cos(angle + angleOffsetBL);
                        machinePositionOffsetBLY[i] = machinePositionBLY[i] + lengthOffsetBL * Math.Sin(angle + angleOffsetBL);

                        // 
                        double lengthOffsetBR = Math.Sqrt((machineOffsetRightLeftBR[i] * machineOffsetRightLeftBR[i]) + (machineOffsetUpDownBR[i] * machineOffsetUpDownBR[i]));
                        // BRとオフセットBRの角度を取得する
                        double angleOffsetBR = Math.Atan2(machineOffsetRightLeftBR[i], machineOffsetUpDownBR[i]);
                        angleDegree = (double)(angleOffsetBR * 180 / Math.PI);
                        machinePositionOffsetBRX[i] = machinePositionBRX[i] + lengthOffsetBR * Math.Cos(angle + angleOffsetBR);
                        machinePositionOffsetBRY[i] = machinePositionBRY[i] + lengthOffsetBR * Math.Sin(angle + angleOffsetBR);

                        machinePositionCx[i] = machinePositionOffsetFLX[i] - Math.Cos(angle2) * machineDiagonalLength[i] / 2.0;
                        machinePositionCy[i] = machinePositionOffsetFLY[i] - Math.Sin(angle2) * machineDiagonalLength[i] / 2.0;
                        //machinePositionCx[i] = machinePositionOffsetFRX[i] - Math.Cos(angle2) * machineDiagonalLength[i] / 2.0;
                        //machinePositionCy[i] = machinePositionOffsetFRY[i] - Math.Sin(angle2) * machineDiagonalLength[i] / 2.0;

                        // 表示用位置（オフセット含）も更新する
                        viewMachinePositionOffsetFLX[i] = OriginX + machinePositionOffsetFLX[i] * (double)ScaleX;
                        viewMachinePositionOffsetFLY[i] = -(OriginY) - machinePositionOffsetFLY[i] * (double)ScaleY;
                        viewMachinePositionOffsetFRX[i] = OriginX + machinePositionOffsetFRX[i] * (double)ScaleX;
                        viewMachinePositionOffsetFRY[i] = -(OriginY) - machinePositionOffsetFRY[i] * (double)ScaleY;
                        viewMachinePositionOffsetBLX[i] = OriginX + machinePositionOffsetBLX[i] * (double)ScaleX;
                        viewMachinePositionOffsetBLY[i] = -(OriginY) - machinePositionOffsetBLY[i] * (double)ScaleY;
                        viewMachinePositionOffsetBRX[i] = OriginX + machinePositionOffsetBRX[i] * (double)ScaleX;
                        viewMachinePositionOffsetBRY[i] = -(OriginY) - machinePositionOffsetBRY[i] * (double)ScaleY;
                    }
                }

                //
                //（右前）の精度が１番高い

                if (tmpStdErr > this.stdErrFR[i])
                {
                    tmpStdErr = this.stdErrFR[i];

                    // 次に精度の高い点を取得する
                    tmpStdErr2 = double.MaxValue;

                    //（右前）-（左前）
                    //　・----〇
                    //  ｜  ／｜
                    //  ｜／  ｜
                    //　・----◎
                    if (tmpStdErr2 > this.stdErrFL[i])
                    {
                        tmpStdErr2 = this.stdErrFL[i];

                        // 中心点を求める
                        angle = Math.Atan2(machinePositionOffsetFLY[i] - machinePositionOffsetFRY[i], machinePositionOffsetFLX[i] - machinePositionOffsetFRX[i]);
                        angle2 = angle - Math.PI / 2.0;
                        angle3 = angle2 - machineDiagonalAngle[i];

                        //
                        // 重機の向きを考慮してオフセットを設定する
                        //
                        // オフセットの長さを取得する
                        double lengthOffsetFL = Math.Sqrt((machineOffsetRightLeftFL[i] * machineOffsetRightLeftFL[i]) + (machineOffsetUpDownFL[i] * machineOffsetUpDownFL[i]));
                        // FLとオフセットFLの角度を取得する
                        double angleOffsetFL = Math.Atan2(machineOffsetRightLeftFL[i], machineOffsetUpDownFL[i]);
                        double angleDegree = (double)(angleOffsetFL * 180 / Math.PI);
                        //Console.WriteLine(angleDegree);
                        machinePositionOffsetFLX[i] = machinePositionFLX[i] + lengthOffsetFL * Math.Cos(angle2 + angleOffsetFL);
                        machinePositionOffsetFLY[i] = machinePositionFLY[i] + lengthOffsetFL * Math.Sin(angle2 + angleOffsetFL);

                        // 
                        double lengthOffsetFR = Math.Sqrt((machineOffsetRightLeftFR[i] * machineOffsetRightLeftFR[i]) + (machineOffsetUpDownFR[i] * machineOffsetUpDownFR[i]));
                        // FRとオフセットFRの角度を取得する
                        double angleOffsetFR = Math.Atan2(machineOffsetRightLeftFR[i], machineOffsetUpDownFR[i]);
                        angleDegree = (double)(angleOffsetFR * 180 / Math.PI);
                        machinePositionOffsetFRX[i] = machinePositionFRX[i] + lengthOffsetFR * Math.Cos(angle2 + angleOffsetFR);
                        machinePositionOffsetFRY[i] = machinePositionFRY[i] + lengthOffsetFR * Math.Sin(angle2 + angleOffsetFR);

                        // 
                        double lengthOffsetBL = Math.Sqrt((machineOffsetRightLeftBL[i] * machineOffsetRightLeftBL[i]) + (machineOffsetUpDownBL[i] * machineOffsetUpDownBL[i]));
                        // BLとオフセットBLの角度を取得する
                        double angleOffsetBL = Math.Atan2(machineOffsetRightLeftBL[i], machineOffsetUpDownBL[i]);
                        angleDegree = (double)(angleOffsetBL * 180 / Math.PI);
                        machinePositionOffsetBLX[i] = machinePositionBLX[i] + lengthOffsetBL * Math.Cos(angle2 + angleOffsetBL);
                        machinePositionOffsetBLY[i] = machinePositionBLY[i] + lengthOffsetBL * Math.Sin(angle2 + angleOffsetBL);

                        // 
                        double lengthOffsetBR = Math.Sqrt((machineOffsetRightLeftBR[i] * machineOffsetRightLeftBR[i]) + (machineOffsetUpDownBR[i] * machineOffsetUpDownBR[i]));
                        // BRとオフセットBRの角度を取得する
                        double angleOffsetBR = Math.Atan2(machineOffsetRightLeftBR[i], machineOffsetUpDownBR[i]);
                        angleDegree = (double)(angleOffsetBR * 180 / Math.PI);
                        machinePositionOffsetBRX[i] = machinePositionBRX[i] + lengthOffsetBR * Math.Cos(angle2 + angleOffsetBR);
                        machinePositionOffsetBRY[i] = machinePositionBRY[i] + lengthOffsetBR * Math.Sin(angle2 + angleOffsetBR);

                        machinePositionCx[i] = machinePositionOffsetFRX[i] - Math.Cos(angle3) * machineDiagonalLength[i] / 2.0;
                        machinePositionCy[i] = machinePositionOffsetFRY[i] - Math.Sin(angle3) * machineDiagonalLength[i] / 2.0;

                        // 表示用位置（オフセット含）も更新する
                        viewMachinePositionOffsetFLX[i] = OriginX + machinePositionOffsetFLX[i] * (double)ScaleX;
                        viewMachinePositionOffsetFLY[i] = -(OriginY) - machinePositionOffsetFLY[i] * (double)ScaleY;
                        viewMachinePositionOffsetFRX[i] = OriginX + machinePositionOffsetFRX[i] * (double)ScaleX;
                        viewMachinePositionOffsetFRY[i] = -(OriginY) - machinePositionOffsetFRY[i] * (double)ScaleY;
                        viewMachinePositionOffsetBLX[i] = OriginX + machinePositionOffsetBLX[i] * (double)ScaleX;
                        viewMachinePositionOffsetBLY[i] = -(OriginY) - machinePositionOffsetBLY[i] * (double)ScaleY;
                        viewMachinePositionOffsetBRX[i] = OriginX + machinePositionOffsetBRX[i] * (double)ScaleX;
                        viewMachinePositionOffsetBRY[i] = -(OriginY) - machinePositionOffsetBRY[i] * (double)ScaleY;
                    }

                    //（右前）-（右後）
                    //  ・----・
                    //  ｜  ／｜
                    //  ｜／  ｜
                    //  〇----◎
                    if (tmpStdErr2 > this.stdErrBR[i])
                    {
                        tmpStdErr2 = this.stdErrBR[i];

                        // 中心点を求める
                        angle = Math.Atan2(machinePositionOffsetFRY[i] - machinePositionOffsetBRY[i], machinePositionOffsetFRX[i] - machinePositionOffsetBRX[i]);
                        angle2 = angle - machineDiagonalAngle[i];

                        double angleDegree = (double)(angle * 180 / Math.PI);
                        //Console.WriteLine(angleDegree);

                        //
                        // 重機の向きを考慮してオフセットを設定する
                        //
                        // オフセットの長さを取得する
                        double lengthOffsetFL = Math.Sqrt((machineOffsetRightLeftFL[i] * machineOffsetRightLeftFL[i]) + (machineOffsetUpDownFL[i] * machineOffsetUpDownFL[i]));
                        // FLとオフセットFLの角度を取得する
                        double angleOffsetFL = Math.Atan2(machineOffsetRightLeftFL[i], machineOffsetUpDownFL[i]);
                        angleDegree = (double)(angleOffsetFL * 180 / Math.PI);
                        machinePositionOffsetFLX[i] = machinePositionFLX[i] + lengthOffsetFL * Math.Cos(angle + angleOffsetFL);
                        machinePositionOffsetFLY[i] = machinePositionFLY[i] + lengthOffsetFL * Math.Sin(angle + angleOffsetFL);

                        // 
                        double lengthOffsetFR = Math.Sqrt((machineOffsetRightLeftFR[i] * machineOffsetRightLeftFR[i]) + (machineOffsetUpDownFR[i] * machineOffsetUpDownFR[i]));
                        // FRとオフセットFRの角度を取得する
                        double angleOffsetFR = Math.Atan2(machineOffsetRightLeftFR[i], machineOffsetUpDownFR[i]);
                        angleDegree = (double)(angleOffsetFR * 180 / Math.PI);
                        machinePositionOffsetFRX[i] = machinePositionFRX[i] + lengthOffsetFR * Math.Cos(angle + angleOffsetFR);
                        machinePositionOffsetFRY[i] = machinePositionFRY[i] + lengthOffsetFR * Math.Sin(angle + angleOffsetFR);

                        // 
                        double lengthOffsetBL = Math.Sqrt((machineOffsetRightLeftBL[i] * machineOffsetRightLeftBL[i]) + (machineOffsetUpDownBL[i] * machineOffsetUpDownBL[i]));
                        // BLとオフセットBLの角度を取得する
                        double angleOffsetBL = Math.Atan2(machineOffsetRightLeftBL[i], machineOffsetUpDownBL[i]);
                        angleDegree = (double)(angleOffsetBL * 180 / Math.PI);
                        machinePositionOffsetBLX[i] = machinePositionBLX[i] + lengthOffsetBL * Math.Cos(angle + angleOffsetBL);
                        machinePositionOffsetBLY[i] = machinePositionBLY[i] + lengthOffsetBL * Math.Sin(angle + angleOffsetBL);

                        // 
                        double lengthOffsetBR = Math.Sqrt((machineOffsetRightLeftBR[i] * machineOffsetRightLeftBR[i]) + (machineOffsetUpDownBR[i] * machineOffsetUpDownBR[i]));
                        // BRとオフセットBRの角度を取得する
                        double angleOffsetBR = Math.Atan2(machineOffsetRightLeftBR[i], machineOffsetUpDownBR[i]);
                        angleDegree = (double)(angleOffsetBR * 180 / Math.PI);
                        machinePositionOffsetBRX[i] = machinePositionBRX[i] + lengthOffsetBR * Math.Cos(angle + angleOffsetBR);
                        machinePositionOffsetBRY[i] = machinePositionBRY[i] + lengthOffsetBR * Math.Sin(angle + angleOffsetBR);

                        machinePositionCx[i] = machinePositionOffsetFRX[i] - Math.Cos(angle2) * machineDiagonalLength[i] / 2.0;
                        machinePositionCy[i] = machinePositionOffsetFRY[i] - Math.Sin(angle2) * machineDiagonalLength[i] / 2.0;

                        // 表示用位置（オフセット含）も更新する
                        viewMachinePositionOffsetFLX[i] = OriginX + machinePositionOffsetFLX[i] * (double)ScaleX;
                        viewMachinePositionOffsetFLY[i] = -(OriginY) - machinePositionOffsetFLY[i] * (double)ScaleY;
                        viewMachinePositionOffsetFRX[i] = OriginX + machinePositionOffsetFRX[i] * (double)ScaleX;
                        viewMachinePositionOffsetFRY[i] = -(OriginY) - machinePositionOffsetFRY[i] * (double)ScaleY;
                        viewMachinePositionOffsetBLX[i] = OriginX + machinePositionOffsetBLX[i] * (double)ScaleX;
                        viewMachinePositionOffsetBLY[i] = -(OriginY) - machinePositionOffsetBLY[i] * (double)ScaleY;
                        viewMachinePositionOffsetBRX[i] = OriginX + machinePositionOffsetBRX[i] * (double)ScaleX;
                        viewMachinePositionOffsetBRY[i] = -(OriginY) - machinePositionOffsetBRY[i] * (double)ScaleY;
                    }

                    // （右前）-（左後）
                    //   〇----・
                    //   ｜＼  ｜
                    //   ｜  ＼｜
                    //   ・----◎
                    if (tmpStdErr2 > this.stdErrBL[i])
                    {
                        tmpStdErr2 = this.stdErrBL[i];

                        // 中心点を求める
                        angle = Math.Atan2(machinePositionOffsetFRY[i] - machinePositionOffsetBLY[i], machinePositionOffsetFRX[i] - machinePositionOffsetBLX[i]);
                        angle2 = Math.PI / 2.0 - machineDiagonalAngle[i] + angle;
                        double angleDegree = (double)(angle2 * 180 / Math.PI);
                        //Console.WriteLine(angleDegree);
                        //
                        // 重機の向きを考慮してオフセットを設定する
                        //
                        // オフセットの長さを取得する
                        double lengthOffsetFL = Math.Sqrt((machineOffsetRightLeftFL[i] * machineOffsetRightLeftFL[i]) + (machineOffsetUpDownFL[i] * machineOffsetUpDownFL[i]));
                        // FLとオフセットFLの角度を取得する
                        double angleOffsetFL = Math.Atan2(machineOffsetRightLeftFL[i], machineOffsetUpDownFL[i]);
                        angleDegree = (double)(angleOffsetFL * 180 / Math.PI);
                        //Console.WriteLine(angleDegree);
                        machinePositionOffsetFLX[i] = machinePositionFLX[i] + lengthOffsetFL * Math.Cos(angle2 + angleOffsetFL);
                        machinePositionOffsetFLY[i] = machinePositionFLY[i] + lengthOffsetFL * Math.Sin(angle2 + angleOffsetFL);

                        // 
                        double lengthOffsetFR = Math.Sqrt((machineOffsetRightLeftFR[i] * machineOffsetRightLeftFR[i]) + (machineOffsetUpDownFR[i] * machineOffsetUpDownFR[i]));
                        // FRとオフセットFRの角度を取得する
                        double angleOffsetFR = Math.Atan2(machineOffsetRightLeftFR[i], machineOffsetUpDownFR[i]);
                        angleDegree = (double)(angleOffsetFR * 180 / Math.PI);
                        machinePositionOffsetFRX[i] = machinePositionFRX[i] + lengthOffsetFR * Math.Cos(angle2 + angleOffsetFR);
                        machinePositionOffsetFRY[i] = machinePositionFRY[i] + lengthOffsetFR * Math.Sin(angle2 + angleOffsetFR);

                        // 
                        double lengthOffsetBL = Math.Sqrt((machineOffsetRightLeftBL[i] * machineOffsetRightLeftBL[i]) + (machineOffsetUpDownBL[i] * machineOffsetUpDownBL[i]));
                        // BLとオフセットBLの角度を取得する
                        double angleOffsetBL = Math.Atan2(machineOffsetRightLeftBL[i], machineOffsetUpDownBL[i]);
                        angleDegree = (double)(angleOffsetBL * 180 / Math.PI);
                        machinePositionOffsetBLX[i] = machinePositionBLX[i] + lengthOffsetBL * Math.Cos(angle2 + angleOffsetBL);
                        machinePositionOffsetBLY[i] = machinePositionBLY[i] + lengthOffsetBL * Math.Sin(angle2 + angleOffsetBL);

                        // 
                        double lengthOffsetBR = Math.Sqrt((machineOffsetRightLeftBR[i] * machineOffsetRightLeftBR[i]) + (machineOffsetUpDownBR[i] * machineOffsetUpDownBR[i]));
                        // BRとオフセットBRの角度を取得する
                        double angleOffsetBR = Math.Atan2(machineOffsetRightLeftBR[i], machineOffsetUpDownBR[i]);
                        angleDegree = (double)(angleOffsetBR * 180 / Math.PI);
                        machinePositionOffsetBRX[i] = machinePositionBRX[i] + lengthOffsetBR * Math.Cos(angle2 + angleOffsetBR);
                        machinePositionOffsetBRY[i] = machinePositionBRY[i] + lengthOffsetBR * Math.Sin(angle2 + angleOffsetBR);

                        machinePositionCx[i] = machinePositionOffsetFRX[i] - Math.Cos(angle) * machineDiagonalLength[i] / 2.0;
                        machinePositionCy[i] = machinePositionOffsetFRY[i] - Math.Sin(angle) * machineDiagonalLength[i] / 2.0;

                        // 表示用位置（オフセット含）も更新する
                        viewMachinePositionOffsetFLX[i] = OriginX + machinePositionOffsetFLX[i] * (double)ScaleX;
                        viewMachinePositionOffsetFLY[i] = -(OriginY) - machinePositionOffsetFLY[i] * (double)ScaleY;
                        viewMachinePositionOffsetFRX[i] = OriginX + machinePositionOffsetFRX[i] * (double)ScaleX;
                        viewMachinePositionOffsetFRY[i] = -(OriginY) - machinePositionOffsetFRY[i] * (double)ScaleY;
                        viewMachinePositionOffsetBLX[i] = OriginX + machinePositionOffsetBLX[i] * (double)ScaleX;
                        viewMachinePositionOffsetBLY[i] = -(OriginY) - machinePositionOffsetBLY[i] * (double)ScaleY;
                        viewMachinePositionOffsetBRX[i] = OriginX + machinePositionOffsetBRX[i] * (double)ScaleX;
                        viewMachinePositionOffsetBRY[i] = -(OriginY) - machinePositionOffsetBRY[i] * (double)ScaleY;
                    }
                }

                //
                // （右後）の精度が１番高い
                //
                if (tmpStdErr > this.stdErrBR[i])
                {
                    tmpStdErr = this.stdErrBR[i];

                    // 次に精度の高い点を取得する
                    tmpStdErr2 = double.MaxValue;

                    // （右後）-（左前）
                    // 　・----〇
                    //   ｜  ／｜
                    //   ｜／  ｜
                    // 　◎----・
                    if (tmpStdErr2 > this.stdErrFL[i])
                    {
                        tmpStdErr2 = this.stdErrFL[i];

                        // 中心点を求める

                        angle = Math.Atan2(machinePositionOffsetFLY[i] - machinePositionOffsetBRY[i], machinePositionOffsetFLX[i] - machinePositionOffsetBRX[i]);
                        angle2 = angle - machineDiagonalAngle[i];


                        // 重機の向きを考慮してオフセットを設定する

                        // オフセットの長さを取得する
                        double lengthOffsetFL = Math.Sqrt((machineOffsetRightLeftFL[i] * machineOffsetRightLeftFL[i]) + (machineOffsetUpDownFL[i] * machineOffsetUpDownFL[i]));
                        // FLとオフセットFLの角度を取得する
                        double angleOffsetFL = Math.Atan2(machineOffsetRightLeftFL[i], machineOffsetUpDownFL[i]);
                        double angleDegree = (double)(angleOffsetFL * 180 / Math.PI);
                        //Console.WriteLine(angleDegree);
                        machinePositionOffsetFLX[i] = machinePositionFLX[i] + lengthOffsetFL * Math.Cos(angle2 + angleOffsetFL);
                        machinePositionOffsetFLY[i] = machinePositionFLY[i] + lengthOffsetFL * Math.Sin(angle2 + angleOffsetFL);


                        double lengthOffsetFR = Math.Sqrt((machineOffsetRightLeftFR[i] * machineOffsetRightLeftFR[i]) + (machineOffsetUpDownFR[i] * machineOffsetUpDownFR[i]));
                        //FRとオフセットFRの角度を取得する
                        double angleOffsetFR = Math.Atan2(machineOffsetRightLeftFR[i], machineOffsetUpDownFR[i]);
                        angleDegree = (double)(angleOffsetFR * 180 / Math.PI);
                        machinePositionOffsetFRX[i] = machinePositionFRX[i] + lengthOffsetFR * Math.Cos(angle2 + angleOffsetFR);
                        machinePositionOffsetFRY[i] = machinePositionFRY[i] + lengthOffsetFR * Math.Sin(angle2 + angleOffsetFR);


                        double lengthOffsetBL = Math.Sqrt((machineOffsetRightLeftBL[i] * machineOffsetRightLeftBL[i]) + (machineOffsetUpDownBL[i] * machineOffsetUpDownBL[i]));
                        //BLとオフセットBLの角度を取得する
                        double angleOffsetBL = Math.Atan2(machineOffsetRightLeftBL[i], machineOffsetUpDownBL[i]);
                        angleDegree = (double)(angleOffsetBL * 180 / Math.PI);
                        machinePositionOffsetBLX[i] = machinePositionBLX[i] + lengthOffsetBL * Math.Cos(angle2 + angleOffsetBL);
                        machinePositionOffsetBLY[i] = machinePositionBLY[i] + lengthOffsetBL * Math.Sin(angle2 + angleOffsetBL);


                        double lengthOffsetBR = Math.Sqrt((machineOffsetRightLeftBR[i] * machineOffsetRightLeftBR[i]) + (machineOffsetUpDownBR[i] * machineOffsetUpDownBR[i]));
                        //BRとオフセットBRの角度を取得する
                        double angleOffsetBR = Math.Atan2(machineOffsetRightLeftBR[i], machineOffsetUpDownBR[i]);
                        angleDegree = (double)(angleOffsetBR * 180 / Math.PI);
                        machinePositionOffsetBRX[i] = machinePositionBRX[i] + lengthOffsetBR * Math.Cos(angle2 + angleOffsetBR);
                        machinePositionOffsetBRY[i] = machinePositionBRY[i] + lengthOffsetBR * Math.Sin(angle2 + angleOffsetBR);

                        machinePositionCx[i] = machinePositionOffsetFLX[i] - Math.Cos(angle) * machineDiagonalLength[i] / 2.0;
                        machinePositionCy[i] = machinePositionOffsetFLY[i] - Math.Sin(angle) * machineDiagonalLength[i] / 2.0;

                        //表示用位置（オフセット含）も更新する
                        viewMachinePositionOffsetFLX[i] = OriginX + machinePositionOffsetFLX[i] * (double)ScaleX;
                        viewMachinePositionOffsetFLY[i] = -(OriginY) - machinePositionOffsetFLY[i] * (double)ScaleY;
                        viewMachinePositionOffsetFRX[i] = OriginX + machinePositionOffsetFRX[i] * (double)ScaleX;
                        viewMachinePositionOffsetFRY[i] = -(OriginY) - machinePositionOffsetFRY[i] * (double)ScaleY;
                        viewMachinePositionOffsetBLX[i] = OriginX + machinePositionOffsetBLX[i] * (double)ScaleX;
                        viewMachinePositionOffsetBLY[i] = -(OriginY) - machinePositionOffsetBLY[i] * (double)ScaleY;
                        viewMachinePositionOffsetBRX[i] = OriginX + machinePositionOffsetBRX[i] * (double)ScaleX;
                        viewMachinePositionOffsetBRY[i] = -(OriginY) - machinePositionOffsetBRY[i] * (double)ScaleY;
                    }

                    //（右後）-（右前）
                    //　・----・
                    //  ｜  ／｜
                    //  ｜／  ｜
                    //  ◎----〇
                    if (tmpStdErr2 > this.stdErrFR[i])
                    {
                        tmpStdErr2 = this.stdErrFR[i];

                        // 中心点を求める
                        angle = Math.Atan2(machinePositionOffsetFRY[i] - machinePositionOffsetBRY[i], machinePositionOffsetFRX[i] - machinePositionOffsetBRX[i]);
                        angle2 = angle - machineDiagonalAngle[i];

                        // 重機の向きを考慮してオフセットを設定する

                        // オフセットの長さを取得する
                        double lengthOffsetFL = Math.Sqrt((machineOffsetRightLeftFL[i] * machineOffsetRightLeftFL[i]) + (machineOffsetUpDownFL[i] * machineOffsetUpDownFL[i]));
                        // FLとオフセットFLの角度を取得する
                        double angleOffsetFL = Math.Atan2(machineOffsetRightLeftFL[i], machineOffsetUpDownFL[i]);
                        double angleDegree = (double)(angleOffsetFL * 180 / Math.PI);
                        //Console.WriteLine(angleDegree);
                        machinePositionOffsetFLX[i] = machinePositionFLX[i] + lengthOffsetFL * Math.Cos(angle + angleOffsetFL);
                        machinePositionOffsetFLY[i] = machinePositionFLY[i] + lengthOffsetFL * Math.Sin(angle + angleOffsetFL);


                        double lengthOffsetFR = Math.Sqrt((machineOffsetRightLeftFR[i] * machineOffsetRightLeftFR[i]) + (machineOffsetUpDownFR[i] * machineOffsetUpDownFR[i]));
                        //FRとオフセットFRの角度を取得する
                        double angleOffsetFR = Math.Atan2(machineOffsetRightLeftFR[i], machineOffsetUpDownFR[i]);
                        angleDegree = (double)(angleOffsetFR * 180 / Math.PI);
                        machinePositionOffsetFRX[i] = machinePositionFRX[i] + lengthOffsetFR * Math.Cos(angle + angleOffsetFR);
                        machinePositionOffsetFRY[i] = machinePositionFRY[i] + lengthOffsetFR * Math.Sin(angle + angleOffsetFR);


                        double lengthOffsetBL = Math.Sqrt((machineOffsetRightLeftBL[i] * machineOffsetRightLeftBL[i]) + (machineOffsetUpDownBL[i] * machineOffsetUpDownBL[i]));
                        //BLとオフセットBLの角度を取得する
                        double angleOffsetBL = Math.Atan2(machineOffsetRightLeftBL[i], machineOffsetUpDownBL[i]);
                        angleDegree = (double)(angleOffsetBL * 180 / Math.PI);
                        machinePositionOffsetBLX[i] = machinePositionBLX[i] + lengthOffsetBL * Math.Cos(angle + angleOffsetBL);
                        machinePositionOffsetBLY[i] = machinePositionBLY[i] + lengthOffsetBL * Math.Sin(angle + angleOffsetBL);


                        double lengthOffsetBR = Math.Sqrt((machineOffsetRightLeftBR[i] * machineOffsetRightLeftBR[i]) + (machineOffsetUpDownBR[i] * machineOffsetUpDownBR[i]));
                        //BRとオフセットBRの角度を取得する
                        double angleOffsetBR = Math.Atan2(machineOffsetRightLeftBR[i], machineOffsetUpDownBR[i]);
                        angleDegree = (double)(angleOffsetBR * 180 / Math.PI);
                        machinePositionOffsetBRX[i] = machinePositionBRX[i] + lengthOffsetBR * Math.Cos(angle + angleOffsetBR);
                        machinePositionOffsetBRY[i] = machinePositionBRY[i] + lengthOffsetBR * Math.Sin(angle + angleOffsetBR);

                        machinePositionCx[i] = machinePositionOffsetFRX[i] - Math.Cos(angle2) * machineDiagonalLength[i] / 2.0;
                        machinePositionCy[i] = machinePositionOffsetFRY[i] - Math.Sin(angle2) * machineDiagonalLength[i] / 2.0;

                        //表示用位置（オフセット含）も更新する
                        viewMachinePositionOffsetFLX[i] = OriginX + machinePositionOffsetFLX[i] * (double)ScaleX;
                        viewMachinePositionOffsetFLY[i] = -(OriginY) - machinePositionOffsetFLY[i] * (double)ScaleY;
                        viewMachinePositionOffsetFRX[i] = OriginX + machinePositionOffsetFRX[i] * (double)ScaleX;
                        viewMachinePositionOffsetFRY[i] = -(OriginY) - machinePositionOffsetFRY[i] * (double)ScaleY;
                        viewMachinePositionOffsetBLX[i] = OriginX + machinePositionOffsetBLX[i] * (double)ScaleX;
                        viewMachinePositionOffsetBLY[i] = -(OriginY) - machinePositionOffsetBLY[i] * (double)ScaleY;
                        viewMachinePositionOffsetBRX[i] = OriginX + machinePositionOffsetBRX[i] * (double)ScaleX;
                        viewMachinePositionOffsetBRY[i] = -(OriginY) - machinePositionOffsetBRY[i] * (double)ScaleY;
                    }

                    // （右後）-（左後）
                    //   〇----・
                    //   ｜  ／｜
                    //   ｜／  ｜
                    //   ◎----・
                    if (tmpStdErr2 > this.stdErrBL[i])
                    {
                        tmpStdErr2 = this.stdErrBL[i];

                        //中心点を求める
                        angle = Math.Atan2(machinePositionOffsetBLY[i] - machinePositionOffsetBRY[i], machinePositionOffsetBLX[i] - machinePositionOffsetBRX[i]);
                        angle2 = angle - Math.PI / 2.0;
                        angle3 = machineDiagonalAngle[i] + angle2;
                        double angleDegree = (double)(angle3 * 180 / Math.PI);
                        //Console.WriteLine(angleDegree);
                        // 重機の向きを考慮してオフセットを設定する

                        // オフセットの長さを取得する
                        double lengthOffsetFL = Math.Sqrt((machineOffsetRightLeftFL[i] * machineOffsetRightLeftFL[i]) + (machineOffsetUpDownFL[i] * machineOffsetUpDownFL[i]));
                        // FLとオフセットFLの角度を取得する
                        double angleOffsetFL = Math.Atan2(machineOffsetRightLeftFL[i], machineOffsetUpDownFL[i]);


                        machinePositionOffsetFLX[i] = machinePositionFLX[i] + lengthOffsetFL * Math.Cos(angle2 + angleOffsetFL);
                        machinePositionOffsetFLY[i] = machinePositionFLY[i] + lengthOffsetFL * Math.Sin(angle2 + angleOffsetFL);


                        double lengthOffsetFR = Math.Sqrt((machineOffsetRightLeftFR[i] * machineOffsetRightLeftFR[i]) + (machineOffsetUpDownFR[i] * machineOffsetUpDownFR[i]));
                        //FRとオフセットFRの角度を取得する
                        double angleOffsetFR = Math.Atan2(machineOffsetRightLeftFR[i], machineOffsetUpDownFR[i]);
                        angleDegree = (double)(angleOffsetFR * 180 / Math.PI);
                        machinePositionOffsetFRX[i] = machinePositionFRX[i] + lengthOffsetFR * Math.Cos(angle2 + angleOffsetFR);
                        machinePositionOffsetFRY[i] = machinePositionFRY[i] + lengthOffsetFR * Math.Sin(angle2 + angleOffsetFR);


                        double lengthOffsetBL = Math.Sqrt((machineOffsetRightLeftBL[i] * machineOffsetRightLeftBL[i]) + (machineOffsetUpDownBL[i] * machineOffsetUpDownBL[i]));
                        //BLとオフセットBLの角度を取得する
                        double angleOffsetBL = Math.Atan2(machineOffsetRightLeftBL[i], machineOffsetUpDownBL[i]);
                        angleDegree = (double)(angleOffsetBL * 180 / Math.PI);
                        machinePositionOffsetBLX[i] = machinePositionBLX[i] + lengthOffsetBL * Math.Cos(angle2 + angleOffsetBL);
                        machinePositionOffsetBLY[i] = machinePositionBLY[i] + lengthOffsetBL * Math.Sin(angle2 + angleOffsetBL);


                        double lengthOffsetBR = Math.Sqrt((machineOffsetRightLeftBR[i] * machineOffsetRightLeftBR[i]) + (machineOffsetUpDownBR[i] * machineOffsetUpDownBR[i]));
                        //BRとオフセットBRの角度を取得する
                        double angleOffsetBR = Math.Atan2(machineOffsetRightLeftBR[i], machineOffsetUpDownBR[i]);
                        angleDegree = (double)(angleOffsetBR * 180 / Math.PI);
                        machinePositionOffsetBRX[i] = machinePositionBRX[i] + lengthOffsetBR * Math.Cos(angle2 + angleOffsetBR);
                        machinePositionOffsetBRY[i] = machinePositionBRY[i] + lengthOffsetBR * Math.Sin(angle2 + angleOffsetBR);

                        machinePositionCx[i] = machinePositionOffsetBRX[i] + Math.Cos(angle3) * machineDiagonalLength[i] / 2.0;
                        machinePositionCy[i] = machinePositionOffsetBRY[i] + Math.Sin(angle3) * machineDiagonalLength[i] / 2.0;

                        //表示用位置（オフセット含）も更新する
                        viewMachinePositionOffsetFLX[i] = OriginX + machinePositionOffsetFLX[i] * (double)ScaleX;
                        viewMachinePositionOffsetFLY[i] = -(OriginY) - machinePositionOffsetFLY[i] * (double)ScaleY;
                        viewMachinePositionOffsetFRX[i] = OriginX + machinePositionOffsetFRX[i] * (double)ScaleX;
                        viewMachinePositionOffsetFRY[i] = -(OriginY) - machinePositionOffsetFRY[i] * (double)ScaleY;
                        viewMachinePositionOffsetBLX[i] = OriginX + machinePositionOffsetBLX[i] * (double)ScaleX;
                        viewMachinePositionOffsetBLY[i] = -(OriginY) - machinePositionOffsetBLY[i] * (double)ScaleY;
                        viewMachinePositionOffsetBRX[i] = OriginX + machinePositionOffsetBRX[i] * (double)ScaleX;
                        viewMachinePositionOffsetBRY[i] = -(OriginY) - machinePositionOffsetBRY[i] * (double)ScaleY;
                    }
                }

                //
                // （左後）の精度が１番高い
                //
                if (tmpStdErr > this.stdErrBL[i])
                {
                    tmpStdErr = this.stdErrBL[i];

                    // 次に精度の高い点を取得する
                    tmpStdErr2 = double.MaxValue;

                    // （左後）-（左前）
                    //   ◎----〇
                    //   ｜  ／｜
                    //   ｜／  ｜
                    //   ・----・
                    if (tmpStdErr2 > this.stdErrFL[i])
                    {
                        tmpStdErr2 = this.stdErrFL[i];

                        // 中心点を求める
                        angle = Math.Atan2(machinePositionOffsetFLY[i] - machinePositionOffsetBLY[i], machinePositionOffsetFLX[i] - machinePositionOffsetBLX[i]);
                        angle2 = Math.PI / 2.0 - angle;
                        angle3 = machineDiagonalAngle[i] + angle;

                        // 重機の向きを考慮してオフセットを設定する

                        // オフセットの長さを取得する
                        double lengthOffsetFL = Math.Sqrt((machineOffsetRightLeftFL[i] * machineOffsetRightLeftFL[i]) + (machineOffsetUpDownFL[i] * machineOffsetUpDownFL[i]));
                        // FLとオフセットFLの角度を取得する
                        double angleOffsetFL = Math.Atan2(machineOffsetRightLeftFL[i], machineOffsetUpDownFL[i]);


                        machinePositionOffsetFLX[i] = machinePositionFLX[i] + lengthOffsetFL * Math.Cos(angle + angleOffsetFL);
                        machinePositionOffsetFLY[i] = machinePositionFLY[i] + lengthOffsetFL * Math.Sin(angle + angleOffsetFL);


                        double lengthOffsetFR = Math.Sqrt((machineOffsetRightLeftFR[i] * machineOffsetRightLeftFR[i]) + (machineOffsetUpDownFR[i] * machineOffsetUpDownFR[i]));
                        //FRとオフセットFRの角度を取得する
                        double angleOffsetFR = Math.Atan2(machineOffsetRightLeftFR[i], machineOffsetUpDownFR[i]);
                        double angleDegree = (double)(angleOffsetFR * 180 / Math.PI);
                        machinePositionOffsetFRX[i] = machinePositionFRX[i] + lengthOffsetFR * Math.Cos(angle + angleOffsetFR);
                        machinePositionOffsetFRY[i] = machinePositionFRY[i] + lengthOffsetFR * Math.Sin(angle + angleOffsetFR);


                        double lengthOffsetBL = Math.Sqrt((machineOffsetRightLeftBL[i] * machineOffsetRightLeftBL[i]) + (machineOffsetUpDownBL[i] * machineOffsetUpDownBL[i]));
                        //BLとオフセットBLの角度を取得する
                        double angleOffsetBL = Math.Atan2(machineOffsetRightLeftBL[i], machineOffsetUpDownBL[i]);
                        angleDegree = (double)(angleOffsetBL * 180 / Math.PI);
                        machinePositionOffsetBLX[i] = machinePositionBLX[i] + lengthOffsetBL * Math.Cos(angle + angleOffsetBL);
                        machinePositionOffsetBLY[i] = machinePositionBLY[i] + lengthOffsetBL * Math.Sin(angle + angleOffsetBL);


                        double lengthOffsetBR = Math.Sqrt((machineOffsetRightLeftBR[i] * machineOffsetRightLeftBR[i]) + (machineOffsetUpDownBR[i] * machineOffsetUpDownBR[i]));
                        //BRとオフセットBRの角度を取得する
                        double angleOffsetBR = Math.Atan2(machineOffsetRightLeftBR[i], machineOffsetUpDownBR[i]);
                        angleDegree = (double)(angleOffsetBR * 180 / Math.PI);
                        machinePositionOffsetBRX[i] = machinePositionBRX[i] + lengthOffsetBR * Math.Cos(angle + angleOffsetBR);
                        machinePositionOffsetBRY[i] = machinePositionBRY[i] + lengthOffsetBR * Math.Sin(angle + angleOffsetBR);

                        machinePositionCx[i] = machinePositionOffsetFLX[i] - Math.Cos(angle3) * machineDiagonalLength[i] / 2.0;
                        machinePositionCy[i] = machinePositionOffsetFLY[i] - Math.Sin(angle3) * machineDiagonalLength[i] / 2.0;

                        //表示用位置（オフセット含）も更新する
                        viewMachinePositionOffsetFLX[i] = OriginX + machinePositionOffsetFLX[i] * (double)ScaleX;
                        viewMachinePositionOffsetFLY[i] = -(OriginY) - machinePositionOffsetFLY[i] * (double)ScaleY;
                        viewMachinePositionOffsetFRX[i] = OriginX + machinePositionOffsetFRX[i] * (double)ScaleX;
                        viewMachinePositionOffsetFRY[i] = -(OriginY) - machinePositionOffsetFRY[i] * (double)ScaleY;
                        viewMachinePositionOffsetBLX[i] = OriginX + machinePositionOffsetBLX[i] * (double)ScaleX;
                        viewMachinePositionOffsetBLY[i] = -(OriginY) - machinePositionOffsetBLY[i] * (double)ScaleY;
                        viewMachinePositionOffsetBRX[i] = OriginX + machinePositionOffsetBRX[i] * (double)ScaleX;
                        viewMachinePositionOffsetBRY[i] = -(OriginY) - machinePositionOffsetBRY[i] * (double)ScaleY;
                    }

                    // （左後）-（右前）
                    // 
                    // 　◎----・
                    //   ｜  ／｜
                    //   ｜／  ｜
                    // 　・----〇
                    if (tmpStdErr2 > this.stdErrFR[i])
                    {
                        tmpStdErr2 = this.stdErrFR[i];

                        // 中心点を求める
                        angle = Math.Atan2(machinePositionOffsetFRY[i] - machinePositionOffsetBLY[i], machinePositionOffsetFRX[i] - machinePositionOffsetBLX[i]);
                        angle2 = angle + machineDiagonalAngle[i];
                        double angleDegree = (double)(angle2 * 180 / Math.PI);
                        //Console.WriteLine(angleDegree);
                        //angle = Math.Atan2(machinePositionOffsetFRY[i] - machinePositionOffsetBLY[i], machinePositionOffsetFRX[i] - machinePositionOffsetBLX[i]);
                        // 重機の向きを考慮してオフセットを設定する

                        // オフセットの長さを取得する
                        double lengthOffsetFL = Math.Sqrt((machineOffsetRightLeftFL[i] * machineOffsetRightLeftFL[i]) + (machineOffsetUpDownFL[i] * machineOffsetUpDownFL[i]));
                        // FLとオフセットFLの角度を取得する
                        double angleOffsetFL = Math.Atan2(machineOffsetRightLeftFL[i], machineOffsetUpDownFL[i]);


                        machinePositionOffsetFLX[i] = machinePositionFLX[i] + lengthOffsetFL * Math.Cos(angle2 + angleOffsetFL);
                        machinePositionOffsetFLY[i] = machinePositionFLY[i] + lengthOffsetFL * Math.Sin(angle2 + angleOffsetFL);


                        double lengthOffsetFR = Math.Sqrt((machineOffsetRightLeftFR[i] * machineOffsetRightLeftFR[i]) + (machineOffsetUpDownFR[i] * machineOffsetUpDownFR[i]));
                        //FRとオフセットFRの角度を取得する
                        double angleOffsetFR = Math.Atan2(machineOffsetRightLeftFR[i], machineOffsetUpDownFR[i]);
                        machinePositionOffsetFRX[i] = machinePositionFRX[i] + lengthOffsetFR * Math.Cos(angle2 + angleOffsetFR);
                        machinePositionOffsetFRY[i] = machinePositionFRY[i] + lengthOffsetFR * Math.Sin(angle2 + angleOffsetFR);


                        double lengthOffsetBL = Math.Sqrt((machineOffsetRightLeftBL[i] * machineOffsetRightLeftBL[i]) + (machineOffsetUpDownBL[i] * machineOffsetUpDownBL[i]));
                        //BLとオフセットBLの角度を取得する
                        double angleOffsetBL = Math.Atan2(machineOffsetRightLeftBL[i], machineOffsetUpDownBL[i]);
                        angleDegree = (double)(angleOffsetBL * 180 / Math.PI);
                        machinePositionOffsetBLX[i] = machinePositionBLX[i] + lengthOffsetBL * Math.Cos(angle2 + angleOffsetBL);
                        machinePositionOffsetBLY[i] = machinePositionBLY[i] + lengthOffsetBL * Math.Sin(angle2 + angleOffsetBL);


                        double lengthOffsetBR = Math.Sqrt((machineOffsetRightLeftBR[i] * machineOffsetRightLeftBR[i]) + (machineOffsetUpDownBR[i] * machineOffsetUpDownBR[i]));
                        //BRとオフセットBRの角度を取得する
                        double angleOffsetBR = Math.Atan2(machineOffsetRightLeftBR[i], machineOffsetUpDownBR[i]);
                        angleDegree = (double)(angleOffsetBR * 180 / Math.PI);
                        machinePositionOffsetBRX[i] = machinePositionBRX[i] + lengthOffsetBR * Math.Cos(angle2 + angleOffsetBR);
                        machinePositionOffsetBRY[i] = machinePositionBRY[i] + lengthOffsetBR * Math.Sin(angle2 + angleOffsetBR);

                        machinePositionCx[i] = machinePositionOffsetFRX[i] - Math.Cos(angle) * machineDiagonalLength[i] / 2.0;
                        machinePositionCy[i] = machinePositionOffsetFRY[i] - Math.Sin(angle) * machineDiagonalLength[i] / 2.0;

                        //表示用位置（オフセット含）も更新する
                        viewMachinePositionOffsetFLX[i] = OriginX + machinePositionOffsetFLX[i] * (double)ScaleX;
                        viewMachinePositionOffsetFLY[i] = -(OriginY) - machinePositionOffsetFLY[i] * (double)ScaleY;
                        viewMachinePositionOffsetFRX[i] = OriginX + machinePositionOffsetFRX[i] * (double)ScaleX;
                        viewMachinePositionOffsetFRY[i] = -(OriginY) - machinePositionOffsetFRY[i] * (double)ScaleY;
                        viewMachinePositionOffsetBLX[i] = OriginX + machinePositionOffsetBLX[i] * (double)ScaleX;
                        viewMachinePositionOffsetBLY[i] = -(OriginY) - machinePositionOffsetBLY[i] * (double)ScaleY;
                        viewMachinePositionOffsetBRX[i] = OriginX + machinePositionOffsetBRX[i] * (double)ScaleX;
                        viewMachinePositionOffsetBRY[i] = -(OriginY) - machinePositionOffsetBRY[i] * (double)ScaleY;
                    }

                    // （左後）-（右後）
                    // 
                    // 　◎----・
                    //   ｜  ／｜
                    //   ｜／  ｜
                    // 　〇----・
                    if (tmpStdErr2 > this.stdErrBR[i])
                    {
                        tmpStdErr2 = this.stdErrBR[i];

                        // 中心点を求める
                        angle = Math.Atan2(machinePositionOffsetBLY[i] - machinePositionOffsetBRY[i], machinePositionOffsetBLX[i] - machinePositionOffsetBRX[i]);
                        angle2 = angle - Math.PI / 2.0;
                        angle3 = machineDiagonalAngle[i] + angle2;

                        // 重機の向きを考慮してオフセットを設定する

                        // オフセットの長さを取得する
                        double lengthOffsetFL = Math.Sqrt((machineOffsetRightLeftFL[i] * machineOffsetRightLeftFL[i]) + (machineOffsetUpDownFL[i] * machineOffsetUpDownFL[i]));
                        // FLとオフセットFLの角度を取得する
                        double angleOffsetFL = Math.Atan2(machineOffsetRightLeftFL[i], machineOffsetUpDownFL[i]);


                        machinePositionOffsetFLX[i] = machinePositionFLX[i] + lengthOffsetFL * Math.Cos(angle2 + angleOffsetFL);
                        machinePositionOffsetFLY[i] = machinePositionFLY[i] + lengthOffsetFL * Math.Sin(angle2 + angleOffsetFL);


                        double lengthOffsetFR = Math.Sqrt((machineOffsetRightLeftFR[i] * machineOffsetRightLeftFR[i]) + (machineOffsetUpDownFR[i] * machineOffsetUpDownFR[i]));
                        //FRとオフセットFRの角度を取得する
                        double angleOffsetFR = Math.Atan2(machineOffsetRightLeftFR[i], machineOffsetUpDownFR[i]);
                        machinePositionOffsetFRX[i] = machinePositionFRX[i] + lengthOffsetFR * Math.Cos(angle2 + angleOffsetFR);
                        machinePositionOffsetFRY[i] = machinePositionFRY[i] + lengthOffsetFR * Math.Sin(angle2 + angleOffsetFR);


                        double lengthOffsetBL = Math.Sqrt((machineOffsetRightLeftBL[i] * machineOffsetRightLeftBL[i]) + (machineOffsetUpDownBL[i] * machineOffsetUpDownBL[i]));
                        //BLとオフセットBLの角度を取得する
                        double angleOffsetBL = Math.Atan2(machineOffsetRightLeftBL[i], machineOffsetUpDownBL[i]);
                        double angleDegree = (double)(angleOffsetBL * 180 / Math.PI);
                        machinePositionOffsetBLX[i] = machinePositionBLX[i] + lengthOffsetBL * Math.Cos(angle2 + angleOffsetBL);
                        machinePositionOffsetBLY[i] = machinePositionBLY[i] + lengthOffsetBL * Math.Sin(angle2 + angleOffsetBL);


                        double lengthOffsetBR = Math.Sqrt((machineOffsetRightLeftBR[i] * machineOffsetRightLeftBR[i]) + (machineOffsetUpDownBR[i] * machineOffsetUpDownBR[i]));
                        //BRとオフセットBRの角度を取得する
                        double angleOffsetBR = Math.Atan2(machineOffsetRightLeftBR[i], machineOffsetUpDownBR[i]);
                        angleDegree = (double)(angleOffsetBR * 180 / Math.PI);
                        machinePositionOffsetBRX[i] = machinePositionBRX[i] + lengthOffsetBR * Math.Cos(angle2 + angleOffsetBR);
                        machinePositionOffsetBRY[i] = machinePositionBRY[i] + lengthOffsetBR * Math.Sin(angle2 + angleOffsetBR);

                        machinePositionCx[i] = machinePositionOffsetBRX[i] + Math.Cos(angle3) * machineDiagonalLength[i] / 2.0;
                        machinePositionCy[i] = machinePositionOffsetBRY[i] + Math.Sin(angle3) * machineDiagonalLength[i] / 2.0;

                        //表示用位置（オフセット含）も更新する
                        viewMachinePositionOffsetFLX[i] = OriginX + machinePositionOffsetFLX[i] * (double)ScaleX;
                        viewMachinePositionOffsetFLY[i] = -(OriginY) - machinePositionOffsetFLY[i] * (double)ScaleY;
                        viewMachinePositionOffsetFRX[i] = OriginX + machinePositionOffsetFRX[i] * (double)ScaleX;
                        viewMachinePositionOffsetFRY[i] = -(OriginY) - machinePositionOffsetFRY[i] * (double)ScaleY;
                        viewMachinePositionOffsetBLX[i] = OriginX + machinePositionOffsetBLX[i] * (double)ScaleX;
                        viewMachinePositionOffsetBLY[i] = -(OriginY) - machinePositionOffsetBLY[i] * (double)ScaleY;
                        viewMachinePositionOffsetBRX[i] = OriginX + machinePositionOffsetBRX[i] * (double)ScaleX;
                        viewMachinePositionOffsetBRY[i] = -(OriginY) - machinePositionOffsetBRY[i] * (double)ScaleY;
                    }
                }

                // 表示用の重機の位置を更新する
                viewMachinePositionCx[i] = OriginX + machinePositionCx[i] * (double)ScaleX;
                viewMachinePositionCy[i] = -(OriginY) - machinePositionCy[i] * (double)ScaleY;


                // 2018/04/06 Rev1.10 ロックが有効の場合、座標を固定する start
                if (deserializedObect.LockMachinePositionList.LockMachinePositions[0].LockEnableX)
                {
                    viewMachinePositionCx[i] = OriginX + deserializedObect.LockMachinePositionList.LockMachinePositions[0].LockPositionX * (double)ScaleX;
                }
                if (deserializedObect.LockMachinePositionList.LockMachinePositions[0].LockEnableY)
                {
                    viewMachinePositionCy[i] = -(OriginY) - deserializedObect.LockMachinePositionList.LockMachinePositions[0].LockPositionY * (double)ScaleY;
                }
                // 2018/04/06 Rev1.10 ロックが有効の場合、座標を固定する end
            }
        }

        delegate void delegateState(object dummy, string[] locationString);
        void setNumber(object dummy, string[] locationString)
        {
            try
            {
                if (formTagState != null)
                {
                    formTagState.TagId = locationString[0];
                    formTagState.StdErr = locationString[1];
                    formTagState.X = locationString[2];
                    formTagState.Y = locationString[3];
                }
            }
            catch (Exception e)
            {
                Console.Write("err" + e.ToString());
            }
        }

        delegate void delegateWarning(object dummy, string warningText);

        void setWarningText(object dummy, string warningText)
        {
            try
            {
                if(formWarning != null)
                {
                    formWarning.WarningText = warningText;
                    WarningText = warningText;
                }
            }
            catch (Exception e)
            {
                Console.Write("err" + e.ToString());
            }
        }

        //
        // 画面右下の各種設定イベント
        //

        private void checkBoxVisibleMachineZone_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxVisibleMachineZone.Checked)
            {

                checkBoxVisibleMachineZone.Text = "☑ 重機ゾーン非表示";

                pictureBox1.Image = null;
                pictureBox1.Invalidate();
            }
            else
            {
                checkBoxVisibleMachineZone.Text = "☐ 重機ゾーン非表示";
                pictureBox1.Image = null;
                pictureBox1.Invalidate();
            }
        }
        
        private void checkBoxAreaLock1_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkBoxAreaLock1.Checked)
            {
                checkBoxAreaLock1.Text = " ☑ No１エリア固定 ";
                checkBoxAreaLocks[0] = true;
            }
            else
            {
                checkBoxAreaLock1.Text = " ☐ No１エリア固定 ";
                checkBoxAreaLocks[0] = false;
            }
        }

        private void checkBoxAreaLock2_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkBoxAreaLock2.Checked)
            {
                checkBoxAreaLock2.Text = " ☑ No２エリア固定 ";
                checkBoxAreaLocks[1] = true;
            }
            else
            {
                checkBoxAreaLock2.Text = " ☐ No２エリア固定 ";
                checkBoxAreaLocks[1] = false;
            }
        }

        private void checkBoxAreaLock3_CheckedChanged(object sender, EventArgs e)
        {

            if (this.checkBoxAreaLock3.Checked)
            {
                checkBoxAreaLock3.Text = " ☑ No３エリア固定 ";
                checkBoxAreaLocks[2] = true;
            }
            else
            {
                checkBoxAreaLock3.Text = " ☐ No３エリア固定 ";
                checkBoxAreaLocks[2] = false;
            }
        }

        private void checkBoxAreaLock4_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkBoxAreaLock4.Checked)
            {
                checkBoxAreaLock4.Text = " ☑ No４エリア固定 ";
                checkBoxAreaLocks[3] = true;
            }
            else
            {
                checkBoxAreaLock4.Text = " ☐ No４エリア固定 ";
                checkBoxAreaLocks[3] = false;
            }

        }

        private void checkBoxAreaLock5_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkBoxAreaLock5.Checked)
            {
                checkBoxAreaLock5.Text = " ☑ No５エリア固定 ";
                checkBoxAreaLocks[4] = true;
            }
            else
            {
                checkBoxAreaLock5.Text = " ☐ No５エリア固定 ";
                checkBoxAreaLocks[4] = false;
            }
        }
        private void checkBoxCoordinate_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxCoordinate.Checked)
            {
                
                checkBoxCoordinate.Text = " ☑ 座標表示           ";

                pictureBox1.Image = null;
                pictureBox1.Invalidate();
            }
            else
            {
                checkBoxCoordinate.Text = " ☐ 座標表示           ";
                pictureBox1.Image = null;
                pictureBox1.Invalidate();
            }
        }


        
        /// <summary>
        /// 開始ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonStart_Click(object sender, EventArgs e)
        {
            // 描画を一時停止する
            StopDraw();
            DialogResult result = MessageBox.Show("開始しますか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
            // 描画を再開する
            StartDraw();

            if (result != DialogResult.Yes)
            {
                return;
            }

            タグ設定ToolStripMenuItem.Enabled = false;

            buttonStop.BackColor = SystemColors.Control;
            buttonStart.BackColor = Color.LightGreen;

            buttonStart.Enabled = false;
            開始ToolStripMenuItem.Enabled = false;
            buttonStop.Enabled = true;
            停止ToolStripMenuItem.Enabled = true;

            checkBoxAreaLock1.Enabled = false;
            checkBoxAreaLock2.Enabled = false;
            checkBoxAreaLock3.Enabled = false;
            checkBoxAreaLock4.Enabled = false;
            checkBoxAreaLock5.Enabled = false;


            try
            {
                // CSVファイルを開く
                lcf = new LocationCsvFile(LocationDirectoryPath);
            }
            catch (Exception eLocationDirectoryPath)
            {
                MessageBox.Show(eLocationDirectoryPath.Message + " " + eLocationDirectoryPath.StackTrace);
            }
            this.lcf.Open();
        }

        /// <summary>
        /// 停止ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonStop_Click(object sender, EventArgs e)
        {
            // 描画を一時停止する
            StopDraw();
            DialogResult result = MessageBox.Show("停止しますか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
            // 描画を再開する
            StartDraw();

            if (result != DialogResult.Yes)
            {
                return;
            }

            タグ設定ToolStripMenuItem.Enabled = true;

            buttonStart.BackColor = SystemColors.Control;
            buttonStop.BackColor = Color.Coral;

            buttonStart.Enabled = true;
            buttonStop.Enabled = false;
            開始ToolStripMenuItem.Enabled = true;
            停止ToolStripMenuItem.Enabled = false;

            checkBoxAreaLock1.Enabled = true;
            checkBoxAreaLock2.Enabled = true;
            checkBoxAreaLock3.Enabled = true;
            checkBoxAreaLock4.Enabled = true;
            checkBoxAreaLock5.Enabled = true;

            // CSVファイルを閉じる
            this.lcf.Close();

            //
            // 停止時にパトライトも停止する
            //
            // 立ち入り禁止エリアの描画をクリアしてパトライトも停止する
            for (int i = 0; i < AREA_MAX; i++)
            {
                for (int j = 0; j < MACHINE_MAX; j++)
                {
                    flgAreaInZone2Machine[i, j] = false;
                    flgAreaInZone1Machine[i, j] = false;
                }
                for (int j = 0; j < USER_MAX; j++)
                {
                    flgAreaInZone2User[i, j] = false;
                    flgAreaInZone1User[i, j] = false;
                }
            }
            // 重機の描画をクリアしてパトライトも停止する
            for (int i = 0; i < MACHINE_MAX; i++)
            {
                for (int j = 0; j < MACHINE_MAX; j++)
                {
                    flgMachineInZone2Machine[i, j] = false;
                    flgMachineInZone1Machine[i, j] = false;
                }
                for (int j = 0; j < USER_MAX; j++)
                {
                    flgMachineInZone2User[i, j] = false;
                    flgMachineInZone1User[i, j] = false;
                }
            }
        }
        

        /// <summary>
        /// 描画イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            paintMonitor();
        }

        private void システム終了ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 描画を停止する
            StopDraw();
            DialogResult result = MessageBox.Show("システムを終了しますか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
            // 描画を再開する
            StartDraw();

            if (result != DialogResult.Yes)
            {
                return;
            }

            // タグ状態画面の更新を停止する
            flgUpdateTagState = false;

            //formTagState.EndThread();
            formTagState.Runnninng = false;
            formTagState.Close();
            formWarning.Close();

            // パトライトが点灯していたら消灯する
            for (int i = 0; i < AREA_MAX; i++)
            {
                // パトライトを消灯させる
                DoDosCommand(@"/c prsh " + areaPtIp[i] + " -l soctec alert 000000");
            }
            for (int i = 0; i < MACHINE_MAX; i++)
            {
                // パトライトを消灯させる
                DoDosCommand(@"/c prsh " + machinePtIp[i] + " -l soctec alert 000000");
            }

            // エリアが固定されている場合値を保持する
            // XML書き込み
            // 00000
            // [チェック有無][X][Y][X+OFFSET][Y+OFFSET]
            using (var dataSet = new DataSet())
            {
                dataSet.ReadXml(FILE_PATH);
                dataSet.Tables["LOCKAREA"].Rows[0]["NO1"] = (checkBoxAreaLock1.Checked) ? "1," + areaPositionX[0].ToString() + "," + areaPositionY[0].ToString() + "," + areaPositionOffsetX[0].ToString() + "," + areaPositionOffsetY[0].ToString() : dataSet.Tables["LOCKAREA"].Rows[0]["NO1"] = "0,0,0,0,0";
                dataSet.Tables["LOCKAREA"].Rows[0]["NO2"] = (checkBoxAreaLock2.Checked) ? "1," + areaPositionX[1].ToString() + "," + areaPositionY[1].ToString() + "," + areaPositionOffsetX[1].ToString() + "," + areaPositionOffsetY[1].ToString() : dataSet.Tables["LOCKAREA"].Rows[0]["NO2"] = "0,0,0,0,0";
                dataSet.Tables["LOCKAREA"].Rows[0]["NO3"] = (checkBoxAreaLock3.Checked) ? "1," + areaPositionX[2].ToString() + "," + areaPositionY[2].ToString() + "," + areaPositionOffsetX[2].ToString() + "," + areaPositionOffsetY[2].ToString() : dataSet.Tables["LOCKAREA"].Rows[0]["NO3"] = "0,0,0,0,0";
                dataSet.Tables["LOCKAREA"].Rows[0]["NO4"] = (checkBoxAreaLock4.Checked) ? "1," + areaPositionX[3].ToString() + "," + areaPositionY[3].ToString() + "," + areaPositionOffsetX[3].ToString() + "," + areaPositionOffsetY[3].ToString() : dataSet.Tables["LOCKAREA"].Rows[0]["NO4"] = "0,0,0,0,0";
                dataSet.Tables["LOCKAREA"].Rows[0]["NO5"] = (checkBoxAreaLock5.Checked) ? "1," + areaPositionX[4].ToString() + "," + areaPositionY[4].ToString() + "," + areaPositionOffsetX[4].ToString() + "," + areaPositionOffsetY[4].ToString() : dataSet.Tables["LOCKAREA"].Rows[0]["NO5"] = "0,0,0,0,0";
                dataSet.WriteXml(FILE_PATH);
            }

            multiCell.UnloadAllCells();
            namingSchema.Disconnect();
            
            sysLogFile.Write("Location Systemを終了します。");
            // ログファイルを閉じる
            sysLogFile.Close();

            Close();
        }

        private void システム設定ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var formSystem = new FormSystem();
            formSystem.CaissonImagePath = this.CaissonImagePath;
            formSystem.UserImagePath = this.UserImagePath;
            formSystem.MachineImagePath = this.MachineImagePath;
            formSystem.StartX = StartX;
            formSystem.StartY = StartY;
            formSystem.EndX = EndX;
            formSystem.EndY = EndY;
            formSystem.OriginX = OriginX;
            formSystem.OriginY = OriginY;
            formSystem.UpText = this.UpText;
            formSystem.DownText = this.DownText;
            formSystem.RightText = this.RightText;
            formSystem.LeftText = this.LeftText;
            formSystem.ViewAreaWidth = this.ViewAreaWidth;
            formSystem.ViewAreaHeight = this.ViewAreaHeight;
            formSystem.ScaleX = this.ScaleX;
            formSystem.ScaleY = this.ScaleY;
            formSystem.LocationDirectoryPath = this.LocationDirectoryPath;
            formSystem.LocationFileSaveDay = this.LocationFileSaveDay;

            menuStrip1.Enabled = false;
            checkBoxVisibleMachineZone.Enabled = false;
            checkBoxAreaLock1.Enabled = false;
            checkBoxAreaLock2.Enabled = false;
            checkBoxAreaLock3.Enabled = false;
            checkBoxAreaLock4.Enabled = false;
            checkBoxAreaLock5.Enabled = false;
            checkBoxCoordinate.Enabled = false;

            formSystem.ShowDialog();

            this.CaissonImagePath = formSystem.CaissonImagePath;
            this.UserImagePath = formSystem.UserImagePath;
            this.MachineImagePath = formSystem.MachineImagePath;
            this.StartX = formSystem.StartX;
            this.StartY = formSystem.StartY;
            this.EndX = formSystem.EndX;
            this.EndY = formSystem.EndY;
            this.OriginX = formSystem.OriginX;
            this.OriginY = formSystem.OriginY;
            this.UpText = formSystem.UpText;
            this.DownText = formSystem.DownText;
            this.RightText = formSystem.RightText;
            this.LeftText = formSystem.LeftText;
            this.ViewAreaWidth = formSystem.ViewAreaWidth;
            this.ViewAreaHeight = formSystem.ViewAreaHeight;
            this.ScaleX = formSystem.ScaleX;
            this.ScaleY = formSystem.ScaleY;
            // パスが変更された場合はLogファイルを開きなおす
            if (this.LocationDirectoryPath != formSystem.LocationDirectoryPath)
            {
                sysLogFile.Close();
                sysLogFile = null;
                sysLogFile = new SysLogFile(formSystem.LocationDirectoryPath, "Log.txt", DateTime.Now);
                sysLogFile.Open();
                sysLogFile.Write("Logフォルダが変更されました");

                // 侵入履歴ファイルを開きなおす
                formWarning.ReOpen(formSystem.LocationDirectoryPath);
            }
            this.LocationDirectoryPath = formSystem.LocationDirectoryPath;
            this.LocationFileSaveDay = formSystem.LocationFileSaveDay;

            formSystem.Dispose();

            menuStrip1.Enabled = true;
            checkBoxVisibleMachineZone.Enabled = true;
            checkBoxAreaLock1.Enabled = true;
            checkBoxAreaLock2.Enabled = true;
            checkBoxAreaLock3.Enabled = true;
            checkBoxAreaLock4.Enabled = true;
            checkBoxAreaLock5.Enabled = true;
            checkBoxCoordinate.Enabled = true;

            pictureBox1.Invalidate();

            // 上下右左設定
            labelUp.Text = UpText;
            labelDown.Text = DownText;

            // 左右は縦書きとする
            String tmp = "";

            for (int i = 0; i < RightText.Length; i++)
            {
                tmp = tmp + RightText[i];
                tmp = tmp + '\n';
            }
            labelRight.Text = tmp;

            tmp = "";

            for (int i = 0; i < LeftText.Length; i++)
            {
                tmp = tmp + LeftText[i];
                tmp = tmp + '\n';
            }
            labelLeft.Text = tmp;

            labelUp.Location = new Point(pictureBox1.Location.X + pictureBox1.Width / 2 - labelUp.Width / 2, pictureBox1.Location.Y + 1);
            labelDown.Location = new Point(pictureBox1.Location.X + pictureBox1.Width / 2 - labelDown.Width / 2, pictureBox1.Location.Y + pictureBox1.Height - labelDown.Height - 1);
            labelRight.Location = new Point(pictureBox1.Location.X + pictureBox1.Width - labelRight.Width - 1, pictureBox1.Location.Y + pictureBox1.Height / 2 - labelRight.Height / 2);
            labelLeft.Location = new Point(pictureBox1.Location.X + 1, pictureBox1.Location.Y + pictureBox1.Height / 2 - labelLeft.Height / 2);

            labelUp.Visible = (labelUp.Text == "") ? false : true;
            labelDown.Visible = (labelUp.Text == "") ? false : true;
            labelRight.Visible = (labelUp.Text == "") ? false : true;
            labelLeft.Visible = (labelUp.Text == "") ? false : true;

            try
            {
                if (bitmapBackImage != null)
                {
                    bitmapBackImage.Dispose();
                }
                bitmapBackImage = null;
                bitmapBackImage = new Bitmap(this.CaissonImagePath);

                if (bitmapMachineImage != null)
                {
                    bitmapMachineImage.Dispose();
                }
                bitmapMachineImage = null;
                bitmapMachineImage = new Bitmap(this.MachineImagePath);

                if (bitmapUserImage != null)
                {
                    bitmapUserImage.Dispose();
                }
                bitmapUserImage = null;
                bitmapUserImage = new Bitmap(this.UserImagePath);
            }
            catch (Exception eImage)
            {
                sysLogFile.Write(eImage.Message + " " + eImage.StackTrace);
            }
        }

        private void タグ設定ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var formSetting = new FormSetting();
            menuStrip1.Enabled = false;
            checkBoxVisibleMachineZone.Enabled = false;
            checkBoxAreaLock1.Enabled = false;
            checkBoxAreaLock2.Enabled = false;
            checkBoxAreaLock3.Enabled = false;
            checkBoxAreaLock4.Enabled = false;
            checkBoxAreaLock5.Enabled = false;
            checkBoxCoordinate.Enabled = false;

            // エリアが固定されている場合値を保持する
            // XML書き込み
            // 00000
            // [チェック有無][X][Y][X+OFFSET][Y+OFFSET]
            using (var dataSet = new DataSet())
            {
                dataSet.ReadXml(FILE_PATH);
                dataSet.Tables["LOCKAREA"].Rows[0]["NO1"] = (checkBoxAreaLock1.Checked) ? "1," + areaPositionX[0].ToString() + "," + areaPositionY[0].ToString() + "," + areaPositionOffsetX[0].ToString() + "," + areaPositionOffsetY[0].ToString() : dataSet.Tables["LOCKAREA"].Rows[0]["NO1"] = "0,0,0,0,0";
                dataSet.Tables["LOCKAREA"].Rows[0]["NO2"] = (checkBoxAreaLock2.Checked) ? "1," + areaPositionX[1].ToString() + "," + areaPositionY[1].ToString() + "," + areaPositionOffsetX[1].ToString() + "," + areaPositionOffsetY[1].ToString() : dataSet.Tables["LOCKAREA"].Rows[0]["NO2"] = "0,0,0,0,0";
                dataSet.Tables["LOCKAREA"].Rows[0]["NO3"] = (checkBoxAreaLock3.Checked) ? "1," + areaPositionX[2].ToString() + "," + areaPositionY[2].ToString() + "," + areaPositionOffsetX[2].ToString() + "," + areaPositionOffsetY[2].ToString() : dataSet.Tables["LOCKAREA"].Rows[0]["NO3"] = "0,0,0,0,0";
                dataSet.Tables["LOCKAREA"].Rows[0]["NO4"] = (checkBoxAreaLock4.Checked) ? "1," + areaPositionX[3].ToString() + "," + areaPositionY[3].ToString() + "," + areaPositionOffsetX[3].ToString() + "," + areaPositionOffsetY[3].ToString() : dataSet.Tables["LOCKAREA"].Rows[0]["NO4"] = "0,0,0,0,0";
                dataSet.Tables["LOCKAREA"].Rows[0]["NO5"] = (checkBoxAreaLock5.Checked) ? "1," + areaPositionX[4].ToString() + "," + areaPositionY[4].ToString() + "," + areaPositionOffsetX[4].ToString() + "," + areaPositionOffsetY[4].ToString() : dataSet.Tables["LOCKAREA"].Rows[0]["NO5"] = "0,0,0,0,0";
                dataSet.WriteXml(FILE_PATH);
            }

            formSetting.ShowDialog();
            formSetting.Dispose();
            menuStrip1.Enabled = true;
            checkBoxVisibleMachineZone.Enabled = true;
            checkBoxAreaLock1.Enabled = true;
            checkBoxAreaLock2.Enabled = true;
            checkBoxAreaLock3.Enabled = true;
            checkBoxAreaLock4.Enabled = true;
            checkBoxAreaLock5.Enabled = true;
            checkBoxCoordinate.Enabled = true;
            ReadXML();
            
            // タグ状態画面のタグNoと名称を更新する
            formTagState.UpdateTagNo();
        }

        private void 開始ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            buttonStart.PerformClick();
        }

        private void 停止ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            buttonStop.PerformClick();
        }
       
        private void 侵入履歴ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            formWarning.Show();
        }

        private void タグ状態ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            formTagState.Show();
        }

        private void バージョンToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var formHelp = new FormHelp();
            formHelp.Show();
        }

        #region ×ボタン無効化
        protected override CreateParams CreateParams
        {
            [SecurityPermission(SecurityAction.Demand,
                Flags = SecurityPermissionFlag.UnmanagedCode)]
            get
            {
                const int CS_NOCLOSE = 0x200;
                CreateParams cp = base.CreateParams;
                cp.ClassStyle = cp.ClassStyle | CS_NOCLOSE;

                return cp;
            }
        }
        #endregion ×ボタン無効化

        #region XML
        /// <summary>
        /// XMLファイルを読み込む
        /// </summary>
        public void ReadXML()
        {
            var doc = new XmlDocument();
            
            doc.Load(@"Setting.xml");

            XmlNodeList nodeList;
            XmlNode root = doc.DocumentElement;

            int i;

            nodeList = root.SelectNodes("USERTEXT");

            //Change the price on the books.
            foreach (XmlNode nd in nodeList)
            {
                // テキスト
                Console.WriteLine(nd.SelectSingleNode("NOTEXT").InnerText);
                Console.WriteLine(nd.SelectSingleNode("USETEXT").InnerText);
                Console.WriteLine(nd.SelectSingleNode("TAGTEXT").InnerText);
                Console.WriteLine(nd.SelectSingleNode("NAMETEXT").InnerText);
            }

            nodeList = root.SelectNodes("AREATEXT");

            i = 0;
            //Change the price on the books.
            foreach (XmlNode nd in nodeList)
            {
                // テキスト
                Console.WriteLine(nd.SelectSingleNode("NOTEXT").InnerText);
                Console.WriteLine(nd.SelectSingleNode("USETEXT").InnerText);
                Console.WriteLine(nd.SelectSingleNode("TAGTEXT").InnerText);
                Console.WriteLine(nd.SelectSingleNode("NAMETEXT").InnerText);
                Console.WriteLine(nd.SelectSingleNode("PTIPTEXT").InnerText);
                Console.WriteLine(nd.SelectSingleNode("OFFSETUPDOWNTEXT").InnerText);
                Console.WriteLine(nd.SelectSingleNode("OFFSETRIGHTLEFTTEXT").InnerText);
                Console.WriteLine(nd.SelectSingleNode("ZONE1TEXT").InnerText);
                Console.WriteLine(nd.SelectSingleNode("RADIUS1TEXT").InnerText);
                Console.WriteLine(nd.SelectSingleNode("PTCOLOR1TEXT").InnerText);
                Console.WriteLine(nd.SelectSingleNode("PTPATTERN1TEXT").InnerText);
                Console.WriteLine(nd.SelectSingleNode("PTBUZZER1TEXT").InnerText);
                Console.WriteLine(nd.SelectSingleNode("ZONE2TEXT").InnerText);
                Console.WriteLine(nd.SelectSingleNode("RADIUS2TEXT").InnerText);
                Console.WriteLine(nd.SelectSingleNode("PTCOLOR2TEXT").InnerText);
                Console.WriteLine(nd.SelectSingleNode("PTBUZZER2TEXT").InnerText);
                i = i + 1;
            }

            nodeList = root.SelectNodes("MACHINETEXT");

            i = 0;
            //Change the price on the books.
            foreach (XmlNode nd in nodeList)
            {
                // テキスト
                Console.WriteLine(nd.SelectSingleNode("NOTEXT").InnerText);
                Console.WriteLine(nd.SelectSingleNode("USETEXT").InnerText);
                Console.WriteLine(nd.SelectSingleNode("TAGFLTEXT").InnerText);
                Console.WriteLine(nd.SelectSingleNode("TAGFRTEXT").InnerText);
                Console.WriteLine(nd.SelectSingleNode("TAGBLTEXT").InnerText);
                Console.WriteLine(nd.SelectSingleNode("TAGBRTEXT").InnerText);
                Console.WriteLine(nd.SelectSingleNode("NAMETEXT").InnerText);
                Console.WriteLine(nd.SelectSingleNode("PTIPTEXT").InnerText);
                Console.WriteLine(nd.SelectSingleNode("OFFSETUPDOWNFLTEXT").InnerText);
                Console.WriteLine(nd.SelectSingleNode("OFFSETRIGHTLEFTFLTEXT").InnerText);
                Console.WriteLine(nd.SelectSingleNode("OFFSETUPDOWNFRTEXT").InnerText);
                Console.WriteLine(nd.SelectSingleNode("OFFSETRIGHTLEFTFRTEXT").InnerText);
                Console.WriteLine(nd.SelectSingleNode("OFFSETUPDOWNBLTEXT").InnerText);
                Console.WriteLine(nd.SelectSingleNode("OFFSETRIGHTLEFTBLTEXT").InnerText);
                Console.WriteLine(nd.SelectSingleNode("OFFSETUPDOWNBRTEXT").InnerText);
                Console.WriteLine(nd.SelectSingleNode("OFFSETRIGHTLEFTBRTEXT").InnerText);
                Console.WriteLine(nd.SelectSingleNode("ZONE1TEXT").InnerText);
                Console.WriteLine(nd.SelectSingleNode("RADIUS1TEXT").InnerText);
                Console.WriteLine(nd.SelectSingleNode("PTCOLOR1TEXT").InnerText);
                Console.WriteLine(nd.SelectSingleNode("PTBUZZER1TEXT").InnerText);
                Console.WriteLine(nd.SelectSingleNode("ZONE2TEXT").InnerText);
                Console.WriteLine(nd.SelectSingleNode("RADIUS2TEXT").InnerText);
                Console.WriteLine(nd.SelectSingleNode("PTCOLOR2TEXT").InnerText);
                Console.WriteLine(nd.SelectSingleNode("PTBUZZER2TEXT").InnerText);
                i = i + 1;
            }

            nodeList = root.SelectNodes("USER");

            i = 0;
            //Change the price on the books.
            foreach (XmlNode nd in nodeList)
            {
                // テキスト
                this.userUse[i] = (nd.SelectSingleNode("USE").InnerText != "0") ? true : false;
                this.userTagNo[i] = nd.SelectSingleNode("TAG").InnerText;
                this.userName[i] = nd.SelectSingleNode("NAME").InnerText;

                //
                Console.WriteLine(nd.SelectSingleNode("NO").InnerText);
                Console.WriteLine(nd.SelectSingleNode("USE").InnerText);
                Console.WriteLine(nd.SelectSingleNode("TAG").InnerText);
                Console.WriteLine(nd.SelectSingleNode("NAME").InnerText);
                i = i + 1;
            }

            nodeList = root.SelectNodes("AREA");

            i = 0;
            //Change the price on the books.
            foreach (XmlNode nd in nodeList)
            {
                // テキスト
                this.areaUse[i] = (nd.SelectSingleNode("USE").InnerText != "0") ? true : false;
                areaTagNo[i] = nd.SelectSingleNode("TAG").InnerText;
                areaName[i] = nd.SelectSingleNode("NAME").InnerText;
                areaPtIp[i] = nd.SelectSingleNode("PTIP").InnerText;
                Double.TryParse(nd.SelectSingleNode("OFFSETUPDOWN").InnerText, out areaOffsetUpDown[i]);
                Double.TryParse(nd.SelectSingleNode("OFFSETRIGHTLEFT").InnerText, out areaOffsetRightLeft[i]);
                Double.TryParse(nd.SelectSingleNode("RADIUS1").InnerText, out areaYellowZone[i]);
                Int32.TryParse(nd.SelectSingleNode("PTCOLOR1").InnerText, out areaPtColor1[i]);
                Int32.TryParse(nd.SelectSingleNode("PTPATTERN1").InnerText, out areaPtPattern1[i]);
                this.areaPtBuzzer1[i] = (nd.SelectSingleNode("PTBUZZER1").InnerText != "0") ? true : false;
                Double.TryParse(nd.SelectSingleNode("RADIUS2").InnerText, out areaRedZone[i]);
                Int32.TryParse(nd.SelectSingleNode("PTCOLOR2").InnerText, out areaPtColor2[i]);
                Int32.TryParse(nd.SelectSingleNode("PTPATTERN2").InnerText, out areaPtPattern2[i]);
                this.areaPtBuzzer2[i] = (nd.SelectSingleNode("PTBUZZER2").InnerText != "0") ? true : false;
                i = i + 1;

            }

            nodeList = root.SelectNodes("MACHINE");

            i = 0;
            //Change the price on the books.
            foreach (XmlNode nd in nodeList)
            {
                // テキスト
                this.machineUse[i] = (nd.SelectSingleNode("USE").InnerText != "0") ? true : false;
                machineTagNoFL[i] = nd.SelectSingleNode("TAGFL").InnerText;
                machineTagNoFR[i] = nd.SelectSingleNode("TAGFR").InnerText;
                machineTagNoBL[i] = nd.SelectSingleNode("TAGBL").InnerText;
                machineTagNoBR[i] = nd.SelectSingleNode("TAGBR").InnerText;
                machineName[i] = nd.SelectSingleNode("NAME").InnerText;
                machinePtIp[i] = nd.SelectSingleNode("PTIP").InnerText;
                Double.TryParse(nd.SelectSingleNode("OFFSETUPDOWNFL").InnerText, out machineOffsetUpDownFL[i]);
                Double.TryParse(nd.SelectSingleNode("OFFSETRIGHTLEFTFL").InnerText, out machineOffsetRightLeftFL[i]);
                Double.TryParse(nd.SelectSingleNode("OFFSETUPDOWNFR").InnerText, out machineOffsetUpDownFR[i]);
                Double.TryParse(nd.SelectSingleNode("OFFSETRIGHTLEFTFR").InnerText, out machineOffsetRightLeftFR[i]);
                Double.TryParse(nd.SelectSingleNode("OFFSETUPDOWNBL").InnerText, out machineOffsetUpDownBL[i]);
                Double.TryParse(nd.SelectSingleNode("OFFSETRIGHTLEFTBL").InnerText, out machineOffsetRightLeftBL[i]);
                Double.TryParse(nd.SelectSingleNode("OFFSETUPDOWNBR").InnerText, out machineOffsetUpDownBR[i]);
                Double.TryParse(nd.SelectSingleNode("OFFSETRIGHTLEFTBR").InnerText, out machineOffsetRightLeftBR[i]);
                Double.TryParse(nd.SelectSingleNode("WIDTH").InnerText, out machineWidth[i]);
                Double.TryParse(nd.SelectSingleNode("HEIGHT").InnerText, out machineHeight[i]);
                Double.TryParse(nd.SelectSingleNode("RADIUS1").InnerText,out machineYellowZone[i]);
                Int32.TryParse(nd.SelectSingleNode("PTCOLOR1").InnerText, out machinePtColor1[i]);
                Int32.TryParse(nd.SelectSingleNode("PTPATTERN1").InnerText, out machinePtPattern1[i]);
                this.machinePtBuzzer1[i] = (nd.SelectSingleNode("PTBUZZER1").InnerText != "0") ? true : false;
                Double.TryParse(nd.SelectSingleNode("RADIUS2").InnerText, out machineRedZone[i]);
                Int32.TryParse(nd.SelectSingleNode("PTCOLOR2").InnerText, out machinePtColor2[i]);
                Int32.TryParse(nd.SelectSingleNode("PTPATTERN2").InnerText, out machinePtPattern2[i]);
                this.machinePtBuzzer2[i] = (nd.SelectSingleNode("PTBUZZER2").InnerText != "0") ? true : false;
                i = i + 1;
            }

            nodeList = root.SelectNodes("LOCATIONFILE");

            //Change the price on the books.
            foreach (XmlNode nd in nodeList)
            {
                // テキスト
                LocationDirectoryPath = nd.SelectSingleNode("PATH").InnerText;
                int tmp = 365;
                Int32.TryParse(nd.SelectSingleNode("SAVEDAY").InnerText, out tmp);
                LocationFileSaveDay = tmp;
            }

            nodeList = root.SelectNodes("MONITOR");

            //Change the price on the books.
            foreach (XmlNode nd in nodeList)
            {
                // テキスト
                Console.WriteLine(nd.SelectSingleNode("AREAHEIGHT").InnerText);
                Console.WriteLine(nd.SelectSingleNode("AREAWIDTH").InnerText);
                Console.WriteLine(nd.SelectSingleNode("SPHEIGHT").InnerText);
                Console.WriteLine(nd.SelectSingleNode("SPWIDTH").InnerText);
            }

            nodeList = root.SelectNodes("TAGSTATETEXT");

            //Change the price on the books.
            foreach (XmlNode nd in nodeList)
            {
                // テキスト
                Console.WriteLine(nd.SelectSingleNode("NOTEXT").InnerText);
                Console.WriteLine(nd.SelectSingleNode("TAGNOTEXT").InnerText);
                Console.WriteLine(nd.SelectSingleNode("NAMETEXT").InnerText);
                Console.WriteLine(nd.SelectSingleNode("STDERRTEXT").InnerText);
                Console.WriteLine(nd.SelectSingleNode("BATTERYTEXT").InnerText);
                Console.WriteLine(nd.SelectSingleNode("XTEXT").InnerText);
                Console.WriteLine(nd.SelectSingleNode("YTEXT").InnerText);
                Console.WriteLine(nd.SelectSingleNode("ZTEXT").InnerText);
            }

            nodeList = root.SelectNodes("TAGSTATE");

            //Change the price on the books.
            foreach (XmlNode nd in nodeList)
            {
                // テキスト
                Console.WriteLine(nd.SelectSingleNode("NO").InnerText);
                Console.WriteLine(nd.SelectSingleNode("TAGNO").InnerText);
                Console.WriteLine(nd.SelectSingleNode("NAME").InnerText);
                Console.WriteLine(nd.SelectSingleNode("STDERR").InnerText);
                Console.WriteLine(nd.SelectSingleNode("BATTERY").InnerText);
                Console.WriteLine(nd.SelectSingleNode("X").InnerText);
                Console.WriteLine(nd.SelectSingleNode("Y").InnerText);
                Console.WriteLine(nd.SelectSingleNode("Z").InnerText);
            }

            nodeList = root.SelectNodes("ORIGIN");

            //Change the price on the books.
            foreach (XmlNode nd in nodeList)
            {
                int x, y;
                // テキスト
                Int32.TryParse(nd.SelectSingleNode("X").InnerText, out x);
                Int32.TryParse(nd.SelectSingleNode("Y").InnerText, out y);
                OriginX = x;
                OriginY = y;
            }

            nodeList = root.SelectNodes("MONITOR");
            //Change the price on the books.
            foreach (XmlNode nd in nodeList)
            {
                CaissonImagePath = nd.SelectSingleNode("CAISSONIMAGEPATH").InnerText;
                UserImagePath = nd.SelectSingleNode("USERIMAGEPATH").InnerText;
                MachineImagePath = nd.SelectSingleNode("MACHINEIMAGEPATH").InnerText;
            }
            
            nodeList = root.SelectNodes("DIRECTION");

            //Change the price on the books.
            foreach (XmlNode nd in nodeList)
            {
                // テキスト
                UpText = nd.SelectSingleNode("UP").InnerText;
                DownText = nd.SelectSingleNode("DOWN").InnerText;
                RightText = nd.SelectSingleNode("RIGHT").InnerText;
                LeftText = nd.SelectSingleNode("LEFT").InnerText;
            }

            nodeList = root.SelectNodes("VIEWAREA");

            //Change the price on the books.
            foreach (XmlNode nd in nodeList)
            {
                // テキスト
                int sx, sy, ex, ey;
                decimal viewAreaWidth, viewAreaHeight;
                Int32.TryParse(nd.SelectSingleNode("STARTX").InnerText, out sx);
                Int32.TryParse(nd.SelectSingleNode("STARTY").InnerText, out sy);
                Int32.TryParse(nd.SelectSingleNode("ENDX").InnerText, out ex);
                Int32.TryParse(nd.SelectSingleNode("ENDY").InnerText, out ey);
                decimal.TryParse(nd.SelectSingleNode("WIDTH").InnerText, out viewAreaWidth);
                decimal.TryParse(nd.SelectSingleNode("HEIGHT").InnerText, out viewAreaHeight);
                StartX = sx;
                StartY = sy;
                EndX = ex;
                EndY = ey;
                ViewAreaWidth = viewAreaWidth;
                ViewAreaHeight = viewAreaHeight;
            }

            nodeList = root.SelectNodes("LOCKAREA");

            //Change the price on the books.
            foreach (XmlNode nd in nodeList)
            {
                string[] areaPosition;

                areaPosition = nd.SelectSingleNode("NO1").InnerText.Split(',');
                checkBoxAreaLock1.Checked =　(areaPosition[0] == "0") ? false : true;
                double.TryParse(areaPosition[1], out areaPositionX[0]);
                double.TryParse(areaPosition[2], out areaPositionY[0]);
                double.TryParse(areaPosition[3], out areaPositionOffsetX[0]);
                double.TryParse(areaPosition[4], out areaPositionOffsetY[0]);

                areaPosition = nd.SelectSingleNode("NO2").InnerText.Split(',');
                checkBoxAreaLock2.Checked = (areaPosition[0] == "0") ? false : true;
                double.TryParse(areaPosition[1], out areaPositionX[1]);
                double.TryParse(areaPosition[2], out areaPositionY[1]);
                double.TryParse(areaPosition[3], out areaPositionOffsetX[1]);
                double.TryParse(areaPosition[4], out areaPositionOffsetY[1]);

                areaPosition = nd.SelectSingleNode("NO3").InnerText.Split(',');
                checkBoxAreaLock3.Checked = (areaPosition[0] == "0") ? false : true;
                double.TryParse(areaPosition[1], out areaPositionX[2]);
                double.TryParse(areaPosition[2], out areaPositionY[2]);
                double.TryParse(areaPosition[3], out areaPositionOffsetX[2]);
                double.TryParse(areaPosition[4], out areaPositionOffsetY[2]);

                areaPosition = nd.SelectSingleNode("NO4").InnerText.Split(',');
                checkBoxAreaLock4.Checked = (areaPosition[0] == "0") ? false : true;
                double.TryParse(areaPosition[1], out areaPositionX[3]);
                double.TryParse(areaPosition[2], out areaPositionY[3]);
                double.TryParse(areaPosition[3], out areaPositionOffsetX[3]);
                double.TryParse(areaPosition[4], out areaPositionOffsetY[3]);

                areaPosition = nd.SelectSingleNode("NO5").InnerText.Split(',');
                checkBoxAreaLock5.Checked = (areaPosition[0] == "0") ? false : true;
                double.TryParse(areaPosition[1], out areaPositionX[4]);
                double.TryParse(areaPosition[2], out areaPositionY[4]);
                double.TryParse(areaPosition[1], out areaPositionOffsetX[4]);
                double.TryParse(areaPosition[2], out areaPositionOffsetY[4]);
            }

            ScaleX = ScaleX = (EndX - StartX) / ViewAreaWidth;
            ScaleY = (EndY - StartY) / ViewAreaHeight;


            // 取得したデータから計算する
            for (int j = 0; j < AREA_MAX; j++)
            {
                viewAreaPositionX[j] = OriginX + areaPositionX[j] * (double)ScaleX;
                viewAreaPositionY[j] = -(OriginY) - areaPositionY[j] * (double)ScaleY;
                viewAreaPositionOffsetX[j] = OriginX + areaPositionOffsetX[j] * (double)ScaleX;
                viewAreaPositionOffsetY[j] = -(OriginY) - areaPositionOffsetY[j] * (double)ScaleY;
            }


            for (int j = 0; j < MACHINE_MAX; j++)
            {
                machineDiagonalLength[j] = Math.Sqrt(machineWidth[j] * machineWidth[j] + machineHeight[j] * machineHeight[j]);
                viewmachineDiagonalLength[j] = (float)Math.Sqrt((machineWidth[j] * (float)ScaleX) * (machineWidth[j] * (float)ScaleX) + (machineHeight[j] * (float)ScaleY) * (machineHeight[j] * (float)ScaleY));
                machineDiagonalLengthHalf[j] = machineDiagonalLength[j] / 2.0;
                viewmachineDiagonalLengthHalf[j] = viewmachineDiagonalLength[j] / 2;
                machineDiagonalAngle[j] = Math.Atan2(machineHeight[j], machineWidth[j]);
                viewMachineWidth[j] = (float)machineWidth[j] * (float)ScaleX;
                viewMachineHeight[j] = (float)machineHeight[j] * (float)ScaleY;
            }

        }
        #endregion XML

        #region パトライト
        /// <summary>
        /// パトライト制御コマンドを作成する
        /// </summary>
        /// <param name="color"></param>
        /// <param name="pattern"></param>
        /// <param name="buzzer"></param>
        /// <returns></returns>
        private string makePtCommand(int color, int pattern, bool buzzer)
        {

            //
            // パトライト制御コマンドを作成する
            //
            // コマンド例(赤色点灯、黄色点滅パターン１、音なし)
            // 1   2   0   0   0   0
            // [赤][黄][×][×][×][音]

            // コマンドのフォーマットにデータを整形する
            string colorPattern = "00000";
            // 【色】 0:無 1:赤 2:黄
            // 【パターン】 0:点灯 1:点滅パターンA 2:点滅パターンB
            if (color == 0)
            {
                colorPattern = "00000";
            }
            else if (color == 1)
            {
                if (pattern == 0)
                {
                    colorPattern = "10000";
                }
                else if (pattern == 1)
                {
                    colorPattern = "20000";
                }
                else if (pattern == 2)
                {
                    colorPattern = "30000";
                }
            }
            else if (color == 2)
            {
                if (pattern == 0)
                {
                    colorPattern = "01000";
                }
                else if (pattern == 1)
                {
                    colorPattern = "02000";
                }
                else if (pattern == 2)
                {
                    colorPattern = "03000";
                }
            }
            else
            {
                // DO NOTHING
            }
            string buzzerStr = (buzzer == true) ? "1" : "0";

            return colorPattern + buzzerStr;
        }

        void DoDosCommand(string command)
        {
            //Processオブジェクトを作成
            System.Diagnostics.Process p = new System.Diagnostics.Process();

            //出力とエラーをストリームに書き込むようにする
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            //OutputDataReceivedとErrorDataReceivedイベントハンドラを追加
            p.OutputDataReceived += p_OutputDataReceived;
            p.ErrorDataReceived += p_ErrorDataReceived;

            p.StartInfo.FileName = System.Environment.GetEnvironmentVariable("ComSpec");
            p.StartInfo.RedirectStandardInput = false;
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.Arguments = command;

            //起動
            p.Start();

            //非同期で出力とエラーの読み取りを開始
            p.BeginOutputReadLine();
            p.BeginErrorReadLine();

            //p.WaitForExit();
            p.Close();

            Console.ReadLine();
        }

        //OutputDataReceivedイベントハンドラ
        //行が出力されるたびに呼び出される
        static void p_OutputDataReceived(object sender,
            System.Diagnostics.DataReceivedEventArgs e)
        {
            //出力された文字列を表示する
            Console.WriteLine(e.Data);

            //ptResult = true;
        }

        //ErrorDataReceivedイベントハンドラ
        static void p_ErrorDataReceived(object sender,
            System.Diagnostics.DataReceivedEventArgs e)
        {
            //エラー出力された文字列を表示する
            Console.WriteLine("ERR>{0}", e.Data);

            //ptResult = false;
        }
        #endregion パトライト

        #region スレッド
        
        //private bool running;//スレッド実行フラグ

        //
        // backgroundworker
        //
        //private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        //{
        //    BackgroundWorker bgWorker = (BackgroundWorker)sender;

        //    //キャンセルされたか調べる
        //    if (bgWorker.CancellationPending)
        //    {
        //        //キャンセルされたとき
        //        e.Cancel = true;
        //        return;
        //    }

        //    lock (this)
        //    {
        //        bgWorker.ReportProgress(0);
        //    }
        //}

        //private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        //{
        //    lock(this)
        //    {
        //        updatePosition();
        //    }
        //}

        //private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        //{
        //}

        //
        // task
        //
        //async void ServerProcess()
        //{
        //    running = true;//これ重要
        //    await Task.Run(() => {
        //        Invoke(new Action(() => {
        //            SetLocationAction();
        //        }));
        //        Task.Delay(50).Wait();
        //    });
        //}

        //void SetLocationAction()
        //{
        //    try
        //    {
        //        paintMonitor();
        //    }
        //    catch (Exception e)
        //    {
        //        Console.Write("err" + e.ToString());
        //    }
        //}
        #endregion スレッド

        #region 描画関数
        void paintMonitor()
        {
            // クリアする
            g.Clear(Color.White);
            pictureBox1.BackColor = Color.White;

            //
            // 背景画像を表示する
            //
            bitmap.Dispose();
            try
            {
                bitmap = (Bitmap)bitmapBackImage.Clone();
                float widthImg = bitmap.Width;
                float heightImg = bitmap.Height;
                float xImg = (float)pictureBox1.Width / (float)2.0 - (float)widthImg / (float)2.0;
                float yImg = (float)pictureBox1.Height / (float)2.0 - (float)heightImg / (float)2.0;

                if (pictureBox1.Width < bitmap.Width || pictureBox1.Height < bitmap.Height)
                {
                    double widthRate = (double)bitmap.Width / (double)pictureBox1.Width;
                    double heightRate = (double)bitmap.Height / (double)pictureBox1.Height;
                    if (widthRate < heightRate)
                    {
                        heightImg = pictureBox1.Height;
                        widthImg = heightImg * bitmap.Width / bitmap.Height;
                        xImg = pictureBox1.Width / 2 - widthImg / 2;
                        yImg = 0;
                    }
                    else
                    {
                        widthImg = pictureBox1.Width;
                        heightImg = widthImg * bitmap.Height / bitmap.Width;
                        xImg = 0;
                        yImg = pictureBox1.Height / 2 - heightImg / 2;
                    }
                }
                g.DrawImage(bitmap, xImg, yImg, widthImg, heightImg);

                // エリアを表示する
                g.DrawRectangle(Pens.LightGreen, StartX, StartY, EndX - StartX, EndY - StartY);

                // 原点位置を表示する
                g.FillEllipse(Brushes.Blue, OriginX - 10 / 2, -(OriginY) - 10 / 2, 10, 10);

                ///////////////////////////////////////////////////////

                var destinationPoints = new PointF[3];

                //double locationX, locationY;
                //double viewLocationX, viewLocationY;
                float width, height;


                ///////////////////////////////////////////////////////
                //
                // タグを描画する
                //

                //bitmap.Dispose();
                //// 重機サイズ
                //bitmap = (Bitmap)bitmapMachineImage.Clone();
                //width = viewMachineWidth;
                //height = viewMachineHeight;

                // エリアサイズ (エリアは画像なしのため幅と高さは0とする)
                width = 0;
                height = 0;

                //
                // エリアに対して人と（重機）とのあたり判定をする
                //
                for (int i = 0; i < AREA_MAX; i++)
                {
                    if (!areaUse[i])
                    {
                        continue;
                    }

                    //重機とのあたり判定をする
                    for (int j = 0; j < MACHINE_MAX; j++)
                    {
                        if (!machineUse[j])
                        {
                            continue;
                        }

                        // 開始時のみ警告を表示する
                        if (!buttonStart.Enabled)
                        {
                            // (xc1 - xc2) ^ 2 + (yc1 - yc2) ^ 2 ≦ (r1 + r2) ^ 2ならば衝突している
                            //２点間の距離が指定範囲以内か
                            double chk_distanceRed = ((areaRedZone[i] + machineDiagonalLengthHalf[j]) * (areaRedZone[i] + machineDiagonalLengthHalf[j]));
                            double chk_distanceYellow = ((areaYellowZone[i] + machineDiagonalLengthHalf[j]) * (areaYellowZone[i] + machineDiagonalLengthHalf[j]));
                            double machineX2 = (areaPositionOffsetX[i] - machinePositionCx[j]) * (areaPositionOffsetX[i] - machinePositionCx[j]);
                            double machineY2 = (areaPositionOffsetY[i] - machinePositionCy[j]) * (areaPositionOffsetY[i] - machinePositionCy[j]);
                            if (machineX2 + machineY2 <= chk_distanceRed)
                            {
                                // エリアのゾーン２に重機が入っている状態
                                flgAreaInZone2Machine[i, j] = true;
                                flgAreaInZone1Machine[i, j] = true;

                                if (!flgAreaInMessage2Machine[i, j])
                                {
                                    // 履歴を入力する
                                    string lv_item_str = DateTime.Now.ToString("MM/dd ") + DateTime.Now.ToLongTimeString() + " " + areaName[i] + "の第２ゾーンに" + machineName[j] + "が侵入しました";
                                    Invoke(new delegateWarning(setWarningText), null, lv_item_str);

                                    flgAreaInMessage2Machine[i, j] = true;
                                    flgAreaInMessage1Machine[i, j] = true;
                                    flgAreaInMessage0Machine[i, j] = false;
                                }

                            }
                            else if (machineX2 + machineY2 <= chk_distanceYellow)
                            {
                                // エリアのゾーン１に重機が入っている状態
                                flgAreaInZone1Machine[i, j] = true;
                                //flgAreaInZone2Machine[j] = false; ２は前回値を保持する

                                if (!flgAreaInMessage1Machine[i, j])
                                {
                                    // 履歴を入力する
                                    string lv_item_str = DateTime.Now.ToString("MM/dd ") + DateTime.Now.ToLongTimeString() + " " + areaName[i] + "の第１ゾーンに" + machineName[j] + "が侵入しました";
                                    Invoke(new delegateWarning(setWarningText), null, lv_item_str);

                                    flgAreaInMessage1Machine[i, j] = true;
                                    flgAreaInMessage0Machine[i, j] = false;
                                }
                            }
                            else
                            {
                                // エリアのゾーン（１、２）に重機が入っていない状態
                                flgAreaInZone1Machine[i, j] = false;
                                flgAreaInZone2Machine[i, j] = false;

                                if (!flgAreaInMessage0Machine[i, j])
                                {
                                    // 履歴を入力する
                                    string lv_item_str = DateTime.Now.ToString("MM/dd ") + DateTime.Now.ToLongTimeString() + " " + areaName[i] + "のゾーンから" + machineName[j] + "が出ました";
                                    Invoke(new delegateWarning(setWarningText), null, lv_item_str);

                                    flgAreaInMessage0Machine[i, j] = true;
                                    flgAreaInMessage1Machine[i, j] = false;
                                    flgAreaInMessage2Machine[i, j] = false;
                                }
                            }
                        }
                    }

                    // 作業員とのあたり判定をする
                    for (int j = 0; j < USER_MAX; j++)
                    {
                        if (!userUse[j])
                        {
                            continue;
                        }

                        // 開始時のみ警告を表示する
                        if (!buttonStart.Enabled)
                        {
                            //２点間の距離が指定範囲以内か
                            double chk_distanceRed = (areaRedZone[i] * areaRedZone[i]);
                            double chk_distanceYellow = (areaYellowZone[i] * areaYellowZone[i]);
                            double userX = (areaPositionOffsetX[i] - userPositionX[j]) * (areaPositionOffsetX[i] - userPositionX[j]);
                            double userY = (areaPositionOffsetY[i] - userPositionY[j]) * (areaPositionOffsetY[i] - userPositionY[j]);
                            if (userX + userY <= chk_distanceRed)
                            {
                                // エリアのゾーン２に作業員が入っている状態
                                flgAreaInZone2User[i, j] = true;
                                flgAreaInZone1User[i, j] = true;

                                if (!flgAreaInMessage2User[i, j])
                                {
                                    // 履歴を入力する
                                    string lv_item_str = DateTime.Now.ToString("MM/dd ") + DateTime.Now.ToLongTimeString() + " " + areaName[i] + "の第２ゾーンに" + userName[j] + "が侵入しました";
                                    Invoke(new delegateWarning(setWarningText), null, lv_item_str);

                                    // 2018/04/05 Rev1.10 シルウォッチ対応 start
                                    ControlIPPower(j);
                                    // 2018/04/05 Rev1.10 シルウォッチ対応 end

                                    flgAreaInMessage2User[i, j] = true;
                                    flgAreaInMessage1User[i, j] = true;
                                    flgAreaInMessage0User[i, j] = false;
                                }

                            }
                            else if (userX + userY <= chk_distanceYellow)
                            {
                                // エリアのゾーン１に作業員が入っている状態
                                flgAreaInZone1User[i, j] = true;
                                //flgAreaInZone2[j] = false; ２は前回値を保持する

                                if (!flgAreaInMessage1User[i, j])
                                {
                                    // 履歴を入力する
                                    string lv_item_str = DateTime.Now.ToString("MM/dd ") + DateTime.Now.ToLongTimeString() + " " + areaName[i] + "の第１ゾーンに" + userName[j] + "が侵入しました";
                                    Invoke(new delegateWarning(setWarningText), null, lv_item_str);

                                    // 2018/04/05 Rev1.10 シルウォッチ対応 start
                                    ControlIPPower(j);
                                    // 2018/04/05 Rev1.10 シルウォッチ対応 end

                                    flgAreaInMessage1User[i, j] = true;
                                    flgAreaInMessage0User[i, j] = false;
                                }

                            }
                            else
                            {
                                // エリアのゾーン（１、２）に作業員が入っていない状態
                                flgAreaInZone1User[i, j] = false;
                                flgAreaInZone2User[i, j] = false;

                                if (!flgAreaInMessage0User[i, j])
                                {
                                    // 履歴を入力する
                                    string lv_item_str = DateTime.Now.ToString("MM/dd ") + DateTime.Now.ToLongTimeString() + " " + areaName[i] + "のゾーンから" + userName[j] + "が出ました";
                                    Invoke(new delegateWarning(setWarningText), null, lv_item_str);

                                    flgAreaInMessage0User[i, j] = true;
                                    flgAreaInMessage1User[i, j] = false;
                                    flgAreaInMessage2User[i, j] = false;
                                }

                                // 2018/04/05 Rev1.10 シルウォッチ対応 start
                                //if (deserializedObect.UserList.Users[j].Enable)
                                //{
                                //    // "http://192.168.1.3/set.cmd?cmd=setpower+p61=0"
                                //    string sURL = "http://" + deserializedObect.UserList.Users[j].IpAddress.ToString() + "/set.cmd?cmd=setpower+p" + deserializedObect.UserList.Users[j].Port.ToString() + "=0";

                                //    WebRequest wrGETURL;
                                //    wrGETURL = WebRequest.Create(sURL);

                                //    WebProxy myProxy = new WebProxy("myproxy", 80);
                                //    myProxy.BypassProxyOnLocal = true;

                                //    wrGETURL.Proxy = WebProxy.GetDefaultProxy();

                                //    Stream objStream;
                                //    objStream = wrGETURL.GetResponse().GetResponseStream();
                                //}
                                // 2018/04/05 Rev1.10 シルウォッチ対応 end
                            }
                        }
                    }

                    // ゾーンに侵入していない
                    areaInZoneState[i] = 0;

                    // 作業員または重機が少なくとも１人（１台）がエリアのゾーンに侵入しているかどうか

                    //
                    // 第１ゾーンに侵入しているかどうか
                    //
                    // エリアに重機が少なくとも１台が第１ゾーンに侵入しているかどうか
                    for (int k = 0; k < MACHINE_MAX; k++)
                    {
                        if (flgAreaInZone1Machine[i, k] == true)
                        {
                            areaInZoneState[i] = 1;
                            break;
                        }
                    }
                    // エリアに作業員が少なくとも１人が第１ゾーンに侵入しているかどうか
                    for (int k = 0; k < USER_MAX; k++)
                    {
                        if (flgAreaInZone1User[i, k] == true)
                        {
                            areaInZoneState[i] = 1;
                            break;
                        }
                    }
                    //
                    // 第２ゾーンに侵入しているかどうか
                    //
                    // エリアに重機が少なくとも１台が第２ゾーンに侵入しているかどうか
                    for (int k = 0; k < MACHINE_MAX; k++)
                    {
                        if (flgAreaInZone2Machine[i, k] == true)
                        {
                            areaInZoneState[i] = 2;
                            break;
                        }
                    }
                    // エリアに作業員が少なくとも１人が第２ゾーンに侵入しているかどうか
                    for (int k = 0; k < USER_MAX; k++)
                    {
                        if (flgAreaInZone2User[i, k] == true)
                        {
                            areaInZoneState[i] = 2;
                            break;
                        }
                    }
                
                    if (areaInZoneState[i] == 2)
                    {
                        // 少なくとも１人の作業員(または重機)がゾーン２に侵入しているとき

                        // エリアのゾーンを赤色で塗りつぶす
                        g.FillEllipse(redBrush, (float)viewAreaPositionOffsetX[i] - ((float)areaYellowZone[i] * 2 * (float)ScaleX / 2) - 3, (float)viewAreaPositionOffsetY[i] - ((float)areaYellowZone[i] * 2 * (float)ScaleY / 2) - 3, (float)areaYellowZone[i] * 2 * (float)ScaleX + 6, (float)areaYellowZone[i] * 2 * (float)ScaleY + 6);

                        // 赤色のパトライトが点灯していなければ点灯させる
                        if (!flgOnPt2Area[i])
                        {
                            string command = makePtCommand(areaPtColor2[i], areaPtPattern2[i], areaPtBuzzer2[i]);
                            DoDosCommand(@"/c prsh " + areaPtIp[i] + " -l soctec alert " + command);
                            // エラーの場合警告を表示する

                            flgOnPt2Area[i] = true;
                            flgOnPt1Area[i] = true;
                            flgOnPt0Area[i] = false;
                        }


                    }
                    else if (areaInZoneState[i] == 1)
                    {
                        // エリアのゾーン２に誰も侵入していなく、少なくとも１人の作業員（または重機）がゾーン１に侵入しているとき

                        // エリアのゾーンを黄色で塗りつぶす
                        g.FillEllipse(yellowBrush, (float)viewAreaPositionOffsetX[i] - ((float)areaYellowZone[i] * 2 * (float)ScaleX / 2) - 3, (float)viewAreaPositionOffsetY[i] - ((float)areaYellowZone[i] * 2 * (float)ScaleY / 2) - 3, (float)areaYellowZone[i] * 2 * (float)ScaleX + 6, (float)areaYellowZone[i] * 2 * (float)ScaleY + 6);

                        // エリアの第２ゾーンは赤点線で表示する
                        g.DrawEllipse(redPen, (float)viewAreaPositionOffsetX[i] - ((float)areaRedZone[i] * 2 * (float)ScaleX / 2), (float)viewAreaPositionOffsetY[i] - ((float)areaRedZone[i] * 2 * (float)ScaleY / 2), (float)areaRedZone[i] * 2 * (float)ScaleX, (float)areaRedZone[i] * 2 * (float)ScaleY);

                        // 黄色のパトライトが点灯していなければ点灯させる
                        if (!flgOnPt1Area[i])
                        {
                            // 黄色のパトライトを点灯させる
                            string command = makePtCommand(areaPtColor1[i], areaPtPattern1[i], areaPtBuzzer1[i]);
                            DoDosCommand(@"/c prsh " + areaPtIp[i] + " -l soctec alert " + command);
                            // エラーの場合警告を表示する

                            flgOnPt2Area[i] = false;
                            flgOnPt1Area[i] = true;
                            flgOnPt0Area[i] = false;
                        }
                    }
                    else if (areaInZoneState[i] == 0)
                    {
                        // エリアに作業員（または重機）が侵入していないとき

                        // エリアのゾーンを塗りつぶさない
                        g.DrawEllipse(yellowPen, (float)viewAreaPositionOffsetX[i] - ((float)areaYellowZone[i] * 2 * (float)ScaleX / 2), (float)viewAreaPositionOffsetY[i] - ((float)areaYellowZone[i] * 2 * (float)ScaleY / 2), (float)areaYellowZone[i] * 2 * (float)ScaleX, (float)areaYellowZone[i] * 2 * (float)ScaleY);
                        g.DrawEllipse(redPen, (float)viewAreaPositionOffsetX[i] - ((float)areaRedZone[i] * 2 * (float)ScaleX / 2), (float)viewAreaPositionOffsetY[i] - ((float)areaRedZone[i] * 2 * (float)ScaleY / 2), (float)areaRedZone[i] * 2 * (float)ScaleX, (float)areaRedZone[i] * 2 * (float)ScaleY);

                        // パトライトが消灯していなければ消灯させる
                        if (!flgOnPt0Area[i])
                        {
                            // パトライトを消灯させる
                            DoDosCommand(@"/c prsh " + areaPtIp[i] + " -l soctec alert 000000");

                            flgOnPt0Area[i] = true;
                            flgOnPt1Area[i] = false;
                            flgOnPt2Area[i] = false;
                        }
                    }
                    else
                    {
                        // DO NOTHING
                    }

                    // 画像を描画する
                    g.DrawImage(bitmap, (float)viewAreaPositionX[i] - (width / 2), (float)viewAreaPositionY[i] - (height / 2), width, height);

                    // エリアの中心を×印で表示する
                    g.DrawLine(Pens.Yellow, (float)viewAreaPositionOffsetX[i] - 100, (float)viewAreaPositionOffsetY[i] - 100, (float)viewAreaPositionOffsetX[i] + 100, (float)viewAreaPositionOffsetY[i] + 100);
                    g.DrawLine(Pens.Yellow, (float)viewAreaPositionOffsetX[i] - 100, (float)viewAreaPositionOffsetY[i] + 100, (float)viewAreaPositionOffsetX[i] + 100, (float)viewAreaPositionOffsetY[i] - 100);

                    // タグを〇で表示する　オフセットは□で表示する
                    g.FillEllipse(Brushes.Black, (float)viewAreaPositionX[i] - (10 / 2), (float)viewAreaPositionY[i] - (10 / 2), 10, 10);
                    g.FillRectangle(Brushes.Black, (float)viewAreaPositionOffsetX[i] - (10 / 2), (float)viewAreaPositionOffsetY[i] - (10 / 2), 10, 10);

                    // オフセットと線で繋ぐ
                    g.DrawLine(blackPen, (float)viewAreaPositionX[i], (float)viewAreaPositionY[i], (float)viewAreaPositionOffsetX[i], (float)viewAreaPositionOffsetY[i]);

                    // 有効時に座標を表示する
                    if (checkBoxCoordinate.Checked)
                    {
                        g.DrawString(areaName[i] + ':' + areaPositionOffsetX[i].ToString("F2") + ',' + areaPositionOffsetY[i].ToString("F2"), font, Brushes.White, (float)viewAreaPositionOffsetX[i] + (20 / 2) - 1, (float)viewAreaPositionOffsetY[i] - (20 / 2));
                        g.DrawString(areaName[i] + ':' + areaPositionOffsetX[i].ToString("F2") + ',' + areaPositionOffsetY[i].ToString("F2"), font, Brushes.White, (float)viewAreaPositionOffsetX[i] + (20 / 2), (float)viewAreaPositionOffsetY[i] - (20 / 2) - 1);
                        g.DrawString(areaName[i] + ':' + areaPositionOffsetX[i].ToString("F2") + ',' + areaPositionOffsetY[i].ToString("F2"), font, Brushes.White, (float)viewAreaPositionOffsetX[i] + (20 / 2) + 1, (float)viewAreaPositionOffsetY[i] - (20 / 2));
                        g.DrawString(areaName[i] + ':' + areaPositionOffsetX[i].ToString("F2") + ',' + areaPositionOffsetY[i].ToString("F2"), font, Brushes.White, (float)viewAreaPositionOffsetX[i] + (20 / 2), (float)viewAreaPositionOffsetY[i] - (20 / 2) + 1);
                        g.DrawString(areaName[i] + ':' + areaPositionOffsetX[i].ToString("F2") + ',' + areaPositionOffsetY[i].ToString("F2"), font, Brushes.Black, (float)viewAreaPositionOffsetX[i] + (20 / 2), (float)viewAreaPositionOffsetY[i] - (20 / 2));
                    }
                }


                ////////////////////////////////////////////////////////////////////////////////////////////
                // レール
                //bitmapTarget = new Bitmap(@"BMP\レール.png");
                //d = 30 / (180 / Math.PI);
                //x = locationX;
                //y = locationY;
                //x1 = x + width * (float)Math.Cos(d);
                //y1 = y + width * (float)Math.Sin(d);
                //x2 = x - height * (float)Math.Sin(d);
                //y2 = y + height * (float)Math.Cos(d);
                //destinationPoints[0] = new PointF(x, y);
                //destinationPoints[1] = new PointF(x1, y1);
                //destinationPoints[2] = new PointF(x2, y2);
                //g.DrawImage(bitmapTarget, 0,100,1500,20);

                ////////////////////////////////////////////////////////////////////////////////////////////

                bitmap.Dispose();
                bitmap = (Bitmap)bitmapMachineImage.Clone();

                for (int i = 0; i < MACHINE_MAX; i++)
                {
                    if (!machineUse[i])
                    {
                        continue;
                    }

                    // 重機サイズ
                    width = viewMachineWidth[i];
                    height = viewMachineHeight[i];

                    //重機同士であたり判定をする
                    for (int j = 0; j < MACHINE_MAX; j++)
                    {
                        if (!machineUse[j])
                        {
                            continue;
                        }

                        if (i == j)
                        {
                            continue;
                        }

                        // 開始時のみ警告を表示する
                        if (!buttonStart.Enabled)
                        {
                            // (xc1 - xc2) ^ 2 + (yc1 - yc2) ^ 2 ≦ (r1 + r2) ^ 2ならば衝突している
                            //２点間の距離が指定範囲以内か
                            double chk_distanceRed = ((machineRedZone[i] + machineDiagonalLengthHalf[i]) * (machineRedZone[i] + machineDiagonalLengthHalf[i]));
                            double chk_distanceYellow = ((machineYellowZone[i] + machineDiagonalLengthHalf[i]) * (machineYellowZone[i] + machineDiagonalLengthHalf[i]));
                            double machineX2 = (machinePositionCx[i] - machinePositionCx[j]) * (machinePositionCx[i] - machinePositionCx[j]);
                            double machineY2 = (machinePositionCy[i] - machinePositionCy[j]) * (machinePositionCy[i] - machinePositionCy[j]);
                            if (machineX2 + machineY2 <= chk_distanceRed)
                            {
                                // 重機のゾーン２に重機が入っている状態
                                flgMachineInZone2Machine[i, j] = true;
                                flgMachineInZone1Machine[i, j] = true;

                                if (!flgMachineInMessage2Machine[i, j])
                                {
                                    // 履歴を入力する
                                    string lv_item_str = DateTime.Now.ToString("MM/dd ") + DateTime.Now.ToLongTimeString() + " " + machineName[i] + "の第２ゾーンに" + machineName[j] + "が侵入しました";
                                    Invoke(new delegateWarning(setWarningText), null, lv_item_str);

                                    flgMachineInMessage2Machine[i, j] = true;
                                    flgMachineInMessage1Machine[i, j] = true;
                                    flgMachineInMessage0Machine[i, j] = false;
                                }

                            }
                            else if (machineX2 + machineY2 <= chk_distanceYellow)
                            {
                                // 重機のゾーン１に重機が入っている状態
                                flgMachineInZone1Machine[i, j] = true;
                                //flgMachineInZone2Machine[j] = false; ２は前回値を保持する

                                if (!flgMachineInMessage1Machine[i, j])
                                {
                                    // 履歴を入力する
                                    string lv_item_str = DateTime.Now.ToString("MM/dd ") + DateTime.Now.ToLongTimeString() + " " + machineName[i] + "の第１ゾーンに" + machineName[j] + "が侵入しました";
                                    Invoke(new delegateWarning(setWarningText), null, lv_item_str);

                                    flgMachineInMessage1Machine[i, j] = true;
                                    flgMachineInMessage0Machine[i, j] = false;
                                }
                            }
                            else
                            {
                                // 重機のゾーン（１、２）に作業員が入っていない状態
                                flgMachineInZone1Machine[i, j] = false;
                                flgMachineInZone2Machine[i, j] = false;

                                if (!flgMachineInMessage0Machine[i, j])
                                {
                                    // 履歴を入力する
                                    string lv_item_str = DateTime.Now.ToString("MM/dd ") + DateTime.Now.ToLongTimeString() + " " + machineName[i] + "のゾーンから" + machineName[j] + "が出ました";
                                    Invoke(new delegateWarning(setWarningText), null, lv_item_str);

                                    flgMachineInMessage0Machine[i, j] = true;
                                    flgMachineInMessage1Machine[i, j] = false;
                                    flgMachineInMessage2Machine[i, j] = false;
                                }
                            }
                        }
                    }

                    // 作業員とのあたり判定
                    for (int j = 0; j < USER_MAX; j++)
                    {
                        if (!userUse[j])
                        {
                            continue;
                        }

                        // 開始時のみ警告を表示する
                        if (!buttonStart.Enabled)
                        {
                            //２点間の距離が指定範囲以内か
                            double chk_distanceRed = (machineRedZone[i] * machineRedZone[i]);
                            double chk_distanceYellow = (machineYellowZone[i] * machineYellowZone[i]);
                            double userX = (machinePositionCx[i] - userPositionX[j]) * (machinePositionCx[i] - userPositionX[j]);
                            double userY = (machinePositionCy[i] - userPositionY[j]) * (machinePositionCy[i] - userPositionY[j]);
                            if (userX + userY <= chk_distanceRed)
                            {
                                // 重機のゾーン２に作業員が入っている状態
                                flgMachineInZone2User[i,j] = true;
                                flgMachineInZone1User[i,j] = true;

                                if (!flgMachineInMessage2User[i,j])
                                {
                                    // 履歴を入力する
                                    string lv_item_str = DateTime.Now.ToString("MM/dd ") + DateTime.Now.ToLongTimeString() + " " + machineName[i] + "の第２ゾーンに" + userName[j] + "が侵入しました";
                                    //string lv_item_str = DateTime.Now.ToString("MM/dd hh:mm:ss ") + machineName[i] + "の第２ゾーンに" + userName[j] + "が侵入しました";
                                    Invoke(new delegateWarning(setWarningText), null, lv_item_str);

                                    // 2018/04/05 Rev1.10 シルウォッチ対応 start
                                    ControlIPPower(j);
                                    // 2018/04/05 Rev1.10 シルウォッチ対応 end

                                    flgMachineInMessage2User[i,j] = true;
                                    flgMachineInMessage1User[i,j] = true;
                                    flgMachineInMessage0User[i,j] = false;
                                }
                            }
                            else if (userX + userY <= chk_distanceYellow)
                            {
                                // 重機のゾーン１に作業員が入っている状態
                                flgMachineInZone1User[i,j] = true;
                                //flgMachineInZone2[j] = false; ２は前回値を保持する

                                if (!flgMachineInMessage1User[i,j])
                                {
                                    // 履歴を入力する
                                    string lv_item_str = DateTime.Now.ToString("MM/dd ") + DateTime.Now.ToLongTimeString() + " " + machineName[i] + "の第１ゾーンに" + userName[j] + "が侵入しました";
                                    //string lv_item_str = DateTime.Now.ToString("MM/dd hh:mm:ss ") + machineName[i] + "の第１ゾーンに" + userName[j] + "が侵入しました";
                                    Invoke(new delegateWarning(setWarningText), null, lv_item_str);

                                    // 2018/04/05 Rev1.10 シルウォッチ対応 start
                                    ControlIPPower(j);
                                    // 2018/04/05 Rev1.10 シルウォッチ対応 end

                                    flgMachineInMessage1User[i,j] = true;
                                    flgMachineInMessage0User[i,j] = false;
                                }
                            }
                            else
                            {
                                // 重機のゾーン（１、２）に作業員が入っていない状態
                                flgMachineInZone1User[i,j] = false;
                                flgMachineInZone2User[i,j] = false;

                                if (!flgMachineInMessage0User[i,j])
                                {
                                    // 履歴を入力する
                                    string lv_item_str = DateTime.Now.ToString("MM/dd ") + DateTime.Now.ToLongTimeString() + " " + machineName[i] + "のゾーンから" + userName[j] + "が出ました";
                                    //string lv_item_str = DateTime.Now.ToString("MM/dd hh:mm:ss ") + machineName[i] + "のゾーンから" + userName[j] + "が出ました";
                                    Invoke(new delegateWarning(setWarningText), null, lv_item_str);

                                    flgMachineInMessage0User[i,j] = true;
                                    flgMachineInMessage1User[i,j] = false;
                                    flgMachineInMessage2User[i,j] = false;
                                }

                                // 2018/04/05 Rev1.10 シルウォッチ対応 start
                                //if (deserializedObect.UserList.Users[j].Enable)
                                //{
                                //    // "http://192.168.1.3/set.cmd?cmd=setpower+p61=1+p61n=0+t61=2"
                                //    string sURL = "http://" + deserializedObect.UserList.Users[j].IpAddress.ToString() + "/set.cmd?cmd=setpower+p" + deserializedObect.UserList.Users[j].Port.ToString() + "=0";

                                //    WebRequest wrGETURL;
                                //    wrGETURL = WebRequest.Create(sURL);

                                //    WebProxy myProxy = new WebProxy("myproxy", 80);
                                //    myProxy.BypassProxyOnLocal = true;

                                //    wrGETURL.Proxy = WebProxy.GetDefaultProxy();

                                //    Stream objStream;
                                //    objStream = wrGETURL.GetResponse().GetResponseStream();
                                //}
                                // 2018/04/05 Rev1.10 シルウォッチ対応 end

                            }
                        }
                    }

                    // 重機のゾーンに侵入していない
                    machineInZoneState[i] = 0;

                    // 作業員または重機が少なくとも１人（１台）が重機のゾーンに侵入しているかどうか

                    //
                    // 第１ゾーンに侵入しているかどうか
                    //
                    // 重機の第１ゾーンに少なくとも１台の重機が侵入しているかどうか
                    for (int k = 0; k < MACHINE_MAX; k++)
                    {
                        if (flgMachineInZone1Machine[i, k] == true)
                        {
                            machineInZoneState[i] = 1;
                            break;
                        }
                    }
                    // 重機の第１ゾーンに少なくとも１人の作業員が侵入しているかどうか
                    for (int k = 0; k < USER_MAX; k++)
                    {
                        if (flgMachineInZone1User[i, k] == true)
                        {
                            machineInZoneState[i] = 1;
                            break;
                        }
                    }
                    //
                    // 第２ゾーンに侵入しているかどうか
                    //
                    // 重機の第２ゾーンに少なくとも１台の重機が侵入しているかどうか
                    for (int k = 0; k < MACHINE_MAX; k++)
                    {
                        if (flgMachineInZone2Machine[i, k] == true)
                        {
                            machineInZoneState[i] = 2;
                            break;
                        }
                    }
                    // 重機の第２ゾーンに少なくとも１人の作業員が侵入しているかどうか
                    for (int k = 0; k < USER_MAX; k++)
                    {
                        if (flgMachineInZone2User[i, k] == true)
                        {
                            machineInZoneState[i] = 2;
                            break;
                        }
                    }

                    if (machineInZoneState[i] == 2)
                    {
                        // 少なくとも１人の作業員(または重機)がゾーン２に侵入しているとき

                        // 重機のゾーンを赤色で塗りつぶす
                        g.FillEllipse(redBrush, (float)viewMachinePositionCx[i] - ((float)machineYellowZone[i] * 2 * (float)ScaleX / 2) - 3, (float)viewMachinePositionCy[i] - ((float)machineYellowZone[i] * 2 * (float)ScaleY / 2) - 3, (float)machineYellowZone[i] * 2 * (float)ScaleX + 6, (float)machineYellowZone[i] * 2 * (float)ScaleY + 6);

                        // 赤色のパトライトが点灯していなければ点灯させる
                        if (!flgOnPt2Machine[i])
                        {
                            string command = makePtCommand(machinePtColor2[i], machinePtPattern2[i], machinePtBuzzer2[i]);
                            DoDosCommand(@"/c prsh " + machinePtIp[i] + " -l soctec alert " + command);
                            // エラーの場合警告を表示する

                            flgOnPt2Machine[i] = true;
                            flgOnPt1Machine[i] = true;
                            flgOnPt0Machine[i] = false;
                        }
                    }
                    else if (machineInZoneState[i] == 1)
                    {
                        // 重機のゾーン２に誰も侵入していなく、少なくとも１人の作業員（または重機）がゾーン１に侵入しているとき

                        // 重機のゾーンを黄色で塗りつぶす
                        g.FillEllipse(yellowBrush, (float)viewMachinePositionCx[i] - ((float)machineYellowZone[i] * 2 * (float)ScaleX / 2) - 3, (float)viewMachinePositionCy[i] - ((float)machineYellowZone[i] * 2 * (float)ScaleY / 2) - 3, (float)machineYellowZone[i] * 2 * (float)ScaleX + 6, (float)machineYellowZone[i] * 2 * (float)ScaleY + 6);

                        // 重機の第２ゾーンは赤点線で表示する
                        g.DrawEllipse(redPen, (float)viewMachinePositionCx[i] - ((float)machineRedZone[i] * 2 * (float)ScaleX / 2), (float)viewMachinePositionCy[i] - ((float)machineRedZone[i] * 2 * (float)ScaleY / 2), (float)machineRedZone[i] * 2 * (float)ScaleX, (float)machineRedZone[i] * 2 * (float)ScaleY);

                        // 黄色のパトライトが点灯していなければ点灯させる
                        if (!flgOnPt1Machine[i])
                        {
                            // 黄色のパトライトを点灯させる
                            string command = makePtCommand(machinePtColor1[i], machinePtPattern1[i], machinePtBuzzer1[i]);
                            DoDosCommand(@"/c prsh " + machinePtIp[i] + " -l soctec alert " + command);
                            // エラーの場合警告を表示する

                            flgOnPt2Machine[i] = false;
                            flgOnPt1Machine[i] = true;
                            flgOnPt0Machine[i] = false;
                        }
                    }
                    else if (machineInZoneState[i] == 0)
                    {
                        // 重機に作業員（または重機）が侵入していないとき

                        // 重機ゾーンの非表示が有効でなければゾーンを表示する
                        if (checkBoxVisibleMachineZone.Checked == false)
                        {
                            // 重機のゾーンを塗りつぶさない
                            g.DrawEllipse(yellowPen, (float)viewMachinePositionCx[i] - ((float)machineYellowZone[i] * 2 * (float)ScaleX / 2), (float)viewMachinePositionCy[i] - ((float)machineYellowZone[i] * 2 * (float)ScaleY / 2), (float)machineYellowZone[i] * 2 * (float)ScaleX, (float)machineYellowZone[i] * 2 * (float)ScaleY);
                            g.DrawEllipse(redPen, (float)viewMachinePositionCx[i] - ((float)machineRedZone[i] * 2 * (float)ScaleX / 2), (float)viewMachinePositionCy[i] - ((float)machineRedZone[i] * 2 * (float)ScaleY / 2), (float)machineRedZone[i] * 2 * (float)ScaleX, (float)machineRedZone[i] * 2 * (float)ScaleY);
                        }

                        // パトライトが消灯していなければ消灯させる
                        if (!flgOnPt0Machine[i])
                        {
                            // パトライトを消灯させる
                            DoDosCommand(@"/c prsh " + machinePtIp[i] + " -l soctec alert 000000");

                            flgOnPt0Machine[i] = true;
                            flgOnPt1Machine[i] = false;
                            flgOnPt2Machine[i] = false;
                        }
                    }
                    else
                    {
                        // DO NOTHING
                    }

                    // 画像を描画する
                    g.DrawImage(bitmap, (float)viewMachinePositionCx[i] - (width / 2), (float)viewMachinePositionCy[i] - (height / 2), width, height);

                    // 重機の中心を×印で表示する
                    g.DrawLine(Pens.Yellow, (float)viewMachinePositionCx[i] - 100, (float)viewMachinePositionCy[i] - 100, (float)viewMachinePositionCx[i] + 100, (float)viewMachinePositionCy[i] + 100);
                    g.DrawLine(Pens.Yellow, (float)viewMachinePositionCx[i] - 100, (float)viewMachinePositionCy[i] + 100, (float)viewMachinePositionCx[i] + 100, (float)viewMachinePositionCy[i] - 100);

                    // タグを〇で表示する　オフセットは□で表示する
                    g.FillEllipse(Brushes.Red, (float)viewMachinePositionFLX[i] - (10 / 2), (float)viewMachinePositionFLY[i] - (10 / 2), 10, 10);
                    g.FillEllipse(Brushes.Green, (float)viewMachinePositionFRX[i] - (10 / 2), (float)viewMachinePositionFRY[i] - (10 / 2), 10, 10);
                    g.FillEllipse(Brushes.Gold, (float)viewMachinePositionBLX[i] - (10 / 2), (float)viewMachinePositionBLY[i] - (10 / 2), 10, 10);
                    g.FillEllipse(Brushes.DarkOrange, (float)viewMachinePositionBRX[i] - (10 / 2), (float)viewMachinePositionBRY[i] - (10 / 2), 10, 10);

                    g.FillRectangle(Brushes.Red, (float)viewMachinePositionOffsetFLX[i] - (10 / 2), (float)viewMachinePositionOffsetFLY[i] - (10 / 2), 10, 10);
                    g.FillRectangle(Brushes.Green, (float)viewMachinePositionOffsetFRX[i] - (10 / 2), (float)viewMachinePositionOffsetFRY[i] - (10 / 2), 10, 10);
                    g.FillRectangle(Brushes.Gold, (float)viewMachinePositionOffsetBLX[i] - (10 / 2), (float)viewMachinePositionOffsetBLY[i] - (10 / 2), 10, 10);
                    g.FillRectangle(Brushes.DarkOrange, (float)viewMachinePositionOffsetBRX[i] - (10 / 2), (float)viewMachinePositionOffsetBRY[i] - (10 / 2), 10, 10);

                    // オフセットと線で繋ぐ
                    g.DrawLine(blackPen, (float)viewMachinePositionFLX[i], (float)viewMachinePositionFLY[i], (float)viewMachinePositionOffsetFLX[i], (float)viewMachinePositionOffsetFLY[i]);
                    g.DrawLine(blackPen, (float)viewMachinePositionFRX[i], (float)viewMachinePositionFRY[i], (float)viewMachinePositionOffsetFRX[i], (float)viewMachinePositionOffsetFRY[i]);
                    g.DrawLine(blackPen, (float)viewMachinePositionBLX[i], (float)viewMachinePositionBLY[i], (float)viewMachinePositionOffsetBLX[i], (float)viewMachinePositionOffsetBLY[i]);
                    g.DrawLine(blackPen, (float)viewMachinePositionBRX[i], (float)viewMachinePositionBRY[i], (float)viewMachinePositionOffsetBRX[i], (float)viewMachinePositionOffsetBRY[i]);

                    // 有効時に座標を表示する
                    if (checkBoxCoordinate.Checked)
                    {
                        g.DrawString(machineName[i] + ':' + machinePositionCx[i].ToString("F2") + ',' + machinePositionCy[i].ToString("F2"), font, Brushes.White, (float)viewMachinePositionCx[i] + (20 / 2) - 1, (float)viewMachinePositionCy[i] - (20 / 2));
                        g.DrawString(machineName[i] + ':' + machinePositionCx[i].ToString("F2") + ',' + machinePositionCy[i].ToString("F2"), font, Brushes.White, (float)viewMachinePositionCx[i] + (20 / 2), (float)viewMachinePositionCy[i] - (20 / 2) - 1);
                        g.DrawString(machineName[i] + ':' + machinePositionCx[i].ToString("F2") + ',' + machinePositionCy[i].ToString("F2"), font, Brushes.White, (float)viewMachinePositionCx[i] + (20 / 2) + 1, (float)viewMachinePositionCy[i] - (20 / 2));
                        g.DrawString(machineName[i] + ':' + machinePositionCx[i].ToString("F2") + ',' + machinePositionCy[i].ToString("F2"), font, Brushes.White, (float)viewMachinePositionCx[i] + (20 / 2), (float)viewMachinePositionCy[i] - (20 / 2) + 1);
                        g.DrawString(machineName[i] + ':' + machinePositionCx[i].ToString("F2") + ',' + machinePositionCy[i].ToString("F2"), font, Brushes.Black, (float)viewMachinePositionCx[i] + (20 / 2), (float)viewMachinePositionCy[i] - (20 / 2));
                    }
                }

                ////////////////////////////////////////////////////////////////////////////////////////////////////////

                // 
                // 作業員を表示する
                //

                bitmap.Dispose();
                // 作業員サイズ
                bitmap = (Bitmap)bitmapUserImage.Clone();

                // 作業員は0.5mで計算する
                width = (EndX - StartX) * ((float)0.5 / (float)ViewAreaWidth);
                height = (EndY - StartY) * ((float)0.5 / (float)ViewAreaHeight);

                //作業員
                for (int i = 0; i < USER_MAX; i++)
                {
                    if (!userUse[i])
                    {
                        continue;
                    }

                    g.DrawImage(bitmap, (float)viewUserPositionX[i] - (width / 2), (float)viewUserPositionY[i] - (height / 2), width, height);
                    g.FillEllipse(Brushes.Black, (float)viewUserPositionX[i] - (10 / 2), (float)viewUserPositionY[i] - (10 / 2), 10, 10);

                    // 有効時に座標を表示する
                    if (checkBoxCoordinate.Checked)
                    {
                        g.DrawString(userName[i] + ':' + userPositionX[i].ToString("F2") + ',' + userPositionY[i].ToString("F2"), font, Brushes.White, (float)viewUserPositionX[i] + width / 2 + (20 / 2) - 1, (float)viewUserPositionY[i] + height / 2 - (20 / 2));
                        g.DrawString(userName[i] + ':' + userPositionX[i].ToString("F2") + ',' + userPositionY[i].ToString("F2"), font, Brushes.White, (float)viewUserPositionX[i] + width / 2 + (20 / 2), (float)viewUserPositionY[i] + height / 2 - (20 / 2) - 1);
                        g.DrawString(userName[i] + ':' + userPositionX[i].ToString("F2") + ',' + userPositionY[i].ToString("F2"), font, Brushes.White, (float)viewUserPositionX[i] + width / 2 + (20 / 2) + 1, (float)viewUserPositionY[i] + height / 2 - (20 / 2));
                        g.DrawString(userName[i] + ':' + userPositionX[i].ToString("F2") + ',' + userPositionY[i].ToString("F2"), font, Brushes.White, (float)viewUserPositionX[i] + width / 2 + (20 / 2), (float)viewUserPositionY[i] + height / 2 - (20 / 2) + 1);
                        g.DrawString(userName[i] + ':' + userPositionX[i].ToString("F2") + ',' + userPositionY[i].ToString("F2"), font, Brushes.Black, (float)viewUserPositionX[i] + width / 2 + (20 / 2), (float)viewUserPositionY[i] + height / 2 - (20 / 2));
                    }
                }

                // 反映する
                pictureBox1.Image = canvas;            
                //
                bitmap.Dispose();
            }
            catch (Exception e)
            {
                sysLogFile.Write(e.Message + " " + e.StackTrace);
            }
        }

        #endregion 描画関数

        public void StopDraw()
        {
            NativeMethods.BeginControlUpdate(pictureBox1);
        }
        public void StartDraw()
        {
            NativeMethods.EndControlUpdate(pictureBox1);
        }

        #region ユビセンス位置情報取得イベント
        Ubisense.UName.Naming.Schema namingSchema;
        Ubisense.ULocation.MultiCell multiCell;

        private void OnInsert(Ubisense.ULocation.CellData.Location.RowType insertedRow)
        {
            ReportPosition(insertedRow);
        }

        private void OnUpdate(Ubisense.ULocation.CellData.Location.RowType oldRow,
            Ubisense.ULocation.CellData.Location.RowType newRow)
        {
            ReportPosition(newRow);
        }
        private void OnDelete(Ubisense.ULocation.CellData.Location.RowType deletedRow)
        {
            ReportPosition(deletedRow);
        }
        #endregion ユビセンス位置情報取得イベント

        private void ReportPosition(Ubisense.ULocation.CellData.Location.RowType row)
        {
            using (Ubisense.UName.Naming.ReadTransaction xact = namingSchema.ReadTransaction())
            {
                // 24時間毎にCSVファイルの期限をチェックする
                checkPositionFile();

                // タグ情報を取得する
                string objType = row.object_.DynamicType.ToString();
                if (objType == "ULocationIntegration::Tag")
                {
                    string tag_id = row.object_.Id.ToString();

                    double yaw, pitch, roll;
                    row.position_.R.ToEuler(out yaw, out pitch, out roll);

                    // 値で保存する
                    listLocation[(int)LOCATION.TAG] = tag_id;
                    listLocation[(int)LOCATION.STD_ERR] = Math.Round(row.accuracy_.stderr_, 3);
                    listLocation[(int)LOCATION.X] = Math.Round(row.position_.P.X, 3);
                    listLocation[(int)LOCATION.Y] = Math.Round(row.position_.P.Y, 3);
                    listLocation[(int)LOCATION.Z] = Math.Round(row.position_.P.Z, 3);

                    // 文字列で保存する
                    locationString[(int)LOCATION.TAG] = tag_id;
                    locationString[(int)LOCATION.STD_ERR] = Math.Round(row.accuracy_.stderr_, 3).ToString("F3");
                    locationString[(int)LOCATION.X] = Math.Round(row.position_.P.X, 3).ToString("F2");
                    locationString[(int)LOCATION.Y] = Math.Round(row.position_.P.Y, 3).ToString("F2");
                    locationString[(int)LOCATION.Z] = Math.Round(row.position_.P.Z, 3).ToString("F2");

                    // CSVファイルに書き込む内容
                    string[] locationStringLcf = {
                                                  tag_id,
                                                  //Math.Round(row.accuracy_.stderr_, 3).ToString("F3"),
                                                  Math.Round(row.position_.P.X, 3).ToString("F3"),
                                                  Math.Round(row.position_.P.Y, 3).ToString("F3"),
                                                  Math.Round(row.position_.P.Z, 3).ToString("F3"),
                                                  //Math.Round(yaw * 180 / Math.PI, 1).ToString(),
                                                  //Math.Round(pitch * 180 / Math.PI, 1).ToString(),
                                                  row.time_.AddHours(9).ToString(),
                                                  row.time_.TimeOfDay.Milliseconds.ToString(),
                                              };

                    // CSVファイルを更新する
                    if (buttonStop.Enabled)
                    {
                        if (lcf != null)
                        {
                            this.lcf.Write(locationStringLcf);
                        }
                    }

                    //lock (this)
                    //{
                    //    // 位置情報を更新する
                    //    updatePosition();
                    //}
                    updatePosition();
                    pictureBox1.Invalidate();
                } //if (objType == "ULocationIntegration::Tag")
            } // using (Ubisense.UName.Naming.ReadTransaction xact = namingSchema.ReadTransaction())
        }

        /// <summary>
        /// 24時間毎にファイルの期限をチェックする
        /// </summary>
        void checkPositionFile()
        {
            if (LocationFileCheckTime > LocationFileCheckTime.AddHours(24))
            {
                try
                {
                    // 格納フォルダがなければ生成する
                    if (!Directory.Exists(LocationDirectoryPath))
                    {
                        DirectoryInfo di = Directory.CreateDirectory(LocationDirectoryPath);
                    }

                    // 格納フォルダ保存期間確認
                    string[] files = Directory.GetFiles(LocationDirectoryPath);
                    List<string> list = new List<string>();
                    foreach (var file in files)
                    {
                        DateTime dtFile = File.GetLastWriteTime(file);
                        DateTime dtLimit = DateTime.Now.AddDays(-LocationFileSaveDay);
                        if (dtFile < dtLimit)
                        {
                            // 保存期間を過ぎた場合はファイルを削除する
                            File.Delete(file);
                        }
                    }
                }
                catch (Exception eLocationDirectoryPath)
                {
                    sysLogFile.Write(eLocationDirectoryPath.Message + " " + eLocationDirectoryPath.StackTrace);
                }

                // チェック時間を更新する
                LocationFileCheckTime = DateTime.Now;
            }
        }

        /// <summary>
        /// 位置情報を更新する(ReportPositionから呼ばれる)
        /// </summary>
        void updatePosition()
        {

            // タグ状態画面用の値を更新する
            if (flgUpdateTagState == true)
            {
                BeginInvoke(new delegateState(setNumber), null, locationString);
            }

            // 作業員
            for (int i = 0; i < USER_MAX; i++)
            {
                // - を削除する
                string tag = this.userTagNo[i];
                var removeChars = new char[] { '-' };
                tag = removeChars.Aggregate(tag, (s, c) => s.Replace(c.ToString(), ""));

                if (locationString[(int)LOCATION.TAG] == "000000000000000" + tag)
                {
                    // 2018/04/06 Rev1.10 加重移動平均対応 start
                    // 数が同じになるため、YでもZでも構わないが、代表してXを使用する
                    if (userPositionXBuffer.Count() < WEIGHTED_MOVING_AVERAGE_NUM_BUFFER_USER)
                    {
                        // バッファ数より小さい
                        userPositionXBuffer.Add((double)listLocation[(int)LOCATION.X]);
                        userPositionYBuffer.Add((double)listLocation[(int)LOCATION.Y]);
                        userPositionZBuffer.Add((double)listLocation[(int)LOCATION.Z]);
                    }
                    else
                    {
                        // バッファ数以上

                        userPositionXBuffer.Add((double)listLocation[(int)LOCATION.X]);
                        userPositionYBuffer.Add((double)listLocation[(int)LOCATION.Y]);
                        userPositionZBuffer.Add((double)listLocation[(int)LOCATION.Z]);

                        // データをずらす
                        for (int j = 0; j < userPositionXBuffer.Count() - 1; j++)
                        {
                            userPositionXBuffer.Insert(j, userPositionXBuffer[j + 1]);
                            userPositionXBuffer.RemoveAt(j + 1);
                            userPositionYBuffer.Insert(j, userPositionYBuffer[j + 1]);
                            userPositionYBuffer.RemoveAt(j + 1);
                            userPositionZBuffer.Insert(j, userPositionZBuffer[j + 1]);
                            userPositionZBuffer.RemoveAt(j + 1);
                        }
                        userPositionXBuffer.RemoveAt(userPositionXBuffer.Count() - 1);
                        userPositionYBuffer.RemoveAt(userPositionYBuffer.Count() - 1);
                        userPositionZBuffer.RemoveAt(userPositionZBuffer.Count() - 1);
                    }
                    int triangularNumber = userPositionXBuffer.Count() * (userPositionXBuffer.Count() + 1) / 2;
                    double aggregateX = 0;
                    double aggregateY = 0;
                    double aggregateZ = 0;
                    for (int j = 0; j < userPositionXBuffer.Count(); j++)
                    {
                        aggregateX = aggregateX + userPositionXBuffer[j] * (j + 1);
                        aggregateY = aggregateY + userPositionYBuffer[j] * (j + 1);
                        aggregateZ = aggregateZ + userPositionZBuffer[j] * (j + 1);
                    }
                    double weightedMovingAverageX = aggregateX / triangularNumber;
                    double weightedMovingAverageY = aggregateY / triangularNumber;
                    double weightedMovingAverageZ = aggregateZ / triangularNumber;

                    userPositionX[i] = weightedMovingAverageX;
                    userPositionY[i] = weightedMovingAverageY;
                    userPositionZ[i] = weightedMovingAverageZ;

                    //userPositionX[i] = (double)listLocation[(int)LOCATION.X];
                    //userPositionY[i] = (double)listLocation[(int)LOCATION.Y];
                    //userPositionZ[i] = (double)listLocation[(int)LOCATION.Z];
                    // 2018/04/06 Rev1.10 加重移動平均対応 end

                    viewUserPositionX[i] = OriginX + Math.Round(userPositionX[i]) * (double)ScaleX;
                    viewUserPositionY[i] = -(OriginY) - Math.Round(userPositionY[i]) * (double)ScaleY;
                    viewUserPositionZ[i] = userPositionZ[i];
                    //userStdErr[i] = (double)listLocation[(int)LOCATION.STD_ERR];
                    break;
                }
            }// USER_MAX loop


            for (int i = 0; i < MACHINE_MAX; i++)
            {
                // - を削除する
                string tagFL = this.machineTagNoFL[i];
                string tagFR = this.machineTagNoFR[i];
                string tagBL = this.machineTagNoBL[i];
                string tagBR = this.machineTagNoBR[i];
                var removeChars = new char[] { '-' };

                tagFL = removeChars.Aggregate(tagFL, (s, c) => s.Replace(c.ToString(), ""));
                tagFR = removeChars.Aggregate(tagFR, (s, c) => s.Replace(c.ToString(), ""));
                tagBL = removeChars.Aggregate(tagBL, (s, c) => s.Replace(c.ToString(), ""));
                tagBR = removeChars.Aggregate(tagBR, (s, c) => s.Replace(c.ToString(), ""));

                if (locationString[(int)LOCATION.TAG] == "000000000000000" + tagFL)
                {

                    // 2018/04/06 Rev1.10 加重移動平均対応 start
                    // 数が同じになるため、YでもZでも構わないが、代表してXを使用する
                    if (machinePositionFLXBuffer.Count() < WEIGHTED_MOVING_AVERAGE_NUM_BUFFER_MACHINE)
                    {
                        // バッファ数より小さい
                        machinePositionFLXBuffer.Add((double)listLocation[(int)LOCATION.X]);
                        machinePositionFLYBuffer.Add((double)listLocation[(int)LOCATION.Y]);
                        machinePositionFLZBuffer.Add((double)listLocation[(int)LOCATION.Z]);
                    }
                    else
                    {
                        // バッファ数以上

                        machinePositionFLXBuffer.Add((double)listLocation[(int)LOCATION.X]);
                        machinePositionFLYBuffer.Add((double)listLocation[(int)LOCATION.Y]);
                        machinePositionFLZBuffer.Add((double)listLocation[(int)LOCATION.Z]);

                        // データをずらす
                        for (int j = 0; j < machinePositionFLXBuffer.Count() - 1; j++)
                        {
                            machinePositionFLXBuffer.Insert(j, machinePositionFLXBuffer[j + 1]);
                            machinePositionFLXBuffer.RemoveAt(j + 1);
                            machinePositionFLYBuffer.Insert(j, machinePositionFLYBuffer[j + 1]);
                            machinePositionFLYBuffer.RemoveAt(j + 1);
                            machinePositionFLZBuffer.Insert(j, machinePositionFLZBuffer[j + 1]);
                            machinePositionFLZBuffer.RemoveAt(j + 1);
                        }
                        machinePositionFLXBuffer.RemoveAt(machinePositionFLXBuffer.Count() - 1);
                        machinePositionFLYBuffer.RemoveAt(machinePositionFLYBuffer.Count() - 1);
                        machinePositionFLZBuffer.RemoveAt(machinePositionFLZBuffer.Count() - 1);
                    }
                    int triangularNumber = machinePositionFLXBuffer.Count() * (machinePositionFLXBuffer.Count() + 1) / 2;
                    double aggregateX = 0;
                    double aggregateY = 0;
                    double aggregateZ = 0;
                    for (int j = 0; j < machinePositionFLXBuffer.Count(); j++)
                    {
                        aggregateX = aggregateX + machinePositionFLXBuffer[j] * (j + 1);
                        aggregateY = aggregateY + machinePositionFLYBuffer[j] * (j + 1);
                        aggregateZ = aggregateZ + machinePositionFLZBuffer[j] * (j + 1);
                    }
                    double weightedMovingAverageX = aggregateX / triangularNumber;
                    double weightedMovingAverageY = aggregateY / triangularNumber;
                    double weightedMovingAverageZ = aggregateZ / triangularNumber;

                    machinePositionFLX[i] = weightedMovingAverageX;
                    machinePositionFLY[i] = weightedMovingAverageY;
                    machinePositionFLZ[i] = weightedMovingAverageZ;

                    //machinePositionFLX[i] = (double)listLocation[(int)LOCATION.X];
                    //machinePositionFLY[i] = (double)listLocation[(int)LOCATION.Y];
                    //machinePositionFLZ[i] = (double)listLocation[(int)LOCATION.Z];
                    // 2018/04/06 Rev1.10 加重移動平均対応 end
                    viewMachinePositionFLX[i] = OriginX + machinePositionFLX[i] * (double)ScaleX;
                    viewMachinePositionFLY[i] = -(OriginY) - machinePositionFLY[i] * (double)ScaleY;
                    stdErrFL[i] = (double)listLocation[(int)LOCATION.STD_ERR];
                    break;
                }
                else if (locationString[(int)LOCATION.TAG] == "000000000000000" + tagFR)
                {
                    // 2018/04/06 Rev1.10 加重移動平均対応 start
                    // 数が同じになるため、YでもZでも構わないが、代表してXを使用する
                    if (machinePositionFRXBuffer.Count() < WEIGHTED_MOVING_AVERAGE_NUM_BUFFER_MACHINE)
                    {
                        // バッファ数より小さい
                        machinePositionFRXBuffer.Add((double)listLocation[(int)LOCATION.X]);
                        machinePositionFRYBuffer.Add((double)listLocation[(int)LOCATION.Y]);
                        machinePositionFRZBuffer.Add((double)listLocation[(int)LOCATION.Z]);
                    }
                    else
                    {
                        // バッファ数以上

                        machinePositionFRXBuffer.Add((double)listLocation[(int)LOCATION.X]);
                        machinePositionFRYBuffer.Add((double)listLocation[(int)LOCATION.Y]);
                        machinePositionFRZBuffer.Add((double)listLocation[(int)LOCATION.Z]);

                        // データをずらす
                        for (int j = 0; j < machinePositionFRXBuffer.Count() - 1; j++)
                        {
                            machinePositionFRXBuffer.Insert(j, machinePositionFRXBuffer[j + 1]);
                            machinePositionFRXBuffer.RemoveAt(j + 1);
                            machinePositionFRYBuffer.Insert(j, machinePositionFRYBuffer[j + 1]);
                            machinePositionFRYBuffer.RemoveAt(j + 1);
                            machinePositionFRZBuffer.Insert(j, machinePositionFRZBuffer[j + 1]);
                            machinePositionFRZBuffer.RemoveAt(j + 1);
                        }
                        machinePositionFRXBuffer.RemoveAt(machinePositionFRXBuffer.Count() - 1);
                        machinePositionFRYBuffer.RemoveAt(machinePositionFRYBuffer.Count() - 1);
                        machinePositionFRZBuffer.RemoveAt(machinePositionFRZBuffer.Count() - 1);
                    }
                    int triangularNumber = machinePositionFRXBuffer.Count() * (machinePositionFRXBuffer.Count() + 1) / 2;
                    double aggregateX = 0;
                    double aggregateY = 0;
                    double aggregateZ = 0;
                    for (int j = 0; j < machinePositionFRXBuffer.Count(); j++)
                    {
                        aggregateX = aggregateX + machinePositionFRXBuffer[j] * (j + 1);
                        aggregateY = aggregateY + machinePositionFRYBuffer[j] * (j + 1);
                        aggregateZ = aggregateZ + machinePositionFRZBuffer[j] * (j + 1);
                    }
                    double weightedMovingAverageX = aggregateX / triangularNumber;
                    double weightedMovingAverageY = aggregateY / triangularNumber;
                    double weightedMovingAverageZ = aggregateZ / triangularNumber;

                    machinePositionFRX[i] = weightedMovingAverageX;
                    machinePositionFRY[i] = weightedMovingAverageY;
                    machinePositionFRZ[i] = weightedMovingAverageZ;

                    //machinePositionFRX[i] = (double)listLocation[(int)LOCATION.X];
                    //machinePositionFRY[i] = (double)listLocation[(int)LOCATION.Y];
                    //machinePositionFRZ[i] = (double)listLocation[(int)LOCATION.Z];
                    // 2018/04/06 Rev1.10 加重移動平均対応 end
                    viewMachinePositionFRX[i] = OriginX + machinePositionFRX[i] * (double)ScaleX;
                    viewMachinePositionFRY[i] = -(OriginY) - machinePositionFRY[i] * (double)ScaleY;
                    stdErrFR[i] = (double)listLocation[(int)LOCATION.STD_ERR];
                    break;
                }
                else if (locationString[(int)LOCATION.TAG] == "000000000000000" + tagBL)
                {
                    // 2018/04/06 Rev1.10 加重移動平均対応 start
                    // 数が同じになるため、YでもZでも構わないが、代表してXを使用する
                    if (machinePositionBLXBuffer.Count() < WEIGHTED_MOVING_AVERAGE_NUM_BUFFER_MACHINE)
                    {
                        // バッファ数より小さい
                        machinePositionBLXBuffer.Add((double)listLocation[(int)LOCATION.X]);
                        machinePositionBLYBuffer.Add((double)listLocation[(int)LOCATION.Y]);
                        machinePositionBLZBuffer.Add((double)listLocation[(int)LOCATION.Z]);
                    }
                    else
                    {
                        // バッファ数以上

                        machinePositionBLXBuffer.Add((double)listLocation[(int)LOCATION.X]);
                        machinePositionBLYBuffer.Add((double)listLocation[(int)LOCATION.Y]);
                        machinePositionBLZBuffer.Add((double)listLocation[(int)LOCATION.Z]);

                        // データをずらす
                        for (int j = 0; j < machinePositionBLXBuffer.Count() - 1; j++)
                        {
                            machinePositionBLXBuffer.Insert(j, machinePositionBLXBuffer[j + 1]);
                            machinePositionBLXBuffer.RemoveAt(j + 1);
                            machinePositionBLYBuffer.Insert(j, machinePositionBLYBuffer[j + 1]);
                            machinePositionBLYBuffer.RemoveAt(j + 1);
                            machinePositionBLZBuffer.Insert(j, machinePositionBLZBuffer[j + 1]);
                            machinePositionBLZBuffer.RemoveAt(j + 1);
                        }
                        machinePositionBLXBuffer.RemoveAt(machinePositionBLXBuffer.Count() - 1);
                        machinePositionBLYBuffer.RemoveAt(machinePositionBLYBuffer.Count() - 1);
                        machinePositionBLZBuffer.RemoveAt(machinePositionBLZBuffer.Count() - 1);
                    }
                    int triangularNumber = machinePositionBLXBuffer.Count() * (machinePositionBLXBuffer.Count() + 1) / 2;
                    double aggregateX = 0;
                    double aggregateY = 0;
                    double aggregateZ = 0;
                    for (int j = 0; j < machinePositionBLXBuffer.Count(); j++)
                    {
                        aggregateX = aggregateX + machinePositionBLXBuffer[j] * (j + 1);
                        aggregateY = aggregateY + machinePositionBLYBuffer[j] * (j + 1);
                        aggregateZ = aggregateZ + machinePositionBLZBuffer[j] * (j + 1);
                    }
                    double weightedMovingAverageX = aggregateX / triangularNumber;
                    double weightedMovingAverageY = aggregateY / triangularNumber;
                    double weightedMovingAverageZ = aggregateZ / triangularNumber;

                    machinePositionBLX[i] = weightedMovingAverageX;
                    machinePositionBLY[i] = weightedMovingAverageY;
                    machinePositionBLZ[i] = weightedMovingAverageZ;

                    //machinePositionBLX[i] = (double)listLocation[(int)LOCATION.X];
                    //machinePositionBLY[i] = (double)listLocation[(int)LOCATION.Y];
                    //machinePositionBLZ[i] = (double)listLocation[(int)LOCATION.Z];
                    // 2018/04/06 Rev1.10 加重移動平均対応 end

                    viewMachinePositionBLX[i] = OriginX + machinePositionBLX[i] * (double)ScaleX;
                    viewMachinePositionBLY[i] = -(OriginY) - machinePositionBLY[i] * (double)ScaleY;
                    stdErrBL[i] = (double)listLocation[(int)LOCATION.STD_ERR];
                    break;
                }
                else if (locationString[(int)LOCATION.TAG] == "000000000000000" + tagBR)
                {
                    // 2018/04/06 Rev1.10 加重移動平均対応 start
                    // 数が同じになるため、YでもZでも構わないが、代表してXを使用する
                    if (machinePositionBRXBuffer.Count() < WEIGHTED_MOVING_AVERAGE_NUM_BUFFER_MACHINE)
                    {
                        // バッファ数より小さい
                        machinePositionBRXBuffer.Add((double)listLocation[(int)LOCATION.X]);
                        machinePositionBRYBuffer.Add((double)listLocation[(int)LOCATION.Y]);
                        machinePositionBRZBuffer.Add((double)listLocation[(int)LOCATION.Z]);
                    }
                    else
                    {
                        // バッファ数以上

                        machinePositionBRXBuffer.Add((double)listLocation[(int)LOCATION.X]);
                        machinePositionBRYBuffer.Add((double)listLocation[(int)LOCATION.Y]);
                        machinePositionBRZBuffer.Add((double)listLocation[(int)LOCATION.Z]);

                        // データをずらす
                        for (int j = 0; j < machinePositionBRXBuffer.Count() - 1; j++)
                        {
                            machinePositionBRXBuffer.Insert(j, machinePositionBRXBuffer[j + 1]);
                            machinePositionBRXBuffer.RemoveAt(j + 1);
                            machinePositionBRYBuffer.Insert(j, machinePositionBRYBuffer[j + 1]);
                            machinePositionBRYBuffer.RemoveAt(j + 1);
                            machinePositionBRZBuffer.Insert(j, machinePositionBRZBuffer[j + 1]);
                            machinePositionBRZBuffer.RemoveAt(j + 1);
                        }
                        machinePositionBRXBuffer.RemoveAt(machinePositionBRXBuffer.Count() - 1);
                        machinePositionBRYBuffer.RemoveAt(machinePositionBRYBuffer.Count() - 1);
                        machinePositionBRZBuffer.RemoveAt(machinePositionBRZBuffer.Count() - 1);
                    }
                    int triangularNumber = machinePositionBRXBuffer.Count() * (machinePositionBRXBuffer.Count() + 1) / 2;
                    double aggregateX = 0;
                    double aggregateY = 0;
                    double aggregateZ = 0;
                    for (int j = 0; j < machinePositionBRXBuffer.Count(); j++)
                    {
                        aggregateX = aggregateX + machinePositionBRXBuffer[j] * (j + 1);
                        aggregateY = aggregateY + machinePositionBRYBuffer[j] * (j + 1);
                        aggregateZ = aggregateZ + machinePositionBRZBuffer[j] * (j + 1);
                    }
                    double weightedMovingAverageX = aggregateX / triangularNumber;
                    double weightedMovingAverageY = aggregateY / triangularNumber;
                    double weightedMovingAverageZ = aggregateZ / triangularNumber;

                    machinePositionBRX[i] = weightedMovingAverageX;
                    machinePositionBRY[i] = weightedMovingAverageY;
                    machinePositionBRZ[i] = weightedMovingAverageZ;

                    //machinePositionBRX[i] = (double)listLocation[(int)LOCATION.X];
                    //machinePositionBRY[i] = (double)listLocation[(int)LOCATION.Y];
                    //machinePositionBRZ[i] = (double)listLocation[(int)LOCATION.Z];
                    // 2018/04/06 Rev1.10 加重移動平均対応 end
                    viewMachinePositionBRX[i] = OriginX + machinePositionBRX[i] * (double)ScaleX;
                    viewMachinePositionBRY[i] = -(OriginY) - machinePositionBRY[i] * (double)ScaleY;
                    stdErrBR[i] = (double)listLocation[(int)LOCATION.STD_ERR];
                    break;
                }
            }// MACHINE_MAX loop

            //lock (this)
            //{
            //    // 重機の中心点を求める
            //    getMachineCenterPoint();
            //}
            getMachineCenterPoint();

            for (int i = 0; i < AREA_MAX; i++)
            {
                // 固定時は更新しない
                if (checkBoxAreaLocks[i])
                {
                    continue;
                }

                // - を削除する
                string tag = this.areaTagNo[i];
                var removeChars = new char[] { '-' };
                tag = removeChars.Aggregate(tag, (s, c) => s.Replace(c.ToString(), ""));

                if (locationString[(int)LOCATION.TAG] == "000000000000000" + tag)
                {
                    areaPositionX[i] = (double)listLocation[(int)LOCATION.X];
                    areaPositionY[i] = (double)listLocation[(int)LOCATION.Y];
                    areaPositionZ[i] = (double)listLocation[(int)LOCATION.Z];
                    areaPositionOffsetX[i] = areaPositionX[i] + areaOffsetRightLeft[i];
                    areaPositionOffsetY[i] = areaPositionY[i] + areaOffsetUpDown[i];
                    viewAreaPositionX[i] = OriginX + areaPositionX[i] * (double)ScaleX;
                    viewAreaPositionY[i] = -(OriginY) - areaPositionY[i] * (double)ScaleY;
                    viewAreaPositionOffsetX[i] = OriginX + areaPositionOffsetX[i] * (double)ScaleX;
                    viewAreaPositionOffsetY[i] = -(OriginY) - areaPositionOffsetY[i] * (double)ScaleY;
                    break;
                }
            } // AREA_MAX loop

        }

        public void WriteLog(string str)
        {
            sysLogFile.Write(str);
        }

        // 2018/04/05 Rev1.10 シルウォッチ対応 start
        void ControlIPPower(int j)
        {
            if (deserializedObect.UserList.Users[j].Enable)
            {
                // IPPowerがOnを維持するため、ONの2秒後にOFFにする
                string SWITCH_TIME = "2";

                // "http://192.168.1.3/set.cmd?cmd=setpower+p61=1+p61n=0+t61=2"
                string sURL = "http://" + deserializedObect.UserList.Users[j].IpAddress.ToString() + "/set.cmd?cmd=setpower+p" + deserializedObect.UserList.Users[j].Port.ToString() + "=1+p" + deserializedObect.UserList.Users[j].Port.ToString() + "n=0+t" + deserializedObect.UserList.Users[j].Port.ToString() + "=" + SWITCH_TIME;

                WebRequest wrGETURL;
                wrGETURL = WebRequest.Create(sURL);

                WebProxy myProxy = new WebProxy("myproxy", 80);
                myProxy.BypassProxyOnLocal = true;

                wrGETURL.Proxy = WebProxy.GetDefaultProxy();

                Stream objStream;
                objStream = wrGETURL.GetResponse().GetResponseStream();

                StreamReader objReader = new StreamReader(objStream);

                string sLine = "";
                int i = 0;

                while (sLine != null)
                {
                    i++;
                    sLine = objReader.ReadLine();
                    if (sLine != null)
                        Console.WriteLine("{0}:{1}", i, sLine);
                }
                Console.ReadLine();

            }
        }
    }
    #region 画面更新一時停止

    public static class NativeMethods
    {
        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(
        HandleRef hWnd, int msg, IntPtr wParam, IntPtr lParam);
        //public static extern IntPtr SendMessage(
        //    HandleRef hWnd, int msg, IntPtr wParam, IntPtr lParam);

        private const int WM_SETREDRAW = 0x000B;

        /// <summary>
        /// コントロールの再描画を停止させる
        /// </summary>
        /// <param name="control">対象のコントロール</param>
        public static void BeginControlUpdate(Control control)
        {
            SendMessage(new HandleRef(control, control.Handle),
                WM_SETREDRAW, IntPtr.Zero, IntPtr.Zero);
        }

        /// <summary>
        /// コントロールの再描画を再開させる
        /// </summary>
        /// <param name="control">対象のコントロール</param>
        public static void EndControlUpdate(Control control)
        {
            SendMessage(new HandleRef(control, control.Handle),
                WM_SETREDRAW, new IntPtr(1), IntPtr.Zero);
            control.Invalidate();
        }
    }
    #endregion 画面更新一時停止
}