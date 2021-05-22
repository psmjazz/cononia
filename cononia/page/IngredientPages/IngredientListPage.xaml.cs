using cononia.src.model;
using cononia.src.rx;
using cononia.src.rx.messages;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
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
        private RxNode _ingredientListPageNode;
        private IDisposable _rxEventDisposable;
        private IDisposable _ingredientManagerEventDisposable;

        List<Ingredient> _ingredientList;

        RxCommandMessage _commandMessage;

        public IngredientListPage()
        {
            Debug.WriteLine("ingredientList Page ctor");
            InitializeComponent();

            _ingredientListPageNode = RxCore.Instance.CreateNode(ERxNodeName.RxIngredientList);
            RegisterEvent();

            _commandMessage = new RxCommandMessage();
            _commandMessage.Command = ECommand.CommandLoadIngredients;
            _ingredientListPageNode.Publish(_commandMessage);
        }

        ~IngredientListPage()
        {
            Debug.WriteLine("ingredientList Page dtor");
        }

        public void RegisterEvent()
        {
            _rxEventDisposable = RxCore.Instance.RxEvent.Subscribe(OnRxEvent);
            _ingredientManagerEventDisposable = RxCore.Instance.GetNode(ERxNodeName.RxIngredientManager).Subscribe(OnIngredeientManagerEvent);
        }

        public void UnregisterEvent()
        {
            _rxEventDisposable.Dispose();
            _ingredientManagerEventDisposable.Dispose();

            RxCore.Instance.DeleteNode(ERxNodeName.RxIngredientList);
        }

        public void OnRxEvent(MessageBase message)
        {

        }

        public void OnIngredeientManagerEvent(MessageBase message)
        {
            Debug.WriteLine("okkkk!");
            RxItemListMessage itemListMessage = (RxItemListMessage)message;
            ItemList.ItemsSource = itemListMessage.ItemList.OfType<Ingredient>();
        }
    }
}
