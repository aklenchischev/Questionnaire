using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Questionnaire.Domain.AggregatesModel.PollAggregate;
using Questionnaire.Domain.SeedWork;
using Questionnaire.Domain.Exceptions;


namespace Questionnaire.Infrastructure.Repositories
{
    public class PollRepository: IPollRepository
    {
        private readonly QuestionnaireContext _context;

        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _context;
            }
        }

        public PollRepository(QuestionnaireContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Poll Add(Poll poll)
        {
            return _context.Polls.Add(poll).Entity;
        }

        public async Task<Poll> GetAsync(int pollId)
        {
            var poll = await _context.Polls.FindAsync(pollId);
            if (poll != null)
            {
                await _context.Entry(poll)
                    .Collection(p => p.Sections)
                    .LoadAsync();

                foreach (Section section in poll.Sections)
                {
                    await _context.Entry(section)
                        .Collection(s => s.Questions)
                        .LoadAsync();
                }
            }

            return poll;
        }

        public void Update(Poll poll)
        {
            _context.Entry(poll).State = EntityState.Modified;
        }
    }
}
