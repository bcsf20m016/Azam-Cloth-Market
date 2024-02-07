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
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Category Name Required!")]
        public string Name { get; set; }
        public string Description { get; set; }
        [ForeignKey("StoreId")]
        public int StoreId { get; set; }
        public float? DiscountPercentage { get; set; }
        
        
        public int ClickCount { get; set; } 
        
        
        public List<Product> Products { get; set; }
        //public List<Store> Stores{get;set;}

        public List<CategoryAttributes> Attributes { get; set; }
    }
}
