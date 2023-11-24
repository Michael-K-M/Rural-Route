using Rural_Route.Data;
using Rural_Route.Views;
using System.Collections.Generic;

namespace Rural_Route;

public partial class AdminDeliveryStatusCheck : ContentPage
{
	public AdminDeliveryStatusCheck()
	{
		InitializeComponent();
	}


    private void Button_Pressed(object sender, EventArgs e)
    {
        var driverOrderAndProducts = App.RuralRouteRepository.DisplayOrder();
        
        bool showHeader = true;
        GridDisplay.Clear();

        foreach (var driverOrderAndProduct in driverOrderAndProducts.Where(x => x.Order.OrderStatus == "Pending").ToList())
        {
            var row = new OrderStatusGridRow(driverOrderAndProduct, showHeader);
            showHeader = false;
            GridDisplay.Add(row);
        }
    }


}