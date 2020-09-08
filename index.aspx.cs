using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Configuration;
using System.Text.RegularExpressions;
using DbMgmAdmin.classes;
/********************************************************************

Author :         (Muhammad Emara - muhammad.emara@hotmail.com)
created:	08/09/2020
file base:	Index.aspx
purpose:	Login/ default page
*********************************************************************/

namespace DbMgmAdmin
{
    /// <summary>
    /// Summary description for Template.
    /// </summary>
    public partial class Index : System.Web.UI.Page
    {
        protected System.Web.UI.WebControls.Label EmailID;
        protected System.Web.UI.WebControls.Label Password;
        protected System.Web.UI.WebControls.Button Log;
        protected System.Web.UI.WebControls.TextBox txtemail;
        protected System.Web.UI.WebControls.TextBox txtpwd;
        protected System.Web.UI.WebControls.Label lblError;
        protected System.Web.UI.WebControls.Button btnLogin;

        private void Page_Load(object sender, System.EventArgs e)
        {
            if (!Page.IsPostBack)
            {

            }
            // Put user code to initialize the page here
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
            this.Log.Click += new System.EventHandler(this.Log_Click);
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            this.Load += new System.EventHandler(this.Page_Load);

        }
        #endregion

        private void Log_Click(object sender, System.EventArgs e)
        {

            string uname;
            uname = txtemail.Text.ToString();

            string pass;
            pass = txtpwd.Text.ToString();

            string s = "none";
            bool noErrorFlag = true;


            noErrorFlag = noErrorFlag && CheckSQLInjection(uname);
            //Response.Write(s + "<hr>1" + noErrorFlag + "<br>"); 

            noErrorFlag = noErrorFlag && CheckSQLInjection(pass);
            //Response.Write(s + "<hr>2" + noErrorFlag + "<br>"); 


            if (noErrorFlag)
            {
                s = "SELECT * FROM " + ConfigurationSettings.AppSettings["AdminTable"] + " where Username ='" + uname + "' AND Password ='" + pass + "'";
                clsDataAccess mylogin = new clsDataAccess();//Login class is called
                SqlDataReader mydr1 = mylogin.Login(s);

                //Response.Write(s + "<hr>3" + noErrorFlag + "<br>"); 

                while (mydr1.Read())
                {
                    //Response.Write(s + "<hr>4" + noErrorFlag + "<br>"); 
                    if (mydr1.GetValue(5).ToString().Trim() == "Admin")
                    {
                        Session["userfullname"] = mydr1.GetValue(3).ToString().Trim() + " " + mydr1.GetValue(4).ToString().Trim();
                        Session["Admin"] = "true";
                        Response.Redirect("DBMain.aspx");
                    }
                    else
                    {
                        lblError.Text = "Error ! - Invalid Login";
                    }

                }
                mydr1.Close();
                mylogin.closeConnection();


            }

            else
            {
                lblError.Text = "Input contains some special character !<br> Only a-z A-Z . _ s is allowed upto 50 characters";
            }

            lblError.Text = lblError.Text + "<hr>Error ! - Invalid Login ";



        }
        private bool CheckSQLInjection(string s)
        {

            bool flagSQLI = false;
            try
            {

                if (!Regex.IsMatch(s, @"^[a-zA-Z._s]{1,50}$"))
                {
                    flagSQLI = false;
                }
                else
                {
                    flagSQLI = true;
                }
            }
            catch (Exception)
            {
                flagSQLI = true;

            }

            if (flagSQLI)
                return true;
            else
                return false;

        }

        private void btnLogin_Click(object sender, System.EventArgs e)
        {
            txtemail.Text = "";
            txtpwd.Text = "";
        }

    }
}