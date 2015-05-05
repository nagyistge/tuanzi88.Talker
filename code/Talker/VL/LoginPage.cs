using System;
using Xamarin.Forms;
using System.Threading.Tasks;

using Talker.BL;
using Talker.DAL;

namespace Talker.VL
{
    public class LoginPage : ContentPage
    {
        IUserService mUserService;

        public LoginPage(IUserService pUserService)
        {
            // Init
            mUserService = pUserService;
            this.SetBinding(ContentPage.TitleProperty, "Login");
            NavigationPage.SetHasNavigationBar(this, true);

            // Name
            var nameLabel = new Label { /*XAlign = TextAlignment.Center,*/ Text = "Name" };
            var nameEntry = new Entry();		
            nameEntry.SetBinding(Entry.TextProperty, "Name");

            // Password
            var passwordLabel = new Label { Text = "Password" };
            var passwordEntry = new Entry();		
            passwordEntry.SetBinding(Entry.TextProperty, "Password");

            // Type
            var typeLabel = new Label { Text = "Type" };
            var picker = new Picker
            {
                    Title = "Select a type",       
                VerticalOptions = LayoutOptions.CenterAndExpand
            };
            picker.Items.Add(UserType.Admin.ToString());
            picker.Items.Add(UserType.Teacher.ToString());
            picker.Items.Add(UserType.Student.ToString());
            picker.SetBinding(Picker.SelectedIndexProperty, "Type");
            picker.SelectedIndexChanged += new EventHandler(UserTypeChange);

            // Login
            var loginButton = new Button { Text = "Login" };
            loginButton.Clicked += new EventHandler(LoginButtonClick);

            // Register
            var registerButton = new Button { Text = "Register" };
            registerButton.Clicked += new EventHandler(RegisterButtonClick);

            Content = new StackLayout
            {
                VerticalOptions = LayoutOptions.StartAndExpand,
                Padding = new Thickness(20),
                Children =
                {
                    nameLabel, nameEntry,
                    passwordLabel, passwordEntry,
                    typeLabel, picker,
                    loginButton, registerButton
                }
            };
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            await mUserService.InitializeStoreAsync();
            await mUserService.RefreshDataAsync();
        }

        protected async void LoginButtonClick(object sender, EventArgs e)
        {
            await mUserService.RefreshDataAsync();

            User one = (User)BindingContext;
            User user = await mUserService.GetUser(one.Name, one.Password, one.Type);

            if (user != null)
            {
                Console.WriteLine("Get User");
                var messagePage = new MessagePage();
                await Navigation.PushAsync(messagePage);
            }
        }

        protected async void RegisterButtonClick(object sender, EventArgs e)
        {
            // YIKANG P1: register name and password to user
            User one = (User)BindingContext;
            await mUserService.InsertUserAsync(one);
        }

        protected void UserTypeChange(object sender, EventArgs e)
        {
            Picker picker = (Picker)sender;
            if (picker == null)
                return;
            
            Console.WriteLine("Type selection changes to " + picker.SelectedIndex.ToString());

            User user = (User)picker.BindingContext;
            UserType newType = (UserType)picker.SelectedIndex;
            user.Type = newType;
            picker.BindingContext = user;
        }


    }
}

