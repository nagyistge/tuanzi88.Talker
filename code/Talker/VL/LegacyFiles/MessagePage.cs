using System;
using Xamarin.Forms;

using Talker.BL;
using Talker.DAL;

namespace Talker.VL
{
	public class MessagePage_Legacy : ContentPage
	{
        public MessagePage_Legacy ()
		{
			NavigationPage.SetHasNavigationBar (this, true);

			var welcomeLable = new Label { Text = "Welcome to message page" };

			Content = new StackLayout {
				VerticalOptions = LayoutOptions.StartAndExpand,
				Padding = new Thickness(20),
				Children = {
					welcomeLable
				}
			};
		}
	}
}

