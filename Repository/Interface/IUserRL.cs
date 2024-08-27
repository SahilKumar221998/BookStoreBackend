using Model.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface
{
    public interface IUserRL
    {
        bool createUser(User entity);
        User login(string userEmail);
    }
}
