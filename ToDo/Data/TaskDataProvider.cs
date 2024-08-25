using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ToDo.Data
{
    public class TaskDataProvider
    {
        //private readonly HttpClient _client;
        private readonly string _baseUrl;

        public TaskDataProvider(HttpClient client, string baseUrl)
        {
            //_client = new HttpClient();
            _baseUrl = baseUrl;
        }

        public async Task<IEnumerable<TaskItem>> GetAllTasksAsync()
        {
            HttpClient _client = new HttpClient();

            return await _client.GetFromJsonAsync<IEnumerable<TaskItem>>($"{_baseUrl}/tasks");
        }

        public async Task<TaskItem> GetTaskByIdAsync(int id)
        {
            HttpClient _client = new HttpClient();

            return await _client.GetFromJsonAsync<TaskItem>($"{_baseUrl}/tasks/{id}");
        }

        public async Task<TaskItem> AddTaskAsync(TaskItem task)
        {
            HttpClient _client = new HttpClient();

            var response = await _client.PostAsJsonAsync($"{_baseUrl}/tasks", task);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<TaskItem>();
        }

        public async Task<TaskItem> UpdateTaskAsync(TaskItem task)
        {
            HttpClient _client = new HttpClient();

            var response = await _client.PutAsJsonAsync($"{_baseUrl}/tasks/{task.Id}", task);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<TaskItem>();
        }

        public async Task DeleteTaskAsync(int id)
        {
            HttpClient _client = new HttpClient();

            var response = await _client.DeleteAsync($"{_baseUrl}/tasks/{id}");
            response.EnsureSuccessStatusCode();
        }
    }

    public class TaskItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsCompleted { get; set; }
        public bool IsStarred { get; set; }
        public DateTime DueDate { get; set; }
        public int ListId { get; set; }

    }
}
