using Rural_Route.Data;

namespace Rural_Route;

public partial class AdminCreateCustomer : ContentPage
{

    public AdminCreateCustomer()
	{
		InitializeComponent();
	}

    private void Button_Clicked(object sender, EventArgs e)
    {
        var createdCustomer = new Customer
        {
            Name = txt_CustomerName.Text,
           

        };

        App.RuralRouteRepository.CreateCustomer(createdCustomer);

        txt_CustomerName.Text = "";
        


        DisplayAlert("Great News!", "You have successfully created a new Customer.", "OK");

    }
}