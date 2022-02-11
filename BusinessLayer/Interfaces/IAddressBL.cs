using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interfaces
{
    public interface IAddressBL
    {
        AddressResponseModel AddingAddressDetails(AddressModel model, long userId);

        AddressResponseModel UpdateAddress(AddressModel model, long userId);

        List<GetAllAddressModel> GetAllAddres(long userId);
    }
}
