using cononia.src.common;
using cononia.src.db;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace cononia.src.model
{
    class OrderInfo : IBaseItem
    {
        private string _phone;
        const string _phonePattern = @"[0-9]{2,3}[-)][0-9]{3,4}-[0-9]{4}";
        const string _onlyNumPattern = @"[0-9]+";

        public long ID { get; set; }
        public string Name { get; set; }

        public string Phone
        {
            get
            {
                return _phone;
            }
            set
            {
                string number = value;
                if (Regex.IsMatch(number, _phonePattern))
                {
                    Debug.WriteLine("!!!!0");
                }
                else if (number.Length == 9 && Regex.IsMatch(number, _onlyNumPattern))
                {
                    Debug.WriteLine("!!!!1");
                    number = number.Insert(2, "-");
                    number = number.Insert(6, "-");
                }
                else if (number.Length == 10 && Regex.IsMatch(number, _onlyNumPattern))
                {
                    Debug.WriteLine("!!!!2");
                    if (number.Substring(0, 2) == "02")
                    {
                        Debug.WriteLine("!!!!3");
                        number = number.Insert(2, "-");
                        number = number.Insert(7, "-");
                    }
                    else
                    {
                        Debug.WriteLine("!!!!4");
                        number = number.Insert(3, "-");
                        number = number.Insert(7, "-");
                    }

                }
                else if (number.Length == 11 && Regex.IsMatch(number, _onlyNumPattern))
                {
                    Debug.WriteLine("before : " +number);
                    Debug.WriteLine("!!!!5");
                    number = number.Insert(3, "-");
                    number = number.Insert(8, "-");
                    
                    Debug.WriteLine("after : " + number);
                }
                else
                    number = "!!!not Phone number!!!";
                _phone = number;

            }
        }

        public OrderInfo() { }
        public OrderInfo(string OrderName, string OrderPhone)
        {
            Name = OrderName;
            Phone = OrderPhone;
        }

        public long GetId()
        {
            return ID;
        }

        public string GetName()
        {
            return Name;
        }

        public override string ToString()
        {
            return "Name : " + Name + "  Phone : " + Phone;
        }

    }

    class OrderInfoManager : ABaseManager<OrderInfoManager, OrderInfo>
    {   
        private enum Columns
        {
            ID, Name, Phone
        }

        protected override void PrepareInsertCommand()
        {
            InsertCommand = new SQLiteCommand(_dbManager.Connection);
            InsertCommand.CommandText
                = @"INSERT INTO OrderInfo(Name, Phone) VALUES(@Name, @Phone)";
            InsertCommand.Parameters.Add("@Name", DbType.String);
            InsertCommand.Parameters.Add("@Phone", DbType.String);
        }
        protected override void PrepareSelectByIDCommand()
        {
            SelectByIdCommand = new SQLiteCommand(_dbManager.Connection);
            SelectByIdCommand.CommandText = @"SELECT * FROM OrderInfo WHERE ID= @ID";
            SelectByIdCommand.Parameters.Add("@ID", DbType.Int64);
        }

        protected override void PrepareSelectByNameCommand()
        {
            SelectByNameCommand = new SQLiteCommand(_dbManager.Connection);
            SelectByNameCommand.CommandText = @"SELECT * FROM OrderInfo WHERE Name= @Name";
            SelectByNameCommand.Parameters.Add("@Name", DbType.String);
        }

        public override void Initialize()
        {
            MaxCacheSize = 50;
            base.Initialize();
        }

        private void MakeOrderInfo(SQLiteDataReader reader, out OrderInfo OrderInfo)
        {
            OrderInfo = new OrderInfo();

            if (reader.Read())
            {
                OrderInfo.ID = reader.GetInt64((int)Columns.ID);
                OrderInfo.Name = reader.GetString((int)Columns.Name);
                OrderInfo.Phone = reader.GetString((int)Columns.Phone); 
            }
        }

        public OrderInfo SelectOrderInfoById(long ID)
        {
            OrderInfo info = RetrieveData(ID);
            if (info != null)
                return info;

            try
            {
                _dbManager.Connection.Open();
                SelectByIdCommand.Parameters["@ID"].Value = ID;
                SQLiteDataReader reader =  SelectByIdCommand.ExecuteReader();
                MakeOrderInfo(reader, out info);
            }
            catch(Exception e)
            {
                Debug.WriteLine(e.StackTrace);
            }
            finally
            {
                _dbManager.Connection.Close();
            }

            return info;
        }
        
        public OrderInfo SelectOrderInfoByName(string Name)
        {
            OrderInfo info = RetrieveData(Name);
            if (info != null)
                return info;

            try
            {
                _dbManager.Connection.Open();
                SelectByIdCommand.Parameters["@Name"].Value = Name;
                SQLiteDataReader reader = SelectByIdCommand.ExecuteReader();
                MakeOrderInfo(reader, out info);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.StackTrace);
            }
            finally
            {
                _dbManager.Connection.Close();
            }

            return info;
        }

        public long InsertOrderInfo(OrderInfo info)
        {
            long lastInsertId = -1;

            try
            {
                _dbManager.Connection.Open();

                InsertCommand.Parameters["@Name"].Value = info.Name;
                InsertCommand.Parameters["@Phone"].Value = info.Phone;
                InsertCommand.ExecuteNonQuery();

                lastInsertId = (long)GetLastInsertRowIdCommand.ExecuteScalar();
                Debug.WriteLine("id : " + lastInsertId);
            }
            catch(Exception e)
            {
                Debug.WriteLine(e.StackTrace);
            }
            finally
            {
                _dbManager.Connection.Close();
            }

            return lastInsertId;
        }
    }
}
