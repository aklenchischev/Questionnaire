using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Questionnaire.Infrastructure;
using Questionnaire.Models;
using System.Threading;


namespace Questionnaire.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionnaireController : ControllerBase
    {
        private readonly QuestionnaireContext _questionnaireContext;

        public QuestionnaireController(QuestionnaireContext context)
        {
            _questionnaireContext = context ?? throw new ArgumentNullException(nameof(context));
        }

        // GET api/[controller]/polls
        [HttpGet]
        [Route("polls")]
        [ProducesResponseType(typeof(IEnumerable<Poll>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> PollsAsync()
        {
            var polls = await _questionnaireContext.Polls
                .Include(p => p.Sections)
                    .ThenInclude(s => s.Questions)
                .ToListAsync();

            return Ok(polls);
        }

        // GET api/[controller]/polls/2
        [HttpGet]
        [Route("polls/{id:int}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(Poll), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Poll>> PollByIdAsync(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var poll = await _questionnaireContext.Polls
                .Include(p => p.Sections)
                    .ThenInclude(s => s.Questions)
                .SingleOrDefaultAsync(p => p.Id == id);

            if (poll != null)
            {
                return poll;
            }

            return NotFound();
        }

        //POST api/[controller]/polls
        [Route("polls")]
        [HttpPost]
        [ProducesResponseType(typeof(Poll), (int)HttpStatusCode.Created)]
        public async Task<ActionResult> CreatePollAsync([FromBody]Poll poll)
        {
            int questionsCount = 0;
            foreach (Section section in poll.Sections)
            {
                if (section.Questions != null)
                    questionsCount += section.Questions.Count;
            }

            var pollToAdd = new Poll
            {
                Name = poll.Name,
                Sections = poll.Sections,
                NotAnsweredQuestionsCount = questionsCount
            };

            _questionnaireContext.Polls.Add(pollToAdd);

            await _questionnaireContext.SaveChangesAsync();

            return CreatedAtAction(nameof(PollByIdAsync), new { id = pollToAdd.Id }, null);
        }

        // PUT api/[controller]/questions
        [Route("questions")]
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<ActionResult> UpdateAnswerForQuestionAsync([FromBody]Question questionToUpdate)
        {
            var saved = false;

            if (questionToUpdate.Answer == null)
            {
                return NotFound(new { Message = $"Expected an answer to the question with id {questionToUpdate.Id}." });
            }

            var question = await _questionnaireContext.Questions
                .Include(q => q.Section)
                    .ThenInclude(s => s.Poll)
                .FirstOrDefaultAsync(q => q.Id == questionToUpdate.Id);

            if (question == null)
            {
                return NotFound(new { Message = $"The question with id {questionToUpdate.Id} not found." });
            }

            while (!saved)
            {
                try
                {
                    if (question.Answer == null && questionToUpdate.Answer != null)
                        question.Section.Poll.NotAnsweredQuestionsCount--;
                    question.Answer = questionToUpdate.Answer;

                    _questionnaireContext.Questions.Update(question);
                    await _questionnaireContext.SaveChangesAsync();

                    saved = true;
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    foreach (var entry in ex.Entries)
                    {
                        if (entry.Entity is Poll)
                        {
                            var proposedValues = entry.CurrentValues;
                            var databaseValues = entry.GetDatabaseValues();
                            
                            foreach (var property in proposedValues.Properties)
                            {
                                var databaseValue = databaseValues[property];

                                proposedValues[property] = databaseValue; 
                            }
                            entry.OriginalValues.SetValues(databaseValues);
                        }
                        else
                        {
                            throw new NotSupportedException(
                                "Don't know how to handle concurrency conflicts for "
                                + entry.Metadata.Name);
                        }
                    }
                }

                _questionnaireContext.Entry(question).Reload();
            }

            return CreatedAtAction(nameof(QuestionByIdAsync), new { id = questionToUpdate.Id }, null);
        }

        // GET api/[controller]/questions/2
        [HttpGet]
        [Route("questions/{id:int}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(Question), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Question>> QuestionByIdAsync(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var question = await _questionnaireContext.Questions.SingleOrDefaultAsync(q => q.Id == id);

            if (question != null)
            {
                return question;
            }

            return NotFound();
        }
    }
}
