using Model.Entites;
using Repository.Context;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace Repository.Services
{
    public class UserRL:IUserRL
    {
        private readonly DapperContext context;
        public UserRL(DapperContext context)
        {
            this.context = context;
        }
        public bool createUser(User entity)
        {
            IDbConnection con = context.CreateConnection();
            using (SqlCommand cmd = new SqlCommand("createUser", (SqlConnection)con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@name", entity.name);
                cmd.Parameters.AddWithValue("@email", entity.email);
                cmd.Parameters.AddWithValue("@password", entity.password);
                cmd.Parameters.AddWithValue("@mobileNumber", entity.mobileNumber);
                con.Open();
                int rows = cmd.ExecuteNonQuery();
                return rows > 0;
            }

        }
        public User login(string userEmail)
        {
            using (IDbConnection con = context.CreateConnection())
            {
                var parameters = new { email = userEmail };
                var entity = con.Query<User>("login", parameters, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return entity;
            }
        }
    }
}
