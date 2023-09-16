using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Data.SqlClient;
using System.Data;

namespace MySecondAngular.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CatController : ControllerBase
    {
        private readonly ILogger<CatController> _logger;

        public CatController(ILogger<CatController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public List<Category> Get()
        {
            SQL.Open();
            string AddedCat = Request.Query["Cat"];
            string Email = Request.Query["Email"];

            if (!string.IsNullOrEmpty(AddedCat) && !string.IsNullOrEmpty(Email))
            {
                string ID = new SqlCommand($"Select ID From Accounts Where Email = '{Email}'", SQL.con).ExecuteScalar().ToString();

                new SqlCommand($@"INSERT INTO [dbo].[Category]
           ([CatName]
           ,[CatOwnerID])
     VALUES
           (N'{AddedCat}'
           ,N'{ID}')", SQL.con).ExecuteNonQuery();
                return null;
            }

            List<Category> Categories = new List<Category>();

            SqlDataAdapter Adapter = new SqlDataAdapter("Select * From Category", SQL.con);
            DataTable DT = new DataTable();
            Adapter.Fill(DT);

            foreach (DataRow Row in DT.Rows)
            {
                Categories.Add(new Category() {
                    ID = int.Parse(Row.ItemArray[0].ToString()),
                    CatName = Row.ItemArray[1].ToString(),
                    CatOwnerID = int.Parse(Row.ItemArray[2].ToString())
                });
            }
            return Categories;
        }
    }
}