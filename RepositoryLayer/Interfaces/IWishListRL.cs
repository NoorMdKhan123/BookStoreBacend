using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interfaces
{
    public interface IWishListRL
    {
        string WishListCreation(long bookId, long userId);

        string DeletWishList(long bookId);

        List<WishListModel> GetWishlistDetailsByUserId(long userId);
    }
}
