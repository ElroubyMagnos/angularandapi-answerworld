using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Data.SqlClient;
using System.Data;

namespace MySecondAngular.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SignUpController : ControllerBase
    {
        private readonly ILogger<SignUpController> _logger;

        public SignUpController(ILogger<SignUpController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public bool Get()
        {
            SQL.Open();
            string Username = Request.Query["username"];
            string Email = Request.Query["email"];
            string Password = Request.Query["Password"];

            if (
                string.IsNullOrEmpty(Username) ||
                string.IsNullOrEmpty(Email) ||
                string.IsNullOrEmpty(Password)
            )
            {
                return false;
            }
            else
            {
                new SqlCommand($@"INSERT INTO [dbo].[Accounts]
           ([Email]
           ,[Password]
           ,[Score]
           ,[ProfileViews]
           ,[EmailActiveCode]
           ,[Banned]
           ,[Admin]
           ,[Username]
           ,[PictureURL]
           ,[Bio]
           ,[CoverURL])
     VALUES
           ('{Email}'
           ,'{Password}'
           ,'0'
           ,''
           ,''
           ,'0'
           ,'0'
           ,N'{Username}'
           ,''
           ,N'اهلا انا علي موقع عالم الاجابات'
           ,'')", SQL.con).ExecuteNonQuery();

                return true;
            }
        }
    }
}