using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Net;

namespace MySecondAngular.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GetPCommentsController : ControllerBase
    {
        private readonly ILogger<GetPCommentsController> _logger;

        public GetPCommentsController(ILogger<GetPCommentsController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public List<Comment> Get()
        {
            SQL.Open();
            
            List<Comment> Comments = new List<Comment>();

            string OwnerID = Request.Query["id"];
            if (!string.IsNullOrEmpty(OwnerID))
            {
                SqlDataAdapter Adapter = new SqlDataAdapter($"Select * From Comments Where OwnerID = '{OwnerID}' Order By ID DESC", SQL.con);
                DataTable DT = new DataTable();
                Adapter.Fill(DT);

                foreach (DataRow Row in DT.Rows)
                {
                    Comment Comment = new Comment();
                    Comment.ID = int.Parse(Row.ItemArray[0].ToString());
                    Comment.Text = Row.ItemArray[1].ToString();
                    Comment.OwnerID = int.Parse(Row.ItemArray[2].ToString());
                    Comment.Views = Row.ItemArray[3].ToString();
                    Comment.ScoreUP = Row.ItemArray[4].ToString();
                    Comment.ScoreDown = Row.ItemArray[5].ToString();
                    Comment.Question = int.Parse(Row.ItemArray[6].ToString());

                    string Object = new SqlCommand($"Select Username From Accounts Where ID = '{Comment.OwnerID}'", SQL.con).ExecuteScalar().ToString();

                    Comment.OwnerName = Object;

                    string Object2 = new SqlCommand($"Select PictureURL From Accounts Where ID = '{Comment.OwnerID}'", SQL.con).ExecuteScalar().ToString();

                    Comment.PIPath = Object2;

                    if (!CheckImage(Comment.PIPath))
                    {
                        Comment.PIPath = "assets/Images/personal.ico";
                    }

                    Comments.Add(Comment);
                }
            }
            
            return Comments;
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