using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Data.SqlClient;
using System.Data;

namespace MySecondAngular.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GetPMessagesController : ControllerBase
    {
        private readonly ILogger<GetPMessagesController> _logger;

        public GetPMessagesController(ILogger<GetPMessagesController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public List<Message> Get()
        {
            SQL.Open();

            List<Message> Messages = new List<Message>();
            
            string SenderEmail = Request.Query["email"];
            string RecID = Request.Query["recid"];
            
            string Sender = new SqlCommand($"Select Username From Accounts Where Email = '{SenderEmail}'", SQL.con).ExecuteScalar().ToString();
            string Receiver = new SqlCommand($"Select Username From Accounts Where ID = '{RecID}'", SQL.con).ExecuteScalar().ToString();

            SqlDataAdapter Adapter = new SqlDataAdapter($"Select Top 25 * From Messages Where (Sender = '{Sender}' And Receiver = '{Receiver}') OR (Receiver = '{Sender}' And Sender = '{Receiver}') Order By ID DESC", SQL.con);
            DataTable DT = new DataTable();
            Adapter.Fill(DT);

            foreach (DataRow Row in DT.Rows)
            {
                Message Message = new Message();
                Message.ID = int.Parse(Row.ItemArray[0].ToString());
                Message.MessageSent = Row.ItemArray[1].ToString();
                Message.Sender = Row.ItemArray[2].ToString();
                Message.Receiver = Row.ItemArray[3].ToString();

                Messages.Add(Message);
            }

            return Messages;
        }
    }
}