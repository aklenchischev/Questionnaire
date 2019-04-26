using System.Threading.Tasks;
using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;
using Questionnaire;
using System.Net.Http;
using System.Text;
using System.Threading;

namespace IntegrationTests
{
    public class QuestionnaireTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;

        public static string Questions = "https://localhost:44359/api/Questionnaire/questions";
        public static string Polls = "https://localhost:44359/api/Questionnaire/polls";

        private static EventWaitHandle ewh;

        public QuestionnaireTests(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Theory]
        [InlineData("https://localhost:44359/api/Questionnaire/polls")]
        [InlineData("https://localhost:44359/api/Questionnaire/polls/1")]
        [InlineData("https://localhost:44359/api/Questionnaire/questions/1")]
        public async Task GetHttpRequests(string url)
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync(url);

            response.EnsureSuccessStatusCode();
            Assert.Equal("application/json; charset=utf-8",
                response.Content.Headers.ContentType.ToString());
        }

        [Fact]
        public async Task PutQuestionHttpRequest()
        {
            var client = _factory.CreateClient();

            var jsonString = "{\"id\":13,\"answer\":\"AnswerWithCount3\"}";

            var response = await client.PutAsync(Questions, 
                new StringContent(jsonString, Encoding.UTF8, "application/json"));

            response.EnsureSuccessStatusCode();
        }
    }
}
