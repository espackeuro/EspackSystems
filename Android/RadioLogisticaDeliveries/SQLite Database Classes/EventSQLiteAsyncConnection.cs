using System;

using SQLite;
using System.Threading.Tasks;

namespace RadioLogisticaDeliveries
{

    public class EventSQLiteAsyncConnection : SQLiteAsyncConnection
    {
        public event EventHandler<AfterInsertEventArgs> AfterInsert;
        protected virtual void OnAfterInsert(AfterInsertEventArgs e)
        {
            EventHandler<AfterInsertEventArgs> handler = AfterInsert;
            if (handler != null)
            {
                handler(this, e);
            }
        }
        public EventSQLiteAsyncConnection(string path) : base(path)
        {

        }

        public new async Task<int> InsertAsync(object item)
        {
            int result = await base.InsertAsync(item);
            OnAfterInsert(new AfterInsertEventArgs() { ItemInserted = item });
            return result;
        }

    }
    public class AfterInsertEventArgs : EventArgs
    {
        public object ItemInserted { get; set; }
    }
}