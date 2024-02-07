using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class CountDetailsDTO
    {
        public int id { get; set; }
        public DateTime date { get; set; }

        public int StoreId { get; set; }
        public int clicks { get; set; }
    }
}
