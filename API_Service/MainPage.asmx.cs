using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using BL;

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
        [ScriptMethod(UseHttpGet = true)]
        public string Calc(string a, string op, string b) 
        {
            Logic logic = new Logic();
            string result = logic.Calculate(a, op , b);
            return result;
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = true)]
        public List<string> OperatorsList()
        {
            Logic logic = new Logic();
            List<string> result = logic.GetOperatorsList();
            return result;
        }
    }
}
