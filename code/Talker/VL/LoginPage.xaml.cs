using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Talker.BL;
using Talker.DAL;

namespace Talker.VL
{
    public partial class LoginPage : ContentPage
    {
        IUserService mUserService;

        public LoginPage(IUserService pUserService)
        {
            InitializeComponent();

            // Service
            mUserService = pUserService;

            // Title
            this.SetBinding(ContentPage.TitleProperty, "Login");
            NavigationPage.SetHasNavigationBar(this, true);

            // Binding user
            nameEntry.SetBinding(Entry.TextProperty, "Name");
            passwordEntry.SetBinding(Entry.TextProperty, "Password");
            typePicker.Items.Add(UserType.Admin.ToString());
            typePicker.Items.Add(UserType.Teacher.ToString());
            typePicker.Items.Add(UserType.Student.ToString());
            typePicker.SetBinding(Picker.SelectedIndexProperty, "Type");
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            await mUserService.InitializeStoreAsync();
            await mUserService.RefreshDataAsync();
        }

        protected async void OnLoginButtonClicked(object sender, EventArgs e)
        {
            await mUserService.RefreshDataAsync();

            User one = (User)BindingContext;
            User user = await mUserService.GetUser(one.Name, one.Password, one.Type);

            if (user != null)
            {
                Console.WriteLine("Get User");
                var messageListPage = new MessageListPage();
                await Navigation.PushAsync(messageListPage);
            }
        }

        protected async void OnRegisterButtonClicked(object sender, EventArgs e)
        {
            // YIKANG P1: register name and password to user
            User one = (User)BindingContext;
            await mUserService.InsertUserAsync(one);
        }

        protected void OnUserTypeChanged(object sender, EventArgs e)
        {
            Picker picker = (Picker)sender;
            if (picker == null)
                return;

            Console.WriteLine("Type selection changes to " + picker.SelectedIndex.ToString());

            User user = (User)picker.BindingContext;
            UserType newType = (UserType)picker.SelectedIndex;
            user.Type = newType;
            picker.BindingContext = user;
        }
    }
}

