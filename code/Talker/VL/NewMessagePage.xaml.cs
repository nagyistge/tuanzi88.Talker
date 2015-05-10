using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Talker.BL;
using Talker.DAL;

namespace Talker
{
    public partial class NewMessagePage : ContentPage
    {
        IMessageService mMessageService;

        public NewMessagePage()
        {
            InitializeComponent();

            mMessageService = GlobalManager.Instance.MessageseService;

            InitReceiverList();
        }

        private void InitReceiverList()
        {
            foreach (User one in GlobalManager.Instance.Friends)
            {
                mSenderPicker.Items.Add(one.Name);
                mSenderPicker.SelectedIndex = 1;
            }
        }

        private async void OnSendButtonClicked(object sender, EventArgs e)
        {
            // Add to Global data
            string receiverName = mSenderPicker.Items[mSenderPicker.SelectedIndex];
            string receiverID = "";
            foreach (User one in GlobalManager.Instance.Friends)
            {
                if (one.Name == receiverName)
                {
                    receiverID = one.ID;
                }
            }


            Message message = new Message(GlobalManager.Instance.CurrentUser.ID, receiverID, mTextEditor.Text, false);
            GlobalManager.Instance.CurrentMessages.Add(message);

            // Add to DB
            await mMessageService.InsertMessageAsync(message);
        }
    }
}

