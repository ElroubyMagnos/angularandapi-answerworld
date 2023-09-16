using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Data.SqlClient;
using System.Data;

namespace MySecondAngular.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AddCommentController : ControllerBase
    {
        private readonly ILogger<AddCommentController> _logger;

        public AddCommentController(ILogger<AddCommentController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public bool Get()
        {
            SQL.Open();
            string Text = Request.Query["Text"];
            string QuesID = Request.Query["QuesID"];
            string Email = Request.Query["Email"];

            if (!string.IsNullOrEmpty(Text) && !string.IsNullOrEmpty(QuesID) && !string.IsNullOrEmpty(Email))
            {
                string Object = new SqlCommand($"Select ID From Accounts Where Email = '{Email}'", SQL.con).ExecuteScalar().ToString();

                new SqlCommand($@"INSERT INTO [dbo].[Comments]
            ([Text]
            ,[OwnerID]
            ,[Views]
            ,[ScoreUP]
            ,[ScoreDown]
            ,[Question])
        VALUES
            (N'{Text}'
            ,'{Object}'
            ,''
            ,''
            ,''
            ,'{QuesID}')", SQL.con).ExecuteNonQuery();
            }
            else return false;
            
           
            return true;
        }
    }
}