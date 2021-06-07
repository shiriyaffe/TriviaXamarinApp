﻿using System;
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
    public partial class AddOuestionView : ContentPage
    {
        public AddOuestionView()
        {
            this.BindingContext = new AddOuestionViewModel();
            InitializeComponent();
            
        }
    }
}