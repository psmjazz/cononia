using cononia.src.model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace cononia.page.IngredientPages
{
    /// <summary>
    /// Interaction logic for IngredientEdit.xaml
    /// </summary>
    public partial class IngredientEditPage : Page
    {
        private string IngredientName{get; set;}
        private float IngredientStock { get; set; }
        private float IngredientPrice { get; set; }
        private string IngredientUnit { get; set; }
        private IngredientManager _ingredientManager;

        private static Dictionary<string, Ingredient.Allergy> allergyName = new Dictionary<string, Ingredient.Allergy>()
        {
            {"난류", Ingredient.Allergy.Egg}, {"우유", Ingredient.Allergy.Milk},
            {"메밀", Ingredient.Allergy.BuckWheat}, {"땅콩", Ingredient.Allergy.Peanut},
            {"콩", Ingredient.Allergy.Soybean}, {"밀", Ingredient.Allergy.Wheat},
            {"고등어", Ingredient.Allergy.Mackerel}, {"게", Ingredient.Allergy.Crab},
            {"새우", Ingredient.Allergy.Shirimp}, {"돼지고기", Ingredient.Allergy.Fork},
            {"복숭아", Ingredient.Allergy.Peach}, {"토마토", Ingredient.Allergy.Tomato},
            {"아황산류", Ingredient.Allergy.Sulfite}, {"호두", Ingredient.Allergy.Wallnut},
            {"닭고기", Ingredient.Allergy.Chicken}, {"소고기", Ingredient.Allergy.Beef},
            {"오징어", Ingredient.Allergy.Squid}, {"조개류", Ingredient.Allergy.Clam}
        };
        private List<Ingredient.Allergy> _allergyList = new List<Ingredient.Allergy>();

        private string dataNotFloat = "숫자를 입력해 주세요";

        public IngredientEditPage()
        {
            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {
            IngredientName = "";
            IngredientStock = 0f;
            IngredientPrice = 0f;
            IngredientUnit = "kg";
            _allergyList.Clear();

            _ingredientManager = IngredientManager.Instance;
            System.Diagnostics.Debug.WriteLine("_ingredientManager initialized? : " + _ingredientManager.Initialized);
            if (!_ingredientManager.Initialized)
                _ingredientManager.Initialize();

            OrderInfoName.ItemsSource = new List<string>() { "코스트코", "청과", "홈플러스" };
        }

        private void Ok_Button_Click(object sender, RoutedEventArgs e)
        {
            IngredientName = this.Name.Text;
            float stock;
            float price;

            if(float.TryParse(this.Stock.Text, out stock))
            {
                IngredientStock = stock;
            }
            else
            {
                MessageBox.Show(dataNotFloat);
            }
            if(float.TryParse(this.Stock.Text, out price))
            {
                IngredientPrice = price;
            }
            else
            {
                MessageBox.Show(dataNotFloat);
            }

            AUnit unit;
            if (IngredientUnit.Equals("g"))
            {
                unit = new Gram(stock, Gram.Units.None);
            }
            else if(IngredientUnit.Equals("kg"))
            {
                unit = new Gram(stock, Gram.Units.Killo);
            }
            else if(IngredientUnit.EndsWith("L"))
            {
                // 구현 필요 임시 코드
                unit = new Gram(stock, Gram.Units.Milli);
            }
            else
            {
                unit = new Gram(stock, Gram.Units.Milli);
            }
            Ingredient ingredient = new Ingredient(IngredientName, unit);
            ingredient.Price = IngredientPrice;
            ingredient.AllergyList = _allergyList;

            _ingredientManager.InsertIngredient(ingredient);
        }

        private void Unit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem item = ((sender as ComboBox).SelectedItem as ComboBoxItem);
            IngredientUnit = item.Content.ToString();
            
        }

        private void Allergy_CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            Ingredient.Allergy allergyType
                = allergyName[(sender as CheckBox).Content.ToString()];
            if (!_allergyList.Contains(allergyType))
                _allergyList.Add(allergyType);
        }

        private void Allergy_CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            Ingredient.Allergy allergyType
                = allergyName[(sender as CheckBox).Content.ToString()];
            if (_allergyList.Contains(allergyType))
            {
                _allergyList.Remove(allergyType);
            }
        }


        
        private void OrderInfoName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Debug.WriteLine("123");
        }        
    }
}
