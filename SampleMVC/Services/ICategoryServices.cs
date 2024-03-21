using MyWebFormApp.BLL.DTOs;
using SampleMVC.Models;

namespace SampleMVC.Services
{
    public interface ICategoryServices
    {
        Task<IEnumerable<Category>> GetAll();
        Task<Category> GetById(int id);
        Task<Task> Insert(CategoryCreateDTO categoryCreateDTO);
        Task<Task> Update(int id, Category category);
        Task<Task> Delete(int id);
        Task<IEnumerable<CategoryDTO>> GetWithPaging(int pageNumber, int pageSize, string name = "");

        Task<int> GetCountCategories(string name);
    }
}
