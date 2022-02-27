using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interfaces
{
    public interface IAddressRL
    {
        AddressResponseModel AddingAddressDetails(AddressModel model, long userId);

        AddressResponseModel UpdateAddress(AddressModel model, long userId);

        List<GetAllAddressModel> GetAllAddres(long userId);

    }
}
