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
            modelBuilder.Entity<Section>()
                .HasOne(s => s.Poll)
                .WithMany(p => p.Sections)
                .HasForeignKey(s => s.PollId);

            modelBuilder.Entity<Question>()
                .HasOne(q => q.Section)
                .WithMany(s => s.Questions)
                .HasForeignKey(q => q.SectionId);
        }
    }
}
