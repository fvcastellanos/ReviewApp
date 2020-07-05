using System;
using System.ComponentModel.DataAnnotations;

namespace ReviewApp.Domain.Views
{
    public class ReviewView
    {
        public long Id { get; set; }
        public DateTime ReviewDate { get; set; }
        
        [Required]
        [MaxLength(50)]
        public string Title { get; set; }
        
        [Required]
        [MaxLength(250)]
        public string Content { get; set; }
        
        public int Stars { get; set; }
        public long ProductId { get; set; }
        public string Sentiment { get; set; }
        
        public string Language { get; set; }
        
    }
}