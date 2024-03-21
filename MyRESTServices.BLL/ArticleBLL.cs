using AutoMapper;
using MyRESTServices.BLL.DTOs;
using MyRESTServices.BLL.Interfaces;
using MyRESTServices.Data.Interfaces;
using MyRESTServices.Domain.Models;

namespace MyRESTServices.BLL
{
    public class ArticleBLL : IArticleBLL
    {
        private readonly IArticleData _articledata;
        private readonly IMapper _mapper;

        public ArticleBLL(IArticleData articledata, IMapper mapper)
        {
            _articledata = articledata;
            _mapper = mapper;
        }
        public async Task<Task> Delete(int id)
        {
            try
            {
                var categoryID = await _articledata.GetById(id);
                if (categoryID == null)
                {
                    throw new ArgumentException("category not found");
                }
                var delete = await _articledata.Delete(id);
                return Task.FromResult(delete);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<ArticleDTO>> GetAll()
        {
            var articles = await _articledata.GetAll();
            var articlesDTO = _mapper.Map<IEnumerable<ArticleDTO>>(articles);
            return articlesDTO;
        }

        public async Task<IEnumerable<ArticleDTO>> GetArticleByCategory(int categoryId)
        {
            var articlebyCategory = await _articledata.GetArticleByCategory(categoryId);
            return _mapper.Map<IEnumerable<ArticleDTO>>(articlebyCategory);
        }

        public async Task<ArticleDTO> GetArticleById(int id)
        {
            var article = await _articledata.GetById(id);
            var articles = _mapper.Map<ArticleDTO>(article);
            return articles;
        }

        public async Task<IEnumerable<ArticleDTO>> GetArticleWithCategory()
        {
            var article = await _articledata.GetArticleWithCategory();
            var articles = _mapper.Map<IEnumerable<ArticleDTO>>(article);
            return articles;
        }

        public Task<int> GetCountArticles()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ArticleDTO>> GetWithPaging(int categoryId, int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }

        public async Task<Task> Insert(ArticleCreateDTO article)
        {
            var map = _mapper.Map<Article>(article);
            var add = await _articledata.Insert(map);
            return Task.FromResult(add);
        }

        public Task<int> InsertWithIdentity(ArticleCreateDTO article)
        {
            throw new NotImplementedException();
        }

        public async Task<Task> Update(ArticleUpdateDTO article)
        {
            var mapCateg = _mapper.Map<Article>(article);
            var add = await _articledata.Update(mapCateg.ArticleId, mapCateg);
            return Task.FromResult(add);

        }
    }
}
