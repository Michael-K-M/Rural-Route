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
        picker.ItemsSource = CustomerList;
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
        Order order = new Order();

        if (picker.SelectedItem == null)
        {
            DisplayAlert("***BAD NEWS***", "You need to choose the company", "OK");
        }

        else {

            if (order.Location == null)
            {
                order.CustomerId = (picker.SelectedItem as Customer).Id;
                order.OrderStatus = "Pending";
                order.Location = "";
                order.DateTime = Date_Picker.Date;

                List<OrderProduct> orderProducts = new List<OrderProduct>();

                foreach (var row in GridDisplay.Children)
                {
                    var gridRow = (DataGridRow)row;
                    if (gridRow.IsFilledEntry())
                    {
                        OrderProduct orderProduct = new OrderProduct();

                        var detail = gridRow.GetGridInfo();
                        orderProduct.ProductId = detail.productId;
                        orderProduct.Quantity = detail.amount;
                        orderProducts.Add(orderProduct);
                    }
                }

                if (orderProducts.Any())
                {
                    App.RuralRouteRepository.CreateOrder(order, orderProducts);

                    picker.SelectedItem = null;
                    txt_customerAddress.Text = "";
                    GridDisplay.Clear();
                    PopulateGridView();

                    DisplayAlert("Great News!", "You have successfully created a new Order", "OK");
                    
                }
                else
                {
                    DisplayAlert("***BAD NEWS***", "You need enter a product and/or quantity", "OK");
                }

                
            }
            else
            {

                order.CustomerId = (picker.SelectedItem as Customer).Id;
                order.OrderStatus = "Pending";
                order.Location = txt_customerAddress.Text;
                order.DateTime = Date_Picker.Date;

                List<OrderProduct> orderProducts = new List<OrderProduct>();

                foreach (var row in GridDisplay.Children)
                {
                    var gridRow = (DataGridRow)row;
                    if (gridRow.IsFilledEntry())
                    {
                        OrderProduct orderProduct = new OrderProduct();

                        var detail = gridRow.GetGridInfo();
                        orderProduct.ProductId = detail.productId;
                        orderProduct.Quantity = detail.amount;
                        orderProducts.Add(orderProduct);
                    }
                }

                App.RuralRouteRepository.CreateOrder(order, orderProducts);

                picker.SelectedItem = null;
                txt_customerAddress.Text = "";
                GridDisplay.Clear();
                PopulateGridView();

                DisplayAlert("Great News!", "You have successfully created a new Order", "OK");

            }
        }
    }
}