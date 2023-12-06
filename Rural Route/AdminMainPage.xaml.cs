using Rural_Route.Data;
using Rural_Route.Views;

namespace Rural_Route;

public partial class AdminMainPage : ContentPage
{
    List<string> tasks = new List<string>();
    public AdminMainPage()
	{
		InitializeComponent();
        DisplayToDoList();

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
        var todoView = checkbox.Parent.Parent.Parent.Parent as ToDoGridRow;
        todoView._todo.Completed = checkbox.IsChecked;
        App.RuralRouteRepository.UpdateToDoList(todoView._todo);
    }
}