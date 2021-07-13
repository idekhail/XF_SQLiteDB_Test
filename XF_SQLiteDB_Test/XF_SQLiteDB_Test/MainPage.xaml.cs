using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XF_SQLiteDB_Test
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            Close.Clicked += (s, e) => Environment.Exit(0);
        }

        private async void Go_Clicked(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(Username.Text))
            {
                var user =  await App.UserSQLite.GetUserAsync(Username.Text);
                if(user != null)
                {
                    if (user.Password == Password.Text)
                    {
                        await Navigation.PushAsync(new SecondPage(Username.Text));
                    }
                    else
                        await DisplayAlert("Error", "Password  is error", "Ok");

                }
                else
                    await DisplayAlert("Error", "Username is error", "Ok");
            }
            else
                await DisplayAlert("Error", "Username is empty", "Ok");
        }

        private async void Add_Clicked(object sender, EventArgs e)
        {
            if((!string.IsNullOrEmpty(Username.Text)) && (await App.UserSQLite.GetUserAsync(Username.Text) == null))
            {
                Users user = new Users()
                {
                    Username = Username.Text,
                    Password = Password.Text
                };
                await App.UserSQLite.SaveUserAsync(user);                
            }
            else
                await DisplayAlert("Error", "Username is empty Or Username is already existe", "Ok");
        }
        private void Clear_Clicked(object sender, EventArgs e)
        {
            Username.Text = string.Empty;
            Password.Text = "";
        }
    }
}
