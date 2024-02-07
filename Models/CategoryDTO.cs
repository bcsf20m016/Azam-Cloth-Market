using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace Models
{
    public class CategoryDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Category Name Required!")]
        public string Name { get; set; }
        public string Description { get; set; }
        public int StoreId { get; set; }
        public int ClickCount { get; set; }
        [Range(0, 100, ErrorMessage = "Discount value must be from 0 to 100")]
        public float? DiscountPercentage { get; set; }
        public List<ProductDTO> Products { get; set; }

        public List<CategoryAttributesDTO> Attributes { get; set; }
    }
}