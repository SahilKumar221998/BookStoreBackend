using Model.Entites;
using Model.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interface
{
    public interface IAddressBL
    {
        bool addAddress(AddressRequest addressRequest, int userId);
        bool deleteAddress(int addressId);
        List<Address> getAllAddress(int userId);
        bool updateAddress(AddressRequest address, int addressId);
    }
}
