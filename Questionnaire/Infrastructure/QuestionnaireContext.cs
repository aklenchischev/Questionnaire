using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Questionnaire.Domain.AggregatesModel.PollAggregate;
using Questionnaire.Domain.SeedWork;

namespace Questionnaire.Infrastructure
{
        public class QuestionnaireContext: DbContext, IUnitOfWork
    {
        public QuestionnaireContext(DbContextOptions<QuestionnaireContext> options) : base (options)
        {
        }

        public DbSet<Poll> Polls { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<Question> Questions { get; set; }

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            var result = await base.SaveChangesAsync();

            return true;
        }
    }
}
