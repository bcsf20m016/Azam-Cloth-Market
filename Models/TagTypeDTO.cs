using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class TagTypeDTO
    {
        [Key]
        public int id { get; set; }
        [Required(ErrorMessage = "Type Name Required")]
        public string name { get; set; }
    }
}
