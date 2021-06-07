using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using TriviaXamarinApp.Services;
using TriviaXamarinApp.Models;


namespace TriviaXamarinApp.ViewModels
{
    class LogInViewModel:INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string email;
        public string Email
        {
            get
            {
                return this.email;
            }
            set
            {
                this.email = value;
                OnPropertyChanged(nameof(Email));
            }
        }
        private string pass;
        public string Pass
        {
            get
            {
                return this.pass;
            }
            set
            {
                this.pass = value;
                OnPropertyChanged(nameof(Pass));
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
        public LogInViewModel()
        {
            Email = "";
            Pass = "";
        }

        public ICommand LogInCommand => new Command(OnLogIn);
        async void OnLogIn()
        {
            TriviaWebAPIProxy proxy = TriviaWebAPIProxy.CreateProxy();
            User u = await proxy.LoginAsync(email, pass);
            if (u != null)
            {
                App a = (App)App.Current;
                a.User = u;
                Message = "Log in succeeded!";
                Color = "Green";
            }
            else
            {
                Message = "Log in failed";
                Color = "Red";
            }
        }
    }
}
