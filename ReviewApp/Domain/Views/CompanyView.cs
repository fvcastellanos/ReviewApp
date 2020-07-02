using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ReviewApp.Domain.Views
{
    public class CompanyView
    {
        public long Id { get; set; }
        
        [Required]
        [MaxLength(150)]
        public string Name { get; set; }
        
        [MaxLength(250)]
        public string Description { get; set; }
        public List<ProductView> ProductViews { get; set; }
    }
}