using Rural_Route.Data;
using System.Collections.ObjectModel;

namespace Rural_Route.Views;

public partial class DataGridRow : ContentView
{

    public ObservableCollection<Product> ProductList = new ObservableCollection<Product>();
    public Product PreviousProduct;
    public DataGridRow(List<Product> productList, bool showHeader = false, string product = null, string quantity = null)
	{
        InitializeComponent();


        if (string.IsNullOrEmpty(product) == false) 
        {
            productLable.Text = product;        
        }

        if (string.IsNullOrEmpty(quantity) == false)
        {
            quantityLable.Text = quantity;
        }

        foreach (var item in productList)
            ProductList.Add(item);

        
		GridHeader.IsVisible = showHeader;
        PopulateProduct();
    }

    private void PopulateProduct()
    {
        ProductPicker.ItemsSource = ProductList;
    }

    

    public Picker GetPicker()
    {
        return ProductPicker;
    }

    public bool IsFilledEntry()
	{
        if (ProductPicker.IsEnabled == false) {
            return false;
        }
       
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