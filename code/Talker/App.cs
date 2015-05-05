using System;
using System.Diagnostics;

using Xamarin.Forms;

using Talker.BL;
using Talker.DL;
using Talker.VL;

namespace Talker
{
	public class App : Application
	{
		public App ()
		{
            IUserService userService = (IUserService)UserService.Instance;
            var user = new User("name", "password", "Student"); 
            var loginPage = new LoginPage (userService);
			loginPage.BindingContext = user;
			var mainNav = new NavigationPage (loginPage);
			MainPage = mainNav;
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
			Debug.WriteLine ("OnStart");
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}

