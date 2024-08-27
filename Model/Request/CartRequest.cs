using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Model.Request
{
   public class CartRequest
    {
        public int quantity { get; set; }
        [JsonIgnore]
        public int userId { get; set; }
        public int bookId { get; set; }
    }
}
