using Rural_Route.Data;
using Rural_Route.Views;
using System.Collections.Generic;

namespace Rural_Route;

public partial class AdminDeliveryStatusCheck : ContentPage
{
    private List<DriverOrderAndProduct> _driverOrderAndProducts;
	public AdminDeliveryStatusCheck()
	{
		InitializeComponent();
        _driverOrderAndProducts = App.RuralRouteRepository.DisplayOrder();
    }


    private void ButtonPending_Pressed(object sender, EventArgs e)
    {
        PopulateOrderStats("Pending");
    }


    private void ButtonDeparted_Pressed(object sender, EventArgs e)
    {
        PopulateOrderStats("Departed");
    }


    private void ButtonComplete_Pressed(object sender, EventArgs e)
    {
        PopulateOrderStats("Complete");
    }

    private void PopulateOrderStats(string status)
    {
        bool showHeader = true;
        GridDisplay.Clear();

        foreach (var driverOrderAndProduct in _driverOrderAndProducts.Where(x => x.Order.OrderStatus == status).ToList())
        {
            var row = new OrderStatusGridRow(driverOrderAndProduct, showHeader);
            showHeader = false;
            GridDisplay.Add(row);
        }
    }


}