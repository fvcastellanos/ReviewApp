using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReviewApp.Data
{
    [Table("review")]
    public class Review
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        [Column("review_date")]
        public DateTime ReviewDate { get; set; }

        [Required]
        [Column("content")]
        public string Content { get; set; }

        [Column("product_id")]
        public long ProductId { get; set; }
        
        public Product Product { get; set; }

    }
}