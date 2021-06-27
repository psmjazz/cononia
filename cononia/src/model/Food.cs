using cononia.src.common;
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

    class FoodManager : Singleton<FoodManager>
    {
        public void GetAll(out List<Food> foods)
        {
            foods = new List<Food>();
        }

        public Food GetFoodById(long id)
        {
            return new Food();
        }

        public Food GetFoodByName(string name)
        {
            return new Food();
        }

        public void RegisterFood(Food food)
        {

        }
    }
}
