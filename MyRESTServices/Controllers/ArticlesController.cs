using Microsoft.AspNetCore.Mvc;
using MyRESTServices.BLL.DTOs;
using MyRESTServices.BLL.Interfaces;

namespace MyRESTServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {
        private readonly IArticleBLL _articleBLL;
        //private readonly IValidator<CategoryCreateDTO> _validatorCreate;
        //private readonly IValidator<CategoryUpdateDTO> _validatorUpdate;
        public ArticlesController(IArticleBLL articleBLL)
        {
            _articleBLL = articleBLL;

        }

        [HttpGet]
        public async Task<IEnumerable<ArticleDTO>> Get()
        {
            var results = await _articleBLL.GetAll();
            return results;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _articleBLL.GetArticleById(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet("article")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _articleBLL.GetArticleWithCategory();
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet("articleID")]
        public async Task<IActionResult> GetArticleByID(int id)
        {
            var result = await _articleBLL.GetArticleByCategory(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post(ArticleCreateDTO articleCreate)
        {
            if (articleCreate == null)
            {
                return BadRequest();
            }

            try
            {
                //var validatorResult = await _validatorCreate.ValidateAsync(categoryCreateDTO);
                await _articleBLL.Insert(articleCreate);
                return Ok("Insert data success");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, ArticleUpdateDTO articleUpdate)
        {
            if (await _articleBLL.GetArticleById(id) == null)
            {
                return NotFound();
            }

            try
            {
                //var validatorResult = await _validatorUpdate.ValidateAsync(categoryUpdateDTO);
                await _articleBLL.Update(articleUpdate);
                return Ok("Update data success");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (await _articleBLL.GetArticleById(id) == null)
            {
                return NotFound();
            }

            try
            {
                await _articleBLL.Delete(id);
                return Ok("Delete data success");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
