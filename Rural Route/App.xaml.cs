using Microsoft.Maui.Controls;
using Rural_Route.Data;

namespace Rural_Route
{
    public partial class App : Application
    {
        public static Database RuralRouteRepository = new Database();
        private LocalDatabase localDatabase;
        
        public static User user;
        
        public App()
        {
            InitializeComponent();
            localDatabase = new LocalDatabase();
            var loggedInUser = localDatabase.GetLoggedInUser();
            if(loggedInUser is not null)
                SetMainPage(loggedInUser);
            else
            {
                var logginPage = new MainPage();
                MainPage = logginPage;
                logginPage.LoggedInButton.Clicked += LoggedInButton_Clicked;
            }
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

            localDatabase.SaveUser(user);
            SetMainPage(loggedInUser);
        }

        public void SetMainPage(User loggedInUser)
        {
            // send user to location
            if (loggedInUser.Pos == Position.Admin)
            {
                var flyout = new AdminFlyOutShell();
                flyout.Navigated += OnNavigated;
                MainPage = flyout;
            }
            else if (loggedInUser.Pos == Position.Driver)
            {

                var flyout = new DriverFlyOutShell();
                flyout.Navigated += OnNavigated;
                MainPage = flyout;
            }
            else if (loggedInUser.Pos == Position.SalesRep)
            {
                var flyout = new SaleRepFlyOutShell();
                flyout.Navigated += OnNavigated;
                MainPage = flyout;
            }
        }

        private void OnNavigated(object sender, ShellNavigatedEventArgs e)
        {
            if (e.Current.Location.OriginalString == "//"+nameof(MainPage))
            {
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    localDatabase.DeleteUser();
                    var loginPage = new MainPage();
                    MainPage = loginPage;
                    loginPage.LoggedInButton.Clicked += LoggedInButton_Clicked;
                
                });

                }
        }
    }
}