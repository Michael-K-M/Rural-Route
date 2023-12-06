using Rural_Route.Data;

namespace Rural_Route.Views;

public partial class ToDoGridRow : ContentView
{
	public readonly ToDo _todo;
    public CheckBox checkbox_completed => cb_Completed;
    public ToDoGridRow(ToDo todo)
	{
		_todo = todo;
		InitializeComponent();
        PopulateToDo();

    }

    private void PopulateToDo()
    {
        cb_Completed.IsChecked = _todo.Completed;
        Description.Text = _todo.Description;
    }


}