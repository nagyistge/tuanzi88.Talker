using System;
using System.Diagnostics;

using Xamarin.Forms;   // YIKANG P1: Check whether this is necessary to contain here, else delete the package.

using Talker.ML;
using Talker.DL;
using Talker.DAL;
using Talker.VL;


/*
 * Yikang TODO:
 * 
 * (The reason to write the design for MVVM projects structure here is 
 * the uml gen website doesn't load the diagram properly)
 * 
 * Talker.iOS / Talker.Andriod
 * App(AL) / View(VL) / ViewModel(VML) / Model(ML) / DatabaseAccessLayer(DAL) / Database Layer(DL)
 * 
 * Talker.iOS / Talker.Andriod
 *   - AL
 * 
 * AL
 *   - VL
 *   - VML
 *   - DAL
 *   - DL
 * 
 * VL
 *   - VML
 * 
 * (DAL and ML must be referenced to one project)
 * 
 * VML
 *   - ML
 *   - DAL
 * 
 * 
 * DL
 *   - ML
 *   - DAL
 */


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

