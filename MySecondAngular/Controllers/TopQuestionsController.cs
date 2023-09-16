using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Data.SqlClient;
using System.Data;

namespace MySecondAngular.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TopQuestionsController : ControllerBase
    {
        private readonly ILogger<TopQuestionsController> _logger;

        public TopQuestionsController(ILogger<TopQuestionsController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public List<Question> Get()
        {
            SQL.Open();
            List<Question> Questions = new List<Question>();

            SqlDataAdapter Adapter = new SqlDataAdapter($"Select Top 5 * From Questions Order By ScoreUP DESC", SQL.con);
            DataTable DT = new DataTable();
            Adapter.Fill(DT);

            foreach (DataRow Row in DT.Rows)
            {
                Question Question = new Question{
                    ID = int.Parse(Row.ItemArray[0].ToString()),
                    Title = Row.ItemArray[1].ToString(),
                    Content = Row.ItemArray[2].ToString(),
                    Category = Row.ItemArray[3].ToString(),
                    Views = Row.ItemArray[4].ToString(),
                    ScoreUP = Row.ItemArray[6].ToString(),
                    ScoreDown = Row.ItemArray[7].ToString(),
                };

                string Object = new SqlCommand($"Select Username From Accounts Where ID = '{Row.ItemArray[5]}'", SQL.con).ExecuteScalar().ToString();

                Question.OwnerName = Object;

                Questions.Add(Question);
            }
            return Questions;
        }
    }
}