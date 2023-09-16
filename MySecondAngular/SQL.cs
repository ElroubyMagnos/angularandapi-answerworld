using System.Data;
using Microsoft.Data.SqlClient;
public static class SQL
{
    public static void Open()
    {
        if (con.State != ConnectionState.Open)
        {
            try
            {
                con.Open();
            }
            catch{

            }
            
        }
    }
    // public static SqlConnection con = new SqlConnection("Data Source=ELROUBY;Initial Catalog=answerworld;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
    public static SqlConnection con = new SqlConnection("Data Source=SQL8006.site4now.net;Initial Catalog=db_a9d539_mydb;User Id=db_a9d539_mydb_admin;Password=a0182163958");
}