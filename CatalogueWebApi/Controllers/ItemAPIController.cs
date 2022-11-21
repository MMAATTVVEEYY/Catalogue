using CatalogueWebApi.Data;
using CatalogueWebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace CatalogueWebApi.Controllers
{
    [Route("api/Item")]
    [ApiController]
    public class ItemAPIController : ControllerBase
    {
        private readonly CatalogueAPIDbContext _context;

        public ItemAPIController(CatalogueAPIDbContext context) => _context = context;



        [HttpGet]
        public async Task<IEnumerable<Item>> Get()
        {
            return await _context.Items.ToListAsync();
        }


        //странно получилось - фильтрую по одной категории, пагинации нет
        [HttpGet("FilterField,FilterFieldValue")]
        
        public async Task<IEnumerable<Item>> GetFiltered(string FilterField, string? FilterValue)
        {   IEnumerable<Item> Items = null;
            //выборка
            switch (FilterField)
            {
                /*case "Price":

                    int a = Int32.Parse(FilterValue);
                    Console.WriteLine(a);
                    //Items = await _context.Items.Where(x => x.CategoryName == FilterValue).ToListAsync();
                    break;*/
                case "Category": Items = await _context.Items.Where(x => x.CategoryName == FilterValue).ToListAsync();
                     break;
                case "Brand": Items = await _context.Items.Where(x => x.BrandName == FilterValue).ToListAsync();
                    break;
                case "": Items = await _context.Items.ToListAsync();
                    break;
                case null: Items = await _context.Items.ToListAsync();
                    break;
                case "none":
                    Items = await _context.Items.ToListAsync();
                    break;
            };
            //сортировка



            return Items.ToList(); ;
            /*if (FilterField == "Category") { Items = await _context.Items.Where(x => x.CategoryName == FilterValue).ToListAsync(); }
            else if (FilterField == "Brand") { Items = await _context.Items.Where(x => x.BrandName == FilterValue).ToListAsync(); } */
            
            
            

        }
    

        //Get one
        [HttpGet("id")]
        [ProducesResponseType(typeof(Item), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var Item = await _context.Items.FindAsync(id);
            return Item == null ? NotFound() : Ok(Item);
        }

        //Post
        [HttpPost]
        public async Task<IActionResult> Create(Item NewItem)
        {   
            var Item = await _context.Items.FirstOrDefaultAsync(x => x.Name == NewItem.Name);
            if (Item != null) return Conflict("Item with this name already exists");
            //  проверить, что есть такой брэнд
            Category Category = await _context.Categories.FirstOrDefaultAsync(x => x.Name == NewItem.CategoryName);
            if (Category == null)
            {
                return Conflict("No such category");
            }
            //  проверить, что есть такая категория
            Brand Brand = await _context.Brands.FirstOrDefaultAsync(x => x.Name == NewItem.BrandName);
            if (Brand == null)
            {
                return Conflict("No such brand");
            }
            
            _context.Items.Add(NewItem);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = NewItem.Id }, NewItem);
        }


        //Put
        [HttpPut("id")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Update(int id, Item Item)
        {
            var ItemToUpdate = await _context.Items.FindAsync(id);
            if (ItemToUpdate == null)
            {
                return NotFound();
            }
            ItemToUpdate.Name = Item.Name;
            ItemToUpdate.Price = Item.Price;
            ItemToUpdate.Description = Item.Description;
             //  проверить, что есть такая категория
            Category Category =  await _context.Categories.FirstOrDefaultAsync(x =>x.Name ==Item.CategoryName );
            if (Category == null) {
                return Conflict($"No such category {Item.CategoryName }");
            }
            ItemToUpdate.CategoryName = Item.CategoryName;
            //  проверить, что есть такой брэнд
            Brand Brand = await _context.Brands.FirstOrDefaultAsync(x => x.Name == Item.BrandName);
            if (Brand == null)
            {
                return Conflict("No such brand");
            }
            ItemToUpdate.BrandName = Item.BrandName;
            ItemToUpdate.ImageUrl = Item.ImageUrl;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        //функции на изменение одного поля - ругается на идентичные маршруты для ендпоинтов патч.


        [HttpPatch("UpdateName/id")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> UpdateName(int id, String NewName)
        {
            var ItemToUpdate = await _context.Items.FindAsync(id);
            if (ItemToUpdate == null)
            {
                return NotFound();
            }
            ItemToUpdate.Name = NewName;
            await _context.SaveChangesAsync();
            return NoContent();
        }
        
        [HttpPatch("UpdatePrice/id")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> UpdatePrice(int id, int NewPrice)
        {
            var ItemToUpdate = await _context.Items.FindAsync(id);
            if (ItemToUpdate == null)
            {
                return NotFound();
            }
            ItemToUpdate.Price = NewPrice;
            await _context.SaveChangesAsync();
            return NoContent();
        }
        
        [HttpPatch("UpdateDescription/id")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> UpdateDescription(int id, String NewDescription)
        {
            var ItemToUpdate = await _context.Items.FindAsync(id);
            if (ItemToUpdate == null)
            {
                return NotFound();
            }
            ItemToUpdate.Description = NewDescription;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPatch("Update/BrandName")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> UpdateBrandName(int id, string NewBrandName)
        {
            var ItemToUpdate = await _context.Items.FindAsync(id);
            if (ItemToUpdate == null)
            {
                return NotFound();
            }
            Brand Brand = await _context.Brands.FirstOrDefaultAsync(x => x.Name == NewBrandName);
            if (Brand == null)
            {
                return Conflict("No such brand");
            }
            ItemToUpdate.BrandName = NewBrandName;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPatch("Update/CategoryName")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> UpdateCategoryName(int id, string NewCategoryName)
        {
            var ItemToUpdate = await _context.Items.FindAsync(id);
            if (ItemToUpdate == null)
            {
                return NotFound();
            }
            Category Category = await _context.Categories.FirstOrDefaultAsync(x => x.Name == NewCategoryName);
            if (Category == null)
            {
                return Conflict("No such Category");
            }
            ItemToUpdate.CategoryName = NewCategoryName;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPatch("UpdateImageURL/id")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> UpdateImageURL(int id, String NewImageURL)
        {
            var ItemToUpdate = await _context.Items.FindAsync(id);
            if (ItemToUpdate == null)
            {
                return NotFound();
            }
            ItemToUpdate.Description = NewImageURL;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPatch("AddQuantity/id")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> AddQuintity(int id, int AddBy)
        {
            if (AddBy <= 0)
            {
                return BadRequest($"Parameter AddBy = {AddBy} should be > 0");
            }
            var ItemToUpdate = await _context.Items.FindAsync(id);
            if (ItemToUpdate == null)
            {
                return NotFound();
            }
            ItemToUpdate.Quantity += AddBy;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPatch("ReduceQuantity/id")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> ReduceQuantity(int id, int ReduceBy)
        {
            if (ReduceBy <= 0)
            {
                return BadRequest($"Parameter ReduceBy = {ReduceBy} should be > 0");
            }
            var ItemToUpdate = await _context.Items.FindAsync(id);
            if (ItemToUpdate == null)
            {
                return NotFound();
            }
            if (ItemToUpdate.Quantity - ReduceBy >= 0)
            {
                ItemToUpdate.Quantity -= ReduceBy;
                await _context.SaveChangesAsync();
            }
            else
            {
                return Conflict($"Parameter ReduceBy = {ReduceBy} is too high, there is only {ItemToUpdate.Quantity} items in stock");
            }
            return NoContent();
        }



        //Delete
        [HttpDelete("id")]
        [ProducesResponseType(typeof(Item), StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var ItemToDelete = await _context.Items.FindAsync(id);
            if (ItemToDelete == null)
            {
                return NotFound();
            }
            _context.Items.Remove(ItemToDelete);
            await _context.SaveChangesAsync();
            return NoContent();
        }

    }
}
