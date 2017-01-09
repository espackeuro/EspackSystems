
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
        [PrimaryKey, AutoIncrement]
        public int idreg { get; set; }
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
        [PrimaryKey, AutoIncrement]
        public int idreg { get; set; }
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
        public DateTime xfec { get; } = DateTime.Now;

    }

    
    public static class ScannedDataControl
    {
        public static string ProcedureParameters(this ScannedData SD)
        {
            return string.Format("@action='{0}', @Service='{1}', @Session='{2}', @Rack='{3}', @Partnumber='{4}', @Qty={5}, @LabelRack='{6}', @Serial='{7}'", SD.Action, SD.Service, SD.Session, SD.Rack, SD.Partnumber, SD.Qty, SD.LabelRack, SD.Serial);
        }
        public static  infoData ToInfoData(this ScannedData SD)
        {
            var result = new infoData();
            switch (SD.Action)
            {
                case "ADD":
                    result.c0 = string.Format("ADD");
                    result.c1 = string.Format("PN:{0}", SD.Partnumber);
                    //result.c2 = string.Format("R:{0}", SD.LabelRack);
                    result.c2 = string.Format("Q:{0}", SD.Qty);
                    result.c3 = SD.xfec.ToShortTimeString();
                    break;
                case "CHECK":
                    result.c0 = string.Format("CHECK");
                    result.c1 = string.Format("SERIAL:{0}", SD.Serial);
                    result.c2 = string.Format("R:{0}", SD.Rack);
                    break;
                case "CLOSE":
                    result.c0 = string.Format("CLOSE");
                    break;
            }
            return result;
        }
    }
    
    #endregion
    public class SQLiteDatabase
    {
        public EventSQLiteAsyncConnection db;
        public bool Complete { get; set; } = false;
        public string DatabaseName { get; private set; }
        public bool Exists
        {
            get
            {
                return File.Exists(dbPath);
            }
        }
        public string dbPath
        {
            get
            {
                return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), DatabaseName + ".db3");
            }
        }
        public void CreateDatabase()
        {
            //dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), dbName+".db3");
            db = new EventSQLiteAsyncConnection(dbPath);
            db.AfterInsert += Db_AfterInsert;
            db.AfterUpdate += Db_AfterUpdate;

        }

        private async void Db_AfterUpdate(object sender, AfterUpdateEventArgs e)
        {
            var _pending = (await Values.SQLidb.db.FindAsync<ScannedData>(r => r.Transmitted == false) != null);
            if (_pending != pendingData)
                pendingData = _pending;
        }

        public void DropDatabase()
        {
            db = null;
            if (File.Exists(dbPath))
                File.Delete(dbPath);
            Complete = false;
        }
        private async void Db_AfterInsert(object sender, AfterInsertEventArgs e)
        {
            if (e.ItemInserted is ScannedData)
                await Values.dFt.pushInfo(((ScannedData)e.ItemInserted).ToInfoData());
            var _pending = (await Values.SQLidb.db.FindAsync<ScannedData>(r => r.Transmitted == false) != null);
            if (_pending != pendingData)
                pendingData = _pending;
        }

        public SQLiteDatabase(string _databaseName)
        {
            DatabaseName = _databaseName;
        }
        public bool pendingData { get; private set; }

    }

}