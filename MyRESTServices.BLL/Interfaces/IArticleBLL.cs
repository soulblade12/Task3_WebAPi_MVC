using MyRESTServices.BLL.DTOs;

namespace MyRESTServices.BLL.Interfaces
{
    public interface IArticleBLL
    {
        Task<IEnumerable<ArticleDTO>> GetAll();
        Task<Task> Insert(ArticleCreateDTO article);
        Task<IEnumerable<ArticleDTO>> GetArticleWithCategory();
        Task<IEnumerable<ArticleDTO>> GetArticleByCategory(int categoryId);
        Task<int> InsertWithIdentity(ArticleCreateDTO article);
        Task<Task> Update(ArticleUpdateDTO article);
        Task<Task> Delete(int id);
        Task<ArticleDTO> GetArticleById(int id);
        Task<IEnumerable<ArticleDTO>> GetWithPaging(int categoryId, int pageNumber, int pageSize);
        Task<int> GetCountArticles();
    }
}
