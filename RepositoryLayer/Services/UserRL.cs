using CommonLayer.User;
using Experimental.System.Messaging;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Interfaces;
using RepositoryLayer.Services.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace RepositoryLayer.Services
{
    public class UserRL : IUserRL
    {
        readonly FunDoNoteContext funDoNoteContext;
        private IConfiguration _config;
        public UserRL(FunDoNoteContext funDoNoteContext , IConfiguration config)
        {
            this.funDoNoteContext = funDoNoteContext;
            this._config = config;
        }

        public string LoginUser(LoginModel loginModel)
        {
            try
            {
                var user = funDoNoteContext.Users.Where(x => x.Email == loginModel.Email && x.Password == loginModel.Password).FirstOrDefault();
                if (user == null)
                {
                    return null;
                }
                
                return GenerateJwtToken(user.Email , user.UserId);
            }
            catch(Exception ex)
            {
                throw ex;
            }

        }

        private string GenerateJwtToken(string email, int userId)
        {

            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenKey = Encoding.ASCII.GetBytes("THIS_IS_MY_KEY_TO_GENERATE_TOKEN");
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim("Email", email),
                    new Claim("UserId",userId.ToString()),
                    }),
                    Expires
                    = DateTime.UtcNow.AddHours(2),

                    SigningCredentials =
                    new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey),
                    SecurityAlgorithms.HmacSha256Signature),
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public void RegisterUser(UserPostModel userPostModel)
        {
            try
            {
                User user = new User();
                user.FirstName = userPostModel.FirstName;
                user.LastName = userPostModel.LastName;
                user.Email = userPostModel.Email;
                user.Password = userPostModel.Password;
                user.ConfirmPassword = userPostModel.ConfirmPassword;
                user.CreatedDate = DateTime.Now;
                user.ModifiedDate = DateTime.Now;
                funDoNoteContext.Users.Add(user);
                funDoNoteContext.SaveChanges();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public bool ForgetPassword(string email)
        {
            try
            {
                var user = funDoNoteContext.Users.Where(x => x.Email == email).FirstOrDefault();
                if (user == null)
                {
                    return false;
                }
                MessageQueue funDoNoteQ = new MessageQueue();
                //Setting the QueuPath where we want to store the messages.
                funDoNoteQ.Path = @".\private$\funDoNote";
                if(MessageQueue.Exists(funDoNoteQ.Path))
                {
                    funDoNoteQ = new MessageQueue(@".\private$\funDoNote");
                    //Exists
                }
                else
                {
                    // Creates the new queue named "funDoNote"
                    MessageQueue.Create(funDoNoteQ.Path);
                }
                Message message = new Message();
                message.Formatter = new BinaryMessageFormatter();
                message.Body = GenerateJwtToken(email, user.UserId);
                message.Label = "Forget Password Email";
                funDoNoteQ.Send(message);
                Message msg = funDoNoteQ.Receive();
                msg.Formatter = new BinaryMessageFormatter();
                EmailService.SendEmail(email, message.Body.ToString());
                funDoNoteQ.ReceiveCompleted += new ReceiveCompletedEventHandler(msmqQueue_ReceiveCompleted);
                funDoNoteQ.BeginReceive();
                funDoNoteQ.Close();
                return true;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        private void msmqQueue_ReceiveCompleted(object sender, ReceiveCompletedEventArgs e)
        {
            try
            {
                MessageQueue queue = (MessageQueue)sender;
                Message msg = queue.EndReceive(e.AsyncResult);
                EmailService.SendEmail(e.Message.ToString(), GenerateToken(e.Message.ToString()));
                queue.BeginReceive();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        private string GenerateToken(string email)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenKey = Encoding.ASCII.GetBytes("THIS_IS_MY_KEY_TO_GENERATE_TOKEN");
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim("Email", email)
                    }),
                    Expires
                    = DateTime.UtcNow.AddHours(2),

                    SigningCredentials =
                         new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey),
                    SecurityAlgorithms.HmacSha256Signature),
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
