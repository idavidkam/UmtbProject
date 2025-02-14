using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SqlServer.Server;

namespace BL
{
    public class Logic
    {
        public Logic()
        {
            // When we start the program - we need to make sure that we have all our tables
            // in case one of tables is missing - we will create them again
            SQLService service = new SQLService();
            service.ExecuteNonQuery(Querys.CRAETE_OPERATOR_LIST_TABLE);
            service.ExecuteNonQuery(Querys.CREATE_TRANSACTION_LOG_TABLE);
        }

        public string Calculate(string a, string op, string b)
        {
            string result = string.Empty;

            if (string.IsNullOrEmpty(a) || string.IsNullOrEmpty(op) || string.IsNullOrEmpty(b))
                return "Fill all the fields";

            try
            {
                var x = CSharpScript.EvaluateAsync("("+a+")" + op + "("+b+")", ScriptOptions.Default.WithEmitDebugInformation(true)).Result;
                result = x.ToString();
                AddHistoryRecord(a,op,b,result);
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }

        public List<string> GetOperatorsList()
        {
            SQLService service = new SQLService();
            List<string> operators = service.GetList(Querys.GET_OPERATORS);
            return operators;
        }

        public void AddHistoryRecord(string a, string op, string b, string result)
        {
            SQLService service = new SQLService();
            SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@Field1", SqlDbType.NVarChar) { Value = a },
                    new SqlParameter("@Operator", SqlDbType.NVarChar) { Value = op },
                    new SqlParameter("@Field2", SqlDbType.NVarChar) { Value = b },
                    new SqlParameter("@Result", SqlDbType.NVarChar) { Value = result }
                };
            service.ExecuteNonQuery(Querys.ADD_NEW_RECORD_TO_TRANSACTION_LOG,parameters);
        }

        public string GetCountThisMonth(string op)
        {
            SQLService service = new SQLService();
            SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@Operator", SqlDbType.NVarChar) { Value = op }
                };
            string count = service.GetString(Querys.GET_COUNT_RECORDES_PER_OPERATOR_FROM_THIS_MONTH, parameters);
            return count;
        }

        public DataTable GetLast3Records(string op)
        {
            SQLService service = new SQLService();
            SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@Operator", SqlDbType.NVarChar) { Value = op }
                };
            DataTable Last3Records = service.GetDataTable(Querys.GET_LAST_3_RECORDS_PER_OPERATOR, parameters);
            return Last3Records;
        }

        /*
        /// <summary>
        /// Function To Eval numbers - Not supporting strings
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public string Eval(String expression)
        {
            try
            {
                System.Data.DataTable table = new System.Data.DataTable();
                return Convert.ToString(table.Compute(expression, String.Empty));
            }
            catch(Exception ex)
            {
                return $"An error occurred in the calculation: {ex.Message}";
            }
        }
        */
    }
}
