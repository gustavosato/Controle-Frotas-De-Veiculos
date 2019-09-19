using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Lean.Test.Cloud.WebApi.Auth;
using Lean.Test.Cloud.Domain.Services;
using Lean.Test.Cloud.Domain;
using Lean.Test.Cloud.Domain.Entities.Users;
using Lean.Test.Cloud.WebApi.Models;

// LINK DE REFERENCIA
//https://code-adda.com/2019/01/jwt-authentication-with-asp-net-web-api/

namespace Lean.Test.Cloud.WebApi.Controllers
{
    public class RequestTokenController : ApiController
    {
        private readonly IUserService _userService;
        private readonly IEncryptService _encryptService;

        public RequestTokenController(IUserService userService,
                                      IEncryptService encryptService)
        {
            _userService = userService;
            _encryptService = encryptService;
        }

        //public HttpResponseMessage Get(string username, string password)
        //{
        //    if (CheckUser(username, password))
        //    {
        //        return Request.CreateResponse(HttpStatusCode.OK,
        //     JwtAuthManager.GenerateJWTToken(username));
        //    }
        //    else
        //    {
        //        return Request.CreateResponse(HttpStatusCode.Unauthorized,
        //     "Invalid Request");
        //    }
        //}

        public string CheckUser(string username, string password)
        {
            Result<User> localUser = _userService.GetByEmail(username);

            if (localUser.Value != null)
            {
                password = _encryptService.GetHash(password);

                if(password == localUser.Value.password)
                {
                    return JwtAuthManager.GenerateJWTToken(username);
                }
                else
                {
                    return "senha invalida";
                }

            }
            else
            {
                return "email invalido";
            }

            //// for demo purpose, I am simply checking username and password with predefined strings. you can have your own logic as per requirement.
            //if (username == "admin" && password == "password")
            //{
            //    return JwtAuthManager.GenerateJWTToken(username);
            //    //return true;
            //}
            //else
            //{
            //    return "false";
            //}
        }
    }
}