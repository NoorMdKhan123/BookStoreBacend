using BusinessLayer.Interfaces;
using CommonLayer.Model;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class AddressBL : IAddressBL
    {
        IAddressRL _addressRL;

        public AddressBL(IAddressRL address)
        {
            _addressRL = address;
        }

        public AddressResponseModel AddingAddressDetails(AddressModel model, long userId)
        {
            return this._addressRL.AddingAddressDetails(model, userId);

        }

        public AddressResponseModel UpdateAddress(AddressModel model, long userId)
        {
            return this._addressRL.UpdateAddress(model, userId);
        }

        public List<GetAllAddressModel> GetAllAddres(long userId)
        {
            return this._addressRL.GetAllAddres(userId);
        }
    }
}
