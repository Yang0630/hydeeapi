using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using Oracle.ManagedDataAccess.Client;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Data;

namespace hydee
{
    /// <summary>
    /// hydeeh2 的摘要说明
    /// </summary>
    public class hydeeh2 : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string JsonString = "", Message = "";
            bool STATUS = true;
            string sql = context.Request.Form["sql"];
            try
            {
                string strConnection = WebConfigurationManager.ConnectionStrings["hydeeH2"].ToString();
                OracleConnection oracleConnection = new OracleConnection(strConnection);
                OracleDataAdapter Oda = new OracleDataAdapter(sql,oracleConnection);
                DataTable  data = new DataTable();
                Oda.Fill(data);
                JsonString = JsonConvert.SerializeObject(data, new DataTableConverter());
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                STATUS = false;
            }
            context.Response.Write("{\"STATUS\":\"" + STATUS + "\",\"mess\":\"" + Message + "\",\"JsonString\":" + JsonString + "}");
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}