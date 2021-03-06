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
    public partial class QuestionsView : ContentPage
    {
        public QuestionsView()
        {
            //NavigationPage.SetHasBackButton(this, false);
            InitializeComponent();
            this.BindingContext = new OuestionsViewModel();
        }


        private void next_Clicked(object sender, EventArgs e)
        {
            App.Current.MainPage = new NavigationPage(new QuestionsView());
        }
    }
}