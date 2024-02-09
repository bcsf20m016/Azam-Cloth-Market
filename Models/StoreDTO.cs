using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class StoreDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please Enter Store Name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Enter Your Store Type")]
        public string Type { get; set; }
        [Required(ErrorMessage = "Enter Country Name")]
        public string Country { get; set; }
        public float? maxDiscount { get; set; }
        [Required]
        public string AdminName { get; set; }
        public string Image { get; set; }
        public int ClickCount { get; set; }

        public string Address { get; set; }
        public string? Description { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]{4}-[0-9]{7}$", ErrorMessage = "Phone number must follow the pattern 'XXXX-XXXXXXX'.")]
        public string phoneNo { get; set; }
        public virtual ICollection<StoreImageDTO> StoreImages { get; set; }
        public virtual ICollection<StoreAttributesDTO> Attributes { get; set; }
        public List<string> ImageUrls { get; set; }

        public string UserId { get; set; }
        [Required]
        public string Block { get; set; }
    }
}