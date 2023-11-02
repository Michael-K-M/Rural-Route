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
        foreach (var product in ProductList)
        {
            picker_Product.Items.Add(product.Name);
        }
    }

    public bool IsFilledEntry()
	{
		return !string.IsNullOrEmpty(picker_Product.SelectedItem.ToString());
	}
	public (string, double) GetGridInfo()
	{
		return (picker_Product.SelectedItem.ToString(), double.Parse(QuantityEntry.Text));
    }
}