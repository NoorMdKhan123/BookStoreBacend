using CommonLayer.Model;
using CommonLayer.Model.GlobalCustomException;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace RepositoryLayer.Services
{
    public class FeedbackRL : IFeedbackRL
    {
        IConfiguration _config;
        public FeedbackRL(IConfiguration config)
        {
            _config = config;
        }
        public string connectionString { get; set; } = "BookstoreAppConnectionString";
        public FeedbackResponse AddingFeedback(long bookId, FeedbackModel model, long userId)
        {
            if (model != null)
            {
                string ConnectionStrings = _config.GetConnectionString(connectionString);
                if (ConnectionStrings != null)
                {
                    using (SqlConnection con = new SqlConnection(ConnectionStrings))
                    {
                        SqlCommand cmd = new SqlCommand("spAddingReviews", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@BookId", bookId);
                        cmd.Parameters.AddWithValue("@UserId", userId);
                        cmd.Parameters.AddWithValue("@Rating", model.Rating);
                        cmd.Parameters.AddWithValue("@Comment", model.Comment);

                        con.Open();
                        cmd.ExecuteNonQuery();

                        FeedbackResponse response = new FeedbackResponse();

                        response.Rating = model.Rating;
                        response.Comment = model.Comment;
                        con.Close();
                        return response;


                    }

                }
                throw new CustomException("Not able to connect to database");
            }

            throw new ArgumentNullException("deatails are empty");
        }

        public List<FeedbackResponse> GetAllReviews(long bookId)
        {
            List<FeedbackResponse> fdModel = new List<FeedbackResponse>();

            string ConnectionStrings = _config.GetConnectionString(connectionString);
            if (ConnectionStrings != null)
            {
                using (SqlConnection con = new SqlConnection(ConnectionStrings))
                {

                    SqlCommand cmd = new SqlCommand("sPReviewBookId", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@BookId", bookId);

                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            FeedbackResponse response = new FeedbackResponse();



                            response.FullName = reader["FullName"].ToString();
                            response.Rating = Convert.ToInt32(reader["Rating"]);
                            response.FeedbackId = Convert.ToInt32(reader["ReviewId"]);
                            response.Comment = reader["Comment"].ToString();



                            fdModel.Add(response);
                        }
                        return fdModel;
                    }
                    con.Close();
                    throw new InvalidOperationException("cannot fetched data by book Id");

                }
            }
            throw new CustomException("not able to connect to database");
        }




    }
}
