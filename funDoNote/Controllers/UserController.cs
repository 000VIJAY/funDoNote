using BusinessLayer.Interfaces;
using CommonLayer.User;
using Microsoft.AspNetCore.Mvc;
using System;

namespace funDoNote.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase 
    {
        IUserBL userBL;
        public UserController(IUserBL userBL)
        {
            this.userBL = userBL;
        }

        [HttpPost("RegisterUser")]

        public IActionResult RegisterUser(UserPostModel userPostModel)
        {
            try
            {
                this.userBL.RegisterUser(userPostModel);
                return this.Ok(new {sucess = true,status = 200,message=$"Registration sucessful for {userPostModel.Email}"});
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
