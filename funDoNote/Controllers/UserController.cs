using BusinessLayer.Interfaces;
using CommonLayer.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;

namespace funDoNote.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase 
    {
        IUserBL userBL;
        private IConfiguration _config;

        public UserController(IUserBL userBL , IConfiguration config)
        {
            this.userBL = userBL;
            this._config = config;
        }

        [HttpPost("RegisterUser")]

        public IActionResult RegisterUser(UserPostModel userPostModel)
        {
            try
            {
                this.userBL.RegisterUser(userPostModel);
                return this.Ok(new {success = true,status = 200,message=$"Registration successful for {userPostModel.Email}"});
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost("LoginUser")]
        public IActionResult LoginUser(LoginModel loginModel)
        {
            try
            {
                string token = this.userBL.LoginUser(loginModel);
                if (token != null)
                {
                    return this.Ok(new { Token = token, success = true, status = 200, message = $"login successful for {loginModel.Email}" });
                }
                return this.Ok(new { Token = token, success = false, status = 404, message = $"{loginModel.Email} not found" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost("ForgetPassword")]
        public IActionResult ForgetPassword(string email)
        {
            try
            {
                bool isTrue = this.userBL.ForgetPassword(email);
                if (isTrue)
                {
                    return this.Ok(new {success = true, status = 200, message = $"Reset link sent to {email}" });
                }
                return this.Ok(new { success = false, status = 404, message = $"wrong {email}" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
