using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace DbMgmAdmin
{
    public partial class TestImage : System.Web.UI.Page
    {
        protected System.Web.UI.WebControls.DataGrid DataGrid1;
        private void Page_Load(object sender, System.EventArgs e)
        {
            Session["tName"] = "image";
            Session["FirstKey"] = "ID";
            // Put user code to initialize the page here
            BindDataGrid();
        }
        private void BindDataGrid()
        {

            SqlConnection myConnection = new SqlConnection();
            myConnection.ConnectionString = ConfigurationSettings.AppSettings["ConnectionString"];

            string sqlQuery = "SELECT * FROM image";
            myConnection.Open();

            SqlDataAdapter myDa = new SqlDataAdapter(sqlQuery, myConnection);
            DataSet myDS = new DataSet();
            myDa.Fill(myDS);
            DataGrid1.DataSource = myDS;
            DataGrid1.DataBind();
            myConnection.Close();
        }
        public string FormatURL(int strArgument)
        {
            return ("image.aspx?id=" + strArgument + "&cnt=2");
        }

        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Load += new System.EventHandler(this.Page_Load);
        }
        #endregion
    }
}