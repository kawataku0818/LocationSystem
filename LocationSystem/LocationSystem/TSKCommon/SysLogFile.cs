// ログ出力クラス
//
// --DATE--   --VRT--     --NAME--      --EVENT--
// 09.06.11   01-00-00    TSK yamaga    初版作成
// 09.11.02   01-01-00    TSK yamaga    削除判断を更新日時に変更
// 10.12.10   01-02-00    TSK yamaga    ﾊﾟｽ指定/ﾌｧｲﾙ名指定異常処理
// 11.04.28   01-03-00    TSK ishimaru  ﾘｾｯﾄ処理の追加及び、排他ｱｸｾｽﾓｰﾄﾞをReadWriteに設定
//
// 指定フォルダ内にOpen時の日付フォルダを作成し、ログファイル管理します。
// 日付フォルダ内に同じ名前のファイルが存在し、
// ファイル更新日時の月が違う場合は一度削除され新規作成されます。
//

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace SysCommon
{
    public class SysLogFile
    {

        /// <summary> ファイル </summary>
        private WRITE_TYPE writeType;
        
        /// <summary> ファイル </summary>
        private StreamWriter sr;

		/// <summary> 現在日付 </summary>
		private DateTime currentTime;

		/// <summary> ログパス </summary>
		private String path;

		/// <summary> ログファイル名 </summary>
		private String name;

        /// <summary> 送受信タイプ </summary>
        public enum WRITE_TYPE
        {
            /// <summary> デフォルト </summary>
            DEFAULT,
            /// <summary> 受信 </summary>
            RECV,
            /// <summary> 送信 </summary>
            SEND
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="path">ファイルパス</param>
        /// <param name="name">ファイル名称</param>
		public SysLogFile(String logPath, String logFile, DateTime dt)
        {
            this.sr = null;
            this.writeType = WRITE_TYPE.DEFAULT;
			this.path = logPath;
			this.name = logFile;
			this.currentTime = dt;
		}

        /// <summary>
        /// オープン
        /// </summary>
        public Boolean Open()
        {
            // パス名 ファイル名チェック
			if (this.path == "" || this.name == "")
            {
                goto FALSE_POINT;
            }

            // フォルダチェック
			if (Directory.Exists(this.path) == false)
            {
                goto FALSE_POINT;
            }

            // フォルダパス（日付フォルダ付き）
			String fp = this.path + @"\" + DateTime.Now.Day.ToString("00");
 
            // 日付フォルダ確認
            if (Directory.Exists(fp) == false)
            {
                // フォルダ作成
                try
                {
                    Directory.CreateDirectory(fp);
                }
                catch
                {
                    //　異常としない・・・
                }
            }

            // ファイルチェック
			if (File.Exists(fp + @"\" + this.name) == true)
            {
                // 更新月が違う場合
				if (File.GetLastWriteTime(fp + @"\" + this.name).Month != DateTime.Now.Month)
                {
                    // 前回ファイル削除
                    try
                    {
						File.Delete(fp + @"\" + this.name);
                    }
                    catch
                    {
                        goto FALSE_POINT;
                    }
                }
            }

            // ファイルオープン
            try
            {
                //this.sr = new StreamWriter(fp + @"\" + this.name, 
                //									   true, 
                //									   Encoding.Default);

                FileStream fs = new FileStream(fp + @"\" + this.name, 
                                                    FileMode.Append, 
                                                    FileAccess.Write, 
                                                    FileShare.ReadWrite);
                this.sr = new StreamWriter(fs, Encoding.Default);


                this.sr.AutoFlush = true;
            }
            catch
            {
                goto FALSE_POINT;
            }

            return true;

        FALSE_POINT:

            return false;
        }

        /// <summary>
        /// クローズ
        /// </summary>
        public void Close()
        {
            if (this.sr != null)
            {
                // クローズ
                try
                {
                    this.sr.Close();
                    this.sr = null;
                }
                catch
                {
                    this.sr = null;
                }
            }
        }
        
        /// <summary>
        /// 書込み
        /// </summary>
        /// <param name="dt">データ</param>
        /// <returns></returns>
        public Boolean Write(String dt)
        {
            // インスタンス確認
            if (this.sr == null)
            {
                goto FALSE_POINT;
            }

			// 日付が変わったかどうかを判定
			if (this.currentTime.Day != DateTime.Now.Day)
			{
				// 現在日付を再設定
				this.currentTime = DateTime.Now;

				// ファイルをリセット
                this.Reset();
			}

            // ファイル書込
            try
            {
                switch(this.writeType)
                {
                    case WRITE_TYPE.DEFAULT:
                        // 書込み
                        this.sr.WriteLine(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff") + "  :  " + dt);
                        break;

                    case WRITE_TYPE.RECV:
                        // 書込み
                        this.sr.WriteLine(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff") + "【受信】:  " + dt);
                        break;

                    case WRITE_TYPE.SEND:
                        // 書込み
                        this.sr.WriteLine(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff") + "【送信】:  " + dt);
                        break;
                }
            }
            catch
            {
                goto FALSE_POINT;
            }

            return true;

        FALSE_POINT:

            // ファイルをリセット
            this.Reset();

            return false;
        }

        /// <summary>
        /// 書込み
        /// </summary>
        /// <param name="dt">データ</param>
        /// <returns></returns>
        public Boolean Write(Byte[] dt, WRITE_TYPE type)
        {
            if (dt == null)
            {
                return true;
            }

            // 書込みタイプを設定
            this.writeType = type;

            return this.Write(BitConverter.ToString(dt));
        }


		/// <summary>
		/// 書込み
		/// </summary>
		/// <param name="dt">データ</param>
		/// <returns></returns>
		public Boolean Write(UInt16[] dt, WRITE_TYPE type)
		{
			Byte[] byDt;
			Byte buf;
			if (dt == null)
			{
				return true;
			}


			byDt = new Byte[dt.Length * 2];

			Buffer.BlockCopy(dt, 0, byDt, 0, byDt.Length);

			for (Int32 i = 0; i < byDt.Length / 2; i++)
			{
				buf = byDt[i * 2];
				byDt[i * 2] = byDt[i * 2 + 1];
				byDt[i * 2 + 1] = buf;
			}

			// 書込みタイプを設定
			this.writeType = type;

			return this.Write(BitConverter.ToString(byDt));
		}

        /// <summary>
        /// リセット
        /// </summary>
        /// <returns></returns>
        public Boolean Reset()
        {
            // クローズ
            this.Close();

            // 再オープン
            return this.Open();
        }

    }
}
