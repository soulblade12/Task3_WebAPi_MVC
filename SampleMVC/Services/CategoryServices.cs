using MyWebFormApp.BLL.DTOs;
using Newtonsoft.Json;
using SampleMVC.Models;
using System.Text;
using System.Text.Json;

namespace SampleMVC.Services
{
    public class CategoryServices : ICategoryServices
    {
        private const string BaseUrl = "http://localhost:5272/api/Categories";
        private readonly HttpClient _client;
        private readonly IConfiguration _configuration;

        public CategoryServices(HttpClient client, IConfiguration configuration)
        {
            _client = client;
            _configuration = configuration;

        }
        private string GetBaseUrl()
        {
            return _configuration["BaseUrl"] + "/Categories";
        }

        public async Task<Task> Delete(int id)
        {
            var httpResponse = await _client.DeleteAsync($"{GetBaseUrl()}/{id}");

            if (!httpResponse.IsSuccessStatusCode)
            {
                throw new Exception("Cannot delete category");
            }
            return Task.CompletedTask;
        }

        public async Task<IEnumerable<Category>> GetAll()
        {
            var httpResponse = await _client.GetAsync(GetBaseUrl());

            if (!httpResponse.IsSuccessStatusCode)
            {
                throw new Exception("Cannot retrieve category");
            }

            var content = await httpResponse.Content.ReadAsStringAsync();
            var categories = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<Category>>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return categories;
        }

        public async Task<Category> GetById(int id)
        {
            var httpResponse = await _client.GetAsync($"{GetBaseUrl()}/{id}");

            if (!httpResponse.IsSuccessStatusCode)
            {
                throw new Exception($"Cannot retrieve category by {id}");
            }

            var content = await httpResponse.Content.ReadAsStringAsync();
            var categories = System.Text.Json.JsonSerializer.Deserialize<Category>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return categories;
        }

        public async Task<Task> Insert(CategoryCreateDTO categoryCreateDTO)
        {
            var json = System.Text.Json.JsonSerializer.Serialize(categoryCreateDTO);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var httpResponse = await _client.PostAsync(GetBaseUrl(), data);

            if (!httpResponse.IsSuccessStatusCode)
            {
                throw new Exception("Cannot insert category");
            }


            return Task.CompletedTask;
        }

        public async Task<Task> Update(int id, Category category)
        {
            var json = JsonConvert.SerializeObject(category);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var httpResponse = await _client.PutAsync($"{GetBaseUrl()}/{id}", data);

            if (!httpResponse.IsSuccessStatusCode)
            {
                throw new Exception("Cannot update category");
            }
            //var content = await httpResponse.Content.ReadAsStringAsync();
            //var categories = JsonSerializer.Deserialize<CategoryDTO>(content, new JsonSerializerOptions
            //{
            //    PropertyNameCaseInsensitive = true
            //});

            return Task.CompletedTask;
        }

        public async Task<IEnumerable<CategoryDTO>> GetWithPaging(int pageNumber, int pageSize, string name = "")
        {
            var httpResponse = await _client.GetAsync($"{GetBaseUrl()}/GetWithPaging?pageNumber={pageNumber}&pageSize={pageSize}&name={name}");

            if (!httpResponse.IsSuccessStatusCode)
            {
                throw new Exception($"Cannot retrieve category");
            }

            var content = await httpResponse.Content.ReadAsStringAsync();
            var categories = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<CategoryDTO>>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return categories;
        }

        public async Task<int> GetCountCategories(string name)
        {
            var httpResponse = await _client.GetAsync($"{GetBaseUrl()}/GetCount?name={name}");
            if (!httpResponse.IsSuccessStatusCode)
            {
                throw new Exception("Cannot retrieve count category");
            }

            var content = await httpResponse.Content.ReadAsStringAsync();
            var count = System.Text.Json.JsonSerializer.Deserialize<int>(content, new JsonSerializerOptions
            {
                //PropertyNameCaseInsensitive = true
            });
            return count;
        }
    }
}
