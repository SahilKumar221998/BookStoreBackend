using Business.Interface;
using Model.Request;
using Repository.Interface;
using Repository.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public class OrderBL:IOrderBL
    {
        private readonly IOrderRL order;
        public OrderBL(IOrderRL order)
        {
            this.order = order;
        }
    
        public List<object> AddOrder(List<OrderRequest> requests, int userId)
        {
            var results = new List<object>();

            foreach (var request in requests)
            {
                try
                {
                    var result = order.AddOrder(requests, userId);
                    results.Add(new { Success = true, Message = "Order placed successfully.", Data = result });
                }
                catch (Exception ex)
                {
                    results.Add(new { Success = false, Error = ex.Message });
                }
            }

            return results;
        }


        public List<object> GetOrder(int userId)
        {
            return order.GetOrder(userId);
        }
    }
}
