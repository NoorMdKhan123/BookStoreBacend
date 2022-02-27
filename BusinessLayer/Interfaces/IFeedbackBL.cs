using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interfaces
{
    public interface IFeedbackBL
    {
        FeedbackResponse AddingFeedback(long bookId, FeedbackModel model, long userId);

        List<FeedbackResponse> GetAllReviews(long bookId);

    }
}
