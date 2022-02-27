using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer
{
    public interface IUserRL
    {
         RegisterResponseModel Registration(RegisterResponseModel user);

        LoginResponseModel GetLogin(UserLogin user);

        string GenerateJWTToken(string EmailId, long userId);

        string ForgotPassword(ForgetResponse forgetResponse);

        string ResetPassword(ResetPasswordModel model, string emailId);

        string DeleteRecord(long Id);

        UpdateResponse UpdateRecord(long Id, UpdateResponse model);

        IEnumerable<User> GetAllUsers();
    }
}
