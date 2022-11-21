using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogueClient
{
    internal class ItemDto
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        public string Description { get; set; }
        
        public int Price { get; set; }
        public string ImageUrl { get; set; }
        public int? BrandId { get; set; }
        //Если бренд - необязательное свойство - указать это можно так 
        //public int? BrandId { get; set; }
        //(тогда при каскажном удалении из БД не удалятся товары этого бренда)
        // public Brand? Brand { get; set; }
        public int? CategoryId { get; set; }
        // public Category? Category { get; set; }
        public DateTime Created { get; set; }
        //public DateTime Updated { get; set; }
    }
}
