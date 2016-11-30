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
        public event EventHandler<AfterUpdateEventArgs> AfterUpdate;
        protected virtual void OnAfterUpdate(AfterUpdateEventArgs e)
        {
            EventHandler<AfterUpdateEventArgs> handler = AfterUpdate;
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

        public new async Task<int> UpdateAsync(object item)
        {
            int result = await base.UpdateAsync(item);
            OnAfterUpdate(new AfterUpdateEventArgs() { ItemUpdated = item });
            return result;
        }

    }
    public class AfterInsertEventArgs : EventArgs
    {
        public object ItemInserted { get; set; }
    }
    public class AfterUpdateEventArgs : EventArgs
    {
        public object ItemUpdated { get; set; }
    }
}