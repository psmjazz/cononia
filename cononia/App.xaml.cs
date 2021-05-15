using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

using System.Diagnostics;
using cononia.src.model;
using cononia.src.controller;
using cononia.src.rx;
using cononia.src.rx.messages;

namespace cononia
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        protected override void OnStartup(StartupEventArgs e)
        {
            
            IngredientManager.Instance.Initialize();
            IngredientController.Instance.Initialize();

            IngredientController.Instance.RegisterEvent();
            IngredientManager.Instance.RegisterEvent();
            

        }
    }
}
