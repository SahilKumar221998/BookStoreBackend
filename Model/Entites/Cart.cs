using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entites
{
    public class Cart
    {
        public int cartId { get; set; }
        public int quantity { get; set; }
        public int userId { get; set; }
        public int bookId { get; set; }
        public bool isOrdered { get; set; } = false;
        public bool isUnCarted { get; set; } = false;
    }
}
