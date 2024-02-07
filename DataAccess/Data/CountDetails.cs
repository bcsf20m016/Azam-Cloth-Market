using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Data
{
    public class CountDetails
    {
        [Key]
        public int id { get; set; }
        public DateTime date { get; set; }
        [ForeignKey("StoreId")]
        public int StoreId { get; set; }
        public int clicks { get; set; }
    }
}
