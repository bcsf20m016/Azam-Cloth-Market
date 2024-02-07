using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Data
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Quantity { get; set; }
        public float Price { get; set; }
        public float? DiscountPercentage { get; set; }
        public float? DiscountPrice { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int StoreId { get; set; }
        public Store Store { get; set; }
        
        public int ClickCount { get; set; }
        public virtual ICollection<ProductImage> ProductImages { get; set; }

        public virtual ICollection<ProductAttributes> Attributes { get; set; }

    }
}
