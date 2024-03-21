using MyRESTServices.BLL.DTOs;

namespace MyRESTServices.BLL.Interfaces
{
    public interface ICategoryBLL
    {
        Task<Task> Delete(int id);
        Task<IEnumerable<CategoryDTO>> GetAll();
        Task<CategoryDTO> GetById(int id);
        Task<IEnumerable<CategoryDTO>> GetByName(string name);
        Task<Task> Insert(CategoryCreateDTO entity);
        Task<Task> Update(CategoryUpdateDTO entity);
        Task<IEnumerable<CategoryDTO>> GetWithPaging(int pageNumber, int pageSize, string name);
        Task<int> GetCountCategories(string name);


    }
}
