using Rural_Route.Data;

namespace Rural_Route;

public partial class AdminCreateProduct : ContentPage
{

   

    public AdminCreateProduct()
	{
		InitializeComponent();
	}

    private void Button_Clicked(object sender, EventArgs e)
    {
        var createdProduct = new Product
        {
            Name = txt_productName.Text,
            Price = int.Parse(txt_productPrice.Text),
            
        };

        App.RuralRouteRepository.CreateProduct(createdProduct);

        txt_productName.Text = "";
        txt_productPrice.Text = "";
       

        DisplayAlert("Great News!", "You have successfully created a new Product.", "OK");

    }
}