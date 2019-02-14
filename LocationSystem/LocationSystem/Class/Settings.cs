using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace LocationSystem
{
    [Serializable]
    public class Settings
    {
        ////設定を保存するフィールド
        //private string _text;
        //private int _number;

        ////設定のプロパティ
        //public string Text
        //{
        //    get { return _text; }
        //    set { _text = value; }
        //}
        //public int Number
        //{
        //    get { return _number; }
        //    set { _number = value; }
        //}

        ////コンストラクタ
        //public Settings()
        //{
        //    _text = "Text";
        //    _number = 0;
        //}

        ////Settingsクラスのただ一つのインスタンス
        //[NonSerialized()]
        //private static Settings _instance;

        //[System.Xml.Serialization.XmlIgnore]
        //public static Settings Instance
        //{
        //    get
        //    {
        //        if (_instance == null)
        //            _instance = new Settings();
        //        return _instance;
        //    }
        //    set { _instance = value; }
        //}

        ///// <summary>
        ///// 設定をXMLファイルから読み込み復元する
        ///// </summary>
        //public static void LoadFromXmlFile()
        //{
        //    string path = GetSettingPath();

        //    StreamReader sr = new StreamReader(path, new UTF8Encoding(false));
        //    System.Xml.Serialization.XmlSerializer xs =
        //        new System.Xml.Serialization.XmlSerializer(typeof(Settings));
        //    //読み込んで逆シリアル化する
        //    //object obj = xs.Deserialize(sr);
        //    sr.Close();

        //    //Instance = (Settings)obj;
        //}

        ///// <summary>
        ///// 現在の設定をXMLファイルに保存する 
        ///// </summary>
        //public static void SaveToXmlFile()
        //{
        //    string path = GetSettingPath();

        //    StreamWriter sw = new StreamWriter(path);
        //    //StreamWriter sw = new StreamWriter(path, new UTF8Encoding(false));
        //    XmlSerializer xs = new XmlSerializer(typeof(FormMain));
        //    //シリアル化して書き込む
        //    xs.Serialize(sw, Instance);
        //    sw.Close();
        //}

        //private static string GetSettingPath()
        //{
        //    return @"C:\test\settings.config";
        //}

    }

}
