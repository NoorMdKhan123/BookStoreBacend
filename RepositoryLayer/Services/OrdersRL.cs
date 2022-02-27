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
    public class OrdersRL : IOrdersRL
    {
        IConfiguration _config;
        public OrdersRL(IConfiguration config)
        {
            _config = config;
        }

        public string connectionString { get; set; } = "BookstoreAppConnectionString";

        public OrderResModel AddingOrderDetails(OrderModel model, long userId)
        {
            if (model != null)
            {
                string ConnectionStrings = _config.GetConnectionString(connectionString);
                if (ConnectionStrings != null)
                {
                    using (SqlConnection con = new SqlConnection(ConnectionStrings))
                    {
                        SqlCommand cmd = new SqlCommand("spOrderAdd", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@UserId", userId);
                        cmd.Parameters.AddWithValue("@BookId", model.BookId);
                        cmd.Parameters.AddWithValue("@AddressId", model.AddressId);
                        cmd.Parameters.AddWithValue("@OrderQuantity", model.OrderQuantity);
                       
                        con.Open();
                        cmd.ExecuteNonQuery();



                        //    int result = Convert.ToInt32(cmd.ExecuteScalar());
                        //con.Close();
                        //if (result == 2)
                        //{
                        //    return "BookId not exists";
                        //}
                        //else if (result == 1)
                        //{
                        //    return "Userid not exists";
                        //}
                        //else
                        //{
                        //    return "Ordered successfully";
                        //}



                        OrderResModel orderResponse = new OrderResModel();
                            orderResponse.UserId = userId;
                            orderResponse.BookId = model.BookId;
                            orderResponse.AddressId = model.AddressId;
                            orderResponse.OrderQuantity = model.OrderQuantity;
                        con.Close();
                        return orderResponse;
                        
                       



                                throw new InvalidExpressionException("not able to execute query");
                    }

                }
                throw new CustomException("Not able to connect to database");
            }

            throw new ArgumentNullException("deatails are empty");

        }


        public List<GetAllOrderData> GetAllCartData(long userId)
        {
            List<GetAllOrderData> list = new List<GetAllOrderData>();

            string ConnectionStrings = _config.GetConnectionString(connectionString);
            if (ConnectionStrings != null)
            {
                using (SqlConnection con = new SqlConnection(ConnectionStrings))
                {

                    SqlCommand cmd = new SqlCommand("spAllOrderDetailsByUserId", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", userId);

                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            GetAllOrderData orderModel = new GetAllOrderData();
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
                            orderModel.Price = Convert.ToInt32(reader["Price"]);
                            orderModel.OrderId= Convert.ToInt64(reader["OrderId"]);
                            orderModel.BookId = Convert.ToInt64(reader["BookId"]);
                            orderModel.AddressId = Convert.ToInt64(reader["AddressId"]);
                            orderModel.bookDetailsModel = bookDetails;
                            list.Add(orderModel);
                        }
                        return list;
                    }
                    con.Close();
                    throw new InvalidOperationException("cannot fetched data by book Id");

                }
            }
            throw new CustomException("not able to connect to database");

        }



    }
}
