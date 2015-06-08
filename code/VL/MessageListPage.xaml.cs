using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Talker.ML;
using Talker.DAL;

namespace Talker.VL
{
    public partial class MessageListPage : ContentPage
    {
        public MessageListPage()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            await GlobalManager.Instance.MessageseService.RefreshDataAsync(GlobalManager.Instance.CurrentUser.ID);
            messageListView.ItemsSource = GlobalManager.Instance.CurrentMessages;

            base.OnAppearing();
        }

        private async void OnSendMessageClicked(object sender, EventArgs e)
        {
            var newMessagePage = new NewMessagePage();
            await Navigation.PushAsync(newMessagePage);
        }
    }
}

