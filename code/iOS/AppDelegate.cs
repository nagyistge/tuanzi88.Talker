using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.WindowsAzure.MobileServices;

using Foundation;
using UIKit;

using Talker;

namespace Talker.iOS
{
    // YIKANG P2: Try Andriod project
	[Register ("AppDelegate")]
	public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
	{
		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			global::Xamarin.Forms.Forms.Init ();

            // YIKANG NOTE: 
            // These 2 lines rely on Microsoft.WindowsAzure.Mobile.Ext, which only
            // can be added in *.iOS project, but not a PCL project.
            CurrentPlatform.Init ();
            SQLitePCL.CurrentPlatform.Init(); 

			LoadApplication (new App ());

			return base.FinishedLaunching (app, options);
		}
	}
}

