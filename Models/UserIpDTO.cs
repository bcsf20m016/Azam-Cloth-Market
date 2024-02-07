using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Models
{
    public class UserIpDTO
    {
        public int ID { get; set; }

        [JsonPropertyName("ip")]
        public string IP { get; set; }

        [JsonPropertyName("geo-ip")]
        public string GeoIP { get; set; }

        [JsonPropertyName("API Help")]
        public string APIHelp { get; set; }
        public DateTime timeNow { get; set; }
        public int StoreId { get; set; }
    }
}
