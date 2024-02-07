using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class TagDTO
    {
        
        public int Id { get; set; }
        [Required(ErrorMessage = "Tag Name Required")]
        public string Name { get; set; }
        public int tagTypeId { get; set; }
        public int storeId { get; set; }
    }
}
