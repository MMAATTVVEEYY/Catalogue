using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace CatalogueWebApi.Models
{
    public class Brand
    {
        [Key]
        public string Name { get; set; }
        
    }
}
