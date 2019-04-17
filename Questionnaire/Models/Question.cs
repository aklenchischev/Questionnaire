using System;

namespace Questionnaire.Models
{
    public class Question
    {
        public int Id { get; set; }

        public string QuestionBody { get; set; }

        public string Answer { get; set; }
    }
}
