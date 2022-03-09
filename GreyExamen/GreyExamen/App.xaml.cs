using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using GreyExamen.Controllers;
using System.IO;
using GreyExamen.Views;

namespace GreyExamen
{
    public partial class App : Application
    {
        static DataBase db;

        public static DataBase InitDB
        {
            get
            {
                if (db == null)
                {
                    db = new DataBase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "EXAM.exam"));
                }

                return db;
            }
        }

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new PageContactos());
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
