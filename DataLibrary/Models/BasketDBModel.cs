using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.Models
{
    public class BasketDBModel
    {
        public int Id { get; set; }
        public string AuthUserId { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
