using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Questionnaire.Infrastructure;
using Questionnaire.Models;


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
            var pollToAdd = new Poll
            {
                Name = poll.Name,
                Sections = poll.Sections
            };

            _questionnaireContext.Polls.Add(pollToAdd);

            try
            {
                await _questionnaireContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return CreatedAtAction(nameof(PollByIdAsync), new { id = pollToAdd.Id }, null);
        }

        // PUT api/[controller]/questions
        [Route("questions")]
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<ActionResult> UpdateQuestionAsync([FromBody]Question questionToUpdate)
        {
            var question = await _questionnaireContext.Questions.SingleOrDefaultAsync(q => q.Id == questionToUpdate.Id);

            if (question == null)
            {
                return NotFound(new { Message = $"Question with id {questionToUpdate.Id} not found." });
            }

            question.Answer = questionToUpdate.Answer;
            question.QuestionBody = questionToUpdate.QuestionBody;
            try { 
                _questionnaireContext.Questions.Update(question);
                await _questionnaireContext.SaveChangesAsync();
            } catch (Exception e)
            {
                Console.WriteLine(e.Message);
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
