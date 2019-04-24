using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Questionnaire.Models
{
    public class Poll
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int NotAnsweredQuestionsCount { get; set; }
        public ICollection<Section> Sections { get; set; }
        [Timestamp]
        public byte[] Timestamp { get; set; }
    }
}
