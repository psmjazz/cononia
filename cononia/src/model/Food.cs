using cononia.src.common;
using cononia.src.rx;
using System;
using System.Collections.Generic;
using System.Text;

namespace cononia.src.model
{
    class Food : IBaseItem
    {
        private Dictionary<long, AUnit> _ingredients;

        public long Id { get; set; }
        public string Name { get; set; }
        public Dictionary<long, AUnit> Ingredients
        {
            get
            {
                if(_ingredients == null)
                {
                    _ingredients = new Dictionary<long, AUnit>();
                }
                return _ingredients;
            }
        }

        public Food() { }
        public Food(string name, Dictionary<long, AUnit> ingredients = null)
        {
            this.Name = name;
            if(ingredients != null)
            {
                _ingredients = ingredients;
            }
        }
    }

    public enum RxFoodCommand
    {
        GetAllFoods,
        GetFoodById,
        GetFoodByName,
        RegisterFood
    }
    class FoodManager : Singleton<FoodManager>
    {
        public override void Initialize()
        {
            base.Initialize();

            RxCore.Instance.RegisterListener(RxFoodCommand.GetAllFoods, OnGetAll);
            RxCore.Instance.RegisterListener(RxFoodCommand.GetFoodById, OnGetFoodById);
        }
        private void OnGetAll(RxMessage message)
        {
            
        }

        private void OnGetFoodById(RxMessage message)
        {
            
        }

        private void OnGetFoodByName(RxMessage message)
        {
            
        }

        private void OnRegisterFood(RxMessage message)
        {

        }
    }
}
