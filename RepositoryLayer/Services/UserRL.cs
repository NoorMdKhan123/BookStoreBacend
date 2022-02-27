using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Microsoft.Extensions.Configuration;
using CommonLayer.Model.GlobalCustomException;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace RepositoryLayer.Services
{
    public class UserRL : IUserRL
    {
        IConfiguration _config;


        public UserRL(IConfiguration config)
        {
            _config = config;
        }

        public string connectionString { get; set; } = "BookstoreAppConnectionString";

        public RegisterResponseModel Registration(RegisterResponseModel user)
        {

            if (user != null)
            {
                string ConnectionStrings = _config.GetConnectionString(connectionString);
                if (ConnectionStrings != null)
                {
                    using (SqlConnection con = new SqlConnection(ConnectionStrings))
                    {
                        SqlCommand cmd = new SqlCommand("spInsertUserRecord", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        SqlParameter sqlParameter = cmd.Parameters.AddWithValue("@FullName", user.FullName);
                        cmd.Parameters.AddWithValue("@EmailId", user.EmailId);
                        cmd.Parameters.AddWithValue("@Password", EncryptPassword.ConvertToEncrypt(user.Password));
                        cmd.Parameters.AddWithValue("@MobileNumber", user.MobileNumber);

                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                        return user;
                    }
                }
                throw new CustomException("Not able to connect to database");
            }

            throw new KeyNotFoundException("deatails are empty");

        }

        public string GenerateJWTToken(string EmailId, long UserId)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[] {
                new Claim(ClaimTypes.Email,EmailId),
                   new Claim("UserId",UserId.ToString())
            };


            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
        _config["Jwt:Issuer"],
        claims,
        expires: DateTime.Now.AddMinutes(120),
        signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }



        public LoginResponseModel GetLogin(UserLogin user)
        {
            if (user != null)
            {
                string ConnectionStrings = _config.GetConnectionString(connectionString);
               
                using (SqlConnection con = new SqlConnection(ConnectionStrings))
                {
                    SqlCommand cmd = new SqlCommand("spUserLoginDetails", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    
                    cmd.Parameters.AddWithValue("@EmailId", user.EmailId);
                    cmd.Parameters.AddWithValue("@Password", EncryptPassword.ConvertToEncrypt( user.Password));
                    con.Open();
                    LoginResponseModel registerModel = new LoginResponseModel();

                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.Read())
                    {
                        registerModel.UserId = Convert.ToInt32(dr["UserId"]);
                        registerModel.FullName = dr["FullName"].ToString();
                        registerModel.EmailId = dr["EmailId"].ToString();
                        registerModel.MobileNumber = (dr["MobileNumber"]).ToString();
                        string token = GenerateJWTToken(registerModel.EmailId, registerModel.UserId);
                        registerModel.Token = token;

                        return registerModel;
                    }
                    throw new CustomException("no value to read");
                }

            }
            throw new KeyNotFoundException("no records found exception");
        }



        public IEnumerable<User> GetAllUsers()
        {
            List<User> users = new List<User>();
            if (users != null)
            {
                string ConnectionStrings = _config.GetConnectionString(connectionString);

                using (SqlConnection con = new SqlConnection(ConnectionStrings))
                {
                    SqlCommand cmd = new SqlCommand("spGetsAllTheUser", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        User user = new User();
                        user.UserId = Convert.ToInt32(rdr["UserId"]);
                        user.FullName = (rdr["FullName"]).ToString();
                        user.EmailId = (rdr["EmailId"]).ToString();
                        user.MobileNumber = (rdr["MobileNumber"]).ToString();
                        users.Add(user);
                    }

                    rdr.Close();
                }
                return users;
            }
            throw new KeyNotFoundException("details are empty");
        }

        public string ForgotPassword(ForgetResponse response)
        {
            string ConnectionStrings = _config.GetConnectionString(connectionString);

            using (SqlConnection con = new SqlConnection(ConnectionStrings))
            {
                SqlCommand cmd = new SqlCommand("spFrgotPswrd", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmailId", response.Email);
                

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                    if (rdr.HasRows)
                    {
                    User user = new User();
                    while (rdr.Read())
                    {
                       
                        user.UserId = Convert.ToInt32(rdr["UserId"]);
                        user.EmailId = (rdr["EmailId"]).ToString();
                    }

                    var token = GenerateJWTToken(user.EmailId,user.UserId);
                    new MsmqModel().MsmqSender(token);
                    return "email is sent successfully";
                }
                throw new CustomException("Email doesn't exist in database");

            }
        }

        public string ResetPassword(ResetPasswordModel model, string emailId)
        {
            if (model != null)
            {
                string ConnectionStrings = _config.GetConnectionString(connectionString);

                using (SqlConnection con = new SqlConnection(ConnectionStrings))
                {
                    if (model.NewPassword == model.ConfirmPassword)
                    {
                        SqlCommand cmd = new SqlCommand("spResetUserPassword", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@EmailId", emailId);
                        cmd.Parameters.AddWithValue("@NewPassword", SecureData.ConvertToEncrypt(model.NewPassword));


                        con.Open();
                        SqlDataReader dr = cmd.ExecuteReader();

                        if (dr.Read())
                        {
                            return "Password reset is successful";
                        }
                        throw new InvalidOperationException("password not updated");

                    }
                    throw new CustomException("values doesn't match");
                   
                }
            }
            throw new ArgumentNullException("object is empty");

        }

        public string DeleteRecord(long Id)
        {
            string ConnectionStrings = _config.GetConnectionString(connectionString);

            using (SqlConnection con = new SqlConnection(ConnectionStrings))
            {
                SqlCommand cmd = new SqlCommand("spDeleteARecord", con);
                cmd.CommandType = CommandType.StoredProcedure;
                var result = cmd.Parameters.AddWithValue("@UserId", Id);
                con.Open();
                if (cmd.ExecuteNonQuery() == 1)
                {
                    return "record deleted successfully";

                }
                con.Close();
                throw new KeyNotFoundException("Id not found");
            }
        }

        public UpdateResponse UpdateRecord(long Id, UpdateResponse model)
        {
            string ConnectionStrings = _config.GetConnectionString(connectionString);

            using (SqlConnection con = new SqlConnection(ConnectionStrings))
            {
                SqlCommand cmd = new SqlCommand("spUpdateUsers", con);
                cmd.CommandType = CommandType.StoredProcedure;

               
                SqlParameter sqlParameter = cmd.Parameters.AddWithValue("@FullName", model.FullName);
                cmd.Parameters.AddWithValue("@EmailId", model.EmailId);
                cmd.Parameters.AddWithValue("@Password", SecureData.ConvertToEncrypt(model.Passowrd));
                cmd.Parameters.AddWithValue("@MobileNumber", model.MobileNumber);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    return model;
                }
                con.Close();
                throw new InvalidOperationException("password not updated");
                
               
            }

        }


    }
}
