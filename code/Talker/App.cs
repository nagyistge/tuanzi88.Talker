using System;
using System.Diagnostics;

using Xamarin.Forms;
using Talker.BL;

namespace Talker
{
	public class App : Application
	{
		public App ()
		{
			/*
			// The root page of your application
			MainPage = new ContentPage {
				Content = new StackLayout {
					VerticalOptions = LayoutOptions.Center,
					Children = {
						new Label {
							XAlign = TextAlignment.Center,
							Text = "Welcome to Xamarin Forms!"
						}
					}
				}
			};
			*/

			var user = new User ("name", "password");
			var loginPage = new LoginPage ();
			loginPage.BindingContext = user;
			var mainNav = new NavigationPage (loginPage);

			MainPage = mainNav;
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
			// YIKANG P1: Init the Constants.gMaxObjectID if auto-incremental doesn't work for cloud
			Debug.WriteLine ("OnStart");
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
			// YIKANG P1: Init the Constants.gMaxObjectID if auto-incremental doesn't work for cloud
		}
	}
}

