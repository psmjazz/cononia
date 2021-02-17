using cononia.src.common;
using cononia.src.db;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace cononia.src.model
{
    abstract class ABaseManager<TManager, TItem>: Singleton<TManager> where TManager:class, new() where TItem: IBaseItem
    {
        private DataCache<long, TItem> _cache;
        private DataCache<string, long> _nameCache;
        protected DBManager _dbManager;

        protected void CacheData(long ID, TItem Item)
        {
            _cache.InsertData(ID, Item);
            _nameCache.InsertData(Item.GetName(), ID);
        }
        protected TItem RetrieveData(long ID)
        {
            return _cache.GetData(ID);
        }
        protected TItem RetrieveData(string Name)
        {
            if (_nameCache.HasData(Name))
                return _cache.GetData(_nameCache.GetData(Name));
            else
                return default(TItem);
        }
        
        protected SQLiteCommand InsertCommand { get; set; }
        protected SQLiteCommand GetLastInsertRowIdCommand { get; set; }
        protected SQLiteCommand SelectByNameCommand { get; set; }
        protected SQLiteCommand SelectByIdCommand { get; set; }

        public int MaxCacheSize { get; set; }

        protected void PrepareGetLastInsertRowIDCommand()
        {
            GetLastInsertRowIdCommand = new SQLiteCommand(_dbManager.Connection);
            GetLastInsertRowIdCommand.CommandText = @"SELECT last_insert_rowid()";
        }
        protected abstract void PrepareInsertCommand();
        protected abstract void PrepareSelectByIDCommand();
        protected abstract void PrepareSelectByNameCommand();

        public override void Initialize()
        {
            if (Initialized)
                return;
            Initialized = true;

            _dbManager = DBManager.Instance;
            if (!_dbManager.IsInitialized())
                _dbManager.Initialize();

            _cache = new DataCache<long, TItem>(MaxCacheSize);
            _nameCache = new DataCache<string, long>(MaxCacheSize);

            PrepareGetLastInsertRowIDCommand();
            PrepareInsertCommand();
            PrepareSelectByIDCommand();
            PrepareSelectByNameCommand();
        }

    }
}
