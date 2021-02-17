using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cononia.src.model
{
    class Cooking
    {
        public string Name { get; set; }
        private List<KeyValuePair<Ingredient, AUnit>> _ingredients = null;
        private List<string> _clients = null;

        public Cooking(string name)
        {
            Name = name;
            // _ingredients와 _clients 도 
            if(_ingredients == null)
            {
                _ingredients = new List<KeyValuePair<Ingredient, AUnit>>();
            }
            if (_clients == null)
            {
                _clients = new List<string>();
            }
            
        }
    }
}
