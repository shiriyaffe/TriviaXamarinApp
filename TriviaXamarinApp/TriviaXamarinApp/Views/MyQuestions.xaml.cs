using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TriviaXamarinApp.ViewModels;
using TriviaXamarinApp.Models;
using TriviaXamarinApp.Views;

using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using TriviaXamarinApp.Services;




namespace TriviaXamarinApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyQuestions : ContentPage
    {
        public MyQuestions()
        {
            //NavigationPage.SetHasBackButton(this, false);
            InitializeComponent();
            this.BindingContext = new MyQuestionsModelView();
            ((MyQuestionsModelView)this.BindingContext).OpenPage();
        }

        private void show_Clicked(object sender, EventArgs e)
        {
            if (sender is Button)
            {
                Button b = (Button)sender;
                AmericanQuestion question = (AmericanQuestion)b.BindingContext;

                ShowQuestionViewModel showContext = new ShowQuestionViewModel
                {
                    Text = question.QText,
                    CorrectA = question.CorrectAnswer,
                    OtherA = new string[3],
                    ChangeCorrectA = question.CorrectAnswer,
                    ChangeOther = new ObservableCollection<string>(),
                    ChangeQues = question.QText
                };
                for (int i = 0; i < question.OtherAnswers.Length; i++)
                {
                    showContext.ChangeOther.Add(question.OtherAnswers[i]);
                    showContext.OtherA[i] = question.OtherAnswers[i];
                }
                
                Page showPage = new ShowQuestionView();
                showPage.BindingContext = showContext;
                App.Current.MainPage.Navigation.PushAsync(showPage);
            }
        }
    }
}