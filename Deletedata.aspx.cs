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
using DbMgmAdmin.classes;

namespace DbMgmAdmin
{
    public partial class Deletedata : System.Web.UI.Page
    {
        
            protected System.Web.UI.WebControls.Button Button1;
            protected System.Web.UI.WebControls.Label lblstatus;
            protected System.Web.UI.WebControls.Button btnBack;
            protected System.Web.UI.WebControls.Table Table2;
            protected System.Web.UI.HtmlControls.HtmlForm frmViewState;
            protected System.Web.UI.WebControls.Button btnSubmit;

            private void Page_Load(object sender, System.EventArgs e)
            {
                //			if ((string)Session["Admin"] == "true")
                //			{
                //			}
                //			else
                //				Response.Redirect("index.aspx");


                if (IsPostBack)
                {
                }
                ShowData();


            }
            private void ShowData()
            {
                // Put user code to initialize the page here
                string s = "";

                SqlDataReader myReader;
                int i = 0, j = 0, k = 0;

                i = Convert.ToInt32(Request.QueryString["id"]);
                s = "SELECT * FROM " + Session["tName"].ToString() + " where " + Session["FirstKey"] + " =" + i;

                clsDataAccess myclass = new clsDataAccess();
                myclass.openConnection();
                myReader = myclass.getData(s);

                while (myReader.Read())
                {
                    for (j = -1; j < myReader.FieldCount; j++)
                    {

                        TableRow r = new TableRow();
                        for (k = 0; k < 3; k++)
                        {
                            TableCell c = new TableCell();
                            c.VerticalAlign = VerticalAlign.Top;

                            if (j == -1)
                            {
                                if (k == 0)
                                {
                                    c.Width = 15;
                                    c.Controls.Add(new LiteralControl("<b>Sno.</b>"));
                                }
                                if (k == 1)
                                {
                                    c.Width = 50;
                                    c.Controls.Add(new LiteralControl("<b>Column</b>"));
                                }
                                if (k == 2)
                                {
                                    c.Width = 50;
                                    c.Controls.Add(new LiteralControl("<b>Value</b>"));
                                }
                            }
                            if (j > -1)
                            {

                                if (k == 0)
                                {
                                    c.Width = 15;
                                    c.Controls.Add(new LiteralControl((j + 1).ToString()));
                                }
                                if (k == 1)
                                {
                                    c.Width = 50;
                                    if (myReader.GetName(j).ToString() == Session["FirstKey"].ToString())
                                        c.Controls.Add(new LiteralControl("<i>&nbsp;" + myReader.GetName(j).ToString() + "</i>"));
                                    else
                                        c.Controls.Add(new LiteralControl("&nbsp;" + myReader.GetName(j).ToString() + "&nbsp;"));
                                }
                                if (k == 2)
                                {
                                    c.Width = 300;

                                    if (String.Compare(myReader.GetDataTypeName(j).ToString(), "image") == 0)
                                    {
                                        c.Controls.Add(new LiteralControl("&nbsp;<a target = '_blank' href= '" + FormatURL((int)myReader.GetValue(0)) + "&cnt=" + j + "'><img border = '0'  src=" + FormatURL((int)myReader.GetValue(0)) + "&cnt=" + j + " Width='150' >"));
                                    }
                                    else
                                    {
                                        c.Controls.Add(new LiteralControl("&nbsp;" + myReader.GetValue(j).ToString() + "&nbsp;"));
                                    }


                                }
                            }
                            c.VerticalAlign = VerticalAlign.Top;
                            c.Height = Unit.Pixel(22);
                            r.Cells.Add(c);
                        }
                        Table2.Rows.Add(r);
                    }
                }
                myReader.Close();
                myclass.closeConnection();

            }
            private string FormatURL(int strArgument)
            {
                return ("image.aspx?id=" + strArgument);
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

            private void InitializeComponent()
            {
                this.Button1.Click += new System.EventHandler(this.Button1_Click);
                this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
                this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
                this.Load += new System.EventHandler(this.Page_Load);

            }
            #endregion
            private void btnBack_Click(object sender, System.EventArgs e)
            {
                Response.Redirect("db.aspx?table=" + Session["tName"]);
            }

            private void Button1_Click(object sender, System.EventArgs e)
            {
                Response.Redirect("db.aspx?table=" + Session["tName"]);
            }

            private void btnSubmit_Click(object sender, System.EventArgs e)
            {
                string tname;
                tname = Session["tName"].ToString();
                try
                {
                    string s;
                    SqlDataAdapter myDA = new SqlDataAdapter();
                    DataSet myDataSet = new DataSet();
                    int i = 0;//, j=0

                    i = Convert.ToInt32(Request.QueryString["id"]);
                    s = "SELECT * FROM " + Session["tName"].ToString() + " where " + Session["FirstKey"] + " =" + i;
                    clsDataAccess myclass = new clsDataAccess();
                    myclass.openConnection();
                    myDA = myclass.getDataforUpdate(s);

                    SqlCommandBuilder mySCB = new SqlCommandBuilder(myDA);
                    myDA.Fill(myDataSet, tname);
                    DataTable myTable;

                    myTable = myDataSet.Tables[0];
                    DataRowCollection rc = myTable.Rows;


                    rc[0].Delete();
                    myDA.Update(myDataSet, tname);
                    myDataSet.AcceptChanges();
                    lblstatus.Text = " Deleted Successfully !!!";
                    Table2.Visible = false;
                    btnSubmit.Visible = false;


                }
                catch (Exception f)
                {
                    lblstatus.Text = f.Message + " Error while Updating";
                    lblstatus.Font.Bold = true;
                    lblstatus.ForeColor = Color.Red;
                }
                finally
                {
                    ShowData();
                }
         
        }
    }
}