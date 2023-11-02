using Rural_Route.Data;
using Rural_Route.Views;

namespace Rural_Route;

public partial class adminCreateDelivery : ContentPage
{
	public List<Customer> CustomerList = App.RuralRouteRepository.SelectCustomerInfo();
    public List<Product> ProductList = App.RuralRouteRepository.SelectProductName();

    public adminCreateDelivery()
	{
		InitializeComponent();
		PopulateCustomers();
        PopulateGridView();
    }

	private void PopulateCustomers()
	{
		foreach (var customer in CustomerList)
		{
			picker.Items.Add(customer.Name);
        }
    }

    

    private void PopulateGridView()
    {
        GridDisplay.Add(new DataGridRow(ProductList, true));
        GridDisplay.Add(new DataGridRow(ProductList));
        GridDisplay.Add(new DataGridRow(ProductList));
        GridDisplay.Add(new DataGridRow(ProductList));
        GridDisplay.Add(new DataGridRow(ProductList));
        GridDisplay.Add(new DataGridRow(ProductList));
        GridDisplay.Add(new DataGridRow(ProductList));
    }

    private void Button_Pressed(object sender, EventArgs e)
    {
        foreach (var row in GridDisplay.Children)
        {

            var gridRow = (DataGridRow)row;
            if (gridRow.IsFilledEntry())
            {
                var detail = gridRow.GetGridInfo();

            }
        }

    }
}