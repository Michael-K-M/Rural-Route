using Rural_Route.Data;
using Rural_Route.Views;

namespace Rural_Route;

public partial class DriverSelectOrder : ContentPage
{
    public List<Customer> CustomerList = App.RuralRouteRepository.DisplayOrder();
    public DriverSelectOrder()
	{
		InitializeComponent();
        PopulateCustomers();

    }

    private void PopulateCustomers()
    {
        picker.ItemsSource = CustomerList;
    }
}