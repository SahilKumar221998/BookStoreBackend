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
    public class AddressRL:IAddressRL
    {
        private readonly DapperContext context;

        public AddressRL(DapperContext context)
        {
            this.context = context;
        }

        public bool addAddress(Address address)
        {
            using (IDbConnection con = context.CreateConnection())
            {
                try
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("address", address.address);
                    parameters.Add("city", address.city);
                    parameters.Add("state", address.state);
                    parameters.Add("type", address.type);
                    parameters.Add("userId", address.userId);
                    parameters.Add("name", address.name);
                    parameters.Add("mobileNumber", address.mobileNumber);

                    con.Execute("spInsertAddress", parameters, commandType: CommandType.StoredProcedure);
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
            }
        }

        public bool deleteAddress(int addressId)
        {
            using (IDbConnection con = context.CreateConnection())
            {
                try
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("addressId", addressId);

                    int rowsAffected = con.Execute("spDeleteAddress", parameters, commandType: CommandType.StoredProcedure);
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
            }
        }

        public List<Address> getAllAddress(int userId)
        {
            using (IDbConnection con = context.CreateConnection())
            {
                return con.Query<Address>("GetAddressByUserId", new { userId = userId }, commandType: CommandType.StoredProcedure).ToList();
            }
        }

        public bool updateAddress(Address address)
        {
            using (IDbConnection con = context.CreateConnection())
            {
                try
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("addressId", address.addressId);
                    parameters.Add("address", address.address);
                    parameters.Add("city", address.city);
                    parameters.Add("state", address.state);
                    parameters.Add("type", address.type);
                    parameters.Add("name", address.name);
                    parameters.Add("mobileNumber", address.mobileNumber);

                    int rowsAffected = con.Execute("UpdateAddress", parameters, commandType: CommandType.StoredProcedure);
                    return true;
                }
                catch (Exception ex)
                {
                    // Log the exception (e.g., using a logging framework)
                    Console.WriteLine(ex.Message);
                    return false;
                }
            }
        }
    }
}
