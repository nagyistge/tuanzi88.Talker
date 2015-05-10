using System;
using System.Diagnostics;

using Xamarin.Forms;

using Talker.BL;
using Talker.DL;
using Talker.DAL;
using Talker.VL;

namespace Talker
{
	public class App : Application
	{
		public App ()
		{
            // Init global manager
            GlobalManager.Instance.Init(UserService.Instance, MessageService.Instance);

            // Init login page
            var user = new User("apple", "apple", UserType.Teacher); 
            var loginPage = new LoginPage ();
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

