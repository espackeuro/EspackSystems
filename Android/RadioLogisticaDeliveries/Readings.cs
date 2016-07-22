
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

    public static class SQLiteDatabase
    {
        public static SQLiteConnection db; 
        public static string dbPath { get; set; }
        public static void CreateDatabase(string dbName)
        {
            dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), dbName+".db3");
            db = new SQLiteConnection(dbPath);
        }

    }

}