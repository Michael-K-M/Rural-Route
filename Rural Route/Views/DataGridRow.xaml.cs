using Rural_Route.Data;

namespace Rural_Route.Views;

public partial class DataGridRow : ContentView
{
    public readonly List<Product> ProductList;

    public DataGridRow(List<Product> productList, bool showHeader = false)
	{
        ProductList = productList;
        InitializeComponent();
		GridHeader.IsVisible = showHeader;
        PopulateProduct();
    }

    private void PopulateProduct()
    {
        ProductPicker.ItemsSource = ProductList;
    }

    public bool IsFilledEntry()
	{
		return !string.IsNullOrEmpty(QuantityEntry.Text);
	}

	public (int productId, int amount) GetGridInfo()
	{
        var product = ProductPicker.SelectedItem as Product;

		return (product.Id, int.Parse(QuantityEntry.Text));
    }

    public void PopulateReadOnlyProducts(int quantity)
    {
        FrameLable.IsVisible = true;
        FramePicker.IsVisible = false;
        ProductPicker.SelectedIndex = 0;
        var product = ProductPicker.SelectedItem as Product;

        ProductName.Text = product.Name;
        QuantityEntry.Text = quantity.ToString();
        ProductPicker.IsEnabled = false;
        QuantityEntry.IsEnabled = false;
    }
}