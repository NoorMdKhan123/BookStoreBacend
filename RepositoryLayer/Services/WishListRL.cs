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
    public class WishListRL : IWishListRL
    {

        IConfiguration _config;


        public WishListRL(IConfiguration config)
        {
            _config = config;
        }

        public string connectionString { get; set; } = "BookstoreAppConnectionString";


        public string WishListCreation(long bookId, long userId)
        {

            if (bookId != 0 && userId != 0)
            {
                string ConnectionStrings = _config.GetConnectionString(connectionString);
                if (ConnectionStrings != null)
                {
                    using (SqlConnection con = new SqlConnection(ConnectionStrings))
                    {
                        SqlCommand cmd = new SqlCommand("spWishListForAddingBooks", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@UserId", userId);
                        cmd.Parameters.AddWithValue("@BookId", bookId);

                        con.Open();
                        cmd.ExecuteNonQuery();
                        int a = Convert.ToInt32(cmd.ExecuteScalar());
                        if (a == 2)
                        {
                            return "BookId does not exists";
                        }
                        else if (a == 1)
                        {
                            return "Book already added to wishlist";
                        }
                        else
                        {
                            return "Book added to wishlist successfully";
                        }
                    }

                }
                throw new CustomException("Not able to connect to database");
            }
            throw new KeyNotFoundException("deatails are empty");
        }


        public string DeletWishList(long wishListId)
        {

            if (wishListId != 0)
            {
                string ConnectionStrings = _config.GetConnectionString(connectionString);
                if (ConnectionStrings != null)
                {
                    using (SqlConnection con = new SqlConnection(ConnectionStrings))
                    {
                        SqlCommand cmd = new SqlCommand("spForDeltngWishList", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@WishListId", wishListId);
                        con.Open();
                        int a = cmd.ExecuteNonQuery();
                        if (a > 0)
                        {
                            return "Book got  deleted from wishlist";
                        }
                        else
                        {
                            return "Book is not present in the wishlist";
                        }

                    }

                }
                throw new CustomException("Not able to connect to database");
            }
            throw new KeyNotFoundException("deatails are empty");

        }


        public List<WishListModel> GetWishlistDetailsByUserId(long userId)
        {
            if (userId != 0)
            {
                string ConnectionStrings = _config.GetConnectionString(connectionString);
                if (ConnectionStrings != null)
                {
                    using (SqlConnection con = new SqlConnection(ConnectionStrings))
                    {
                        SqlCommand cmd = new SqlCommand("spGetngDetailsById", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@UserId", userId);
                        con.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        List<WishListModel> list = new List<WishListModel>();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                WishListModel wishlistModel = new WishListModel();
                                BookDetailsModel bookDetails = new BookDetailsModel();
                                bookDetails.BookTitle = reader["BookTitle"].ToString();
                                bookDetails.BookAuthor = reader["BookAuthor"].ToString();
                                bookDetails.OriginalPrice = Convert.ToInt32(reader["OriginalPrice"]);
                                //bookDetails.Rating = (float)reader["Rating"];
                                bookDetails.RatingCount = Convert.ToInt32(reader["RatingCount"]);
                                bookDetails.Description = reader["Description"].ToString();
                                bookDetails.BookQty = Convert.ToInt32(reader["BookQty"]);
                                bookDetails.DiscountedPrice = Convert.ToInt32(reader["DiscountedPrice"]);
                                bookDetails.Image = reader["Image"].ToString();
                                wishlistModel.WishListId = Convert.ToInt32(reader["WishListId"]);
                                wishlistModel.UserId = Convert.ToInt64(reader["UserId"]);
                                wishlistModel.BookId = Convert.ToInt64(reader["BookId"]);
                                wishlistModel.bookDetails = bookDetails;
                                list.Add(wishlistModel);
                            }
                            return list;
                        }
                        con.Close();
                        throw new InvalidOperationException("cannot fetched data by user Id");

                    }
                }
                throw new CustomException("not able to connect to database");

            }
            throw new ArgumentNullException("No data in paramter is present");
        }

    }
}
