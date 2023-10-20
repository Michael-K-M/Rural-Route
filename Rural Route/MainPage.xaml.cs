
using Rural_Route.Data;
using System.Collections.ObjectModel;


namespace Rural_Route
{
    public partial class MainPage : ContentPage
    {
        int count = 0;
        Database db = new Database();


        public MainPage()
        {
            InitializeComponent();
            //new Database().Connect();
        }

        private async void OnCounterClicked(object sender, EventArgs e)
        {
            
            var loggedInUser = db.SignInUser(txt_LoginUsername.Text, txt_LoginPassword.Text);
            if(loggedInUser == null)
            {
                await DisplayAlert("ERROR!", "Sorry but your username or password is incorrect.", "Retry");
                return;
            }
            // send user to location
            if(loggedInUser.Pos == Position.Admin)
            {
                await Navigation.PushAsync(new Admin());
            }
            else if (loggedInUser.Pos == Position.Driver)
            {
                await Navigation.PushAsync(new DriverHomepage());
            }
            else if (loggedInUser.Pos == Position.SalesRep)
            {
                await Navigation.PushAsync(new SalesRepHomepage());
            }
            // 
            /*
            count++;

            if (count == 1)
                CounterBtn.Text = $"Clicked {count} time";
            else
                CounterBtn.Text = $"Clicked {count} times";

            SemanticScreenReader.Announce(CounterBtn.Text);
        */
        }
    }
}