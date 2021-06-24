using System;
using System.Collections.Generic;
using System.Text;

namespace cononia.src.db
{
    class DBInfo
    {
        private static string _dbVersion = "0.0.1";
        private static string _dbName = "cononia.db";
        private static Dictionary<string, string> _tableSchema = new Dictionary<string, string>()
        {
            { "OrderInfo", @"CREATE TABLE OrderInfo(ID INTEGER PRIMARY KEY, Name TEXT NOT NULL, Phone TEXT)"},
            { "Ingredient", @"CREATE TABLE Ingredient(
                    ID INTEGER PRIMARY KEY, 
                    Name TEXT NOT NULL, 
                    Stock REAL NOT NULL, 
                    UnitType TEXT NOT NULL,
                    Price REAL,
                    Allergies INTEGER,
                    OrderInfoID INTEGER REFERENCES OrderInfo(ID) ON UPDATE CASCADE ON DELETE SET NULL)"
            },
            { "PriceTrace",  @"CREATE TABLE PriceTrace(
                    ID INTEGER REFERENCES Ingredient(ID) ON UPDATE CASCADE ON DELETE CASCADE, 
                    Date DATE, 
                    Price INTEGER, 
                    PRIMARY KEY(ID, Date))"
            }
        };

        public static string DBVersion { get { return _dbVersion; } }
        public static string DBName { get { return _dbName; } }

        public static void GetTableName(out string[] tables)
        {
            tables = new string[_tableSchema.Count];
            _tableSchema.Keys.CopyTo(tables, 0);
        }

        public static string GetSchema(string tableName)
        {
            if (_tableSchema.ContainsKey(tableName))
            {
                return _tableSchema[tableName];
            }
            else return "";
        }


    }
}
