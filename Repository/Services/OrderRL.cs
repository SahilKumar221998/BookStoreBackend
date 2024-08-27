using Dapper;
using Model.Request;
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
    public class OrderRL:IOrderRL
    {
        private readonly DapperContext context;

        public OrderRL(DapperContext context)
        {
            this.context = context;
        }
        public List<object> AddOrder(List<OrderRequest> requests, int userId)
        {
            var results = new List<object>();

            try
            {
                using (IDbConnection connection = context.CreateConnection())
                {
                    foreach (var request in requests)
                    {
                        var parameters = new DynamicParameters();
                        parameters.Add("@addressId", request.addressId);
                        parameters.Add("@orderDate", DateTime.Now);
                        parameters.Add("@bookId", request.bookId);
                        parameters.Add("@userId", userId);

                        connection.Execute("spAddOrder", parameters, commandType: CommandType.StoredProcedure);
                        results.Add("Order placed successfully");
                    }
                }
            }
            catch (Exception ex)
            {
                results.Add(new { Error = ex.Message });
            }

            return results;
        }

        public List<object> GetOrder(int userId)
        {
            try
            {
                using (var connection = context.CreateConnection())
                {
                    var orders = connection.Query<object>("spGetOrdersByUserId", new { userId = userId }, commandType: CommandType.StoredProcedure);
                    return orders.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error: {ex.Message}");
            }
        }
    }
}
