using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interfaces
{
    public interface IWishListBL
    {
        string WishListCreation(long bookId, long userId);

        string DeletWishList(long wishListId);

        List<WishListModel> GetWishlistDetailsByUserId(long userId);
    }
}
