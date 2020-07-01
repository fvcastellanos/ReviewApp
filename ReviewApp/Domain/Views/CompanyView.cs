using System.Collections.Generic;

namespace ReviewApp.Domain.Views
{
    public class CompanyView
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<ProductView> ProductViews { get; set; }
    }
}