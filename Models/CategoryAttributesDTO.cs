using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class CategoryAttributesDTO
    {
        [Required]
        public string Key { get; set; } //It's the attribute name

        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public CategoryDTO Category { get; set; }
    }
}
