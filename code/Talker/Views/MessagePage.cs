using System;
using Xamarin.Forms;

namespace Talker
{
	public class MessagePage : ContentPage
	{
		public MessagePage ()
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

