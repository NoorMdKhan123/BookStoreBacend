using CommonLayer.Model;
using RepositoryLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class UserBL :IUserBL
    {
        IUserRL userRL;
        public UserBL(IUserRL userRL)
        {
            this.userRL = userRL;
        }
        public RegisterResponseModel Registration(RegisterResponseModel user)
        {
           
                return this.userRL.Registration(user);
          
        }
        public LoginResponseModel GetLogin(UserLogin credentials)
        {
            return this.userRL.GetLogin(credentials);
        }

        public string GenerateJWTToken(string EmailId, long userId)
        {
            return this.userRL.GenerateJWTToken(EmailId, userId);
        }


        public string ForgotPassword(ForgetResponse forgetResponse)
        {
            return this.userRL.ForgotPassword(forgetResponse);
        }

        public string ResetPassword(ResetPasswordModel model, string emailId)
        {

            return this.userRL.ResetPassword(model, emailId);

        }
        public string DeleteRecord(long Id)
        {
            return this.userRL.DeleteRecord(Id);
        }

        public UpdateResponse UpdateRecord(long Id, UpdateResponse model)
        {
            return this.userRL.UpdateRecord(Id, model);
        }

        public IEnumerable<User> GetAllUsers()
        {
            return this.userRL.GetAllUsers();
        }
    }
}
