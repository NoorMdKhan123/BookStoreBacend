using BusinessLayer.Interfaces;
using CommonLayer.Model;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class CartBL : ICartBL
    {
        ICartRL _cartRL;

        public CartBL(ICartRL cartRL)
        {
            _cartRL = cartRL;

        }
        public CartModel AddingBook(BookCart model, long userId, long bookId)
        {
            return this._cartRL.AddingBook(model, userId,bookId);
        }

        public BookUpdateModel UpdateCart(BookUpdateModel model, long cartId)
        {
            return this._cartRL.UpdateCart(model, cartId);
        }

        public List<CartModel> GetAllCartData(long userId)
        {
            return this._cartRL.GetAllCartData(userId);
        }

        public string DeleteCartDetails(long cartId)
        {
            return this._cartRL.DeleteCartDetails(cartId);
        }
    }
}
