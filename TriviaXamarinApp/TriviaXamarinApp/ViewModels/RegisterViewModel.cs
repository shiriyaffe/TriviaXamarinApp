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
    class RegisterViewModel:INotifyPropertyChanged
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

        private string nName;
        public string NickName
        {
            get
            {
                return this.nName;
            }
            set
            {
                this.nName = value;
                OnPropertyChanged(nameof(NickName));
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
        public RegisterViewModel()
        {
            Email = "";
            Pass = "";
            NickName = "";
        }

        public ICommand RegisterCommand => new Command(SignUp);
        async void SignUp()
        {
            TriviaWebAPIProxy proxy = TriviaWebAPIProxy.CreateProxy();
            User u = new User
            {
                Email = email,
                NickName = nName,
                Password = pass
            };
            bool isSignUp = await proxy.RegisterUser(u);
            if (isSignUp)
            {
                App a = (App)App.Current;
                a.User = u;
                Message = "Sign up succeeded!";
                Color = "Green";
            }
            else
            {
                Message = "Sign up failed";
                Color = "Red";
            }
        }
    }
}
