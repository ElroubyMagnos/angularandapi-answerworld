using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Net;
using System.Reflection;

namespace MySecondAngular.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly ILogger<AccountController> _logger;

        public AccountController(ILogger<AccountController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public List<User> Get()
        {
            SQL.Open();
            string ID = Request.Query["id"];
            
            SqlDataAdapter adapter = null;

            if (string.IsNullOrEmpty(ID))
                adapter = new SqlDataAdapter("Select Top 10 * From Accounts Order By Score DESC", SQL.con);
            else adapter = new SqlDataAdapter($"exec GetOneAccount {ID}", SQL.con);

            DataTable DT = new DataTable();
            adapter.Fill(DT);

            List<User> Users = new List<User>();

            foreach (DataRow Row in DT.Rows)
            {
                User User = new User{
                    ID = int.Parse(Row.ItemArray[0].ToString()),
                    Email = Row.ItemArray[1].ToString(),
                    Password = Row.ItemArray[2].ToString(),
                    Score = int.Parse(Row.ItemArray[3].ToString()),
                    ViewsData = Row.ItemArray[4].ToString(),
                    EmailActiveCode = Row.ItemArray[5].ToString(),
                    Banned = int.Parse(Row.ItemArray[6].ToString()),
                    Admin = int.Parse(Row.ItemArray[7].ToString()),
                    Username = Row.ItemArray[8].ToString(),
                    PictureURL = Row.ItemArray[9].ToString(),
                    Bio = Row.ItemArray[10].ToString(),
                    CoverURL = Row.ItemArray[11].ToString()
                };

                if (!CheckImage(User.PictureURL))
                {
                    User.PictureURL = "assets/Images/personal.ico";
                }

                if (!CheckImage(User.CoverURL))
                {
                    User.CoverURL = "assets/Images/cover.jpg";
                }
                

                Users.Add(User);
            }
            return Users;
        }

        public bool CheckImage(string url)
        {
            try 
            {
                WebRequest webRequest = WebRequest.Create(url);  
                WebResponse webResponse;
                webResponse = webRequest.GetResponse();
            }
            catch //If exception thrown then couldn't get response from address
            {
                return false;
            } 

            return true;
        }
    }
}