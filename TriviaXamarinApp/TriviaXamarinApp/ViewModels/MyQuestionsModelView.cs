using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using TriviaXamarinApp.Services;
using TriviaXamarinApp.Models;
using TriviaXamarinApp.Views;
using System.Collections.Generic;


namespace TriviaXamarinApp.ViewModels
{
    class MyQuestionsModelView:INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ObservableCollection<AmericanQuestion> Questions { get; }

        public MyQuestionsModelView()
        {
            Questions = new ObservableCollection<AmericanQuestion>();
            CreateList();
        }

        private void CreateList()
        {
            App a = (App)App.Current;
            if(a.User != null)
            {
                List<AmericanQuestion> lst = a.User.Questions;
                foreach(AmericanQuestion aq in lst)
                {
                    Questions.Add(aq);
                }
            }
        }

        public ICommand DeleteCommand => new Command<AmericanQuestion>(RemoveQues);
        async void RemoveQues(AmericanQuestion q)
        {
            TriviaWebAPIProxy proxy = TriviaWebAPIProxy.CreateProxy();
            bool delete = await proxy.DeleteQuestion(q);
            if(delete == true)
            {
                App a = (App)App.Current;
                a.User.Questions.Remove(q);
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

        private string size;
        public string Size
        {
            get
            {
                return this.size;
            }
            set
            {
                this.size = value;
                OnPropertyChanged(nameof(Size));
            }
        }

        private string text;
        public string HeadText
        {
            get
            {
                return this.text;
            }
            set
            {
                this.text = value;
                OnPropertyChanged(nameof(HeadText));
            }
        }

        //public ICommand BuildCommand => new Command(OpenPage);

        public void OpenPage()
        {
            App a = (App)App.Current;
            if (a.User == null)
            {
                HeadText = "Log in requierd!";
                Color = "Red";
                Size = "Medium";
            }
            else
            {
                HeadText = "My Questions:";
                Color = "MediumPurple";
                Size = "Large";
            }
        }

        public ICommand PlayCommand => new Command(PlayPage);
        void PlayPage()
        {
            App.Current.MainPage = new NavigationPage(new QuestionsView());
        }
    }
}
