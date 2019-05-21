using System.Threading.Tasks;
using Questionnaire.Domain.SeedWork;

namespace Questionnaire.Domain.AggregatesModel.PollAggregate
{
    public interface IPollRepository : IRepository<Poll>
    {
        Poll Add(Poll poll);

        void Update(Poll poll);

        Task<Poll> GetAsync(int pollId);
    }
}
