using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CatalogueWebApi.Models
{
    public class Category
    {
        [Key]
       // [Index(IsUnique = true)] - не работает
        public string Name { get; set; }
        
    }
}
