using Rural_Route.Data;
using Rural_Route.Views;

namespace Rural_Route;

public partial class SalesRepHomepage : ContentPage
{
	public SalesRepHomepage()
	{
		InitializeComponent();
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


}