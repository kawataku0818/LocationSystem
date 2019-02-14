// システム情報設定ファイル（XML）読込クラス
//
// --DATE--   --VRT--     --NAME--    --EVENT--
// 08.03.24   01-00-00    TSK yamaga  初版作成
// 09.11.12   01-01-00    TSK yamaga  .NET Compact Framework対応
//

using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Xml;
using System.IO;
using System.Windows.Forms;
using System.Reflection;

namespace TskCommon
{

    /// <summary>
    /// システム情報設定ファイル（XML）読込クラス
    /// </summary>
    public abstract class InitialXml
    {

        /// <summary>
        /// 読込異常メッセージボックス出力設定
        /// </summary>
        private Boolean errMsg;


        /// <summary>
        /// コンストラクタ
        /// </summary>
        public InitialXml()
        {
            errMsg = true;
        }

        /// <summary>
        ///  読込異常メッセージボックス出力設定
        /// </summary>
        public Boolean ErrMsg
        {
            get { return this.errMsg; }
            set { this.errMsg = value; }
        }


        /// <summary>
        /// 初期化
        /// </summary>
        /// <returns>true : 正常終了、 false : 異常終了</returns>
        public Boolean Init()
        {

            // 設定ファイルパス取得
            String path = ((Module)(Assembly.GetExecutingAssembly().GetModules().GetValue(0))).FullyQualifiedName;
            path = path.Replace(Path.GetExtension(path), ".xml");


            // XMLファイルのロード
            XmlDocument xml = new XmlDocument();
            if (File.Exists(path) == false)
            {
                goto FALSE_POINT;
            }

            // 設定ファイル読込
            try
            {
                xml.Load(path);
            }
            // 例外処理
            catch (Exception e)
            {
                // Exception
                Debug.WriteLine(e.Message);
                goto FALSE_POINT;
            }

            // 各種設定読込
            if (Load(xml) == false)
            {
                goto FALSE_POINT;
            }


            return true;

        FALSE_POINT:

            return false;

        }


        /// <summary>
        /// 各種設定読込
        /// </summary>
        /// <param name="xml">対象Xml情報</param>
        /// <returns>true : 正常終了、 false : 異常終了</returns>
        protected abstract Boolean Load(XmlDocument xml);


        /// <summary>
        /// 指定情報読込
        /// </summary>
        /// <param name="xml">対象Xml情報</param>
        /// <param name="SelectNodeName">対象ノード名称</param>
        /// <param name="Key">対象キー名称</param>
        /// <returns>設定情報</returns>
        protected String GetText(XmlDocument xml, String selectNodeName, String key)
        {
            try
            {
                return xml.SelectSingleNode("SYSTEM/" + selectNodeName + "/" + key).InnerText;
            }
            // 例外処理
            catch (Exception e)
            {
                // Exception
                Debug.WriteLine("Error XML Read (" + selectNodeName + "/" + key + ") [" + e.Message + "]");
                goto FALSE_POINT;
            }

        FALSE_POINT:

            if (this.errMsg == true)
            {
                // 異常表示
                MessageBox.Show("設定ファイル読込異常 [" + selectNodeName + "/" + key + "]", "初期設定");
            }

            return "";

        }
       
    }
}
