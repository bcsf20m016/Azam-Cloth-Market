using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class StoreTagDTO
    {
        public int tagId { get; set; }
        public string tagName { get; set; }
        public int storeId { get; set; }
    }
}
