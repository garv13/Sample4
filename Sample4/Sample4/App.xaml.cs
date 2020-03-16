using Sample4.Utilities;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Sample4
{
    public partial class App : Application
    {
        static DatabaseHelper database;
        public static DatabaseHelper Database
        {
            get
            {
                if (database == null)
                {
                    database = new DatabaseHelper();
                }
                return database;
            }
        }
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new Views.LogIn());
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
