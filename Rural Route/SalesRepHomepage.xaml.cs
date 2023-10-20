namespace Rural_Route;

public partial class SalesRepHomepage : ContentPage
{
	public SalesRepHomepage()
	{
		InitializeComponent();
	}



    private async void OnShowMenuClicked(object sender, EventArgs e)
    {
        var menu = new List<MenuItem>
            {
                new MenuItem { Text = "Admin", Command = new Command(() => HandleOption1()) },
                new MenuItem { Text = "Driver", Command = new Command(() => HandleOption2()) },
                 new MenuItem { Text = "Logout", Command = new Command(() => HandleOption2()) },
                // Add more menu items as needed
            };

        var selectedOption = await DisplayActionSheet("Select an Option", "Cancel", null, menu.Select(item => item.Text).ToArray());

        // Handle selected option if needed

        if (selectedOption == "Admin") {

            await Navigation.PushAsync(new Admin());
        }

        else if (selectedOption == "Driver")
        {
            await Navigation.PushAsync(new DriverHomepage());
        }

        else if (selectedOption == "Logout")
        {
            await Navigation.PushAsync(new MainPage());
        }





    }

    private void HandleOption1()
    {
         Navigation.PushAsync(new Admin());
    }

    private void HandleOption2()
    {
        Navigation.PushAsync(new DriverHomepage());
    }

}