using Rural_Route.Data;
using Rural_Route.Views;
using System.Collections.Generic;

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
        picker.SelectedIndexChanged += Picker_SelectedIndexChanged;
    }
    public void Picker_SelectedIndexChanged(object sender, EventArgs e)
    {
        var picker = (Picker)sender;
        if (picker.SelectedIndex == -1)
            return;
        var driverOrderAndProduct = picker.SelectedItem as DriverOrderAndProduct;
        txt_customerAddress.Text = driverOrderAndProduct.Order.Location;
        txt_DeliveryDate.Text = driverOrderAndProduct.Order.DateTime.ToLocalTime().ToString();

        bool showHeader = true;
        GridDisplay.Clear();

        foreach (var productAndQuantity in driverOrderAndProduct.Products)
        {
            var row = new DataGridRow(new List<Product> { new Product { Name = productAndQuantity.name } }, showHeader);
            row.PopulateReadOnlyProducts(productAndQuantity.quantity);
            GridDisplay.Add(row);
            showHeader = false;
        }
    }

    private void Button_Pressed(object sender, EventArgs e)
    {
        string change = "Complete";
        var driverOrderAndProduct = picker.SelectedItem as DriverOrderAndProduct;
        var user = new User();

        App.RuralRouteRepository.UpdateDelivery(change, driverOrderAndProduct.Order.Id);
        picker.SelectedItem = null;
        txt_customerAddress.Text = "";
        GridDisplay.Clear();
        PopulateCustomers();
    }

}