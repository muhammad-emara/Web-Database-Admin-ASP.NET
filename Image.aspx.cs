using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace DbMgmAdmin
{
    public partial class MyImage : System.Web.UI.Page
    {
        private void Page_Load(object sender, System.EventArgs e)
        {
            // Put user code to initialize the page here
            // Put user code to initialize the page here
            SqlConnection myConnection = new SqlConnection(ConfigurationSettings.AppSettings["ConnectionString"]);
            SqlCommand myCommand = new SqlCommand("Select * from " + Session["tName"] + " where " + Session["FirstKey"].ToString() + "=" + Request.QueryString["id"], myConnection);
            try
            {
                myConnection.Open();
                SqlDataReader myDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (myDataReader.Read())
                {
                    //Response.ContentType = (string)myDataReader.GetValue(6);
                    Response.BinaryWrite((byte[])myDataReader.GetValue(Convert.ToInt32(Request.QueryString["cnt"])));
                }

                myConnection.Close();
                //Response.Write("Person info successfully retrieved!");
            }

            catch (SqlException SQLexc)
            {
                Response.Write("Read Failed : " + SQLexc.ToString());
            }
        }
        public byte[] GetImage(int id, int cnt)
        {
            byte[] imgBinary = null;
            SqlConnection myConnection = new SqlConnection(ConfigurationSettings.AppSettings["ConnectionString"]);
            SqlCommand myCommand = new SqlCommand("Select * from " + Session["tName"] + " where " + Session["FirstKey"].ToString() + "=" + id, myConnection);

            myConnection.Open();
            SqlDataReader myDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
            while (myDataReader.Read())
            {
                //Response.ContentType = (string)myDataReader.GetValue(6);
                imgBinary = (byte[])myDataReader.GetValue(cnt);
            }
            myConnection.Close();
            return imgBinary;
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