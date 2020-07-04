using System;
using System.Collections.Generic;
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
        [Column("review_date", TypeName = "timestamp")]
        public DateTime ReviewDate { get; set; }

        [Required]
        [Column("starts", TypeName = "int")]
        public int Starts { get; set; }
        
        [Required]
        [Column("title", TypeName = "varchar(150)")]
        public string Title { get; set; }

        [Required]
        [Column("content", TypeName = "text")]
        public string Content { get; set; }
        
        [Column("product_id")]
        public long ProductId { get; set; }
        
        public Product Product { get; set; }

        public IEnumerable<TextAnalysis> TextAnalysis { get; } = new List<TextAnalysis>();
    }
}