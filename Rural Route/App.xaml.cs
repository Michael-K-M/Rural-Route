using Microsoft.Maui.Controls;
using Rural_Route.Data;

namespace Rural_Route
{
    public partial class App : Application
    {

        public static Database RuralRouteRepository = new Database();
        public static User user;
        
        
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

            var loginPage = (MainPage) button.Parent.Parent;
            var loggedInUser = App.RuralRouteRepository.SignInUser(loginPage.UserName, loginPage.Password);
            user = loggedInUser;
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
                MainPage = new AdminFlyOutShell();
            }
            else if (loggedInUser.Pos == Position.Driver)
            {
                MainPage = new DriverFlyOutShell();
            }
            else if (loggedInUser.Pos == Position.SalesRep)
            {
                MainPage = new SaleRepFlyOutShell();
            }
        }
    }
}