using System;
using System.Collections.Generic;

namespace Questionnaire.Models
{
    public class Section
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Question> Questions { get; set; }
    }
}
