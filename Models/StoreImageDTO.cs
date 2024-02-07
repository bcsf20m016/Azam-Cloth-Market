using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class StoreImageDTO
    {
        public int Id { get; set; }
        public int StoreId { get; set; }
        public string StoreImageUrl { get; set; }
    }
}
