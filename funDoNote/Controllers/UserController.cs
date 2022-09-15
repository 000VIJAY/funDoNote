using BusinessLayer.Interfaces;
using CommonLayer.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace funDoNote.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase 
    {
        IUserBL userBL;
        private IConfiguration _config;
        private FunDoNoteContext _funDoNoteContext;

        public UserController(IUserBL userBL , IConfiguration config, FunDoNoteContext funDoNoteContext)
        {
            this.userBL = userBL;
            this._config = config;
            this._funDoNoteContext = funDoNoteContext;
        }

        [HttpPost("RegisterUser")]

        public IActionResult RegisterUser(UserPostModel userPostModel)
        {
            try
            {
                var valid = _funDoNoteContext.Users.Where(x=>x.Email == userPostModel.Email).FirstOrDefault();
                if (valid == null)
                {
                    this.userBL.RegisterUser(userPostModel);
                    return this.Ok(new {success = true,status = 200,message=$"Registration successful for {userPostModel.Email}"});
                }
                return this.BadRequest(new { success = false, message = $"Provided email {userPostModel.Email} already present" });
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
                return this.BadRequest(new { success = false,status= 401, message = "Email not found" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost("ForgotPassword/{email}")]
        public IActionResult ForgotPassword(string email)
        {
            try
            {
                bool isTrue = this.userBL.ForgotPassword(email);
                if (isTrue)
                {
                    return this.Ok(new {success = true, status = 200, message = $"Reset password link has been sent to {email}" });
                }
                return this.BadRequest(new {success = false, status = 401, message = "Wrong email"});
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [Authorize]
        [HttpPut("ResetPassword")]
        public IActionResult ResetPassword(ResetModel resetModel)
        {
            try
            {
                //authorization match email from token
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("UserId", StringComparison.InvariantCultureIgnoreCase));
                int UserID = Int32.Parse(userid.Value);
                var result = _funDoNoteContext.Users.Where(u => u.UserId == UserID).FirstOrDefault();

                if (result.Password == resetModel.NewPassword)
                {
                    return this.BadRequest(new { success = false, message = "New Password and old password is same kindly give different password" });
                }

                string Email = result.Email.ToString();
                bool res = this.userBL.ResetPassword(Email, resetModel);
                if(res == false)
                {
                    return this.BadRequest(new { success = false,message = "New Password and Confirm Password are not same." });
                }
                return this.Ok(new { success = true,status = 200, message = "Password Changed Sucessfully" });

                ////Authorization by email
                //var identity = User.Identity as ClaimsIdentity;
                //if (identity != null)
                //{
                //    IEnumerable<Claim> claims = identity.Claims;
                //    var email = claims.Where(p => p.Type == @"Email").FirstOrDefault()?.Value;
                //    this.userBL.ResetPassword(email, resetModel);
                //    return this.Ok(new { success = true, message = "Password Changed Sucessfully", email = $"{email}" });
                //}
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
