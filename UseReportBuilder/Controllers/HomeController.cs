using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace UseReportBuilder.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            //to use report viewer, have to Install-Package Microsoft.SqlServer.Types -Version 14.0.1016.290

            ViewBag.Message = "Your application description page.";

            string _urlRemote = System.Configuration.ConfigurationManager.AppSettings["SSRSReportURL"].ToString();

            ReportViewer rp = new ReportViewer();
            //rp.ProcessingMode = ProcessingMode.Local;
            rp.SizeToReportContent = true;
            rp.AsyncRendering = true;
            //rp.ServerReport.ReportServerUrl = new Uri(_urlRemote);
            //rp.ServerReport.ReportPath = @"D:\ReportBuilder\sampleRP.rdl";
            rp.LocalReport.ReportPath = @"D:\ReportBuilder\sampleRP4.rdl";


            List<ReportParameter> lst = new List<ReportParameter>
            {
                new ReportParameter("pTitle", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss.fff")),
                new ReportParameter("tblName", "_UserPortals"),
                //new ReportParameter("orFrom", "15"),
                //new ReportParameter("orTo", "100"),
                new ReportParameter("ordinal_from", "15"),
                new ReportParameter("ordinal_to", "100"),
            };
            rp.LocalReport.SetParameters(lst);

            //get dataset bằng code
            //GetDataSet(ref rp);

            ViewBag.ReportViewer = rp;

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public DataSet GetDataSet(ref ReportViewer rp)
        {
            
            using (SqlConnection cnn = new SqlConnection(@"Data Source=.\SQLEXPRESS; Initial Catalog=DSIGN;Integrated Security=true;"))
            {
                cnn.Open();

                string queryString = "EXEC GET_COLS_TABLE_NAME '_UserPortals', 0,100 ";
                SqlDataAdapter adapter = new SqlDataAdapter(queryString, cnn);

                DataSet customers = new DataSet();
                adapter.Fill(customers, "Customers");

                cnn.Close();

                ReportDataSource rds = new ReportDataSource();
                DataSet ds = customers;
                rp.LocalReport.DataSources.Clear();
                rds.Name = "DataSet1";//cùng tên với dataset1 trong xml
                rds.Value = ds.Tables[0];
                rp.LocalReport.DataSources.Add(rds);

                return customers;
            }
        }
    }
}