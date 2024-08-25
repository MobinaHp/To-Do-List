using System.Windows;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using ToDo.Model;
using ToDo.Data;
using ToDo.ViewModels;
using System.Net.Http;

namespace ToDo
{
    public partial class MainWindow : Window
    {
        private readonly ToDoViewModel _viewModel;

        public MainWindow()
        {
            InitializeComponent();
            var httpClient = new HttpClient();
            var baseUrl = "http://localhost:5059/api";

            var listDataProvider = new ListDataProvider(httpClient, baseUrl);
            var taskDataProvider = new TaskDataProvider(httpClient, baseUrl);
            _viewModel = new ToDoViewModel(listDataProvider, taskDataProvider);
            DataContext = _viewModel;
            Loaded += Onload;
        }

        public void Onload(object sender, RoutedEventArgs e)
        {
            _viewModel.LoadData();
        }

        private void AddListButton_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.AddList();
        }

        private void RemoveListButton_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.DeleteList();
        }
        private void EditListButton_Click(Object sender, RoutedEventArgs e)
        {
            _viewModel.EditList();
        }
        private void AddTaskButton_Click(object sender, RoutedEventArgs e)
        {
            //var taskName = TaskNameTextBox.Text;
            //var dueDate = TaskDueDatePicker.SelectedDate;
            //var isStarred = TaskStarredCheckBox.IsChecked ?? false;
            //var isChecked = TaskCheckedCheckBox.IsChecked ?? false;
            _viewModel.AddTask();

        }

        private void RemoveTaskButton_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.DeleteTask();
        }

        private void UpdateTaskButton_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.EditTask();
        }
    }
}
