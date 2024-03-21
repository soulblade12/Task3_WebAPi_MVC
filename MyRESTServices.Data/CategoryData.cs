using Microsoft.EntityFrameworkCore;
using MyRESTServices.Data.Interfaces;
using MyRESTServices.Domain.Models;


namespace MyRESTServices.Data
{
    public class CategoryData : ICategoryData
    {
        private readonly AppDbContext _context;

        public CategoryData(AppDbContext context)
        {
            _context = context;
        }

        public async Task<int> Delete(int id)
        {
            var getID = await GetById(id);
            _context.Categories.Remove(getID);
            await _context.SaveChangesAsync();
            return id;
        }

        public async Task<IEnumerable<Category>> GetAll()
        {
            var categories = await _context.Categories.ToListAsync();
            return categories;
        }

        public async Task<Category> GetById(int id)
        {
            var category = await _context.Categories.SingleOrDefaultAsync(c => c.CategoryId == id);
            if (category == null)
            {
                throw new ArgumentException("category not found");
            }
            return category;
        }

        public async Task<IEnumerable<Category>> GetByName(string name)
        {
            var categories = await _context.Categories
        .Where(c => c.CategoryName.Contains(name))
        .ToListAsync();
            if (categories == null)
            {
                throw new ArgumentException("category not found");
            }
            return categories;
        }

        public async Task<int> GetCountCategories(string name)
        {
            var count = await _context.Categories
                .Where(c => c.CategoryName.Contains(name))
                .CountAsync();

            return count;
        }

        public async Task<IEnumerable<Category>> GetWithPaging(int pageNumber, int pageSize, string name)
        {
            var categories = await _context.Categories
                .Where(c => c.CategoryName.Contains(name))
                .OrderBy(c => c.CategoryName)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            return categories;
        }

        public async Task<Category> Insert(Category entity)
        {
            try
            {
                await _context.Categories.AddAsync(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {

                throw new ArgumentException($"{ex.Message}");
            }
        }

        public Task<int> InsertWithIdentity(Category category)
        {
            throw new NotImplementedException();
        }

        public async Task<Category> Update(int id, Category entity)
        {
            var categByID = await GetById(id);
            if (categByID == null)
            {
                throw new ArgumentException("Samurai not found");
            }
            categByID.CategoryName = entity.CategoryName;
            await _context.SaveChangesAsync();
            return categByID;
        }
    }
}
