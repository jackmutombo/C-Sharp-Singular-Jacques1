using DataLibrary.Internal.AccessHelper;
using DataLibrary.Models;
using DataLibrary.Models.MapClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.DataAccess
{
    class TextConnector : IDataConnection
    {

        public UserModel CreateUser(UserModelAPI model)
        {
            //get all user
            List<UserModel> user = TextConnectorProcessor.UserFile.FullFilePath().LoadFile().ConvertToUserModels();
            if (user.Where(st => st.CellPhone == model.CellPhone && st.EmailAddress == model.EmailAddress).ToList().Count() > 0)
            {
                return user.Where(st => st.CellPhone == model.CellPhone).FirstOrDefault();
            }
            int currentId = 1;
            if (user.Count > 0)
            {
                currentId = user.OrderByDescending(x => x.Id).First().Id + 1;
            }

            UserModel newModel = MapUser.USERApiToDB(model);
            newModel.Id = currentId;

            user.Add(newModel);
            user.SaveToPeopleFile();

            //create the address
            CreateAddress(newModel);

            CreateBasket(new BasketModel { Time = DateTime.Now, Active = true, OrderBy = newModel });

            return newModel;

        }
        public List<UserModel> GetUser_All()
        {
            return TextConnectorProcessor.UserFile.FullFilePath().LoadFile().ConvertToUserModels();
           
        }

        public UserModel GetUserById(string AuthUserId)
        {
            return TextConnectorProcessor.LookupUserById(AuthUserId);
        }


        public AddressModel CreateAddress(UserModel model)
        {
            List<AddressModel> addresses = TextConnectorProcessor.AddressFile.FullFilePath().LoadFile().ConvertToAddressModels();
            int currentId = 1;
            if (addresses.Count > 0)
            {
                currentId = addresses.OrderByDescending(x => x.Id).First().Id + 1;
            }
            AddressModel modelAddress = model.Address;

            modelAddress.Id = currentId;
            addresses.Add(modelAddress);

            addresses.SaveToAddressFile();

            return modelAddress;
        }



        public ProductModel CreateProduct(ProductModel model)
        {
            List<ProductModel> products = TextConnectorProcessor.ProductFile.FullFilePath().LoadFile().ConvertToProductModels();
            int currentId = 1;
            if (products.Count > 0)
            {
                currentId = products.OrderByDescending(x => x.Id).First().Id + 1;
            }
            model.Id = currentId;
            products.Add(model);

            products.SaveToProductFile();

            return model;
        }

        public BasketModel CreateBasket(BasketModel model)
        {
            List<BasketModel> Baskets = TextConnectorProcessor.BasketFile.FullFilePath().LoadFile().ConvertToBasketModels();
            int currentId = 1;
            if (Baskets.Count > 0)
            {
                currentId = Baskets.OrderByDescending(x => x.Id).First().Id + 1;
            }
            model.Id = currentId;
            Baskets.Add(model);
            Baskets.SaveToBasketFile();
            return model;
        }
        public BasketDetailModel CreateBasketDetail(BasketDetailModel model)
        {
            List<BasketDetailModel> OrderDetails =
                TextConnectorProcessor.BasketDetailFile.FullFilePath().LoadFile().ConvertToBasketDetailModels();
            int currentId = 1;
            if (OrderDetails.Count > 0)
            {
                currentId = OrderDetails.OrderByDescending(x => x.Id).First().Id + 1;
            }
            model.Id = currentId;
            OrderDetails.Add(model);
            OrderDetails.SaveToBasketDetailFile();
            return model;
        }

        public void UpdateBasket(BasketModel model)
        {
            List<BasketModel> Baskets = TextConnectorProcessor.BasketFile.FullFilePath().LoadFile().ConvertToBasketModels();
            BasketModel oldBasket = Baskets.Where(x => x.Id == model.Id).FirstOrDefault();

            bool revomebd = Baskets.Remove(oldBasket);
            Baskets.Add(model);
            Baskets.SaveToBasketFile();
        }


        public List<BasketModel> GetBasket_All()
        {
            return TextConnectorProcessor.BasketFile.FullFilePath().LoadFile().ConvertToBasketModels();
        }
        public BasketModel GetBasketById(int Id)
        {
            return TextConnectorProcessor.LookupBasketById(Id.ToString());
        }



        public List<ProductModel> GetProducts()
        {
            return TextConnectorProcessor.ProductFile.FullFilePath().LoadFile().ConvertToProductModels();
        }
        public ProductModel GetProductById(int ProductId)
        {
            return TextConnectorProcessor.LookupProductById(ProductId.ToString());
        }


       

        public BasketDetailModel GetBasketDetailById(int Id)
        {
            throw new NotImplementedException();
        }

        public List<BasketDetailModel> GetBasketDetail_All()
        {
            throw new NotImplementedException();
        }

       

      

       

        public List<UserModel> GetStaffs_All()
        {
            throw new NotImplementedException();
        }

       

        public void SaveBasket(BasketModel basket, string AuthUserId)
        {
            throw new NotImplementedException();
        }


        public void UpdateQuantityBasketDetail(BasketDetailModel basketDetail)
        {
            throw new NotImplementedException();
        }

        public void SaveBasket(BasketModelAPI basket, string AuthUserId)
        {
            throw new NotImplementedException();
        }

    }
}
