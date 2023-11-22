namespace Rural_Route;

public partial class DriverEndOrder : ContentPage
{
	public DriverEndOrder()
	{
		InitializeComponent();
        PopulateCustomers();

    }


    private void PopulateCustomers()
    {
        var customerOrderList = App.RuralRouteRepository.DisplayDriverOrder(App.user.Id);

        picker.ItemsSource = customerOrderList;
        //orderid = picker.SelectedIndex;
    }
}