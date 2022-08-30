﻿using BusinessLayer.Interfaces;
using CommonLayer.User;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class UserBL : IUserBL
    {
        readonly IUserRL userRL;
        public UserBL(IUserRL userRL)
        {
            this.userRL = userRL;
        }

        public bool ForgotPassword(string email)
        {
            try
            {
                return userRL.ForgotPassword(email);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public string LoginUser(LoginModel loginModel)
        {
            try
            {
               return this.userRL.LoginUser(loginModel);
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
                this.userRL.RegisterUser(userPostModel);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public bool ResetPassword(string email, ResetModel resetModel)
        {
           try
            {
                return this.userRL.ResetPassword(email, resetModel);
            }catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
 