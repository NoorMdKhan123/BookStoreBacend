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
    public class CartRL : ICartRL
    {
        IConfiguration _config;


        public CartRL(IConfiguration config)
        {
            _config = config;
        }

        public string connectionString { get; set; } = "BookstoreAppConnectionString";

        public CartModel AddingBook(BookCart model, long userId, long bookId)
        {
            if (model != null) 
            {
                string ConnectionStrings = _config.GetConnectionString(connectionString);
                if (ConnectionStrings != null)
                {
                    using (SqlConnection con = new SqlConnection(ConnectionStrings))
                    {
                        SqlCommand cmd = new SqlCommand("spAddingBook", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@BookId", bookId);
                        cmd.Parameters.AddWithValue("@UserId", userId);
                        cmd.Parameters.AddWithValue("@QtyToOrder", model.QtyToOrder);

                        con.Open();
                        if (cmd.ExecuteNonQuery() > 0)
                        {
                            CartModel cart = new CartModel();
                            cart.BookId = bookId;
                            cart.UserId = userId;
                            cart.QtyToOrder = model.QtyToOrder;

                            return cart;
                        }
                        con.Close();
                        throw new InvalidExpressionException("not able to execute query");
                    }

                }
                throw new CustomException("Not able to connect to database");
            }

            throw new ArgumentNullException("deatails are empty");
        }

        public BookUpdateModel UpdateCart(BookUpdateModel model, long cartId)
        {

            string ConnectionStrings = _config.GetConnectionString(connectionString);
            if (ConnectionStrings != null)
            {
                using (SqlConnection con = new SqlConnection(ConnectionStrings))
                {
                    SqlCommand cmd = new SqlCommand("spUpdtCart", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@BookId", model.BookId);
                    cmd.Parameters.AddWithValue("@QtyToOrder", model.QtyToOrder);
                    cmd.Parameters.AddWithValue("@CartId", cartId);
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


        public List<CartModel> GetAllCartData(long userId)
        {
            List<CartModel> carts = new List<CartModel>();
          
            string ConnectionStrings = _config.GetConnectionString(connectionString);
            if (ConnectionStrings != null)
            {
                using (SqlConnection con = new SqlConnection(ConnectionStrings))
                {

                    SqlCommand cmd = new SqlCommand("spCartDetails", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", userId);

                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    List<CartModel> cartsList = new List<CartModel>();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            CartModel cartDetails = new CartModel();
                            BookDetailsModel bookDetails = new BookDetailsModel();

                           
                            bookDetails.BookTitle = reader["BookTitle"].ToString();
                            bookDetails.BookAuthor = reader["BookAuthor"].ToString();
                            bookDetails.OriginalPrice = Convert.ToInt32(reader["OriginalPrice"]);
                            bookDetails.Rating = Convert.ToSingle(reader["Rating"]);
                            bookDetails.RatingCount = Convert.ToInt32(reader["RatingCount"]);
                            bookDetails.Description = reader["Description"].ToString();
                            bookDetails.BookQty = Convert.ToInt32(reader["BookQty"]);
                            bookDetails.DiscountedPrice = Convert.ToInt32(reader["DiscountedPrice"]);
                            bookDetails.Image = reader["Image"].ToString();
                           
                            cartDetails.CartId = Convert.ToInt64(reader["CartId"]);
                            cartDetails.UserId = userId;
                            cartDetails.BookId = Convert.ToInt64(reader["BookId"]);
                            cartDetails.QtyToOrder = Convert.ToInt32(reader["QtyToOrder"]);
                            cartDetails.bookModel = bookDetails;
                            cartsList.Add(cartDetails);

                          
                        }
                        return cartsList;
                    }
                    con.Close();
                    throw new InvalidOperationException("cannot fetched data by book Id");

                }
            }
            throw new CustomException("not able to connect to database");

        }


        public string DeleteCartDetails(long cartId)
        {
            if (cartId != 0)
            {
                

                string ConnectionStrings = _config.GetConnectionString(connectionString);
                if (ConnectionStrings != null)
                {
                    using (SqlConnection con = new SqlConnection(ConnectionStrings))
                    {
                        SqlCommand cmd = new SqlCommand("spDeleteCartDeatils", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@CartId", cartId);
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
                        throw new KeyNotFoundException("Id not found");
                    }

                }
                throw new InvalidOperationException("no operation found on parameters");

            }
            throw new ArgumentNullException("value is not present");

        }


    }
}
