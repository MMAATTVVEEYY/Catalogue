using CatalogueWebApi.Data;
using CatalogueWebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace CatalogueWebApi.Controllers
{
    [Route("api/Category")]
    //Эта строка вставит название контроллера до слова контроллер, рут выше выглядит лучше
    //[Route("api/[controller]")]
    [ApiController]
    public class CategoryApiController : ControllerBase
    {
        private readonly CatalogueAPIDbContext _context;

        public CategoryApiController(CatalogueAPIDbContext context) => _context = context;
        
        //get
        [HttpGet]
        public async Task<IEnumerable<Category>> Get()
        {
            return await _context.Categories.ToListAsync();
        }


        //Get one
        [HttpGet("Name")]
        [ProducesResponseType(typeof(Category),StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task < IActionResult> GetById(string Name)
        {
            var Category = await _context.Categories.FindAsync(Name);
            return Category== null ? NotFound() : Ok(Category);
        }

        //Post
        [HttpPost]
        public async Task<IActionResult> Create(string NewCategoryName) 
        {   //Проверка на наличие категории с таким именем. + Запрос - нехорошо. Мб добавить ограничение в модель? Как?
            var Category = await _context.Categories.FirstOrDefaultAsync(x=>x.Name ==NewCategoryName);
            if (Category != null) return Conflict("Category with this name already exists");
            var NewCategory = new Category { Name = NewCategoryName };
            _context.Categories.Add(NewCategory);
            //await _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return CreatedAtAction("Create", NewCategory);
        }

        //Delete
        [HttpDelete("Name")]
        [ProducesResponseType(typeof(Category), StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(string Name)
        {
            var CategoryToDelete = await _context.Categories.FindAsync(Name);
            if (CategoryToDelete == null) 
            { 
                return NotFound(); 
            }
            _context.Categories.Remove(CategoryToDelete);
            await _context.SaveChangesAsync();
            return NoContent();
     
        }
        //Put
        //В запросе нужно указывать правильный id - поменять так, чтобы этого небыло - только имя
        [HttpPut("OldName, NewName")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Update(string OldName,string NewName)
        {
            var CategoryToUpdate = await _context.Categories.FindAsync(OldName);
            if (CategoryToUpdate == null)
            {
                return NotFound();
            }
            var PotentialConflictCategory = await _context.Categories.FindAsync(NewName);
            if (PotentialConflictCategory != null)
            {
                return Conflict("Brand with this NewName already exists");
            }
            var NewCategory = new Category { Name = NewName };
            _context.Categories.Remove(CategoryToUpdate);
            _context.Categories.Add(NewCategory);

            await _context.SaveChangesAsync();
            return NoContent();
        }

    }
}
