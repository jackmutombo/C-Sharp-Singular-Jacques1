using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.Models.MapClass
{
    public static class MapUser
    {
        public static UserModel USERApiToDB(UserModelAPI model)
        {
            return new UserModel
            {
                AuthUserId = model.AuthUserId,
                FirstName = model.FirstName,
                LastName = model.LastName,
                EmailAddress = model.EmailAddress,
                CellPhone = model.CellPhone,
                Address = model.Address,
             
              
            };
        }


        public static UserForGeneric USER_API_GENRIC(UserModelAPI model)
        {
            return new UserForGeneric {
                AuthUserId = model.AuthUserId,
                FirstName = model.FirstName,
                LastName = model.LastName,
                EmailAddress = model.EmailAddress,
                CellPhone = model.CellPhone
            };
        }
    }
}
