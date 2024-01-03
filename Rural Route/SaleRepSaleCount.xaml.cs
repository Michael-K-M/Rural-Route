using Rural_Route.Data;
using Rural_Route.Views;

namespace Rural_Route;

public partial class SaleRepSaleCount : ContentPage
{
    public List<Product> ProductList = App.RuralRouteRepository.SelectProductName();

    public SaleRepSaleCount()
	{
		InitializeComponent();
        PopulateGridView();

    }


    private void PopulateGridView()
    {
        GridDisplay.Add(new DataGridRow(ProductList, true));
       // GridDisplay.Add(new DataGridRow(ProductList));
        
    }

    private void Button_Pressed(object sender, EventArgs e)
    {
        Order order = new Order();
        order.CustomerId = 9;
        order.OrderStatus = "Complete";
        order.Location = "Off the farm";
        order.DateTime = DateTime.Now;

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

        
        
        GridDisplay.Clear();
        PopulateGridView();

        DisplayAlert("Great News!", "You have successfully created a new Order", "OK");
    }
}