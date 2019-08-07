using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.Models
{
    public class AddressModel
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string AddressName { get; set; }
        public string Street { get; set; }
        public string Suburb { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string AuthUserId { get; set; }
    }
}
