using Rural_Route.Data;

namespace Rural_Route;

public partial class OrderStatusGridRow : ContentPage
{
    private readonly DriverOrderAndProduct _driverOrderAndProduct;

    public OrderStatusGridRow(DriverOrderAndProduct driverOrderAndProduct, bool showHeader = false)
	{
        _driverOrderAndProduct = driverOrderAndProduct;
        InitializeComponent();
        GridHeader.IsVisible = showHeader;
        PopulateInfo();
    }

    private void PopulateInfo()
    {
        CompanyName.Text = _driverOrderAndProduct.Customer.Name;
        CompanyLocation.Text = _driverOrderAndProduct.Order.Location;
        Date.Text = _driverOrderAndProduct.Order.DateTime.ToString();
        DriverName.Text = _driverOrderAndProduct.Driver.Name;
    }

}