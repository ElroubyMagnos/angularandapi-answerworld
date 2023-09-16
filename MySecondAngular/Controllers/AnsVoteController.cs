using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Data.SqlClient;
using System.Data;

namespace MySecondAngular.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AnsVoteController : ControllerBase
    {
        private readonly ILogger<AnsVoteController> _logger;

        public AnsVoteController(ILogger<AnsVoteController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public bool Get()
        {
            SQL.Open();

            string GetUp = Request.Query["getupans"];
            string GetDown = Request.Query["getdownans"];
            string quesid = Request.Query["id"];
            string email = Request.Query["email"];
            string userid;

            if (!string.IsNullOrEmpty(email))
            {
                userid = new SqlCommand($"Select ID From Accounts Where Email = '{email}'", SQL.con).ExecuteScalar().ToString();
            }
            else return false;

            if (!string.IsNullOrEmpty(GetUp))
            {
                SqlDataAdapter Adapter = new SqlDataAdapter($"Select ScoreUP From Comments Where ID = '{quesid}'", SQL.con);
                DataTable DT = new DataTable();
                Adapter.Fill(DT);

                DataRow UPReader = DT.Rows[0];

                if (UPReader[0].ToString().Contains(userid + ','))
                {
                    return true;
                }
            }
            else if (!string.IsNullOrEmpty(GetDown))
            {
                SqlDataAdapter Adapter = new SqlDataAdapter($"Select ScoreDown From Comments Where ID = '{quesid}'", SQL.con);
                DataTable DT = new DataTable();
                Adapter.Fill(DT);

                DataRow DownReader = DT.Rows[0];

                if (DownReader[0].ToString().Contains(userid + ','))
                {
                    return true;
                }
            }

            return false;
        }
    }
}