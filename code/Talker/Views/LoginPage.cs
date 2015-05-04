using System;
using System.Collections.Generic;
using System.Diagnostics;
using Xamarin.Forms;


namespace Talker.BL
{
	public class LoginPage : ContentPage
	{
		public LoginPage ()
		{
			this.SetBinding (ContentPage.TitleProperty, "Name");
			NavigationPage.SetHasNavigationBar (this, true);

			var nameLabel = new Label { XAlign = TextAlignment.Center, Text = "Name" };
			var nameEntry = new Entry ();		
			nameEntry.SetBinding (Entry.TextProperty, "Name");

			var passwordLabel = new Label { Text = "Password" };
			var passwordEntry = new Entry ();		
			passwordEntry.SetBinding (Entry.TextProperty, "Password");

			var loginButton = new Button { Text = "Login" };
			loginButton.Clicked += (sender, e) => 
			{
				var one = (User)BindingContext;
				if( UserManager.IsThisUserExisted(one.Name, one.Password) )
				{
					var messagePage = new MessagePage();
					Navigation.PushAsync(messagePage);
				}
				else
				{
					one.Name = "";			// YIKANG P2: check how to refresh the page
					one.Password = "";
				}
			};

			var registerButton = new Button { Text = "Register" };
			registerButton.Clicked += (sender, e) => 
			{
				// YIKANG P1: register name and password to user
				var one = (User)BindingContext;
				UserManager.SaveUser(one);
			};

			Content = new StackLayout {
				VerticalOptions = LayoutOptions.StartAndExpand,
				Padding = new Thickness(20),
				Children = {
					nameLabel, nameEntry,
					passwordLabel, passwordEntry,
					loginButton, registerButton
				}
			};
		}
	}
}

