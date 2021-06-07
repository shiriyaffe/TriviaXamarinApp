using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using TriviaXamarinApp.Models;
using TriviaXamarinApp.Views;
using TriviaXamarinApp.Services;

namespace TriviaXamarinApp.ViewModels
{
    class ShowQuestionViewModel:INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string Text { get; set; }
        public string CorrectA { get; set; }
        public string[] OtherA { get; set; }

        private string changeQues;
        public string ChangeQues {
            get
            {
                return this.changeQues;
            }
            set
            {
                this.changeQues = value;
                OnPropertyChanged(nameof(ChangeQues));
            }
        }

        private string changeCorrect;
        public string ChangeCorrectA
        {
            get
            {
                return this.changeCorrect;
            }
            set
            {
                this.changeCorrect = value;
                OnPropertyChanged(nameof(ChangeCorrectA));
            }
        }

        private ObservableCollection<string> changeOther;
        public ObservableCollection<string> ChangeOther
        {
            get
            {
                return this.changeOther;
            }
            set
            {
                this.changeOther = value;
                OnPropertyChanged(nameof(ChangeOther));
            }
        }


        public ICommand UpdateCommand => new Command(UpdateQues);
        async void UpdateQues()
        {
            App app = (App)App.Current;
            List<AmericanQuestion> arr = app.User.Questions;

            AmericanQuestion change = new AmericanQuestion
            {
                QText = "",
                CorrectAnswer = "",
                OtherAnswers = new string[3],
                CreatorNickName = app.User.NickName
            };

            if (ChangeQues != "")
                change.QText = ChangeQues;
            else
                change.QText = Text;

            if (ChangeCorrectA != "")
                change.CorrectAnswer = ChangeCorrectA;
            else
                change.CorrectAnswer = CorrectA;

            if (ChangeOther[0] != "")
                change.OtherAnswers[0] = ChangeOther[0];
            else
                change.OtherAnswers[0] = OtherA[0];

            if (ChangeOther[1] != "")
                change.OtherAnswers[1] = ChangeOther[1];
            else
                change.OtherAnswers[1] = OtherA[1];

            if (ChangeOther[2] != "")
                change.OtherAnswers[2] = ChangeOther[2];
            else
                change.OtherAnswers[2] = OtherA[2];

            TriviaWebAPIProxy proxy = TriviaWebAPIProxy.CreateProxy();
            bool delete = false;
            AmericanQuestion q = null;

            foreach (AmericanQuestion a in arr)
            {
                if (a.QText == Text)
                {
                    q = a;
                }
            }

            if(q != null)
            {
                delete = await proxy.DeleteQuestion(q);
                app.User.Questions.Remove(q);
                bool add = await proxy.PostNewQuestion(change);
                app.User.Questions.Add(change);
            }
            
            await App.Current.MainPage.Navigation.PushAsync(new MyQuestions());
        }
    }
}
