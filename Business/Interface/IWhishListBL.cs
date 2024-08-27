using Model.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interface
{
    public interface IWhishListBL
    {
        bool addWishList(WishListRequest request, int Uid);
        bool deleteWishList(int uId, int cartId);
        List<Object> getWishList(int uId);
    }
}
