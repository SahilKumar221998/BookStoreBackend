using Dapper;
using Model.Entites;
using Repository.Context;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Services
{
    public class WishListRL:IWishListRL
    {
        private readonly DapperContext context;
        public WishListRL(DapperContext context)
        {
            this.context = context;
        }
        public bool addWishList(WishList wishList)
        {
            using (var connection = context.CreateConnection())
            {
                var storedProcedure = "AddWishList";

                var parameters = new
                {
                    UserId = wishList.UserId,
                    BookId = wishList.BookId
                };

                try
                {
                    var result = connection.Execute(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
                    return result > 0;
                }
                catch (Exception ex)
                {
                    throw new Exception("An error occurred while adding to the wish list.", ex);
                }
            }
        }

        public bool deleteWishList(int uId, int wishListId)
        {
            using (var connection = context.CreateConnection())
            {
                var storedProcedure = "DeleteWishList";

                var parameters = new
                {
                    UserId = uId,
                    WishListId = wishListId
                };

                try
                {
                    var result = connection.Execute(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
                    return result > 0;
                }
                catch (Exception ex)
                {
                    // Handle the exception as needed
                    // Log exception
                    throw new Exception("An error occurred while deleting from the wish list.", ex);
                }
            }
        }

        public List<object> getWishList(int uId)
        {
            using (var connection = context.CreateConnection())
            {
                var storedProcedure = "GetWishList";

                try
                {
                    var wishLists = connection.Query<Object>(storedProcedure, new { UserId = uId }, commandType: CommandType.StoredProcedure).ToList();
                    return wishLists;
                }
                catch (Exception ex)
                {
                    // Handle the exception as needed
                    // Log exception
                    throw new Exception("An error occurred while retrieving the wish list.", ex);
                }
            }
        }
    }
}
