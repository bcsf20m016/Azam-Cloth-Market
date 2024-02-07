using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Data
{
    public class StoreTags
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("tagId")]
        public int tagId { get; set; }
        public string tagName { get; set; }
        [ForeignKey("storeId")]
        public int storeId { get; set; }
    }
}
