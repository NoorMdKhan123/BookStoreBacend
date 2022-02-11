
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
    public class AddressRL : IAddressRL
    {

        IConfiguration _config;
        public AddressRL(IConfiguration config)
        {
            _config = config;
        }
        public string connectionString { get; set; } = "BookstoreAppConnectionString";
        public AddressResponseModel AddingAddressDetails(AddressModel model, long userId)
        {
            if (model != null)
            {
                string ConnectionStrings = _config.GetConnectionString(connectionString);
                if (ConnectionStrings != null)
                {
                    using (SqlConnection con = new SqlConnection(ConnectionStrings))
                    {
                        SqlCommand cmd = new SqlCommand("spAdngUsrAddrs", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@UserId", userId);
                        cmd.Parameters.AddWithValue("@Address", model.Address);
                        cmd.Parameters.AddWithValue("@City", model.City);
                        cmd.Parameters.AddWithValue("@State", model.State);
                        cmd.Parameters.AddWithValue("@TypeId", model.TypeId);

                        con.Open();
                        if (cmd.ExecuteNonQuery() > 0)
                        {
                            AddressResponseModel responseModel = new AddressResponseModel();
                            responseModel.UserId = userId;
                            responseModel.Address = model.Address;
                            responseModel.City = model.City;
                            responseModel.State = model.State;
                            responseModel.TypeId = model.TypeId;
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

        public AddressResponseModel UpdateAddress(AddressModel model, long userId)
        {
            if (model != null)
            {
                string ConnectionStrings = _config.GetConnectionString(connectionString);
                if (ConnectionStrings != null)
                {
                    using (SqlConnection con = new SqlConnection(ConnectionStrings))
                    {
                        SqlCommand cmd = new SqlCommand("spUpdateAddress", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@UserId", userId);
                        cmd.Parameters.AddWithValue("@Address", model.Address);
                        cmd.Parameters.AddWithValue("@City", model.City);
                        cmd.Parameters.AddWithValue("@State", model.State);
                        cmd.Parameters.AddWithValue("@TypeId", model.TypeId);
                        con.Open();
                        if (cmd.ExecuteNonQuery() > 0)
                        {
                            AddressResponseModel responseModel = new AddressResponseModel();
                            responseModel.UserId = userId;
                            responseModel.Address = model.Address;
                            responseModel.City = model.City;
                            responseModel.State = model.State;
                            responseModel.TypeId = model.TypeId;
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


        public List<GetAllAddressModel> GetAllAddres(long userId)
        {
            if (userId != 0)
            {
                string ConnectionStrings = _config.GetConnectionString(connectionString);
                if (ConnectionStrings != null)
                {
                    using (SqlConnection con = new SqlConnection(ConnectionStrings))
                    {
                        SqlCommand cmd = new SqlCommand("spGetAllDetails", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        SqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            List<GetAllAddressModel> list = new List<GetAllAddressModel>();
                            GetAllAddressModel addressModel = new GetAllAddressModel();
                            while (dr.Read())
                            {

                                addressModel.AddressId = Convert.ToInt32(dr["AddressId"]);
                                addressModel.Address = dr["Address"].ToString();
                                addressModel.City = dr["City"].ToString();
                                addressModel.State = dr["State"].ToString();
                                addressModel.TypeId = Convert.ToInt32(dr["TypeId"]);
                                addressModel.UserId = Convert.ToInt32(dr["UserId"]);
                                list.Add(addressModel);
                            }
                            return list;
                        }
                        con.Close();
                        throw new InvalidOperationException("cannot fetched data by book Id");

                    }
                }
                throw new CustomException("not able to connect to database");
            }
            throw new ArgumentNullException("deatails are empty");
        }
    }


}
