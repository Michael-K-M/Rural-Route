using Rural_Route.Data;
using Rural_Route.Views;

namespace Rural_Route;

public partial class AdminMainPage : ContentPage
{
    List<string> tasks = new List<string>();
    private List<DriverOrderAndProduct> _driverOrderAndProducts;
    public AdminMainPage()
	{
		InitializeComponent();
        
        
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        DisplayToDoList();
        _driverOrderAndProducts = App.RuralRouteRepository.DisplayOrder();
        PopulateOrderStats("Pending");
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        var createToDo = new ToDo
        {
            Description = txt_ToDo.Text,

        };

        App.RuralRouteRepository.CreateToDo(createToDo);

        txt_ToDo.Text = "";

        DisplayAlert("Great News!", "You have successfully added to the To-Do list", "OK");

        DisplayToDoList();
    }

    public void DisplayToDoList()
    {
       var todoList = App.RuralRouteRepository.SelectToDoInfo();
       GridDisplay.Children.Clear();

        foreach (var todo in todoList)
        {
            var todoView = new ToDoGridRow(todo);
            GridDisplay.Children.Add(todoView);
            todoView.checkbox_completed.CheckedChanged += Checkbox_completed_CheckedChanged;
        }
    }

    private void Checkbox_completed_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        var checkbox = sender as CheckBox;
        var todoView = checkbox.Parent.Parent.Parent as ToDoGridRow;
        todoView._todo.Completed = checkbox.IsChecked;
        App.RuralRouteRepository.UpdateToDoList(todoView._todo);
    }

    private void ButtonDelete_Pressed(object sender, EventArgs e)
    {
        App.RuralRouteRepository.DeleteToDoItems();
        DisplayAlert("Deleted!", "You have successfully deleted from the To-Do list", "OK");
        DisplayToDoList();
    }

    private void PopulateOrderStats(string status, DateTime? startDate = null, DateTime? endDate = null)
    {
        bool showHeader = true;
        var filteredList = _driverOrderAndProducts;
        GridDisplayPending.Clear();
        if (startDate.HasValue && endDate.HasValue)
        {
            filteredList = _driverOrderAndProducts.Where(x => x.Order.DateTime >= startDate.Value && x.Order.DateTime <= endDate.Value).ToList();
        }

        foreach (var driverOrderAndProduct in filteredList.Where(x => x.Order.OrderStatus == status).ToList())
        {
            var row = new OrderStatusGridRow(driverOrderAndProduct, showHeader);
            showHeader = false;
            GridDisplayPending.Add(row);
        }
    }

}