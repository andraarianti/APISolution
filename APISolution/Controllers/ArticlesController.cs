using Microsoft.AspNetCore.Mvc;
using MyWebFormApp.BLL.DTOs;
using MyWebFormApp.BLL.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace APISolution.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ArticlesController : ControllerBase
	{

		private IArticleBLL _articleBLL;
		public ArticlesController(IArticleBLL articleBLL)
		{
			_articleBLL = articleBLL;
		}

		// GET: api/<ArticlesController>
		[HttpGet]
		public IActionResult GetAll()
		{
			var articles = _articleBLL.GetArticleWithCategory();
			return Ok(articles);
		}

		// GET api/<ArticlesController>/5
		[HttpGet("{id}")]
		public ArticleDTO GetArticleById(int id)
		{
			return _articleBLL.GetArticleById(id);
		}

		// POST api/<ArticlesController>
		[HttpPost]
		public IActionResult Post(ArticleCreateDTO article)
		{
			_articleBLL.Insert(article);
			return Ok();
		}

		// PUT api/<ArticlesController>/5
		[HttpPut("{id}")]
		public IActionResult Put(int id, [FromBody] ArticleUpdateDTO article)
		{
			_articleBLL.Update(article);
			return Ok();
		}

		// DELETE api/<ArticlesController>/5
		[HttpDelete("{id}")]
		public IActionResult Delete(int id)
		{
			_articleBLL.Delete(id);
			return Ok($"Data Article {id} berhasil didelete");
		}
	}
}
