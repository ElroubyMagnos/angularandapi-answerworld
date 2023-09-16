using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Data.SqlClient;
using System.Data;

namespace MySecondAngular.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BioController : ControllerBase
    {
        private readonly ILogger<BioController> _logger;

        public BioController(ILogger<BioController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public bool Get()
        {
            SQL.Open();

            string New = Request.Query["New"];
            string Email = Request.Query["Email"];

            if (!string.IsNullOrEmpty(New) && !string.IsNullOrEmpty(New))
            {
                new SqlCommand($"Update Accounts Set Bio = N'{New}' Where Email = '{Email}'", SQL.con).ExecuteNonQuery();

                return true;
            }
            
            return false;
        }
    }
}