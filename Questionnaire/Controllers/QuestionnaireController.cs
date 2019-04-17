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
        public async Task<ActionResult<IEnumerable<Poll>>> GetPolls()
        {
            return await _questionnaireContext.Polls
                    .Include(p => p.Sections)
                    .ToListAsync();
        }

        // GET: api/[controller]/polls/1
        [HttpGet("polls/{id:int}")]
        [Route("polls")]
        public async Task<ActionResult<Poll>> GetPoll(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var poll = await _questionnaireContext.Polls.FindAsync(id);

            if (poll == null)
            {
                return NotFound();
            }

            return poll;
        }

        // POST: api/Questionnaire
        [HttpPost]
        [Route("polls")]
        public async Task<ActionResult<Poll>> PostPoll(Poll poll)
        {
            _questionnaireContext.Polls.Add(poll);
            await _questionnaireContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPoll), new { id = poll.Id }, poll);
        }
    }
}
