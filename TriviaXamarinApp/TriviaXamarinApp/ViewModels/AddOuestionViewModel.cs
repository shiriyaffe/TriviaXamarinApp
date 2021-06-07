using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using TriviaXamarinApp.Models;
using TriviaXamarinApp.Services;


namespace TriviaXamarinApp.ViewModels
{
    class AddOuestionViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string textQ;
        public string TextQ
        {
            get
            {
                return this.textQ;
            }
            set
            {
                this.textQ = value;
                OnPropertyChanged(nameof(TextQ));
            }
        }

        private string correct;
        public string Correct
        {
            get
            {
                return this.correct;
            }
            set
            {
                this.correct = value;
                OnPropertyChanged(nameof(Correct));
            }
        }

        private ObservableCollection<string> answers;
        public ObservableCollection<string> Answers
        {
            get
            {
                return this.answers;
            }
            set
            {
                this.answers = value;
                OnPropertyChanged(nameof(Answers));
            }
        }


        private string color;
        public string Color
        {
            get
            {
                return this.color;
            }
            set
            {
                this.color = value;
                OnPropertyChanged(nameof(Color));
            }
        }

        private string message;
        public string Message
        {
            get
            {
                return this.message;
            }
            set
            {
                this.message = value;
                OnPropertyChanged(nameof(Message));
            }
        }

        public AddOuestionViewModel()
        {
            Answers = new ObservableCollection<string>();
            Answers.Add("");
            Answers.Add("");
            Answers.Add("");
            Correct = "";
            TextQ = "";
            Message = "";
            Color = "";
        }

        public ICommand AddCommand => new Command(AddQuestion);
        async void AddQuestion()
        {
            TriviaWebAPIProxy proxy = TriviaWebAPIProxy.CreateProxy();
            App a = (App)App.Current;
            
            AmericanQuestion q = new AmericanQuestion
            {
                QText = TextQ,
                CorrectAnswer = Correct,
                OtherAnswers = new string[3],
                CreatorNickName = a.User.NickName
            };
            for (int i = 0; i < q.OtherAnswers.Length; i++)
            {
                q.OtherAnswers[i] = Answers[i];
            }
            bool postQ = await proxy.PostNewQuestion(q);

            if(postQ == true)
            {
                a.User.Questions.Add(q);
                Color = "Green";
                Message = "Question added successfuly!";
            }
            else
            {
                Color = "Red";
                Message = "Somthing went wrong";
            }
        }
    }
}
