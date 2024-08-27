using Dapper;
using Model.Entites;
using Model.Response;
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
    public class CartRL:ICartRL
    {
        private readonly DapperContext _dapperContext;
        public CartRL(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }

        public int addCart(Cart cart)
        {
            IDbConnection connection = _dapperContext.CreateConnection();
            var parameters = new
            {
                quantity = cart.quantity,
                userId = cart.userId,
                bookId = cart.bookId,
                isOrdered = cart.isOrdered,
                isUnCarted = cart.isUnCarted,
            };
            try
            {
                int nora = connection.Query("addCart", parameters, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return nora;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while adding the cart.", ex);
            }
        }


        public List<CartResponse> getByUserId(int id)
        {
            IDbConnection connection = _dapperContext.CreateConnection();
            try
            {
                return connection.Query<CartResponse>("GetCartByUserId", new { userId = id }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving the cart by user ID.", ex);
            }
        }

        public bool unCart(int cartId, int userId)
        {
            using (IDbConnection con = _dapperContext.CreateConnection())
            {
                try
                {
                    int rowsAffected = con.Execute("uncart", new { cartId = cartId }, commandType: CommandType.StoredProcedure);
                    return rowsAffected > 0;
                }
                catch (Exception ex)
                {
                    throw new Exception("An error occurred while uncarting the item.", ex);
                }
            }
        }

        public bool updateCartOrder(int cartId, bool isOrdered)
        {
            IDbConnection con = _dapperContext.CreateConnection();
            string storedProcedure = "UpdateCartOrder";

            try
            {
                int rowsAffected = con.Execute(storedProcedure, new { CartId = cartId, IsOrdered = isOrdered }, commandType: CommandType.StoredProcedure);
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {

                throw new Exception("An error occurred while updating the cart order status.", ex);
            }
        }

        public bool updateCartquantity(int cartId, int quantity)
        {
            IDbConnection con = _dapperContext.CreateConnection();
            string storedProcedure = "UpdateCartQuantity";

            try
            {
                int rowsAffected = con.Execute(storedProcedure, new { CartId = cartId, Quantity = quantity }, commandType: CommandType.StoredProcedure);
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {

                throw new Exception("An error occurred while updating the cart quantity.", ex);
            }
        }
    }
}
