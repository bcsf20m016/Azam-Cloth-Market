using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Data
{
    public class StoreImage
    {
        public int Id { get; set; }
        public int? StoreId { get; set; }
        public string StoreImageUrl { get; set; }
        [ForeignKey("StoreId")]
        public virtual Store store { get; set; }
    }
}