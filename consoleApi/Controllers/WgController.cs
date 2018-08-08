using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using APWebGrabberActiveXControlLib;
using System.Configuration;
using System.Data.SqlClient;

namespace consoleApi
{

    public class Setting
    {
        public string HTML { get; set; }
        public string DocName { get; set; }
    }

    public class WgController : ApiController
    {
        public SqlConnection connection;

        //Get api/wg
        [HttpGet]
        public IEnumerable<string> Get()
        {
            Properties.Settings S = new Properties.Settings();
            string DocName = "doc";
            string HTML = "https://google.com";
            using (connection = new SqlConnection(S.Database2ConnectionString))
            {



                connection.Open();

                SqlCommand HTMLCommand = new SqlCommand("SELECT HTML FROM Settings WHERE Id = 1", connection);

                SqlDataReader reader = HTMLCommand.ExecuteReader();

                while (reader.Read())
                {
                    HTML = reader[0].ToString();
                }
                connection.Close();

                connection.Open();

                SqlCommand DocNameCommand = new SqlCommand("SELECT NewDocName FROM Settings WHERE Id = 1", connection);

                reader = DocNameCommand.ExecuteReader();

                while (reader.Read())
                {
                    DocName = reader[0].ToString();
                }
                Console.WriteLine(HTML + DocName);

                connection.Close();
            };

            APWebGrabber wg = new APWebGrabber();

            wg.OutputDirectory = @"C:/Users/Administrator/Desktop";
            wg.NewDocumentName = DocName;
            wg.URL = HTML;
            wg.ConvertToPDF("127.0.0.1", 52525);

            return new string[] { "Hello", "World" };
        }
        
        
            
        //Post
        [HttpPost]
        public void Post([FromBody] Setting opt)
        {
            Console.WriteLine(opt.DocName);
            Console.WriteLine(opt.HTML);

            Properties.Settings S = new Properties.Settings();

            using (connection = new SqlConnection(S.Database2ConnectionString))
            {
                connection.Open();

                
                SqlParameter DocName = new SqlParameter("@DocName", System.Data.SqlDbType.VarChar, 50);
                DocName.Value = opt.DocName;

                SqlCommand Command = new SqlCommand("UPDATE Settings SET NewDocName = @DocName WHERE Id = 1", connection);

                SqlParameter param = new SqlParameter(
                    "@DocName", System.Data.SqlDbType.NVarChar, 16);
                param.Value = opt.DocName;
                Command.Parameters.Add(param);

                Command.ExecuteNonQuery();



                connection.Close();
            };

        }


    }
    
}
