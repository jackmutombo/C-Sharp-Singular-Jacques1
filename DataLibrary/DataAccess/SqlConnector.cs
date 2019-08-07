using DataLibrary.Internal.DataAccess;
using DataLibrary.Models;
using DataLibrary.Models.MapClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.DataAccess
{
    public class SqlConnector : IDataConnection
    {
        private const String databaseNameConn = "databasetestjacques";
        public UserModel GetUserById(string AuthUserId)
        {
            SqlDataAccess sql = new SqlDataAccess();
            var p = new { AuthUserId };

            var output = sql.LoadData<UserModel, dynamic>("dbo.spUserLookup", p, databaseNameConn);

            return output.FirstOrDefault();
        }

        public UserModel CreateUser(UserModelAPI model)
        {
            UserForGeneric data = MapUser.USER_API_GENRIC(model);
            //UserModel newModel = MapUser.USERApiToDB(model);
            SqlDataAccess sql = new SqlDataAccess();
             sql.SaveData("dbo.spUser_Insert", data, databaseNameConn);
            model.Address.AuthUserId = model.AuthUserId;

            CreateAddress(model.Address);

            return MapUser.USERApiToDB(model); ;
        }


        public AddressModel CreateAddress(AddressModel model)
        {
            SqlDataAccess sql = new SqlDataAccess();
            sql.SaveData("dbo.spAddress_Insert", model, "SingularTestJacquesData");

            return model;
        }


        public List<ProductModel> GetProducts()
        {
            SqlDataAccess sql = new SqlDataAccess();


            var output = sql.LoadData<ProductModel, dynamic>("dbo.spProduct_GetAll", new { }, "SingularTestJacquesData");

            return output;
        }

        public void SaveBasket(BasketModelAPI basketInfo, string AuthUserId)
        {
            // Start filling the basket detail model that will be save to the db
            List<BasketDetailDBModel> details = new List<BasketDetailDBModel>();


            foreach (var item in basketInfo.BasketDetails)
            {
                var detail = new BasketDetailDBModel
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity
                };

                // Get the information about this product
                var ProductInfo = GlobalConfig.Connection.GetProductById(detail.ProductId);

                if (ProductInfo == null)
                {
                    throw new Exception($"The product Id of {detail.ProductId} could not be found in the database");
                }

                detail.Price = ProductInfo.Price * detail.Quantity;

                details.Add(detail);
            }

            //create the Basket Database model
            BasketDBModel basket = new BasketDBModel
            {
                TotalAmount = details.Sum(x => x.Price),
                AuthUserId = AuthUserId
            };

            // Save the Basket BD model to the database and //Get the ID from the basket model
            SqlDataAccess sql = new SqlDataAccess();
            basket.Id = sql.SaveDataGetId("dbo.spBasket_Insert", basket, "SingularTestJacquesData");





            //Finish filling in the basket detail model
            foreach (var item in details)
            {
                item.BasketId = basket.Id;

                //Save the sale detail models
                sql.SaveData("dbo.spBasketDetail_Insert", item, "SingularTestJacquesData");
            }

        }

        public ProductModel GetProductById(int ProductId)
        {
            SqlDataAccess sql = new SqlDataAccess();

            var output = sql.LoadData<ProductModel, dynamic>("dbo.spProduct_GetById", new { Id = ProductId }, "SingularTestJacquesData").FirstOrDefault();

            return output;
        }

        public ProductModel CreateProduct(ProductModel model)
        {
            throw new NotImplementedException();
        }

        public BasketModel GetBasketById(int Id)
        {
            throw new NotImplementedException();
        }

        public BasketDetailModel GetBasketDetailById(int Id)
        {
            throw new NotImplementedException();
        }

        public List<BasketModel> GetBasket_All()
        {
            throw new NotImplementedException();
        }

        public void UpdateBasket(BasketModel model)
        {
            throw new NotImplementedException();
        }

        public List<BasketDetailModel> GetBasketDetail_All()
        {
            throw new NotImplementedException();
        }

        public void UpdateQuantityBasketDetail(BasketDetailModel basketDetail)
        {
            throw new NotImplementedException();
        }

        public List<UserModel> GetUser_All()
        {
            throw new NotImplementedException();
        }

        public BasketDetailModel CreateBasketDetail(BasketDetailModel model)
        {
            throw new NotImplementedException();
        }
    }
}
