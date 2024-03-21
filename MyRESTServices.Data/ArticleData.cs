using Microsoft.EntityFrameworkCore;
using MyRESTServices.Data.Interfaces;
using MyRESTServices.Domain.Models;

namespace MyRESTServices.Data
{
    public class ArticleData : IArticleData
    {
        private readonly AppDbContext _context;

        public ArticleData(AppDbContext context)
        {
            _context = context;
        }
        public async Task<int> Delete(int id)
        {
            var getID = await GetById(id);
            _context.Articles.Remove(getID);
            await _context.SaveChangesAsync();
            return id;
        }

        public async Task<IEnumerable<Article>> GetAll()
        {
            var articles = await _context.Articles.ToListAsync();
            return articles;
        }

        public async Task<IEnumerable<Article>> GetArticleByCategory(int categoryId)
        {
            var articles = await _context.Articles.Where(c => c.CategoryId == categoryId).ToListAsync();
            return articles;
        }

        public async Task<IEnumerable<Article>> GetArticleWithCategory()
        {
            var articles = await _context.Articles
    .Include(a => a.Category) // Eager load the related Category
    .ToListAsync();

            return articles;
        }

        public async Task<Article> GetById(int id)
        {
            var article = await _context.Articles.SingleOrDefaultAsync(c => c.ArticleId == id);
            if (article == null)
            {
                throw new ArgumentException("article not found");
            }
            return article;
        }

        public Task<int> GetCountArticles()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Article>> GetWithPaging(int categoryId, int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }

        public async Task<Article> Insert(Article entity)
        {
            try
            {
                await _context.Articles.AddAsync(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {

                throw new ArgumentException($"{ex.Message}");
            }
        }

        public Task<Task> InsertArticleWithCategory(Article article)
        {
            throw new NotImplementedException();
        }

        public Task<int> InsertWithIdentity(Article article)
        {
            throw new NotImplementedException();
        }

        public async Task<Article> Update(int id, Article entity)
        {
            var articleID = await GetById(id);
            if (articleID == null)
            {
                throw null;
            }
            articleID.Title = entity.Title;
            articleID.Details = entity.Details;
            articleID.IsApproved = entity.IsApproved;
            articleID.Pic = entity.Pic;
            //articleID.CategoryId = entity.CategoryId;
            await _context.SaveChangesAsync();
            return articleID;
        }
    }
}
