using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Net;

namespace MySecondAngular.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SignedInController : ControllerBase
    {
        private readonly ILogger<SignedInController> _logger;

        public SignedInController(ILogger<SignedInController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public List<SignedInUser> Get()
        {
            SQL.Open();
            string Email = Request.Query["email"];
            List<SignedInUser> SIUs = new List<SignedInUser>();
            
            SqlDataAdapter Adapter = new SqlDataAdapter($"Select * From Accounts Where Email = '{Email}'", SQL.con);
            DataTable DT = new DataTable();
            Adapter.Fill(DT);

            SIUs.Add(new SignedInUser{
                ID = int.Parse(DT.Rows[0][0].ToString()),
                Username = DT.Rows[0][8].ToString(),
                Picpath = DT.Rows[0][9].ToString(),
                Email = Email
            });

            if (!CheckImage(SIUs[0].Picpath))
            {
                SIUs[0].Picpath = "assets/Images/personal.ico";
            }

            return SIUs;
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