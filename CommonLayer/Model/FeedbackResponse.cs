using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.Model
{
    public class FeedbackResponse
    {


        public long FeedbackId { get; set; }
        public string FullName { get; set; }
        public string Comment { get; set; }
        public float Rating { get; set; }


    }
}
