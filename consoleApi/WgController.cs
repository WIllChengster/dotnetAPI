using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using APWebGrabberActiveXControlLib;
namespace consoleApi
{

    public class Setting
    {
        public bool Linearize { get; set; }
        public string DocName { get; set; }
    }

    public class WgController : ApiController
    {
        //Get api/wg
        public IEnumerable<string> Get()
        {
            APWebGrabber wg = new APWebGrabber();

            wg.OutputDirectory = @"C:/Users/Administrator/Desktop";
            wg.NewDocumentName = "New Document.pdf";
            wg.URL = "https://google.com";
            wg.ConvertToPDF("127.0.0.1", 52525);

            return new string[] { "Hello", "World" };
        }
        
        

        //Post
        [HttpPost]
        public void Post([FromBody] Setting opt)
        {
            Console.WriteLine(opt.Linearize);

        }

    }
    
}
