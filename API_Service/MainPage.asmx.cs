using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using BL;
using Microsoft.SqlServer.Server;
using Newtonsoft.Json;

namespace API_Service
{
    /// <summary>
    /// Summary description for MainPage
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class MainPage : System.Web.Services.WebService 
    {
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
        public void Calc(string a, string ope, string b)
        {
            Logic logic = new Logic();
            string result = logic.Calculate(a, ope, b);
            HttpContext.Current.Response.Write("{\"Result\": " +result + "}");
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void OperatorsList()
        {
            Logic logic = new Logic();
            List<string> result = logic.GetOperatorsList();
            HttpContext.Current.Response.Write("{\"Result\": " + JsonConvert.SerializeObject(result) + "}");
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetCountThisMonth(string ope)
        {
            Logic logic = new Logic();
            string result = logic.GetCountThisMonth(ope);
            HttpContext.Current.Response.Write("{\"Result\": " + result + "}");
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetLast3Records(string ope)
        {
            Logic logic = new Logic();
            DataTable LastRecords = logic.GetLast3Records(ope);
            HttpContext.Current.Response.Write("{\"Result\": " + JsonConvert.SerializeObject(LastRecords) + "}");
        }
    }
}
