using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Talker.ML;
using Talker.DAL;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using Microsoft.WindowsAzure.MobileServices;
using System.Threading.Tasks;

namespace Talker.VL
{
    public partial class LoginPage : ContentPage
    {
        IUserDBLocal mUserServiceLocal;
		IUserDBRemote mUserServiceRemote;
        IMessageDB mMessageService;

        public LoginPage()
		{
            InitializeComponent();

            // Service
			mUserServiceLocal = GlobalManager.Instance.UserServiceLocal;
			mUserServiceRemote = GlobalManager.Instance.UserServiceRemote;
            mMessageService = GlobalManager.Instance.MessageseService;

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

            await mUserServiceLocal.RefreshDataAsync();
        }

		protected async void OnLoginButtonClicked(object sender, EventArgs e)
        {
			User one = (User)BindingContext;

			//Remote
			//Shuran: How to deal with Json
			//Reference: https://components.xamarin.com/view/json.net
			JObject loginResult = await mUserServiceRemote.LogInUserRemote(one.Name,one.Password);

			if (loginResult["Exception"] != null) 
			{
				DisplayAlert ("Failed", loginResult["Exception"].ToString(), "OK");
			} 
			else 
			{
				DisplayAlert ("Success", loginResult.ToString(), "OK");

				//Local
				await mUserServiceLocal.RefreshDataAsync ();

				User user = await mUserServiceLocal.GetUser (one.Name, one.Password, one.Type);

				if (user != null) {
					//Console.WriteLine("Get User");

					// Init the current user ID
					GlobalManager.Instance.CurrentUser = user;

					// Init the friends
					mUserServiceLocal.InitFriends ();

					// Pop messagelist
					await mMessageService.RefreshDataAsync (user.ID);
					var messageListPage = new MessageListPage ();
					await Navigation.PushAsync (messageListPage);
				}
			}
        }

        protected async void OnRegisterButtonClicked(object sender, EventArgs e)
        {
			User one = (User)BindingContext;

			//Remote
			JObject registerResult = await mUserServiceRemote.RegisterUserRemote(one.Name,one.Password,one.Type);

			if (registerResult["Exception"] != null) 
			{
				DisplayAlert ("Failed", registerResult["Exception"].ToString(), "OK");
			} 
			else 
			{
				DisplayAlert ("Success", registerResult["Created"].ToString(), "OK");
				//Local
				// YIKANG P1: register name and password to user
				await mUserServiceLocal.InsertUserAsync (one);
			}
        }

        protected void OnUserTypeChanged(object sender, EventArgs e)
        {
            Picker picker = (Picker)sender;
            if (picker == null)
                return;

            //Console.WriteLine("Type selection changes to " + picker.SelectedIndex.ToString());

            User user = (User)picker.BindingContext;
            UserType newType = (UserType)picker.SelectedIndex;
            user.Type = newType;
            picker.BindingContext = user;
        }
    }
}

