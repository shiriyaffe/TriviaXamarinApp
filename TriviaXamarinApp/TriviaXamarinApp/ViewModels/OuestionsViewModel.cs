using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using TriviaXamarinApp.Models;
using TriviaXamarinApp.Services;
using TriviaXamarinApp.Views;



namespace TriviaXamarinApp.ViewModels
{
    class ChangeColor : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
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

        private string aText;

        public string AText
        {
            get
            {
                return this.aText;
            }
            set
            {
                this.aText = value;
                OnPropertyChanged(nameof(AText));
            }
        }
    }
    class OuestionsViewModel:INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ObservableCollection<ChangeColor> Answers { get; set; }


        private string qText;
        public string QText 
        {
            get
            {
                return this.qText;
            }
            set
            {
                this.qText = value;
                OnPropertyChanged(nameof(QText));
            }
        }

        private bool click;
        public bool Click
        {
            get
            {
                return this.click;
            }
            set
            {
                this.click = value;
                OnPropertyChanged(nameof(Click));
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

        private static int counter = 0;

        public AmericanQuestion Ques { get; set; }
        private bool answerd;
        public OuestionsViewModel()
        {
            Answers = new ObservableCollection<ChangeColor>();
            Ques = new AmericanQuestion();
            GetQues();
            answerd = false;
            Click = false;
            Message = "";
        }

        private async void GetQues()
        {
            TriviaWebAPIProxy proxy = TriviaWebAPIProxy.CreateProxy();
            Ques = await proxy.GetRandomQuestion();
            if (Ques != null)
                QText = Ques.QText;
            Show();
        }

        public void Show()
        {
            Random r = new Random();
            int num = r.Next(0, 4);
            string[] arr = new string[4];
            arr[num] = Ques.CorrectAnswer;
            int j = 0;

            for (int i = 0; i < arr.Length; i++)
            {
                if(arr[i] == null)
                {
                    arr[i] = Ques.OtherAnswers[j];
                    j++;
                }
            }
            foreach(string s in arr)
            {
                ChangeColor c = new ChangeColor
                {
                    Color = "Black",
                    AText = s
                };
                Answers.Add(c);
            }
        }

        public ICommand IsCorrectCommand => new Command<ChangeColor>(ChangeColor);

        void ChangeColor(ChangeColor chosen)
        {
            if (answerd == false)
            {
                if (chosen.AText == Ques.CorrectAnswer)
                {
                    chosen.Color = "Green";
                    counter++;
                }
                else
                    chosen.Color = "Red";

                answerd = true;
            }

            if (counter!= 0 && counter % 3 == 0)
                Click = true;
        }

        public ICommand ToAddPageCommand => new Command(AddPage);
        void AddPage()
        {
            App a = (App)App.Current;
            if(a.User != null)
            {
                Page p = new AddOuestionView();
                p.Title = "Add Question";
                App.Current.MainPage.Navigation.PushAsync(p);
            }
            else
            {
                Message = "Log in is requierd!";
            }
        }

        public ICommand ToUserPage => new Command(UserPage);
        void UserPage()
        {
            App a = (App)App.Current;
            if (a.User != null)
            {
                Page p = new MyQuestions();
                App.Current.MainPage.Navigation.PushAsync(p);
            }
            else
            {
                Message = "Log in is requierd!";
            }
        }

        public ICommand ToLogIn => new Command(LogInPage);
        void LogInPage()
        {
            App.Current.MainPage.Navigation.PushAsync(new LogInView());
        }

        public ICommand ToSignUp => new Command(SignUpPage);
        void SignUpPage()
        {
            App.Current.MainPage.Navigation.PushAsync(new RegisterView());
        }
    }
}
