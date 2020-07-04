using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReviewApp.Data
{
    [Table("product")]
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        [Column("name", TypeName = "varchar(150)")]
        public string Name { get; set; }

        [Column("description", TypeName = "varchar(250)")]
        public string Description { get; set; }

        [Column("company_id")]
        public long CompanyId { get; set; }
        
        [Column("image_url", TypeName = "text")]
        public string ImageUrl { get; set; }
        public Company Company { get; set; }

        public IEnumerable<Review> Reviews { get; } = new List<Review>();
    }
}