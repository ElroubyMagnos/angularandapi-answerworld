using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Data.SqlClient;
using System.Data;

namespace MySecondAngular.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GetOneQuesController : ControllerBase
    {
        private readonly ILogger<GetOneQuesController> _logger;

        public GetOneQuesController(ILogger<GetOneQuesController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public Question Get()
        {
            SQL.Open();
            Question Question = new Question();
            string ID = Request.Query["id"];

            SqlDataAdapter Adapter = new SqlDataAdapter($"Select * From Questions Where ID = '{ID}'", SQL.con);
            DataTable DT = new DataTable();
            Adapter.Fill(DT);

            DataRow Reader = DT.Rows[0];
            if (!string.IsNullOrEmpty(ID))
            {
                Question.ID = int.Parse(Reader[0].ToString());
                Question.Title = Reader[1].ToString();
                Question.Content = Reader[2].ToString();
                Question.Category = Reader[3].ToString();
                Question.Views = Reader[4].ToString();
                Question.ScoreUP = Reader[6].ToString();
                Question.ScoreDown = Reader[7].ToString();

                string UserID = Reader[5].ToString();

                string Object = new SqlCommand($"Select Username From Accounts Where ID = '{UserID}'", SQL.con).ExecuteScalar().ToString();

                Question.OwnerName = Object;
            }


            return Question;
        }
    }
}