using System.ComponentModel.DataAnnotations;

namespace CatalogueWebApi.Models
{
    public class Item
    {
        //Поле Id будет являться ключем автоматом,
        //если нужно вручную указать,что поле является ключем - применить атрибут [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public int Price { get; set; }
        public string ImageUrl { get; set; }
        [Required]
        public int Quantity { get; set; }
        public string? BrandName { get; set; }
        public string? CategoryName { get; set; }
        public DateTime Created { get; set; }


    }
}
