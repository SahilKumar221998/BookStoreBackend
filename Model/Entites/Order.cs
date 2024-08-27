using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entites
{
    public class Order
    {
        public int orderId { get; set; }
        public int addressId { get; set; }
        public int userId { get; set; }
        public DateTime orderDate { get; set; }
        public int bookId { get; set; }
    }
}
