using cononia.src.model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Design;
using System.Diagnostics;
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
    /// 
    class OrderInfoList : ObservableCollection<OrderInfo>
    {
        public OrderInfoList(List<OrderInfo> orderInfos) : base(orderInfos)
        {
            
        }

        public OrderInfoList()
        {
            Add(new OrderInfo("a", "010-1111-2334"));
        }
    }

    public partial class OrderInfoEditPage : Page
    {
        private OrderInfoManager _orderInfoManager;

        private const int _maxShow = 10;
        private int _currentPage = 0;
        private static List<OrderInfo> _orderInfoList;
        private List<OrderInfo> _orderInfos;

        //private static OrderInfoList OIList 
        //{ 
        //    get 
        //    {
        //        if (_orderInfoList == null)
        //        {
        //            Debug.WriteLine("??!!");
        //            OrderInfoList lst= new OrderInfoList();
        //            lst.Add(new OrderInfo("fff", "010-2233-3323"));
        //            return lst;
        //        }
        //        Debug.WriteLine("!!@@!!");
        //        return _orderInfoList; 
        //    } 
        //}

        public OrderInfoEditPage()
        {
            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {
            _orderInfoManager = OrderInfoManager.Instance;
            if (!_orderInfoManager.Initialized)
                _orderInfoManager.Initialize();

            _orderInfoManager.GetAll(out _orderInfos);

            Debug.WriteLine("?");
            int startIdx = _currentPage * _maxShow;
            Debug.WriteLine("startIdx : " + startIdx);
            Debug.WriteLine("_orderInfos.Count : " + _orderInfos.Count);
            if (startIdx < _orderInfos.Count)
            {
                Debug.WriteLine("startIdx : " + startIdx);
                Debug.WriteLine("_orderInfos.Count : "+ _orderInfos.Count);
                if (startIdx + _maxShow > _orderInfos.Count)
                {
                    Debug.WriteLine("AAAAAA");
                    _orderInfoList = _orderInfos.GetRange(startIdx, _orderInfos.Count - startIdx);
                }
                else
                {
                    Debug.WriteLine("BBBBB");
                    _orderInfoList = _orderInfos.GetRange(startIdx, _maxShow);
                }
                TestList.ItemsSource = _orderInfoList;
            }
            
            
        }

        //private void 

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
