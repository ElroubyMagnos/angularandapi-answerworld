using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Data.SqlClient;
using System.Data;

namespace MySecondAngular.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AddFollowController : ControllerBase
    { 
        private readonly ILogger<AddFollowController> _logger;

        public AddFollowController(ILogger<AddFollowController> logger)
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

            if (DT.Rows.Count > 0)
            {
                new SqlCommand($"Delete From Follow Where ID = '{DT.Rows[0][0]}'", SQL.con).ExecuteNonQuery();
            }
            else
            {
                new SqlCommand($@"INSERT INTO [dbo].[Follow]
           ([Follower]
           ,[Following])
     VALUES
           (N'{FollowerID}'
           ,N'{WantfollowID}')", SQL.con).ExecuteNonQuery();
            }

            return true;
        }
    }
}