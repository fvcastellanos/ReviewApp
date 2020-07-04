using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReviewApp.Data
{
    [Table("company")]
    public class Company
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        [Column("name", TypeName = "varchar(150)")]
        public string Name { get; set; }

        [Column("description", TypeName = "varchar(250)")]
        public string Description { get; set; }

        public IEnumerable<Product> Products { get; } = new List<Product>();
    }
}