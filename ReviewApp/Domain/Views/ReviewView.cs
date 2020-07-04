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
        
        [Required]
        [Range(1, 5)]
        public int Stars { get; set; }
        public long ProductId { get; set; }
        
    }
}