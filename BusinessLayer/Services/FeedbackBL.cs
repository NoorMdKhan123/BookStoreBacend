using BusinessLayer.Interfaces;
using CommonLayer.Model;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
      public class FeedbackBL : IFeedbackBL
    {
        IFeedbackRL _feedbackRL;

        public FeedbackBL(IFeedbackRL feedbackRL)
        {
            _feedbackRL = feedbackRL;
        }

        public FeedbackResponse AddingFeedback(long bookId, FeedbackModel model, long userId)
        {
            return this._feedbackRL.AddingFeedback(bookId, model, userId);
        }

        public List<FeedbackResponse> GetAllReviews(long bookId)
        {
            return this._feedbackRL.GetAllReviews(bookId);

        }
    }
}
