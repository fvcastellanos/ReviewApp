
using Microsoft.EntityFrameworkCore;

namespace ReviewApp.Data
{
    public class ReviewContext: DbContext
    {
        
        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Review> Reviews { get; set; }
        
        public virtual DbSet<TextAnalysis> TextAnalyses { get; set; }

        public ReviewContext(DbContextOptions<ReviewContext> dbContext): base(dbContext)
        {
            // db context initialize
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("review_application");

            // Company

            modelBuilder.Entity<Company>()
                .HasMany(p => p.Products);

            modelBuilder.Entity<Company>()
                .HasIndex(p => p.Name)
                .HasName("uq_company_name")
                .IsUnique();

            // Product 

            modelBuilder.Entity<Product>()
                .HasOne(p => p.Company)
                .WithMany(p => p.Products)
                .HasForeignKey(p => p.CompanyId);

            modelBuilder.Entity<Product>()
                .HasIndex(p => p.Name)
                .HasName("uq_product_name")
                .IsUnique();

            // Review

            modelBuilder.Entity<Review>()
                .HasOne(p => p.Product)
                .WithMany(p => p.Reviews)
                .HasForeignKey(p => p.ProductId);

            modelBuilder.Entity<Review>()
                .Property(p => p.ReviewDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            modelBuilder.Entity<Review>()
                .HasIndex(p => p.ReviewDate)
                .HasName("idx_review_date");

            modelBuilder.Entity<Review>()
                .HasIndex(p => p.Stars)
                .HasName("idx_review_starts");

            modelBuilder.Entity<Review>()
                .HasIndex(p => p.Title)
                .HasName("idx_review_title");
            
            // TextAnalysis
            
            modelBuilder.Entity<TextAnalysis>()
                .HasOne(p => p.Review)
                .WithMany(p => p.TextAnalyses)
                .HasForeignKey(p => p.ReviewId);

            modelBuilder.Entity<TextAnalysis>()
                .Property(p => p.QueryDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            modelBuilder.Entity<TextAnalysis>()
                .HasIndex(p => p.Sentiment)
                .HasName("idx_text_analysis_sentiment");
            
            modelBuilder.Entity<TextAnalysis>()
                .HasIndex(p => p.QueryDate)
                .HasName("idx_text_analysis_query_date");
        }
    }
}