using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Data.SqlClient;
using System.Data;

namespace MySecondAngular.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CheckFollowController : ControllerBase
    { 
        private readonly ILogger<CheckFollowController> _logger;

        public CheckFollowController(ILogger<CheckFollowController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public bool Get()
        {
            SQL.Open();
            string WantfollowID = Request.Query["wantfollow"];
            string FEmail = Request.Query["email"];

            string FollowerID = new SqlCommand($"Select ID From Accounts Where Email = '{FEmail}'", SQL.con).ExecuteScalar().ToString();
            
            SqlDataAdapter Adapter = new SqlDataAdapter($"Select * From Follow Where Follower = '{FollowerID}' And Following = '{WantfollowID}'", SQL.con);
            DataTable DT = new DataTable();
            Adapter.Fill(DT);

            return DT.Rows.Count > 0;
        }
    }
}