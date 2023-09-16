using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Data.SqlClient;
using System.Data;

namespace MySecondAngular.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VottingQuesController : ControllerBase
    {
        private readonly ILogger<VottingQuesController> _logger;

        public VottingQuesController(ILogger<VottingQuesController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public int Get()
        {
            SQL.Open();

            string UP = Request.Query["UP"];
            string Down = Request.Query["Down"];
            string quesid = Request.Query["id"];
            string email = Request.Query["email"];
            int Score = 0;

            string userid = new SqlCommand($"Select ID From Accounts Where Email = '{email}'", SQL.con).ExecuteScalar().ToString();

            SqlDataAdapter Adapter = new SqlDataAdapter($"Select * From Questions Where ID = '{quesid}'", SQL.con);
            DataTable DT = new DataTable();
            Adapter.Fill(DT);

            DataRow Reader = DT.Rows[0];
            string Up6 = Reader[6].ToString();
            string Down7 = Reader[7].ToString();
                
            if (!string.IsNullOrEmpty(UP))
            {
                if (Up6.Contains(userid + ","))
                {
                    Up6 = Up6.Replace(userid + ",", "");
                }
                else
                {
                    Up6 = Up6 + userid + ",";
                }

                new SqlCommand($"Update Questions Set ScoreUP = '{Up6}' Where ID = '{quesid}'", SQL.con).ExecuteNonQuery();

                if (Down7.Contains(userid + ","))
                {
                    Down7 = Down7.Replace(userid + ",", "");

                    new SqlCommand($"Update Questions Set ScoreDown = '{Down7}' Where ID = '{quesid}'", SQL.con).ExecuteNonQuery();
                }
            }
            else if (!string.IsNullOrEmpty(Down))
            {
                if (Down7.Contains(userid + ","))
                {
                    Down7 = Down7.Replace(userid + ",", "");
                }
                else
                {
                    Down7 = Down7 + userid + ",";
                }

                new SqlCommand($"Update Questions Set ScoreDown = '{Down7}' Where ID = '{quesid}'", SQL.con).ExecuteNonQuery();

                if (Up6.Contains(userid + ","))
                {
                    Up6 = Up6.Replace(userid + ",", "");

                    new SqlCommand($"Update Questions Set ScoreUP = '{Up6}' Where ID = '{quesid}'", SQL.con).ExecuteNonQuery();
                }
            }
            Score = (Up6.Split(',').Length - 1) - (Down7.Split(',').Length - 1);
            
            return Score;
        }
    }
}