using Rural_Route.Data;
using Rural_Route.Views;

namespace Rural_Route;

public partial class SaleRepStock : ContentPage
{
    public List<Product> ProductList = App.RuralRouteRepository.SelectProductName();

    public SaleRepStock()
	{
		InitializeComponent();
        DisplayStockDisplay();
        PopulateGridView();

    }

    public void DisplayStockDisplay()
    {
        var stockList = App.RuralRouteRepository.SelectStockInfo();
        bool showHeader = true;
        GridDisplay.Clear();

        foreach (var stock in stockList)
        {
            var product = ProductList.First(x => x.Id == stock.ProductId);
            var row = new DataGridRow(new List<Product> { product }, showHeader);
            row.PopulateReadOnlyProducts(stock.Quantity);
            GridDisplay.Add(row);
            showHeader = false;
          
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
        List<Stock> stockList = new List<Stock>();

        
        foreach (var row in GridDisplay.Children)
        {
            var gridRow = (DataGridRow)row;
            if (gridRow.IsFilledEntry())
            {
                Stock stock = new Stock();

                var detail = gridRow.GetGridInfo();
                stock.Quantity = detail.amount;
                stock.ProductId = detail.productId;
                stockList.Add(stock);
            }
        }

        App.RuralRouteRepository.UpdateStockNumbers(stockList);

        GridDisplay.Clear();
        DisplayStockDisplay();
        PopulateGridView();
    }

    }