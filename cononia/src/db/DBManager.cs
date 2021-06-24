using cononia.src.common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;

namespace cononia.src.db
{
    class DBManager : Singleton<DBManager>
    {
        //private static DBManager _instance;
        private string _dbBasePath;
        private SQLiteConnection _connection = null;
        
        private bool _needSchemaUpdate = false;

        public SQLiteConnection Connection
        {
            get
            {
                if(_connection == null)
                {

                    string dbPath = System.IO.Path.Combine(_dbBasePath, DBInfo.DBName);
                    string dataSource = String.Format(@"Data Source={0}", dbPath);
                    _connection = new SQLiteConnection(dataSource);
                }
                return _connection;
            }
        }

        public override void Initialize()
        {
            if (Initialized)
            {
                return;
            }
            base.Initialize();

            // db 위치 폴더 체크
            _dbBasePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "sqlite");
            if (!System.IO.Directory.Exists(_dbBasePath))
            {
                Debug.WriteLine("make db dir");
                System.IO.Directory.CreateDirectory(_dbBasePath);
            }

            // db 위치 파일 체크
            string dbPath = System.IO.Path.Combine(_dbBasePath, DBInfo.DBName);
            string[] tables;
            DBInfo.GetTableName(out tables);
            if (!System.IO.File.Exists(dbPath))
            {
                foreach(var table in tables )
                {
                    CreateTable(table, DBInfo.GetSchema(table));
                }
            }

            // db 스키마 초기화
            if (_needSchemaUpdate)
            {
                foreach (var table in tables)
                {
                    DropTable(table);
                    CreateTable(table, DBInfo.GetSchema(table));
                }
            }

            
        }

        private void CreateTable(string tableName, string sql)
        {

            if(!CheckTable(tableName))
            {
                Connection.Open();
                SQLiteCommand command = new SQLiteCommand(Connection);
                command.CommandText = sql;
                command.ExecuteNonQuery();
                Connection.Close();
            }
        }

        private void DropTable(string tableName)
        {
            if (CheckTable(tableName))
            {
                Connection.Open();
                SQLiteCommand command = new SQLiteCommand(Connection);
                command.CommandText = String.Format(@"DROP TABLE {0}", tableName);
                command.ExecuteNonQuery();
                Connection.Close();
            }
        }

        private bool CheckTable(string tableName)
        {
            // 테이블이 있는지 체크
            bool isExist = true;
            Connection.Open();
            SQLiteCommand command = new SQLiteCommand(Connection);
            command.CommandText = String.Format(@"SELECT name FROM sqlite_master WHERE type='table' AND name='{0}'", tableName);
            SQLiteDataReader reader = command.ExecuteReader();
            if (!reader.Read())
            {
                isExist = false;
            }
            reader.Close();
            Connection.Close();
            return isExist;
        }

        public long GetLastInsertRowID()
        {
            Connection.Open();
            SQLiteCommand command = new SQLiteCommand(Connection);
            command.CommandText = String.Format(@"SELECT last_insert_rowid()");
            SQLiteDataReader reader = command.ExecuteReader();
            long lastInsertRowID;
            if (reader.Read())
            {
                lastInsertRowID = reader.GetInt64(0);
            }
            else
            {
                lastInsertRowID = -1;
            }
            Connection.Close();
            return lastInsertRowID;
        }
    }
}
