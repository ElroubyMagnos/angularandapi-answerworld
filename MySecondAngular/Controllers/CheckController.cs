using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Data.SqlClient;
using System.Data;

namespace MySecondAngular.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CheckController : ControllerBase
    {
        private readonly ILogger<CheckController> _logger;

        public CheckController(ILogger<CheckController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public bool Get()
        {
            SQL.Open();
            string ID = Request.Query["id"];

            string Email = Request.Query["Email"];
            string Password = Request.Query["Password"];
            string Username = Request.Query["Username"];

            if (!string.IsNullOrEmpty(Username))
            {
                SqlDataAdapter Adapter = new SqlDataAdapter($"Select * From Accounts Where Username = '{Username}'", SQL.con);
                DataTable DT = new DataTable();
                Adapter.Fill(DT);

                return DT.Rows.Count > 0;
            }
            else if (string.IsNullOrEmpty(Password) && !string.IsNullOrEmpty(Email))
            {
                SqlDataAdapter Adapter = new SqlDataAdapter($"Select * From Accounts Where Email = '{Email}'", SQL.con);
                DataTable DT = new DataTable();
                Adapter.Fill(DT);

                return DT.Rows.Count > 0;
            }
            else if (!string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(Password))
            {
                SqlDataAdapter Adapter = new SqlDataAdapter($"Select * From Accounts Where Email = '{Email}' And Password = '{Password}'", SQL.con);
                DataTable DT = new DataTable();
                Adapter.Fill(DT);

                return DT.Rows.Count > 0;
            }
            else if (!string.IsNullOrEmpty(ID))
            {
                SqlDataAdapter Adapter = new SqlDataAdapter($"Select * From Accounts Where ID = '{ID}'", SQL.con);
                DataTable DT = new DataTable();
                Adapter.Fill(DT);

                return DT.Rows.Count > 0;
            }
           
            return false;
        }
    }
}