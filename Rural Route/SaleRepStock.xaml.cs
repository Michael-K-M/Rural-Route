using Rural_Route.Data;
using Rural_Route.Views;
using System.Collections.ObjectModel;

namespace Rural_Route;

public partial class SaleRepStock : ContentPage
{
    public List<Product> ProductList = App.RuralRouteRepository.SelectProductName();

    public List<OrderProduct> OrderProductList;

    public ObservableCollection<Product> ObservableProductList = new ObservableCollection<Product>();

    public SaleRepStock()
	{
		InitializeComponent();
        DisplayStockDisplay();
        PopulateGridView();

    }

    // display updated stock
    public void DisplayStockDisplay()
    {
        OrderProductList = App.RuralRouteRepository.DisplayAvailableQuantity();
        var stockList = App.RuralRouteRepository.SelectTodayStockInfo();
        bool showHeader = true;
        GridDisplayStock.Clear();

        foreach (var stock in stockList)
        {
            var product = ProductList.First(x => x.Id == stock.ProductId);
            var row = new DataGridRow(new List<Product> { product }, showHeader);
            row.PopulateReadOnlyProducts(stock.Quantity);
            GridDisplayStock.Add(row);
            showHeader = false;
          
        }
    }

    private void PopulateGridView()
    {

        for(int i = 0; i < ProductList.Count; i++) {
            bool showHeader = i == 0;
            var stockView = new AddStockGridRow(ProductList, showHeader);
            stockView.AddStockCount(null);
            GridDisplay.Add(stockView);
            var picker = stockView.GetPicker();
            picker.SelectedIndexChanged += Picker_SelectedIndexChanged;
        }
    }

    private void Picker_SelectedIndexChanged(object sender, EventArgs e)
    {
        var picker = (Picker) sender;
        if (picker.SelectedIndex == -1)
            return;

        //Fetch Avalible Quantity

        Product clearedProduct = null;
        var parent = picker.Parent.Parent.Parent.Parent as AddStockGridRow;
        
        //Get previous picker item to re-add them to other pickers
        if (parent.PreviousProduct is null || parent.PreviousProduct != picker.SelectedItem)
        {
            clearedProduct = parent.PreviousProduct;
            parent.PreviousProduct = picker.SelectedItem as Product;
        }

        parent.AddStockCount(OrderProductList);

        foreach (var row in GridDisplay.Children)
        {
            var gridRow = (AddStockGridRow)row;
            var otherPicker = gridRow.GetPicker();
            if (picker == otherPicker)
                continue;
            otherPicker.SelectedIndexChanged -= Picker_SelectedIndexChanged;

            if (clearedProduct is not null)
                otherPicker.ItemsSource.Add(clearedProduct);
            
            var temp = otherPicker.SelectedItem as Product;
            otherPicker.ItemsSource.Remove(picker.SelectedItem);
            otherPicker.SelectedItem = temp;
            otherPicker.SelectedIndexChanged += Picker_SelectedIndexChanged;

        }
    }

    private void Button_Pressed(object sender, EventArgs e)
    {
        List<Stock> stockList = new List<Stock>();

        
        foreach (var row in GridDisplay.Children)
        {
            var gridRow = (AddStockGridRow)row;
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