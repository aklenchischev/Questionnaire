using System;
using System.Collections.Generic;

namespace Questionnaire.Models
{
    public class Poll
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<Section> Sections { get; set; }
    }
}
