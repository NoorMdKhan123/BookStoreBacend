using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interfaces
{
    public interface IBookBL
    {
        BookResponseModel AddingBookDetails(BookModel bookModel, long userId);

        BookDetailsModel UpdateBookDetails(BookDetailsModel model, long bookId);

        List<GetBookDeatils> GetBookDetailsById(long bookId, long userId);

        List<GetBookDeatils> GetAllBookDeatils();

         string DeletBookRecord(long bookId);

        BookResponseModel UpdateImage(long bookId, IFormFile bookImage, long userId);

    }
}
