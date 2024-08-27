using Model.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface
{
    public interface IOrderRL
    {
        List<object> GetOrder(int userId);
        List<object> AddOrder(List<OrderRequest> requests, int userId);
    }
}
