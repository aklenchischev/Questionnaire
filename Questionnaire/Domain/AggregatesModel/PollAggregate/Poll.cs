using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using Questionnaire.Domain.SeedWork;
using Questionnaire.Domain.Exceptions;

namespace Questionnaire.Domain.AggregatesModel.PollAggregate
{
    public class Poll : Entity, IAggregateRoot
    {
        private string _pollName;
        private int _notAnsweredQuestions;

        private readonly List<Section> _sections;
        public IReadOnlyCollection<Section> Sections => _sections;

        [Timestamp]
        public byte[] Timestamp { get; set; }

        protected Poll()
        {
            _sections = new List<Section>();
        }

        public Poll(string pollName, int notAnsweredQuestions = 0)
        {
            _pollName = pollName;
            _notAnsweredQuestions = notAnsweredQuestions;
        }

        public string GetPollName() => _pollName;

        public int GetNotAnsweredQuestions()
        {
            return _notAnsweredQuestions;
        }

        public void DecrementNotAnsweredQuestions()
        {
            if (_notAnsweredQuestions <= 0)
            {
                throw new PollDomainException("Total of not answered question cannot be negative");
            }

            _notAnsweredQuestions--;
        }

        public void AddSection(int sectionId, string sectionName)
        {
            var existingSectionForPoll = _sections.Where(s => s.SectionId == sectionId)
                .SingleOrDefault();

            if (existingSectionForPoll == null)
            {
                var section = new Section(sectionId, sectionName);
                _sections.Add(section);
            }
        }
    }
}
