using cononia.page.ItemsPage;
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
            if (!OIManager.IsInitialized())
                OIManager.Initialize();
            
            OrderInfo oi = new OrderInfo("테스트", "010-9065-8986");
            OrderInfo oi2 = new OrderInfo("테스트2", "01090658986");
            Debug.WriteLine(oi.ToString());
            Debug.WriteLine(oi2.ToString());
        }

        public void IngredientButtonClick(object sender, RoutedEventArgs args)
        {
            IngredientEditPage ingredientEdit = new IngredientEditPage();
            this.NavigationService.Navigate(ingredientEdit);
        }

        public void FoodButtonClick(object sender, RoutedEventArgs args)
        {
            OrderInfoEditPage orderInfoEdit = new OrderInfoEditPage();
            this.NavigationService.Navigate(orderInfoEdit);
        }
    }
}
