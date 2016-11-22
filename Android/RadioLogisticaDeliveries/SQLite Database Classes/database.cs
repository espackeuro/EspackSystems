
using SQLite;
using System;
using System.IO;


namespace RadioLogisticaDeliveries
{


    #region Tables
    [Table("Readings")]
    public class Readings
    {
        [PrimaryKey, AutoIncrement]
        public int idreg { get; set; }
        public string Session { get; set; }
        public string Rack { get; set; }
        public string Partnumber { get; set; }
        public string Service { get; set; }
        public int Qty { get; set; }
        public string LabelRack { get; set; }
    }

    //table Labels
    public class Labels
    {
        [PrimaryKey]
        public string Serial { get; set; }
        public string Partnumber { get; set; }
        public int qty { get; set; }
        public int boxes { get; set; }
        public string rack { get; set; }
        public string mod { get; set; }
    }

    /*
    //table Referencias
    public class Referencias
    {
        [PrimaryKey]
        public string partnumber { get; set; }
    }
    */
    //table RacksBlocks
    public class RacksBlocks
    {
        [PrimaryKey, AutoIncrement]
        public int idreg { get; set; }
        public string Block { get; set; }
        public string Rack { get; set; }
    }

    //table PartnumbersRacks
    public class PartnumbersRacks
    {
        [PrimaryKey, AutoIncrement]
        public int idreg { get; set; }
        public string Rack { get; set; }
        public string Partnumber { get; set; }
        public int MinBoxes { get; set; }
        public int MaxBoxes { get; set; }

    }
    public class SerialTracking
    {
        [PrimaryKey]
        public string Serial { get; set; }
    } 
    

    public class ScannedData
    {
        [PrimaryKey, AutoIncrement]
        public int idreg { get; set; }
        public string Action { get; set; }
        public string Service { get; set; } = "";
        public string Session { get; set; }
        public string Rack { get; set; } = "";
        public string Partnumber { get; set; } = "";
        public int Qty { get; set; } = 0;
        public string LabelRack { get; set; } = "";
        public string Serial { get; set; } = "";
        public bool Transmitted { get; set; } = false;
        public string TransmissionResult { get; set; } = "";
        public infoData ToInfoData()
        {
            var result = new infoData();
            switch (Action)
            {
                case "ADD":
                    result.c0 = string.Format("ADD");
                    result.c1 = string.Format("PN:{0}",Partnumber);
                    result.c2 = string.Format("R:{0}", LabelRack);
                    result.c3 = string.Format("Q:{0}", Qty);
                    break;
                case "CHECK":
                    result.c0 = string.Format("CHECK");
                    result.c1 = string.Format("SERIAL:{0}", Serial);
                    result.c2 = string.Format("R:{0}", Rack);
                    break;
                case "CLOSE":
                    result.c0 = string.Format("CLOSE");
                    break;
            }
            return result;
        }
    }
    #endregion
    public static class SQLidb
    {
        public static EventSQLiteAsyncConnection db; 
        public static string dbPath { get; set; }
        public static void CreateDatabase(string dbName)
        {
            dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), dbName+".db3");
            File.Delete(dbPath);
            db = new EventSQLiteAsyncConnection(dbPath);
            db.AfterInsert += Db_AfterInsert; 


        }

        private static void Db_AfterInsert(object sender, AfterInsertEventArgs e)
        {
            if (e.ItemInserted is ScannedData)
                Values.dFt.pushInfo(((ScannedData)e.ItemInserted).ToInfoData());
        }
    }

}