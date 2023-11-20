using Rural_Route.Data;
using Rural_Route.Views;
using System.Collections.Generic;

namespace Rural_Route;


public partial class DriverSelectOrder : ContentPage
{
    // public List<Product> ProductList = App.RuralRouteRepository.SelectProductName();
    //public List<DriverOrderAndProduct> customerOrderList = App.RuralRouteRepository.DisplayOrder();
    int orderid;
    public DriverSelectOrder()
    {
        InitializeComponent();
        PopulateCustomers();
        picker.SelectedIndexChanged += Picker_SelectedIndexChanged;
    }

    private void PopulateCustomers()
    {
        var customerOrderList = App.RuralRouteRepository.DisplayOrder();

        picker.ItemsSource = customerOrderList;
        //orderid = picker.SelectedIndex;
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
        // BUSY CODING HERE  #GridDisplay
        foreach (var productAndQuantity in driverOrderAndProduct.Products)
        {
            var row = new DataGridRow(new List<Product> { new Product {Name = productAndQuantity.name } }, showHeader);
            row.PopulateReadOnlyProducts(productAndQuantity.quantity);
            GridDisplay.Add(row);
            showHeader = false;
        } 
    }


}
