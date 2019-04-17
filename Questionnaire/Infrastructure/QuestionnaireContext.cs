namespace Questionnaire.Infrastructure
{
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class QuestionnaireContext: DbContext
    {
        public QuestionnaireContext(DbContextOptions<QuestionnaireContext> options) : base (options)
        {
        }

        public DbSet<Poll> Polls { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<Question> Questions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Poll>().HasMany(p => p.Sections).WithOne(s => s.Poll);
            modelBuilder.Entity<Section>().HasMany(s => s.Questions).WithOne(q => q.Section);
        }
    }
}
