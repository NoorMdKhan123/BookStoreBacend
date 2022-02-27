using CommonLayer.Model;
using CommonLayer.Model.GlobalCustomException;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Http;
using System.Text;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace RepositoryLayer.Services
{
    public class BookRL : IBookRL
    {
        IConfiguration _config;
        public BookRL(IConfiguration config)
        {
            _config = config;
        }

        public string connectionString { get; set; } = "BookstoreAppConnectionString";

        public BookResponseModel AddingBookDetails(BookModel model, long userId)
        {
            if (model != null)
            {
                string ConnectionStrings = _config.GetConnectionString(connectionString);
                if (ConnectionStrings != null)
                {
                    using (SqlConnection con = new SqlConnection(ConnectionStrings))
                    {
                        SqlCommand cmd = new SqlCommand("spAddngBooksDetails", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@BookTitle", model.BookTitle);
                        cmd.Parameters.AddWithValue("@BookAuthor", model.BookAuthor);
                        cmd.Parameters.AddWithValue("@Rating", model.Rating);
                        cmd.Parameters.AddWithValue("@RatingCount", model.RatingCount);
                        cmd.Parameters.AddWithValue("@OriginalPrice", model.OriginalPrice);
                        cmd.Parameters.AddWithValue("@DiscountedPrice", model.DiscountedPrice);
                        cmd.Parameters.AddWithValue("@Description", model.Description);
                        cmd.Parameters.AddWithValue("@BookQty", model.BookQty);
                        cmd.Parameters.AddWithValue("@Image", model.Image);
                        cmd.Parameters.AddWithValue("@UserId", userId);



                        con.Open();
                        if (cmd.ExecuteNonQuery() > 0)
                        {
                            BookResponseModel responseModel = new BookResponseModel();
                            responseModel.BookTitle = model.BookTitle;
                            responseModel.BookAuthor = model.BookAuthor;
                            responseModel.Rating = model.Rating;
                            responseModel.RatingCount = model.RatingCount;
                            responseModel.OriginalPrice = model.OriginalPrice;
                            responseModel.DiscountedPrice = model.DiscountedPrice;
                            responseModel.Description = model.Description;
                            responseModel.BookQty = model.BookQty;
                            responseModel.Image = model.Image;
                            responseModel.UserId = userId;
                            return responseModel;
                        }
                        con.Close();
                        throw new InvalidExpressionException("not able to execute query");
                    }

                }
                throw new CustomException("Not able to connect to database");
            }

            throw new ArgumentNullException("deatails are empty");

        }



        public BookDetailsModel UpdateBookDetails(BookDetailsModel model, long bookId)
        {

            string ConnectionStrings = _config.GetConnectionString(connectionString);
            if (ConnectionStrings != null)
            {
                using (SqlConnection con = new SqlConnection(ConnectionStrings))
                {
                    SqlCommand cmd = new SqlCommand("spUpdatingBookDetl", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@BookId", bookId);
                    cmd.Parameters.AddWithValue("@BookTitle", model.BookTitle);
                    cmd.Parameters.AddWithValue("@BookAuthor", model.BookAuthor);
                    cmd.Parameters.AddWithValue("@Rating", model.Rating);
                    cmd.Parameters.AddWithValue("@RatingCount", model.RatingCount);
                    cmd.Parameters.AddWithValue("@BookQty", model.BookQty);
                    cmd.Parameters.AddWithValue("@OriginalPrice", model.OriginalPrice);
                    cmd.Parameters.AddWithValue("@DiscountedPrice", model.DiscountedPrice);
                    cmd.Parameters.AddWithValue("@Description", model.Description);
                    cmd.Parameters.AddWithValue("@Image", model.Image);
                    cmd.Parameters.AddWithValue("@Image", model.Image);
                    con.Open();
                    int a = cmd.ExecuteNonQuery();
                    if (a > 0)
                    {

                        con.Close();
                        return model;
                    }
                    throw new InvalidOperationException("no query excuted");
                }

            }
            throw new ArgumentNullException("no details to update");

        }


        public List<GetBookDeatils> GetBookDetailsById(long bookId, long userId)
        {
            List<GetBookDeatils> allBooksList = new List<GetBookDeatils>();
            GetBookDeatils getBookDeatils = new GetBookDeatils();
            string ConnectionStrings = _config.GetConnectionString(connectionString);
            if (ConnectionStrings != null)
            {
                using (SqlConnection con = new SqlConnection(ConnectionStrings))
                {

                    SqlCommand cmd = new SqlCommand("spGtDeatilsByBookId", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@BookId", bookId);
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            getBookDeatils.BookId = Convert.ToInt64(dr["BookId"]);
                            getBookDeatils.BookTitle = dr["BookTitle"].ToString();
                            getBookDeatils.BookAuthor = dr["BookAuthor"].ToString();
                            getBookDeatils.Rating = Convert.ToInt32(dr["Rating"]);
                            getBookDeatils.RatingCount = Convert.ToInt32(dr["RatingCount"]);
                            getBookDeatils.OriginalPrice = Convert.ToInt32(dr["OriginalPrice"]);
                            getBookDeatils.DiscountedPrice = Convert.ToInt32(dr["DiscountedPrice"]);
                            getBookDeatils.Description = dr["Description"].ToString();
                            getBookDeatils.BookQty = Convert.ToInt32(dr["BookQty"]);
                            getBookDeatils.Image = dr["Image"].ToString();
                            getBookDeatils.UserId = Convert.ToInt64(dr["UserId"]);
                            allBooksList.Add(getBookDeatils);
                        }
                        return allBooksList;
                    }
                    con.Close();
                    throw new InvalidOperationException("cannot fetched data by book Id");

                }
            }
            throw new CustomException("not able to connect to database");



        }


        public List<GetBookDeatils> GetAllBookDeatils()
        {
            List<GetBookDeatils> allBooksList = new List<GetBookDeatils>();
            
            string ConnectionStrings = _config.GetConnectionString(connectionString);
            if (ConnectionStrings != null)
            {
                using (SqlConnection con = new SqlConnection(ConnectionStrings))
                {

                    SqlCommand cmd = new SqlCommand("spGetAllBkDetails", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            GetBookDeatils getBookDeatils = new GetBookDeatils();
                            getBookDeatils.BookId = Convert.ToInt64(dr["BookId"]);
                            getBookDeatils.BookTitle = dr["BookTitle"].ToString();
                            getBookDeatils.BookAuthor = dr["BookAuthor"].ToString();
                            getBookDeatils.Rating = Convert.ToInt32(dr["Rating"]);
                            getBookDeatils.RatingCount = Convert.ToInt32(dr["RatingCount"]);
                            getBookDeatils.OriginalPrice = Convert.ToInt32(dr["OriginalPrice"]);
                            getBookDeatils.DiscountedPrice = Convert.ToInt32(dr["DiscountedPrice"]);
                            getBookDeatils.Description = dr["Description"].ToString();
                            getBookDeatils.BookQty = Convert.ToInt32(dr["BookQty"]);
                            getBookDeatils.Image = dr["Image"].ToString();
                            getBookDeatils.UserId = Convert.ToInt64(dr["UserId"]);
                            allBooksList.Add(getBookDeatils);
                            
                        }
                        return allBooksList;
                        throw new AccessViolationException("not able to access");
                    }
                    con.Close();
                    throw new InvalidOperationException("cannot fetched data by book Id");

                }
            }
            throw new CustomException("not able to connect to database");
        }


        public string DeletBookRecord(long bookId)
        {
            if (bookId != 0)
            {
                string ConnectionStrings = _config.GetConnectionString(connectionString);
                if (ConnectionStrings != null)
                {
                    using (SqlConnection con = new SqlConnection(ConnectionStrings))
                    {
                        SqlCommand cmd = new SqlCommand("spDeleteBookRecord", con);
                        cmd.Parameters.AddWithValue("@BookId", bookId);
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        if (cmd.ExecuteNonQuery() == 1)
                        {
                            return "record deleted successfully";

                        }
                        con.Close();
                        throw new KeyNotFoundException("Id not found");
                    }

                    //model.BookTitle = dr["BookTitle"].ToString();
                    //    model.BookAuthor = dr["BookAuthor"].ToString();
                    //    model.Rating = Convert.ToInt32(dr["Rating"]);
                    //    model.RatingCount = Convert.ToInt32(dr["RatingCount"]);
                    //    model.OriginalPrice = Convert.ToInt32(dr["OriginalPrice"]);
                    //    model.DiscountedPrice = Convert.ToInt32(dr["DiscountedPrice"]);
                    //    model.Description = dr["Description"].ToString();
                    //    model.BookQty = Convert.ToInt32(dr["BookQty"]);
                    //    model.Image = dr["Image"].ToString();
                    //    model.UserId = Convert.ToInt64(dr["UserId"]);
                    //    return model;






                }
                throw new InvalidOperationException("no operation found on parameters");

            }
            throw new ArgumentNullException("value is not present");

        }




        public BookResponseModel UpdateImage(long bookId, IFormFile bookImage, long userId)
        {

            Account account = new Account(this._config["Cloudinary:CloudName"], this._config["Cloudinary:APIKey"], this._config["Cloudinary:APISecret"]);
            var imagePath = bookImage.OpenReadStream();
            Cloudinary cloudinary = new Cloudinary(account);
            ImageUploadParams imageParams = new ImageUploadParams()
            {
                File = new FileDescription(bookImage.FileName, imagePath)
            };
            string uploadImage = cloudinary.Upload(imageParams).Url.ToString();
            string ConnectionStrings = _config.GetConnectionString(connectionString);
            if (ConnectionStrings != null)
            {
                using (SqlConnection con = new SqlConnection(ConnectionStrings))
                {

                    SqlCommand cmd = new SqlCommand("spUpdateImge", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@BookId", bookId);
                    cmd.Parameters.AddWithValue("@BookImage", uploadImage);
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    con.Open();

                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())

                        {
                            BookResponseModel model = new BookResponseModel();

                            model.UserId = userId;
                            model.Image = dr["Image"].ToString();



                            con.Close();
                            return model;
                        }
                        throw new InvalidOperationException("no query excuted");
                    }
                    throw new CustomException("no rows found");

                }
                throw new ArgumentNullException("no image found to add");

            }
            throw new CustomException("Not able to connect to DB");



        }


    }
}
