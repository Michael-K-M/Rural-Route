using Rural_Route.Data;

namespace Rural_Route;

public partial class adminCreateDelivery : ContentPage
{
	public List<Customer> CustomerList = App.RuralRouteRepository.SelectCustomerInfo();
	public Customer Customer { get; set; }
    public bool IsRefreshing { get; set; }
    public bool PaginationEnabled { get; set; }

    public adminCreateDelivery()
	{
		InitializeComponent();
		PopulateCustomers();

    }

	private void PopulateCustomers()
	{
		foreach (var customer in CustomerList)
		{
			picker.Items.Add(customer.Name);

        }

    }


}