using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.Models
{
    public class BasketOrder
    {
        public int Id { get; set; }
        public List<BasketDetailOrder> BasketDeatils { get; set; }
    }
}
