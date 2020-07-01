using System;

namespace ReviewApp.Domain.Views
{
    public class ReviewView
    {
        public long Id { get; set; }
        public DateTime ReviewDate { get; set; }
        public string Content { get; set; }
        public long ProductId { get; set; }
        public ProductView ProductView { get; set; }
    }
}