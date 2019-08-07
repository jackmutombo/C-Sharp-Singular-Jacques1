using DataLibrary;
using DataLibrary.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApplicationJacques.Controllers
{

    [System.Web.Http.Authorize]
    [System.Web.Http.RoutePrefix("api/User")]
    public class UserController : ApiController
    {
        public UserModel GetById()
        {
            string AuthUserId = RequestContext.Principal.Identity.GetUserId();
            return GlobalConfig.Connection.GetUserById(AuthUserId);
        }

        public void Post(UserModelAPI newUser)
        {
            newUser.AuthUserId = RequestContext.Principal.Identity.GetUserId();          
            GlobalConfig.Connection.CreateUser(newUser);
        }
    }
}
