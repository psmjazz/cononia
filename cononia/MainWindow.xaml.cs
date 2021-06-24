using cononia.page.IngredientPages;
using cononia.src.ui;
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

namespace cononia
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        private Button _currentSelectedButton = null;

        public MainWindow()
        {
            InitializeComponent();

            IngredientButton.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));

            
        }

        private void Deactivatebutton(Button selected)
        {
            selected.IsEnabled = false;
            if (_currentSelectedButton != null)
            {
                _currentSelectedButton.IsEnabled = true;
            }
            _currentSelectedButton = selected;
        }

        private void Ingredient_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            Deactivatebutton(button);
            currentFrame.Content = PageLoader.Instance.GetPage<IngredientListPage>();
        }

        private void Recipe_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            Deactivatebutton(button);
            currentFrame.Content = PageLoader.Instance.GetPage<IngredientEditPage>();
        }
    }
}
