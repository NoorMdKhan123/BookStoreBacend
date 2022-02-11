using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interfaces
{
    public interface IBookRL
    {
         BookResponseModel AddingBookDetails(BookModel model, long userId);
        BookDetailsModel UpdateBookDetails(BookDetailsModel model, long bookId);

        List<GetBookDeatils> GetBookDetailsById(long bookId, long userId);

        List<GetBookDeatils> GetAllBookDeatils();

        string DeletBookRecord(long bookId);

    }
}
