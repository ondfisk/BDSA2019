using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using BDSA2019.Lecture10.MobileApp.Models;

namespace BDSA2019.Lecture10.MobileApp.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class NewSuperheroPage : ContentPage
    {
        public SuperheroCreateDTO Superhero { get; set; }

        public NewSuperheroPage()
        {
            InitializeComponent();

            Superhero = new SuperheroCreateDTO
            {
                //Text = "Superhero name",
                //Description = "This is an item description."
            };

            BindingContext = this;
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "AddSuperhero", Superhero);
            await Navigation.PopModalAsync();
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}