using BusinessLayer.Interfaces;
using CommonLayer.Model;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class BookBL : IBookBL
    {
        IBookRL bookRL;
        public BookBL(IBookRL bookRL)
        {
            this.bookRL = bookRL;
        }
        public BookResponseModel AddingBookDetails(BookModel model, long userId)
        {

            return this.bookRL.AddingBookDetails(model, userId);
        }

        public BookDetailsModel UpdateBookDetails(BookDetailsModel model, long bookId)
        {
            return this.bookRL.UpdateBookDetails(model, bookId);
        }

        public List<GetBookDeatils> GetBookDetailsById(long bookId, long userId)
        {
            return this.bookRL.GetBookDetailsById(bookId, userId);

        }

        public List<GetBookDeatils> GetAllBookDeatils()
        {
            return this.bookRL.GetAllBookDeatils();
        }
       

        public string DeletBookRecord(long bookId)
        {
            return this.bookRL.DeletBookRecord(bookId);
        }
    }
}
