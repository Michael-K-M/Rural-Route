
using Rural_Route.Data;
using Rural_Route.Views;
using System.Collections.ObjectModel;

namespace Rural_Route;

public partial class adminViewStock : ContentPage
{
    public List<Product> ProductList = App.RuralRouteRepository.SelectProductName();

    public List<OrderProduct> OrderProductList;

    public adminViewStock()
	{
		InitializeComponent();
        Date_Picker_Start.Date = DateTime.Now.AddMonths(-1);
        Date_Picker_Start.DateSelected += Date_Picker_DateSelected;
        Date_Picker_End.DateSelected += Date_Picker_DateSelected;
        DisplayStockDisplay();
    }



    private void Date_Picker_DateSelected(object sender, DateChangedEventArgs e)
    {
        if(Date_Picker_Start.Date <= Date_Picker_End.Date)
        {
            DisplayStockDisplay();
        }
    }

    public void DisplayStockDisplay()
    {
        OrderProductList = App.RuralRouteRepository.DisplayAvailableQuantity();
        var stockList = App.RuralRouteRepository.SelectAllStockInfo().OrderByDescending(x => x.CreationDate);
        
        var filteredList = stockList.Where(x => x.CreationDate >= Date_Picker_Start.Date && x.CreationDate <= Date_Picker_End.Date).ToList();
        
        bool showHeader = true;
        GridDisplay.Clear();

        var date = DateTime.Today;

        foreach(var stock in OrderProductList)
        {
            var product = ProductList.First(x => x.Id == stock.ProductId);
            var row = new DataGridRow(new List<Product> { product }, showHeader,quantity: "Available Stock");
            row.PopulateReadOnlyProducts(stock.Quantity);
            GridDisplay.Add(row);
            showHeader = false;
        }

        showHeader = true;







        var productLable = "Product";
        var quantityLable = "Quantity";
        foreach (var stock in filteredList)
        {
            if (stock.CreationDate != date) 
            {
                productLable = stock.CreationDate.ToString();
                date = stock.CreationDate;
                quantityLable = " ";
                showHeader = true;
            }

            var product = ProductList.First(x => x.Id == stock.ProductId);
            var row = new DataGridRow(new List<Product> { product }, showHeader, productLable, quantityLable);
            row.PopulateReadOnlyProducts(stock.Quantity);
            GridDisplay.Add(row);
            showHeader = false;

         

        }
    }
}