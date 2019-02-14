using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocationSystem
{
    //XMLファイルに保存するオブジェクトのためのクラス
    //public class SampleClass
    //{
    //    public int Number;
    //    public string Message;
    //}

    //class LocationSystemXML
    //{
    //    //SampleClassオブジェクトをXMLファイルに保存する
    //    public static void SaveToXML()
    //    {
    //        //保存先のファイル名
    //        string fileName = @"C:\test\sample.xml";

    //        //保存するクラス(SampleClass)のインスタンスを作成
    //        SampleClass obj = new SampleClass();
    //        obj.Message = "テストです。";
    //        obj.Number = 123;

    //        //XmlSerializerオブジェクトを作成
    //        //オブジェクトの型を指定する
    //        System.Xml.Serialization.XmlSerializer serializer =
    //            new System.Xml.Serialization.XmlSerializer(typeof(SampleClass));
    //        //書き込むファイルを開く（UTF-8 BOM無し）
    //        System.IO.StreamWriter sw = new System.IO.StreamWriter(
    //            fileName, false, new System.Text.UTF8Encoding(false));
    //        //シリアル化し、XMLファイルに保存する
    //        serializer.Serialize(sw, obj);
    //        //ファイルを閉じる
    //        sw.Close();
    //    }

    //    //XMLファイルをSampleClassオブジェクトに復元する
    //    public static void Restore()
    //    {
    //        //保存元のファイル名
    //        string fileName = @"C:\test\sample.xml";

    //        //XmlSerializerオブジェクトを作成
    //        System.Xml.Serialization.XmlSerializer serializer =
    //            new System.Xml.Serialization.XmlSerializer(typeof(SampleClass));
    //        //読み込むファイルを開く
    //        System.IO.StreamReader sr = new System.IO.StreamReader(
    //            fileName, new System.Text.UTF8Encoding(false));
    //        //XMLファイルから読み込み、逆シリアル化する
    //        SampleClass obj = (SampleClass)serializer.Deserialize(sr);
    //        //ファイルを閉じる
    //        sr.Close();
    //    }
    //}
}
