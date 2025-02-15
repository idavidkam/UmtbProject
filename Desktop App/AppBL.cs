using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace Desktop_App
{
    class AppBL
    {
        string url = "https://localhost:44303/MainPage.asmx/";
        public string Calc(string a, string op, string b)
        {
            string calcURL = url + "Calc";

            // Create query string with parameters
            var queryString = new StringBuilder();
            queryString.Append("?a=").Append(Uri.EscapeDataString(a));
            queryString.Append("&ope=").Append(Uri.EscapeDataString(op));
            queryString.Append("&b=").Append(Uri.EscapeDataString(b));

            string fullURL = calcURL + queryString;
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, fullURL);
            var response = client.SendAsync(request).Result;
            response.EnsureSuccessStatusCode();
            string result = response.Content.ReadAsStringAsync().Result;
            
            // read the json
            JObject json = JObject.Parse(result);
            
            return json["Result"].ToString();
        }

        public string GetCountThisMonth(string op)
        {
            string countURL = url + "GetCountThisMonth";

            // Create query string with parameters
            var queryString = new StringBuilder();
            queryString.Append("?ope=").Append(Uri.EscapeDataString(op));

            string fullURL = countURL + queryString;
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, fullURL);
            var response = client.SendAsync(request).Result;
            response.EnsureSuccessStatusCode();
            string result = response.Content.ReadAsStringAsync().Result;

            // read the json
            JObject json = JObject.Parse(result);

            return json["Result"].ToString();

        }

        public List<string> GetOperatorList()
        {
            string operatorListURL = url + "OperatorsList";
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, operatorListURL);
            var response = client.SendAsync(request).Result;
            response.EnsureSuccessStatusCode();
            string result = response.Content.ReadAsStringAsync().Result;

            // read the json
            JObject json = JObject.Parse(result);
            List<string> operators = JsonConvert.DeserializeObject<List<string>>(json["Result"].ToString());

            return operators;

        }


        public DataTable GetLast3Records(string op)
        {
            string lastRecordsURL = url + "GetLast3Records";
   
            var queryString = new StringBuilder();
            queryString.Append("?ope=").Append(Uri.EscapeDataString(op));

            string fullURL = lastRecordsURL + queryString;

            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, fullURL);
            var response = client.SendAsync(request).Result;
            response.EnsureSuccessStatusCode();
            string result = response.Content.ReadAsStringAsync().Result;

            // read the json
            JObject json = JObject.Parse(result);
            DataTable lastRecords = JsonConvert.DeserializeObject<DataTable>(json["Result"].ToString());

            return lastRecords;
        }
    }
}
