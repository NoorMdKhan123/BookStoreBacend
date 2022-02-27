using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interfaces
{
    public interface IFeedbackRL
    {
        FeedbackResponse AddingFeedback(long bookId, FeedbackModel model, long userId);
        List<FeedbackResponse> GetAllReviews(long bookId);
    }
}
