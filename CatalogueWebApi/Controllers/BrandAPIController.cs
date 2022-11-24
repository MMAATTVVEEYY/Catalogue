using CatalogueWebApi.Data;
using CatalogueWebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CatalogueWebApi.Controllers
{
    [Route("api/Brand")]
    [ApiController]
    public class BrandAPIController : ControllerBase
    {
        private readonly CatalogueAPIDbContext _context;

        public BrandAPIController(CatalogueAPIDbContext context) => _context = context;

        [HttpGet]
        public async Task<IEnumerable<Brand>> Get() 
        {
            return await _context.Brands.ToListAsync();
        }

        //Get one нужен вобще?
        [HttpGet("name")]
        [ProducesResponseType(typeof(Brand), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(string name)
        {
            var Brand = await _context.Brands.FindAsync(name);
            return Brand == null ? NotFound() : Ok(Brand);
        }

        //Post
        [HttpPost]
        public async Task<IActionResult> Create(string NewBrandName)
        {   //Проверка на наличие категории с таким именем. + Запрос - нехорошо. Мб добавить ограничение в модель? Как?
            var Brand = await _context.Brands.FirstOrDefaultAsync(x => x.Name == NewBrandName);
            if (Brand != null) return Conflict("Brand with this name already exists");
            var NewBrand = new Brand { Name = NewBrandName };
            _context.Brands.Add(NewBrand);
            //await _context.Brands.Add(brand);
            await _context.SaveChangesAsync();
            return CreatedAtAction("Create",Brand);
        }

        //Delete
        [HttpDelete("BrandName")]
        [ProducesResponseType(typeof(Brand), StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(string BrandName)
        {
            var BrandToDelete = await _context.Brands.FindAsync(BrandName);
            if (BrandToDelete == null)
            {
                return NotFound();
            }
            _context.Brands.Remove(BrandToDelete);
            await _context.SaveChangesAsync();
            return NoContent();

        }
        
        //Put
        [HttpPut("OldName,NewName")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Update(string OldName,string NewName)
        {
            var BrandToUpdate = await _context.Brands.FindAsync(OldName);
            if (BrandToUpdate == null)
            {
                return NotFound();
            }
            var PotentialConflictBrand = await _context.Brands.FindAsync(NewName);
            if (PotentialConflictBrand!= null)
            {
                return Conflict("Brand with this NewName already exists");
            }
            var NewBrand = new Brand { Name = NewName };
            _context.Brands.Remove(BrandToUpdate);
            _context.Brands.Add(NewBrand);

            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
