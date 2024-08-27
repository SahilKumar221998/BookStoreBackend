using Business.Interface;
using Model.Entites;
using Model.Request;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public class WishListBL:IWhishListBL
    {
        private readonly IWishListRL repository;
        public WishListBL(IWishListRL repository)
        {
            this.repository = repository;
        }
        private WishList mapToEntity(WishListRequest request, int Uid)
        {
            return new WishList
            {
                BookId = request.BookId,
                UserId = Uid
            };
        }
        public bool addWishList(WishListRequest request, int Uid)
        {
            return repository.addWishList(mapToEntity(request, Uid));
        }

        public bool deleteWishList(int uId, int wishListId)
        {
            return repository.deleteWishList(uId, wishListId);
        }

        public List<object> getWishList(int uId)
        {
            return repository.getWishList(uId);
        }
    }
}
