using System;
using System.Collections.Generic;
using System.Linq;
using Questionnaire.Domain.SeedWork;

namespace Questionnaire.Domain.AggregatesModel.PollAggregate
{
    public class Section : Entity
    {
        public int SectionId { get; private set; }

        private string _sectionName;

        private readonly List<Question> _questions;
        public IReadOnlyCollection<Question> Questions => _questions;

        protected Section()
        {
            _questions = new List<Question>();
        }

        public Section(int sectionId, string sectionName)
        {
            SectionId = sectionId;
            _sectionName = sectionName;
        }

        public void AddQuestion(int questionId, string questionBody)
        {
            var existingQuestionForSection = _questions.Where(q => q.Id == questionId)
                .SingleOrDefault();

            if (existingQuestionForSection == null)
            {
                var question = new Question(questionId, questionBody);
                _questions.Add(question);
            }
        }

        public string GetSectionName() => _sectionName;
    }
}