using Microsoft.Maui.Controls;
using Rural_Route.Data;

namespace Rural_Route
{
    public partial class App : Application
    {

        Database db = new Database();
        public App()
        {
            InitializeComponent();
            var logginPage = new MainPage();
            MainPage = logginPage;
            logginPage.LoggedInButton.Clicked += LoggedInButton_Clicked;

        }

        private void LoggedInButton_Clicked(object sender, EventArgs e)
        {
            var button = (Button)sender;

            var loginPage = (MainPage) button.Parent.Parent.Parent;
            var loggedInUser = db.SignInUser(loginPage.UserName, loginPage.Password);
            if (loggedInUser == null)
            {
                Current.MainPage.DisplayAlert("ERROR!", "Sorry but your username or password is incorrect.", "Retry");
                return;
            }
            
            SetMainPage(loggedInUser);
        }

        public void SetMainPage(User loggedInUser)
        {
            // send user to location
            if (loggedInUser.Pos == Position.Admin)
            {
                //await Navigation.PushAsync(new Admin());
            }
            else if (loggedInUser.Pos == Position.Driver)
            {
                MainPage = new CustomFlyoutShell();
            }
            else if (loggedInUser.Pos == Position.SalesRep)
            {
                //await Navigation.PushAsync(new SalesRepHomepage());
            }
        }
    }
}