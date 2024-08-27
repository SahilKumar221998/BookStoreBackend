using Model.Entites;
using Model.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface
{
    public interface ICartRL
    {
        int addCart(Cart cart);
        List<CartResponse> getByUserId(int id);
        bool unCart(int cartId, int userId);
        bool updateCartOrder(int cartId, bool isOrdered);
        bool updateCartquantity(int cartId, int quantity);
    }
}
