using cononia.src.model;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
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

namespace cononia.page.ItemsPage
{
    /// <summary>
    /// Interaction logic for OrderInfoEditPage.xaml
    /// </summary>
    public partial class OrderInfoEditPage : Page
    {
        private OrderInfoManager _orderInfoManager;

        public OrderInfoEditPage()
        {
            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {
            _orderInfoManager = OrderInfoManager.Instance;
            if (!_orderInfoManager.IsInitialized())
                _orderInfoManager.Initialize();
        }

        private void Ok_Button_Click(object sender, RoutedEventArgs e)
        {
            string name = this.Name.Text;
            string phone = this.Phone.Text;

            if(_orderInfoManager.SelectOrderInfoByName(name) == null)
            {
                _orderInfoManager.InsertOrderInfo(new OrderInfo(name, phone));
            }
            else
            {
                // TODO : 알림 메세지
            }
        }
    }
}
