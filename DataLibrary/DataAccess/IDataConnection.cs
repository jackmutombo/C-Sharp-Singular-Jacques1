using DataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.DataAccess
{
    public interface IDataConnection
    {
        UserModel GetUserById(string AuthUserId);
        UserModel CreateUser(UserModelAPI model);
        List<UserModel> GetUser_All();
        List<ProductModel> GetProducts();



        ProductModel CreateProduct(ProductModel model);
        ProductModel GetProductById(int ProductId);
    


        void SaveBasket(BasketModelAPI basket, string AuthUserId);
        BasketModel GetBasketById(int Id);
        BasketDetailModel GetBasketDetailById(int Id);
        List<BasketModel> GetBasket_All();
        void UpdateBasket(BasketModel model);


        BasketDetailModel CreateBasketDetail(BasketDetailModel model);
        List<BasketDetailModel> GetBasketDetail_All();
        void UpdateQuantityBasketDetail(BasketDetailModel basketDetail);
    }
}
