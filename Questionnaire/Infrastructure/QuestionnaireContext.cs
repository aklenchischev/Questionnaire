namespace Questionnaire.Infrastructure
{
    using Microsoft.EntityFrameworkCore;
    using Domain.AggregatesModel.PollAggregate;

    public class QuestionnaireContext: DbContext
    {
        public QuestionnaireContext(DbContextOptions<QuestionnaireContext> options) : base (options)
        {
        }

        public DbSet<Poll> Polls { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<Question> Questions { get; set; }
    }
}
