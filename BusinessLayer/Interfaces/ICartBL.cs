using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interfaces
{
    public interface ICartBL
    {
        CartModel AddingBook(BookCart model, long userId, long bookId);

        BookUpdateModel UpdateCart(BookUpdateModel model, long cartId);

        List<CartModel> GetAllCartData(long userId);

        string DeleteCartDetails(long cartId);
    }
}
