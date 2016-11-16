
using SQLite;
using System;
using System.IO;

namespace RadioLogisticaDeliveries
{
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
        [PrimaryKey, AutoIncrement]
        public int idreg { get; set; }
        public string Partnumber { get; set; }
        public int qty { get; set; }
        public int boxes { get; set; }
        public string rack { get; set; }
        public string mod { get; set; }
    }


    //table Referencias
    public class Referencias
    {
        [PrimaryKey]
        public string partnumber { get; set; }
    }

    //table RacksBlocks
    public class RacksBlocks
    {
        [PrimaryKey, AutoIncrement]
        public int idreg { get; set; }
        public string Block { get; set;  }
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


    public static class SQLiteDatabase
    {
        public static SQLiteAsyncConnection db; 
        public static string dbPath { get; set; }
        public static void CreateDatabase(string dbName)
        {
            dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), dbName+".db3");
            File.Delete(dbPath);
            db = new SQLiteAsyncConnection(dbPath);
        }

    }

}