using DataLibrary;
using DataLibrary.Internal.AccessHelper;
using DataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplicationJacques.Helper
{
    public static class MapData
    {
        public static void MapToBasketOrderAndUpdate(BasketOrder basketOrder)
        {
            BasketModel model = GlobalConfig.Connection.GetBasketById(basketOrder.Id);
            foreach (BasketDetailOrder bDetO in basketOrder.BasketDeatils)
            {
                if (model.BasketDetails.Where(x => x.Product.Id == bDetO.ProductId).ToList().Count > 0)
                {
                    BasketDetailModel bDM = model.BasketDetails.Where(x => x.Product.Id == bDetO.ProductId).ToList().FirstOrDefault();
                    bDM.Quantity = bDetO.quantity;
                    bDM.Price = bDetO.quantity * bDM.Product.Price;
                    GlobalConfig.Connection.UpdateQuantityBasketDetail(bDM);
                    // update BasketModel file
                    List<BasketDetailModel> listBasketDetail = model.BasketDetails;
                    listBasketDetail.Remove(model.BasketDetails.Where(x => x.Product.Id == bDetO.ProductId).ToList().FirstOrDefault());
                    listBasketDetail.Add(bDM);
                    model.BasketDetails = listBasketDetail;
                    GlobalConfig.Connection.UpdateBasket(model.UpdateTotalAmountBasket());

                }
                else
                {
                    BasketDetailModel newBasketDe = new BasketDetailModel();
                    newBasketDe.Quantity = bDetO.quantity;
                    newBasketDe.Product = bDetO.ProductId.ToString().LookupProductById();
                    newBasketDe.Price = bDetO.quantity * newBasketDe.Product.Price;
                    newBasketDe.Time = DateTime.Now;
                    GlobalConfig.Connection.CreateBasketDetail(newBasketDe);
                    // TODO: update BasketModel file
                    List<BasketDetailModel> listBasketDetail = model.BasketDetails;
                    listBasketDetail.Add(newBasketDe);
                    model.BasketDetails = listBasketDetail;
                    GlobalConfig.Connection.UpdateBasket(model.UpdateTotalAmountBasket());

                }
            }


        }
        public static BasketModel UpdateTotalAmountBasket(this BasketModel Bmodel)
        {
            decimal total = 0.0M;
            foreach (BasketDetailModel bsDM in Bmodel.BasketDetails)
                total += bsDM.Price;
            Bmodel.TotalAmount = total;
            return Bmodel;
        }
        public static List<BasketModel> UpdateTotalAmountAllBaskets(this List<BasketModel> Bmodels)
        {

            foreach (BasketModel bskM in Bmodels)
            {
                decimal total = 0.0M;
                foreach (BasketDetailModel bsDM in bskM.BasketDetails)
                    total += bsDM.Price;
                bskM.TotalAmount = total;

            }

            return Bmodels;
        }
    }
}