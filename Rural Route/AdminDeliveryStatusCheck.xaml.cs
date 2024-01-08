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
        Date_Picker_Start.Date = DateTime.Now.AddMonths(-1);
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _driverOrderAndProducts = App.RuralRouteRepository.DisplayOrder();
        Date_Picker_Start.Date = DateTime.Now.AddMonths(-1);
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
        var startDate = Date_Picker_Start.Date; 
        var endDate = Date_Picker_End.Date;
        PopulateOrderStats("Complete", startDate, endDate);
    }

    

    private void PopulateOrderStats(string status, DateTime? startDate = null, DateTime? endDate = null)
    {
        bool showHeader = true;
        var filteredList = _driverOrderAndProducts;
        GridDisplay.Clear();
        if (startDate.HasValue && endDate.HasValue )
        {
            filteredList = _driverOrderAndProducts.Where(x => x.Order.DateTime >= startDate.Value && x.Order.DateTime <= endDate.Value).ToList();
        }
     
        foreach (var driverOrderAndProduct in filteredList.Where(x => x.Order.OrderStatus == status).ToList())
        {
            var row = new OrderStatusGridRow(driverOrderAndProduct, showHeader);
            showHeader = false;
            GridDisplay.Add(row);
        }
    }

    

}