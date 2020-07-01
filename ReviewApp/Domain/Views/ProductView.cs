using System.Collections.Generic;
using LanguageExt;

namespace ReviewApp.Domain.Views
{
    public class ProductView
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public long CompanyId { get; set; }
        
        public List<ReviewView> ReviewViews { get; set; }
    }
}