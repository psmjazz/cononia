using cononia.src.common;
using cononia.src.rx;
using cononia.src.rx.messages;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;

namespace cononia.src.controller
{
    class IngredientController : Singleton<IngredientController>
    {
        private RxNode _ingredientControllerNode;

        private IDisposable _rxEventDisposable;
        private IDisposable _ingredientListDisposable;

        public override void Initialize()
        {
            if (Initialized)
                return;
            Initialized = true;

            _ingredientControllerNode = RxCore.Instance.CreateNode(ERxNodeName.RxIngredientController);
        }

        public void RegisterEvent()
        {
            _rxEventDisposable = RxCore.Instance.RxEvent.Subscribe(OnRxEventReceived);

        }

        public void UnregisterEvent()
        {
            _rxEventDisposable.Dispose();
            _ingredientListDisposable.Dispose();
        }

        private void OnRxEventReceived(MessageBase message)
        {
            RxEventMessage rxEventMessage = (RxEventMessage)message;
            Debug.WriteLine("OnRxEventReceived base : "+rxEventMessage.Message + "rx message : "+rxEventMessage.PublishingNode.ToString() );

            switch(rxEventMessage.PublishingNode)
            {
                case ERxNodeName.RxIngredientList:
                    Debug.WriteLine("RxIngredientList");
                    _ingredientListDisposable = RxCore.Instance.GetNode(ERxNodeName.RxIngredientList).Subscribe(OnIngredientListUpdateRequested);
                    break;
                default:
                    break;

            }

        }

        private void OnIngredientListUpdateRequested(MessageBase message)
        {
            Debug.WriteLine("Ingredient controller");
            _ingredientControllerNode.Publish(message);
        }

    }
}
