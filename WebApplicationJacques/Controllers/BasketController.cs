using DataLibrary;
using DataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplicationJacques.Helper;

namespace WebApplicationJacques.Controllers
{
    [System.Web.Http.Authorize]
    public class BasketController : ApiController
    {
        public List<BasketModel> Get()
        {
            return GlobalConfig.Connection.GetBasket_All();
        }
        public BasketModel Get(int id)
        {
            return GlobalConfig.Connection.GetBasket_All().Where(x => x.OrderBy.Id == id).FirstOrDefault();
        }
        public void Post(BasketOrder value)
        {
            MapData.MapToBasketOrderAndUpdate(value);
        }

    }
}
