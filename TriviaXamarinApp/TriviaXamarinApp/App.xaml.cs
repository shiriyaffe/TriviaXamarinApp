using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TriviaXamarinApp.Services;
using TriviaXamarinApp.Models;
using System.Threading.Tasks;
using TriviaXamarinApp.ViewModels;
using TriviaXamarinApp.Views;



namespace TriviaXamarinApp
{
    public partial class App : Application
    {
        public User User { get; set; }
        public App()
        {
            InitializeComponent();
            User = new User();
            User = null;
            Page p = new HomePage();
            p.Title = "Trivia";
            MainPage = new NavigationPage(p);
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
