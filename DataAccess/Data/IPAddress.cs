using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Data
{
    public class IPAddress
    {
        [Key]
        public int ID { get; set; }
        [JsonPropertyName("ip")]
        public string IP { get; set; }
        public DateTime timeNow { get; set; }

        [ForeignKey("StoreId")]

        public int StoreId { get; set; }

    }
}
