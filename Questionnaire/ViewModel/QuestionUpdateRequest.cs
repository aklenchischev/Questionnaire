namespace Questionnaire.ViewModel
{
    public class QuestionUpdateRequest
    {
        public int Id { get; set; }
        public string QuestionBody { get; set; }
        public string Answer { get; set; }

        public int PollId { get; set; }
    }
}
