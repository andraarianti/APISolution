using APISolution.Models;
using Microsoft.AspNetCore.Mvc;
using MyWebFormApp.BLL.DTOs;
using MyWebFormApp.BLL.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace APISolution.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CategoriesController : ControllerBase
	{
		private ICategoryBLL _categoryBLL;
		public CategoriesController(ICategoryBLL categoryBLL)
		{
			_categoryBLL = categoryBLL;
		}

		// GET: api/Categories
		[HttpGet]
		public IActionResult GetAll()
		{
			return Ok(_categoryBLL.GetAll());
		}

		// GET api/Categories/5
		[HttpGet("{id}")]
		public CategoryDTO GetById(int id)
		{
			return _categoryBLL.GetById(id);
		}

		// POST api/<Categories_Controller>
		[HttpPost]
		public IActionResult Post(CategoryCreateDTO category)
		{
			_categoryBLL.Insert(category);
			return Ok();
		}

		// PUT api/<Categories_Controller>/5
		[HttpPut("{id}")]
		public IActionResult Put(int id, [FromBody] CategoryUpdateDTO category)
		{
			_categoryBLL.Update(category);
			return Ok();
		}

		// DELETE api/<Categories_Controller>/5
		[HttpDelete("{id}")]
		public IActionResult Delete(int id)
		{
			_categoryBLL.Delete(id);
			return Ok($"Data Category {id} berhasil didelete");
		}
	}
}
