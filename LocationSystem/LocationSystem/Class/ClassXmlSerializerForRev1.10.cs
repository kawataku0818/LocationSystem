using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace LocationSystem.Class
{
    [XmlRoot(ElementName = "root")]
    public class Root
    {

        [XmlElement(ElementName = "userlist")]
        public UserList UserList { get; set; }

        [XmlElement(ElementName = "weightedmovingaveragelist")]
        public WeightedMovingAverageList WeightedMovingAverageList { get; set; } // 複数要素の場合リスト
        //[XmlElement(ElementName = "weightedmovingaverage")]
        //public List<WeightedMovingAverage> WeightedMovingAverageList { get; set; } // 複数要素の場合リスト

        [XmlElement(ElementName = "machinelist")]
        public LockMachinePositionList LockMachinePositionList { get; set; }
    }

    public class UserList
    {
        [XmlElement(ElementName = "user")]
        public List<User> Users { get; set; } // 複数要素の場合リスト
    }

    public class User
    {
        [XmlAttribute(AttributeName = "no")]
        public int No { get; set; }

        [XmlElement(ElementName = "enable")]
        public bool Enable { get; set; }

        [XmlElement(ElementName = "ipaddress")]
        public string IpAddress { get; set; }

        [XmlElement(ElementName = "port")]
        public string Port { get; set; }
    }

    public class WeightedMovingAverageList
    {
        [XmlElement(ElementName = "weightedmovingaverage")]
        public List<WeightedMovingAverage> WeightedMovingAverages { get; set; } // 複数要素の場合リスト
    }

    public class WeightedMovingAverage
    {
        [XmlElement(ElementName = "usergroupcount")]
        public int CountUserGroup { get; set; }

        [XmlElement(ElementName = "machinegroupcount")]
        public int CountMachineGroup { get; set; }
    }
    
    public class LockMachinePositionList
    {
        [XmlElement(ElementName = "machine")]
        public List<LockMachinePosition> LockMachinePositions { get; set; } // 複数要素の場合リスト
    }

    public class LockMachinePosition
    {
        [XmlAttribute(AttributeName = "no")]
        public int No { get; set; }

        [XmlElement(ElementName = "LockEnableX")]
        public bool LockEnableX { get; set; }

        [XmlElement(ElementName = "LockPositionX")]
        public double LockPositionX { get; set; }

        [XmlElement(ElementName = "LockEnableY")]
        public bool LockEnableY { get; set; }

        [XmlElement(ElementName = "LockPositionY")]
        public double LockPositionY { get; set; }
    }


    public static class MyXmlSerializer
    {
        // ファイルに書き出すときに使う
        //public static void Serialize<T>(string savePath, T graph)
        //{
        //    using (var sw = new StreamWriter(savePath, false, Encoding.UTF8))
        //    {
        //        var ns = new XmlSerializerNamespaces();
        //        ns.Add(string.Empty, string.Empty);

        //        new XmlSerializer(typeof(T)).Serialize(sw, graph, ns);
        //    }
        //}

        // ファイルを読み取るときに使う
        public static T Deserialize<T>(string loadPath)
        {
            using (var sr = new StreamReader(loadPath))
            {
                return (T)new XmlSerializer(typeof(T)).Deserialize(sr);
            }
        }
    }
}
