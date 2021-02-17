using cononia.src.common;
using System;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;

namespace cononia.src.db
{
    class DBManager : Singleton<DBManager>
    {
        //private static DBManager _instance;
        private string _dbBasePath;
        private string _dbName = "cononia.db";
        private SQLiteConnection _connection = null;
        
        private bool _needSchemaUpdate = false;
        private string[,] _createSQLs = new string[,]
        {
            {
                "OrderInfo",
                @"CREATE TABLE OrderInfo(ID INTEGER PRIMARY KEY, Name TEXT NOT NULL, Phone TEXT)"
            },
            {
                "Ingredient",
                @"CREATE TABLE Ingredient(
                    ID INTEGER PRIMARY KEY, 
                    Name TEXT NOT NULL, 
                    Stock REAL NOT NULL, 
                    UnitType TEXT NOT NULL,
                    Price REAL,
                    Allergies INTEGER,
                    OrderInfoID INTEGER REFERENCES OrderInfo(ID) ON UPDATE CASCADE ON DELETE SET NULL)"
            },
            {
                "PriceTrace",
                @"CREATE TABLE PriceTrace(
                    ID INTEGER REFERENCES Ingredient(ID) ON UPDATE CASCADE ON DELETE CASCADE, 
                    Date DATE, 
                    Price INTEGER, 
                    PRIMARY KEY(ID, Date))"
            }

        };

        public SQLiteConnection Connection
        {
            get
            {
                if(_connection == null)
                {
                    string dbPath = System.IO.Path.Combine(_dbBasePath, _dbName);
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
            Initialized = true;

            // db 파일 체크
            _dbBasePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "sqlite");
            if (!System.IO.Directory.Exists(_dbBasePath))
            {
                Debug.WriteLine("make db dir");
                System.IO.Directory.CreateDirectory(_dbBasePath);
            }
            string dbPath = System.IO.Path.Combine(_dbBasePath, _dbName);
            if (!System.IO.File.Exists(dbPath))
            {
                SQLiteConnection.CreateFile(dbPath);
            }

            // db 스키마 초기화 및 업데이트
            if (_needSchemaUpdate)
            {
                for (int i = 0; i < _createSQLs.Length / 2; i++)
                {
                    DropTable(_createSQLs[i, 0]);
                    CreateTable(_createSQLs[i, 0], _createSQLs[i, 1]);
                    //Debug.WriteLine(_createSQLs[i, 0]);
                    //Debug.WriteLine(_createSQLs[i, 1]);
                    //Debug.WriteLine("+++++++++++++++++++");
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
