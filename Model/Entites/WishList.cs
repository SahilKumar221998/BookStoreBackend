using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entites
{
    public class WishList
    {
        public int WishListId { get; set; }
        public int UserId { get; set; }
        public int BookId { get; set; }
    }
}
