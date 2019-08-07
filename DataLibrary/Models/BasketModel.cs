using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.Models
{
    public class BasketModel
    {
        public int Id { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime Time { get; set; }
        public bool Active { get; set; }
        public UserModel OrderBy { get; set; }
        public List<BasketDetailModel> BasketDetails { get; set; } = new List<BasketDetailModel>();
    }
}
