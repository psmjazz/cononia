using cononia.src.common;
using cononia.src.controller;
using cononia.src.db;
using cononia.src.rx;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace cononia.src.model
{
    
    //class Allergy
    //{
    //    public enum Allergies
    //    {
    //        Egg,
    //        Milk,
    //        BuckWheat,
    //        Peanut,
    //        Soybean,
    //        Wheat,
    //        Mackerel,
    //        Crab,
    //        Shirimp,
    //        Fork,
    //        Peach,
    //        Tomato,
    //        Sulfite,
    //        Wallnut,
    //        Chicken,
    //        Beef,
    //        Squid,
    //        Clam
    //    }

    //    public Allergies Name { get; set; }

    //    public override string ToString()
    //    {
    //        return Name.ToString();
    //    }
    //}

    
    class Ingredient :IBaseItem
    {
        public enum Allergy
        {
            Egg,// 난류
            Milk, // 우유
            BuckWheat, // 메밀
            Peanut, // 땅콩
            Soybean, // 콩
            Wheat, // 밀
            Mackerel, // 고등어
            Crab, // 게
            Shirimp, // 새우
            Fork, // 돼지고기
            Peach, // 복숭아
            Tomato, // 토마토
            Sulfite, // 아황산류
            Wallnut, // 호두
            Chicken, // 닭고기
            Beef, // 소고기
            Squid, // 오징어
            Clam // 조개류
        }

        private float _price = 0;
        private OrderInfo _orderInfo = null;
        private List<Allergy> _allergyList = null;

        public long Id { get; set; }
        public string Name { get; set; }
        public long OrderInfoID { get; set; }
        public AUnit Stock { get; set; }
        public float Price
        {
            get
            {
                return _price;
            }
            set
            {
                if(_price != value)
                {
                    DateTime today = DateTime.Today;
                    KeyValuePair<DateTime, float> priceInfo = new KeyValuePair<DateTime, float>(today, _price);
                    // db에 저장하는 로직
                    _price = value;
                }
            }
        }
        public OrderInfo OrderInformation
        { 
            //DB에서 갖고오고 하는거로 바꿔야 한다.
            get 
            {
                return _orderInfo;
            }
            set
            {
                _orderInfo = value;
            }
            
        }
        public List<Allergy> AllergyList 
        {
            get
            {
                if(_allergyList == null)
                {
                    _allergyList = new List<Allergy>();
                }
                return _allergyList;
            }
            set
            {
                _allergyList = value;
            }
        }

        public Ingredient() { }

        public Ingredient(string name, AUnit stock)
        {
            Name = name;
            Stock = stock;
        }
        public Ingredient(string name, AUnit stock, long orderInfoID, List<Allergy> allergies = null)
            :this(name, stock)
        {
            OrderInfoID = orderInfoID;
            AllergyList = allergies;
        }

        public override string ToString()
        {
            return "Id : " + Id + "  Name : " + Name + "  Stock" + Stock.ToString() + "  Price : " + Price;
        }

    }

    public enum RxIngredientCommand
    {
        GetAllIngredients,
    }

    class IngredientManager : Singleton<IngredientManager>
    {
        //private DataCache<long, Ingredient> _cache;
        //private DataCache<string, long> _nameCache;
        //const int _maxCacheSize = 100;
        private DBManager _dbManager = null;
        private List<IBaseItem> _ingredients;

        private SQLiteCommand _insertCommand;
        private SQLiteCommand _getLastInsertRowIDCommand;
        private SQLiteCommand _selectByIDCommand;
        private SQLiteCommand _selectByNameCommand;
        private SQLiteCommand _selectAllCommand;

        private enum _colunms
        {
            ID,
            Name,
            Stock,
            UnitType,
            Price,
            Allergies,
            OrderInfoID
        }

        public override void Initialize()
        {
            Debug.WriteLine("Ingredient init");
            if (Initialized)
                return;
            Initialized = true;

            _dbManager = DBManager.Instance;
            System.Diagnostics.Debug.WriteLine("_dbManager init? " + _dbManager.Initialized);
            if (!_dbManager.Initialized)
                _dbManager.Initialize();

            _ingredients = new List<IBaseItem>();
            //_cache = new Dictionary<long, Ingredient>();
            //_cache = new DataCache<long, Ingredient>(_maxCacheSize);
            //_nameCache = new DataCache<string, long>(_maxCacheSize);

            PrepareInsertCommand();
            PrepareGetLastInsertRowIDCommand();
            PrepareSelectByIDCommand();
            PrepareSelectByNameCommand();
            PrepareSelectAllCommand();

            RxCore.Instance.RegisterListener(RxIngredientCommand.GetAllIngredients, OnGetAllIngredients);
        }

        private void OnRxEvent(RxMessage message)
        {

        }

        private void OnGetAllIngredients(RxMessage message)
        {
            int count = SelectAll();
            Debug.WriteLine("all ingredients : " + count);
            RxMessage itemListMessage = new RxMessage();
            itemListMessage.Content = _ingredients;
            RxCore.Instance.Publish(EUpdateEvent.UpdateIngredientList, itemListMessage);
            //RxItemListMessage itemListMessage = new RxItemListMessage();
            //itemListMessage.ItemList = _ingredients;

            //_ingredientManagerNode.Publish(itemListMessage);
        }

        private void PrepareInsertCommand()
        {
            _insertCommand = new SQLiteCommand(_dbManager.Connection);
            _insertCommand.CommandText
                = @"INSERT INTO Ingredient(Name, Stock, UnitType, Price, Allergies, OrderInfoID) 
                    VALUES(@name, @stock, @unitType, @price, @allergies, @orderInfoID)";
            _insertCommand.Parameters.Add("@name", DbType.String);
            _insertCommand.Parameters.Add("@stock", DbType.Double);
            _insertCommand.Parameters.Add("@unitType", DbType.String);
            _insertCommand.Parameters.Add("@price", DbType.Double);
            _insertCommand.Parameters.Add("@allergies", DbType.Int32);
            _insertCommand.Parameters.Add("@orderInfoID", DbType.Int64);

        }
        private void PrepareGetLastInsertRowIDCommand()
        {
            _getLastInsertRowIDCommand = new SQLiteCommand(_dbManager.Connection);
            _getLastInsertRowIDCommand.CommandText = @"SELECT last_insert_rowid()";
        }
        private void PrepareSelectByIDCommand()
        {
            _selectByIDCommand = new SQLiteCommand(_dbManager.Connection);
            _selectByIDCommand.CommandText = @"SELECT * FROM Ingredient WHERE ID= @ID";
            _selectByIDCommand.Parameters.Add("@ID", DbType.Int64);
        }
        private void PrepareSelectByNameCommand()
        {
            _selectByNameCommand = new SQLiteCommand(_dbManager.Connection);
            _selectByNameCommand.CommandText = @"SELECT * FROM Ingredient WHERE Name= @Name";
            _selectByNameCommand.Parameters.Add("@Name", DbType.String);
        }
        private void PrepareSelectAllCommand()
        {
            _selectAllCommand = new SQLiteCommand(_dbManager.Connection);
            _selectAllCommand.CommandText = @"SELECT * FROM Ingredient";
        }

        private bool MakeIngredient(SQLiteDataReader reader, out Ingredient ingredient)
        {
            ingredient = new Ingredient();
            Debug.WriteLine("###########!!!");
            if (reader.Read())
            {
                Debug.WriteLine("###########2222");
                // ID, 이름
                ingredient.Id = reader.GetInt64((int)_colunms.ID);
                ingredient.Name = reader.GetString((int)_colunms.Name);

                // 알러지 정보 갖고오기
                if (reader[(int)_colunms.Allergies].GetType() != typeof(DBNull))
                {
                    BitMask<Ingredient.Allergy> bm = new BitMask<Ingredient.Allergy>(reader.GetInt32((int)_colunms.Allergies));
                    List<Ingredient.Allergy> allergyList;
                    bm.ParseValues(out allergyList);
                    ingredient.AllergyList = allergyList;
                }
                

                // 재고 정보 갖고 오기
                string unitType = reader.GetString((int)_colunms.UnitType);
                if (unitType.Equals("Gram"))
                {
                    Gram stock = new Gram(reader.GetFloat((int)_colunms.Stock), Gram.Units.None);
                    ingredient.Stock = stock;
                }
                else if (unitType.Equals("Litter"))
                {
                    // 리터 구현 필요
                }

                // 발주처 정보 갖고 오기
                if (reader[(int)_colunms.OrderInfoID].GetType() != typeof(DBNull))
                {
                    long id = reader.GetInt64((int)_colunms.OrderInfoID);
                    if (id != 0)
                        ingredient.OrderInfoID = reader.GetInt64((int)_colunms.OrderInfoID);
                }

                // 가격 정보 갖고 오기
                if (reader[(int)_colunms.Price].GetType() != typeof(DBNull))
                {
                    ingredient.Price = reader.GetFloat((int)_colunms.Price);
                }

                return true;
            }
            else
            {
                return false;
            }
        }

        //private void CacheData(long ID, Ingredient ingredient)
        //{
        //    _cache.InsertData(ID, ingredient);
        //    _nameCache.InsertData(ingredient.Name, ID);
        //}
        //private Ingredient RetrieveData(long ID)
        //{
        //    return _cache.GetData(ID);
        //}
        //private Ingredient RetrieveData(string Name)
        //{
        //    if (_nameCache.HasData(Name))
        //        return _cache.GetData(_nameCache.GetData(Name));
        //    else
        //        return null;
        //}

        public Ingredient SelectIngredientByID(long ingredientId)
        {

            //Ingredient ingredient = RetrieveData(ID);//new Ingredient();
            //if (ingredient != null)
            //    return ingredient;

            Ingredient ingredient = (Ingredient)_ingredients.Find(ingredient => ingredient.Id == ingredientId);
            if(ingredient != default)
            {
                return ingredient;
            }
            
            try
            {
                _dbManager.Connection.Open();

                _selectByIDCommand.Parameters["@ID"].Value = ingredientId;
                SQLiteDataReader reader = _selectByIDCommand.ExecuteReader();
                MakeIngredient(reader, out ingredient);
                
                _ingredients.Add(ingredient);
                //CacheData(ID, ingredient);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.StackTrace);
                throw e;
            }
            finally
            {
                _dbManager.Connection.Close();
            }

            return ingredient;
        }

        public Ingredient SelectIngredientByName(string ingredientName)
        {
            Ingredient ingredient = (Ingredient)_ingredients.Find(ingredient => ingredient.Name == ingredientName); ;
            //Ingredient ingredient = RetrieveData(Name);//new Ingredient();
            //if (ingredient != null)
            //    return ingredient;

            try
            {
                _dbManager.Connection.Open();

                _selectByNameCommand.Parameters["@Name"].Value = ingredientName;
                SQLiteDataReader reader = _selectByNameCommand.ExecuteReader();
                MakeIngredient(reader, out ingredient);

                //CacheData(ingredient.Id, ingredient);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.StackTrace);
                throw e;
            }
            finally
            {
                _dbManager.Connection.Close();
            }

            _ingredients.Add(ingredient);
            return ingredient;
        }

        public long InsertIngredient(Ingredient ingredient)
        {
            long last_insert_id = -1;

            try
            {
                BitMask<Ingredient.Allergy> mask = new BitMask<Ingredient.Allergy>();
                mask.SetValues(ingredient.AllergyList);

                _dbManager.Connection.Open();

                _insertCommand.Parameters["@name"].Value = ingredient.Name;
                _insertCommand.Parameters["@stock"].Value = ingredient.Stock.Quantity;
                _insertCommand.Parameters["@unitType"].Value = ingredient.Stock.GetType().Name;
                _insertCommand.Parameters["@price"].Value = ingredient.Price;
                _insertCommand.Parameters["@allergies"].Value = mask.Bit;
                _insertCommand.Parameters["@orderInfoID"].Value = ingredient.OrderInfoID;
                _insertCommand.ExecuteNonQuery();

                // 방금전에 입력한 row id 얻기
                last_insert_id = (long)_getLastInsertRowIDCommand.ExecuteScalar();
                Debug.WriteLine("id : " + last_insert_id);

            }
            catch (Exception e)
            {
                Debug.WriteLine(e.StackTrace);
                throw e;
            }
            finally
            {
                _dbManager.Connection.Close();
            }

            ingredient.Id = last_insert_id;
            //CacheData(last_insert_id, ingredient);
            _ingredients.Add(ingredient);
            return last_insert_id;
        }

        private int SelectAll()
        {
            _ingredients.Clear();

            try
            {
                _dbManager.Connection.Open();

                SQLiteDataReader reader = _selectAllCommand.ExecuteReader();

                Ingredient ingredient;
                Debug.WriteLine("AAAAAAAAAAAa");
                while(MakeIngredient(reader, out ingredient))
                {
                    Debug.WriteLine("BBBBBBBBBBBB");
                    _ingredients.Add(ingredient);
                }
                reader.Close();

            }
            catch (Exception e)
            {
                Debug.WriteLine(e.StackTrace);
                throw e;
            }
            finally
            {
                _dbManager.Connection.Close();
            }

            return _ingredients.Count();
        }

    }
}
