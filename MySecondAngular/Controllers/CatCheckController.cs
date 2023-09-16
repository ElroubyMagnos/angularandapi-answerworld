using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Data.SqlClient;
using System.Data;

namespace MySecondAngular.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CatCheckController : ControllerBase
    {
        private readonly ILogger<CatCheckController> _logger;

        public CatCheckController(ILogger<CatCheckController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public bool Get()
        {
            SQL.Open();
            string CatString = Request.Query["Cat"];
            
            if (!string.IsNullOrEmpty(CatString))
            {
                SqlDataAdapter Adapter = new SqlDataAdapter($"Select * From Category Where CatName = N'{CatString}'", SQL.con);
                DataTable DT = new DataTable();
                Adapter.Fill(DT);
                DataRow Reader = DT.Rows[0];
                
                return DT.Rows.Count > 0;
            }
            
            return false;
        }
    }
}