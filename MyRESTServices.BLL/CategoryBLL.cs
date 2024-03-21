using AutoMapper;
using MyRESTServices.BLL.DTOs;
using MyRESTServices.BLL.Interfaces;
using MyRESTServices.Data.Interfaces;
using MyRESTServices.Domain.Models;


namespace MyRESTServices.BLL
{
    public class CategoryBLL : ICategoryBLL
    {
        private readonly ICategoryData _categoryData;
        private readonly IMapper _mapper;

        public CategoryBLL(ICategoryData categoryData, IMapper mapper)
        {
            _categoryData = categoryData;
            _mapper = mapper;
        }

        public async Task<Task> Delete(int id)
        {
            try
            {
                var categoryID = await _categoryData.GetById(id);
                if (categoryID == null)
                {
                    throw new ArgumentException("category not found");
                }
                var delete = await _categoryData.Delete(id);
                return Task.FromResult(delete);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<CategoryDTO>> GetAll()
        {
            var categories = await _categoryData.GetAll();
            var categoriesDTO = _mapper.Map<IEnumerable<CategoryDTO>>(categories);
            return categoriesDTO;
        }

        public async Task<CategoryDTO> GetById(int id)
        {
            var category = await _categoryData.GetById(id);
            var categories = _mapper.Map<CategoryDTO>(category);
            return categories;
        }

        public async Task<IEnumerable<CategoryDTO>> GetByName(string name)
        {
            var categories = await _categoryData.GetByName(name);
            var categoryMAP = _mapper.Map<IEnumerable<CategoryDTO>>(categories);
            return categoryMAP;
        }

        public Task<int> GetCountCategories(string name)
        {
            var count = _categoryData.GetCountCategories(name);
            return count;
        }

        public async Task<IEnumerable<CategoryDTO>> GetWithPaging(int pageNumber, int pageSize, string name)
        {
            var categories = await _categoryData.GetWithPaging(pageNumber, pageSize, name);
            var categoriesDto = _mapper.Map<IEnumerable<CategoryDTO>>(categories);
            return categoriesDto;
        }

        public async Task<Task> Insert(CategoryCreateDTO entity)
        {
            var map = _mapper.Map<Category>(entity);
            var add = await _categoryData.Insert(map);
            return Task.FromResult(add);
        }

        public async Task<Task> Update(CategoryUpdateDTO entity)
        {
            var mapCateg = _mapper.Map<Category>(entity);
            var add = await _categoryData.Update(mapCateg.CategoryId, mapCateg);
            return Task.FromResult(add);

        }
    }
}