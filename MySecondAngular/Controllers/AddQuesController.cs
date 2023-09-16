using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Data.SqlClient;
using System.Data;

namespace MySecondAngular.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AddQuesController : ControllerBase
    {
        private readonly ILogger<AddQuesController> _logger;

        public AddQuesController(ILogger<AddQuesController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public void Get()
        {
            SQL.Open();
            string Title = Request.Query["title"];
            string Content = Request.Query["content"];
            string Cat = Request.Query["cat"];
            string Email = Request.Query["email"];

            string Object = new SqlCommand($"Select ID From Accounts Where Email = '{Email}'", SQL.con).ExecuteScalar().ToString();

            new SqlCommand($@"INSERT INTO [dbo].[Questions]
           ([Title]
           ,[QuesContent]
           ,[Category]
           ,[Views]
           ,[OwnerID]
           ,[ScoreUP]
           ,[ScoreDown])
     VALUES
           (N'{Title}'
           ,N'{Content}'
           ,N'{Cat}'
           ,''
           ,'{Object}'
           ,''
           ,'')", SQL.con).ExecuteNonQuery();
        }
    }
}