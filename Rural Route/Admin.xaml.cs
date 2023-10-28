using Rural_Route.Data;
using System.Data.SqlClient;
using System.Reflection.PortableExecutable;

namespace Rural_Route;

public partial class Admin : ContentPage
{
   
    public Admin()
	{
		InitializeComponent();
		new Database();
	}

    private void Button_Clicked(object sender, EventArgs e)
    {
        var createdUser = new User
        {
            Name = txt_firstname.Text,
            LastName = txt_Surname.Text,
            Username = txt_Username.Text
        };

        if (picker.SelectedItem.ToString() == "Sales Rep")
            createdUser.Pos = Position.SalesRep;
        else
            createdUser.Pos = (Position)Enum.Parse(typeof(Position), picker.SelectedItem.ToString());

        App.RuralRouteRepository.CreateAccount(createdUser, txt_Password.Text);

        txt_firstname.Text = "";
        txt_Surname.Text = "";
        txt_Username.Text = "";
        txt_Password.Text = "";
        picker.SelectedItem = "";

        DisplayAlert("Great News!", "You have successfully created a new user.", "OK");

    }
}