using BusinessLayer.Interfaces;
using CommonLayer.Model;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class WishListBL : IWishListBL
    {
        IWishListRL _wishListRL;


        public WishListBL(IWishListRL wishListRL)
        {
            _wishListRL = wishListRL;
        }

        public string WishListCreation(long bookId, long userId)
        {
            return this._wishListRL.WishListCreation(bookId, userId);
        }

        public string DeletWishList(long wishListId)
        {
            return this._wishListRL.DeletWishList(wishListId);
        }

        public List<WishListModel> GetWishlistDetailsByUserId(long userId)
        {
            return this._wishListRL.GetWishlistDetailsByUserId(userId);
        }
    }
    
}
