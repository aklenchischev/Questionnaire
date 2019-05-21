using System;
using Questionnaire.Domain.SeedWork;

namespace Questionnaire.Domain.AggregatesModel.PollAggregate
{
    public class Question : Entity
    {
        public int QuestionId { get; private set; }

        private string _questionBody;
        private string _answer;

        protected Question() { }

        public Question(int questionId, string questionBody)
        {
            QuestionId = questionId;
            _questionBody = questionBody;
        }

        public string GetQuestionBody() => _questionBody;

        public string GetAnswer() => _answer;

        public void SetNewAnswer(string answer)
        {
            _answer = answer;
        }
    }
}