using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class StoreAttributesDTO
    {
        [Required]
        public string Key { get; set; }
        [Required]
        public string Value { get; set; }
        public StoreDTO Store { get; set; }
    }
}
