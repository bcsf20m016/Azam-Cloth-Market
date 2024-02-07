using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class ProductDTO
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Please Enter Product Name")]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required(ErrorMessage = "Please Enter Product Quantity")]
        [Range(0, Int32.MaxValue, ErrorMessage = "Quantity should be positive")]

        public int Quantity { get; set; }
        [Required(ErrorMessage = "Please Enter Product Price")]
        [Range(0.0, Double.MaxValue, ErrorMessage = "Price should be positive")]
        public float Price { get; set; }
        public string Image { get; set; }
        public int CategoryId { get; set; }
        public int StoreId { get; set; }
        [Range(0, 100, ErrorMessage = "Discount value must be from 0 to 100")]
        public float? DiscountPercentage { get; set; }
        [ReadOnly(true)]

        public float? DiscountPrice
        {
            get
            {
                if (DiscountPercentage >= 0 && DiscountPercentage <= 100)
                {
                    return Price - ((DiscountPercentage / 100) * Price);
                }
                return null;
            }
        }
        public int ClickCount { get; set; }
        public virtual ICollection<ProductImageDTO> ProductImages { get; set; }
        public List<string> ImageUrls { get; set; }

        public virtual ICollection<ProductAttributesDTO> Attributes { get; set; }
    }
}