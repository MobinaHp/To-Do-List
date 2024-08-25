using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using ToDo.Data;

namespace ToDo.ViewModels
{
    public class ToDoViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<TaskList> TaskLists { get; set; }
        public ObservableCollection<TaskItem> TaskItems { get; set; }
        public ListDataProvider _listDataProvider;
        public TaskDataProvider _taskDataProvider;
        public TaskList? _selectedList;
        public TaskItem? _selectedTask;
        private TaskList _selectedListForNewTask;


        public TaskList SelectedList
        {
            get => _selectedList;
            set
            {
                _selectedList = value;
                OnPropertyChanged();
            }
        }

        public TaskItem SelectedTask
        {
            get => _selectedTask;
            set
            {
                _selectedTask = value;
                OnPropertyChanged();
            }
        }
        public TaskList SelectedListForNewTask
        {
            get => _selectedListForNewTask;
            set
            {
                _selectedListForNewTask = value;
                OnPropertyChanged();
            }
        }


        public ToDoViewModel(ListDataProvider listDataProvider, TaskDataProvider taskDataProvider)
        {
            _listDataProvider = listDataProvider;
            _taskDataProvider = taskDataProvider;
            TaskLists = new ObservableCollection<TaskList>();
            TaskItems = new ObservableCollection<TaskItem>();
        }
        private async Task LoadTaskItems()
        {
            var lists = await _taskDataProvider.GetAllTasksAsync();
            foreach (var list in lists)
            {
                TaskItems.Add(list);
            }
        }
        public async Task LoadData()
        {
            TaskItems.Clear();
            await LoadTaskItems();

            TaskLists.Clear();
            await LoadTaskLists();

            int a = 10;
        }

        private async Task LoadTaskLists()
        {
            var lists = await _listDataProvider.GetAllListsAsync();
            foreach (var list in lists)
            {
                TaskLists.Add(list);
            }
        }

        private void LoadTasksForSelectedList()
        {
            if (SelectedList != null)
            {
                TaskItems.Clear();
                foreach (var task in SelectedList.Tasks)
                {
                    TaskItems.Add(task);
                }
            }
        }

        public async void AddList()
        {
            var newList = new TaskList
            {
                Name = "New List Name",
            };
            var addedList = await _listDataProvider.AddListAsync(newList);
            LoadData();
        }
        public async void EditList()
        {
            var addedList = await _listDataProvider.UpdateListAsync(_selectedList);
            LoadData();
        }
        public async void DeleteList()
        {
            await _listDataProvider.DeleteListAsync(SelectedList.Id);
            TaskLists.Remove(SelectedList);
        }
        public async void AddTask()
        {
            var listId = SelectedListForNewTask?.Id ?? 1;

            var newTask = new TaskItem
            {
                Name = "New Task Text",
                DueDate = DateTime.Now,
                IsStarred = false,
                IsCompleted = false,
                ListId = listId
            };
            var addedTask = await _taskDataProvider.AddTaskAsync(newTask);
            LoadData();
        }


        public async void EditTask()
        {
            var addedTask = await _taskDataProvider.UpdateTaskAsync(_selectedTask);
            LoadData();
        }

        public async void DeleteTask()
        {
            await _taskDataProvider.DeleteTaskAsync(SelectedTask.Id);
            LoadData();

        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }

}
