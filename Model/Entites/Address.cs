using Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entites
{
    public class Address
    {
        public int addressId { get; set; }
        public String address { get; set; }
        public String city { get; set; }
        public String state { get; set; }
        public AddressTypes type { get; set; }
        public int userId { get; set; }
        public string name { get; set; }
        public long mobileNumber { get; set; }
    }
}
