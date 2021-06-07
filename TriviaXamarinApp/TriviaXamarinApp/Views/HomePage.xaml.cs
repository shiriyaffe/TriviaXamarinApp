using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TriviaXamarinApp.ViewModels;

namespace TriviaXamarinApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
            this.BindingContext = new HomePageViewModel();

        }

        private void login_Clicked(object sender, EventArgs e)
        {
            Page p = new LogInView();
            p.Title = "Log In";
            App.Current.MainPage.Navigation.PushAsync(p);
        }

        private void signup_Clicked(object sender, EventArgs e)
        {
            Page p = new RegisterView();
            p.Title = "Register";
            App.Current.MainPage.Navigation.PushAsync(p);
        }

        private void ques_Clicked(object sender, EventArgs e)
        {
            Page p = new QuestionsView();
            p.Title = "Questions";
            App.Current.MainPage.Navigation.PushAsync(p);
        }

        private void me_Clicked(object sender, EventArgs e)
        {
            Page p = new MyQuestions();
            p.Title = "My Questions";
            App.Current.MainPage.Navigation.PushAsync(p);
        }
    }
}