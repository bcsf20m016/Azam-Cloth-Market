using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class ProductAttributesDTO
    {
        public int AttributeId { get; set; }
        [Required(ErrorMessage = "Please Enter Attribute Name")]
        public string Key { get; set; }
        [Required(ErrorMessage = "Please Enter Attribute Value")]
        public string Value { get; set; }
        public bool Fixed { get; set; }
        public ProductDTO Product { get; set; }
    }
}
