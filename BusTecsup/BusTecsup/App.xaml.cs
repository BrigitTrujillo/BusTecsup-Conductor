﻿using BusTecsup.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BusTecsup
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new Views.login();
            BindingContext = new ViewModels.LoginPageViewModel(); 
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
