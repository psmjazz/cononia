using cononia.page.ItemsPage;
using cononia.page.IngredientPages;
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
using cononia.src.ui;

namespace cononia.page
{
    /// <summary>
    /// Interaction logic for main.xaml
    /// </summary>
    public partial class WelcomePage : Page
    {
        public WelcomePage()
        {
            InitializeComponent();

            OrderInfoManager OIManager = OrderInfoManager.Instance;
            if (!OIManager.Initialized)
                OIManager.Initialize();
            
            OrderInfo oi = new OrderInfo("테스트", "010-1111-2222");
            OrderInfo oi2 = new OrderInfo("테스트2", "01011123334");
            Debug.WriteLine(oi.ToString());
            Debug.WriteLine(oi2.ToString());
        }

        public void IngredientButtonClick(object sender, RoutedEventArgs args)
        {
            //IngredientEditPage ingredientEdit = new IngredientEditPage();
            IngredientListPage ingredientListPage = (IngredientListPage) PageLoader.Instance.GetPage<IngredientListPage>();
            //IngredientListPage ingredientListPage = new IngredientListPage();
            this.NavigationService.Navigate(ingredientListPage);
        }

        public void FoodButtonClick(object sender, RoutedEventArgs args)
        {
            OrderInfoEditPage orderInfoEdit = new OrderInfoEditPage();
            this.NavigationService.Navigate(orderInfoEdit);
        }
    }
}
