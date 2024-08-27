using Business.Interface;
using Model.Entites;
using Model.Request;
using Model.Response;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public class CartBL:ICartBL
    {
        private readonly ICartRL cartRepo;
        public CartBL(ICartRL cartRepo)
        {
            this.cartRepo = cartRepo;
        }
        private Cart MapToEntity(CartRequest request)
        {
            return new Cart
            {
                bookId = request.bookId,
                quantity = request.quantity,
                userId = request.userId
            };
        }
        public int addCart(CartRequest request)
        {
            bool flag = true;
            List<CartResponse> li = cartRepo.getByUserId(request.userId);
            if (li == null || !li.Any())
            {
                return cartRepo.addCart(MapToEntity(request));
            }

            foreach (var item in li)
            {


                if (item.BookId == request.bookId)
                {
                    if (item.IsOrdered)
                    {
                        flag = true;
                        break;
                    }
                    else if (item.isUnCarted)
                    {
                        flag = true;
                        break;
                    }
                    else
                        flag = false;

                }


            }

            if (flag)
                return cartRepo.addCart(MapToEntity(request));
            else
                return 0;
        }

        public List<CartResponse> getByUserId(int id)
        {
            return cartRepo.getByUserId(id);
        }

        public bool unCart(int cartId, int userId)
        {
            return cartRepo.unCart(cartId, userId);
        }

        public bool updateCartOrder(int cartId, bool isOrdered)
        {
            return cartRepo.updateCartOrder(cartId, isOrdered);
        }

        public bool updateCartquantity(int cartId, int quantity)
        {
            return cartRepo.updateCartquantity(cartId, quantity);
        }
    }
}
