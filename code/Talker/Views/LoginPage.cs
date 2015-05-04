using System;
using System.Collections.Generic;
using System.Diagnostics;
using Xamarin.Forms;

using Talker.BL;
using Talker.DAL;


namespace Talker.View
{
    public class LoginPage : ContentPage
    {
        public LoginPage()
        {
            this.SetBinding(ContentPage.TitleProperty, "Name");
            NavigationPage.SetHasNavigationBar(this, true);

            // Name
            var nameLabel = new Label { XAlign = TextAlignment.Center, Text = "Name" };
            var nameEntry = new Entry();		
            nameEntry.SetBinding(Entry.TextProperty, "Name");

            // Password
            var passwordLabel = new Label { Text = "Password" };
            var passwordEntry = new Entry();		
            passwordEntry.SetBinding(Entry.TextProperty, "Password");

            // Type
            var typeLabel = new Label { Text = "Type" };
            var picker = new Picker
            {
                Title = "Student",
                VerticalOptions = LayoutOptions.CenterAndExpand
            };
            picker.Items.Add(UserType.Admin.ToString());
            picker.Items.Add(UserType.Student.ToString());
            picker.Items.Add(UserType.Teacher.ToString());
            picker.SetBinding(Picker.SelectedIndexProperty, "Student");

            // Login
            var loginButton = new Button { Text = "Login" };
            loginButton.Clicked += new EventHandler(LoginButtonClick);

            // Register
            var registerButton = new Button { Text = "Register" };
            registerButton.Clicked += new EventHandler(RegisterButtonClick);

            Content = new StackLayout
            {
                VerticalOptions = LayoutOptions.StartAndExpand,
                Padding = new Thickness(20),
                Children =
                {
                    nameLabel, nameEntry,
                    passwordLabel, passwordEntry,
                    typeLabel, picker,
                    loginButton, registerButton
                }
            };
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            await UserService.Default.InitializeStoreAsync();
            await UserService.Default.RefreshDataAsync();
        }

        protected async void LoginButtonClick(object sender, EventArgs e)
        {
            var one = (User)BindingContext;
            var user = UserService.Default.GetUser(one.Name, one.Password, one.Type);

            if (user != null)
            {
                Console.WriteLine("Get User");
                var messagePage = new MessagePage();
                await Navigation.PushAsync(messagePage);
            }
        }

        protected async void RegisterButtonClick(object sender, EventArgs e)
        {
            // YIKANG P1: register name and password to user
            var one = (User)BindingContext;
            await UserService.Default.InsertUserAsync(one);
        }
    }
}

