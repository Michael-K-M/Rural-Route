
using Rural_Route.Data;
using System.Collections.ObjectModel;


namespace Rural_Route
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        public Button LoggedInButton => LoginBtn;
        public string UserName => txt_LoginUsername.Text;
        public string Password => txt_LoginPassword.Text;

        private void Button_Clicked(object sender, EventArgs e)
        {

            txt_LoginUsername.Text = "";
            txt_LoginPassword.Text = "";

        }
    }
}