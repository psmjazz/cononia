using cononia.src.model;
using cononia.src.rx;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
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
    /// Interaction logic for IngredientListPage.xaml
    /// </summary>
    public partial class IngredientListPage : Page
    {

        List<Ingredient> _ingredientList;

        public IngredientListPage()
        {
            Debug.WriteLine("ingredientList Page ctor");
            InitializeComponent();

            RxMessage getAllMessage = new RxMessage();
            getAllMessage.Callback = OnIngredientInfoUpdated;
            RxCore.Instance.SendMessage(RxIngredientCommand.GetAllIngredients, getAllMessage);
        }

        ~IngredientListPage()
        {
            Debug.WriteLine("ingredientList Page dtor");
        }

        public void OnRxEvent(RxMessage message)
        {

        }

        public void OnIngredientInfoUpdated(RxMessage message)
        {
            Debug.WriteLine("okkkk!");
            ItemList.ItemsSource = ((List<IBaseItem>) message.Content).OfType<Ingredient>();

        }

        private void ListViewItem_MouseDoubleClick(object sender, EventArgs e)
        {
            ListView listView = (ListView)sender;
            Ingredient x = (Ingredient)listView.SelectedItem;
        } 


        private void ItemList_Refresh_Click(object sender, RoutedEventArgs e)
        {
            RxCore.Instance.SendMessage(RxIngredientCommand.GetAllIngredients, new RxMessage());
        }

    }
}
