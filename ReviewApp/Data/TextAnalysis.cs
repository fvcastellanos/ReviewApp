using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReviewApp.Data
{
    [Table("text_analysis")]
    public class TextAnalysis
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        
        [Column("review_id")]
        public long ReviewId { get; set; }
        
        public Review Review { get; set; }
        
        [Column("query_date", TypeName = "timestamp")]
        public DateTime QueryDate { get; set; }
        
        [Column("json", TypeName = "text")]
        public string Json { get; set; }
        
        [Column("sentiment", TypeName = "varchar(50)")]
        public string Sentiment { get; set; }
        
        [Column("positive_score")]
        public double PositiveScore { get; set; }

        [Column("neutral_score")]
        public double NeutralScore { get; set; }

        [Column("negative_score")]
        public double NegativeScore { get; set; }
    }
}