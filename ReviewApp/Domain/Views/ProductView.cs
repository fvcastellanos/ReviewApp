using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using LanguageExt;

namespace ReviewApp.Domain.Views
{
    public class ProductView
    {
        public long Id { get; set; }

        [Required]
        [MaxLength(150)]
        public string Name { get; set; }
        
        [MaxLength(250)]
        public string Description { get; set; }

        public long CompanyId { get; set; }
        
        [Required]
        [Range(1, long.MaxValue)]
        public string CompanyIdValue { get; set; }
        
        public string CompanyName { get; set; }
        public List<ReviewView> ReviewViews { get; set; }
    }
}