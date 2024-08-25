using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ToDo.Data
{
    public class ListDataProvider
    {
        //private readonly HttpClient _client;
        private readonly string _baseUrl;

        public ListDataProvider(HttpClient client, string baseUrl)
        {
            //_client = client;
            _baseUrl = baseUrl;
        }

        public async Task<IEnumerable<TaskList>> GetAllListsAsync()
        {

            HttpClient _client = new HttpClient();
            return await _client.GetFromJsonAsync<IEnumerable<TaskList>>($"{_baseUrl}/lists");
        }

        public async Task<TaskList> GetListByIdAsync(int id)
        {
            HttpClient _client = new HttpClient();

            return await _client.GetFromJsonAsync<TaskList>($"{_baseUrl}/lists/{id}");
        }

        public async Task<TaskList> AddListAsync(TaskList list)
        {
            HttpClient _client = new HttpClient();

            var response = await _client.PostAsJsonAsync($"{_baseUrl}/lists", list);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<TaskList>();
        }

        public async Task<TaskList> UpdateListAsync(TaskList list)
        {
            HttpClient _client = new HttpClient();

            var response = await _client.PutAsJsonAsync($"{_baseUrl}/lists/{list.Id}", list);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<TaskList>();
        }

        public async Task DeleteListAsync(int id)
        {
            HttpClient _client = new HttpClient();

            var response = await _client.DeleteAsync($"{_baseUrl}/lists/{id}");
            response.EnsureSuccessStatusCode();
        }
    }

    public class TaskList
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<TaskItem> Tasks { get; set; }
    }


}
