using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interfaces
{
    public interface ICartRL
    {
        CartModel AddingBook(BookCart model, long userId);

        BookUpdateModel UpdateCart(BookUpdateModel model, long cartId);

        List<CartModel> GetAllCartData(long userId);

        string DeleteCartDetails(long cartId);
    }
}
