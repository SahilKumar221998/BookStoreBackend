using Model.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface
{
    public interface IWishListRL
    {
        bool addWishList(WishList wishList);
        bool deleteWishList(int uId, int wishListId);
        List<Object> getWishList(int uId);
    }
}
