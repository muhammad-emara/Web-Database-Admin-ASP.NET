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
using System.IO;
using System.Configuration;
using DbMgmAdmin.classes;
using static System.Net.Mime.MediaTypeNames;

namespace DbMgmAdmin
{
    public partial class Editdata : System.Web.UI.Page
    {
        protected System.Web.UI.WebControls.Table Table1;
        protected System.Web.UI.HtmlControls.HtmlForm frmViewState;
        protected System.Web.UI.WebControls.Table Table2;
        protected System.Web.UI.WebControls.Button btnBack;
        protected System.Web.UI.WebControls.Label lblstatus;
        protected System.Web.UI.WebControls.Button Button1;
        protected System.Web.UI.WebControls.Button Button2;
        protected System.Web.UI.WebControls.Button btnSubmit;
        protected bool TableHasImage = false;
        protected System.Web.UI.HtmlControls.HtmlInputFile File1 = new HtmlInputFile();
        protected ArrayList myDataTypeList = new ArrayList();
        protected ArrayList myColumnNames = new ArrayList();
        protected ArrayList ImageColumnNoAndNameList = new ArrayList();
        System.Web.UI.HtmlControls.HtmlInputFile[] FileUploads = new System.Web.UI.HtmlControls.HtmlInputFile[10];


        private void getImageColumnNumberAndName()
        {
            ImageColumnNoAndNameList.Clear();
            string s = "";
            SqlDataReader myReader;
            int j = 0;
            s = "SELECT * FROM " + Session["tName"].ToString();
            clsDataAccess myclass = new clsDataAccess();
            myclass.openConnection();
            myReader = myclass.getData(s);

            while (myReader.Read() && (j < 1))
            {
                for (j = 1; j < myReader.FieldCount; j++)
                {
                    if (String.Compare(myReader.GetDataTypeName(j).ToString().ToLower(), "image") == 0)
                    {
                        Photo p = new Photo(j, j, myReader.GetName(j).ToString());
                        ImageColumnNoAndNameList.Add(p);
                    }
                }
                j++;
            }

        }


        private void Page_Load(object sender, System.EventArgs e)
        {
            if (IsPostBack)
            {

            }
            ShowData();
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
            this.Button2.Click += new System.EventHandler(this.Button2_Click);
            this.Load += new System.EventHandler(this.Page_Load);

        }
        #endregion

        private void ShowData()
        {
            getImageColumnNumberAndName();
            int imageCount = 0;

            TableHasImage = false;
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
                    for (k = 0; k < 4; k++)
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
                            if (k == 3)
                            {
                                c.Width = 35;
                                c.Controls.Add(new LiteralControl("<b>&nbsp;Datatype</b>"));
                            }
                        }
                        if (j > -1)
                        {
                            TextBox TextBox1 = new TextBox();
                            TextBox1.ID = "Text" + j;
                            TextBox1.Text = myReader.GetValue(j).ToString();

                            if (myReader.GetName(j).ToString() == Session["FirstKey"].ToString())
                            {
                                TextBox1.ReadOnly = true;
                                TextBox1.BackColor = Color.GhostWhite;
                            }

                            TextBox1.Width = 300;


                            if (k == 0)
                            {
                                c.Width = 15;
                                c.Controls.Add(new LiteralControl((j + 1).ToString()));
                            }
                            if (k == 1)
                            {
                                c.Width = 50;
                                myColumnNames.Add(myReader.GetName(j));
                                if (myReader.GetName(j).ToString() == Session["FirstKey"].ToString())
                                    c.Controls.Add(new LiteralControl("<i>&nbsp;" + myReader.GetName(j).ToString() + "</i>"));
                                else
                                    c.Controls.Add(new LiteralControl("&nbsp;" + myReader.GetName(j).ToString() + "&nbsp;"));
                            }
                            if (k == 2)
                            {
                                c.Width = 300;

                            }
                            if (k == 2)
                            {
                                c.Width = 300;

                                if (String.Compare(myReader.GetDataTypeName(j).ToString(), "image") == 0)
                                {
                                    TableHasImage = true;
                                    c.Controls.Add(new LiteralControl("&nbsp;<a target = '_blank' href= '" + FormatURL((int)myReader.GetValue(0)) + "&cnt=" + j + "'><img border = '0'  src=" + FormatURL((int)myReader.GetValue(0)) + "&cnt=" + j + " Width='150' ><br>"));

                                    FileUploads[imageCount] = new System.Web.UI.HtmlControls.HtmlInputFile();
                                    FileUploads[imageCount].Size = 30;
                                    c.Controls.Add(FileUploads[imageCount]);
                                    imageCount++;
                                }
                                else
                                {
                                    c.Controls.Add(TextBox1);
                                }


                            }
                            if (k == 3)
                            {
                                myDataTypeList.Add(myReader.GetDataTypeName(j));
                                c.Width = 35;
                                c.Controls.Add(new LiteralControl("&nbsp;" + myReader.GetDataTypeName(j).ToString() + "::" + myReader.GetFieldType(j).ToString() + "&nbsp;"));
                            }
                        }
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

                if (TableHasImage)
                {

                    int imageCount = 0;
                    int n = myDataTypeList.Count;

                    SqlParameter[] myParams = new SqlParameter[n];


                    int numRowsAffected = 0;
                    try
                    {

                        SqlConnection connection = new SqlConnection(ConfigurationSettings.AppSettings["ConnectionString"]);
                        string mystring;

                        int j = 0; //update tbl set @colname = @value where keycol = @keyval


                        mystring = "UPDATE " + tname + " SET ";

                        for (j = 1; j < myDataTypeList.Count - 1; j++)
                        {
                            mystring = mystring + myColumnNames[j] + "= @" + j + ", ";

                        }
                        mystring = mystring + myColumnNames[j] + "= @" + j + " WHERE " + Session["FirstKey"].ToString() + "=" + Request.QueryString["id"];
                        SqlCommand command = new SqlCommand(mystring, connection);
                        int k = 1;

                        for (j = 1; j < myDataTypeList.Count; j++)
                        {
                            //Response.Write ("<hr>" + myDataTypeList[j].ToString()  + "<BR>" + EnumerateDataType(myDataTypeList[j].ToString()));
                            string s1 = Convert.ToString(j);

                            myParams[j] = new SqlParameter("@" + s1, EnumerateDataType(myDataTypeList[j].ToString()));

                            if (String.Compare(myDataTypeList[j].ToString(), "image") == 0)
                            {
                                Stream imgStream = FileUploads[imageCount].PostedFile.InputStream;
                                int imgLen = FileUploads[imageCount].PostedFile.ContentLength;
                                string imgContentType = FileUploads[imageCount].PostedFile.ContentType;
                                FileUploads[imageCount].Accept = "image/jpg";

                                k = j - 1;
                                if ((FileUploads[imageCount].PostedFile != null) && (FileUploads[imageCount].PostedFile.ContentLength > 0))
                                {
                                    byte[] imgBinaryData = new byte[imgLen];
                                    int n1 = imgStream.Read(imgBinaryData, 0, imgLen);
                                    myParams[j].Value = imgBinaryData;
                                }
                                else
                                {
                                    MyImage mImage = new MyImage();
                                    byte[] imgBinaryData = mImage.GetImage(Convert.ToInt32(Request.QueryString["id"]), j);
                                    int n1 = imgStream.Read(imgBinaryData, 0, imgLen);
                                    myParams[j].Value = imgBinaryData;
                                }
                                imageCount++;
                            }
                            else
                            {
                                myParams[j].Value = Request.Form[k + 1];
                            }
                            k = k + 1;
                            command.Parameters.Add(myParams[j]);
                        }

                        connection.Open();
                        //Response.Write (mystring);
                        numRowsAffected = command.ExecuteNonQuery();
                        connection.Close();
                        lblstatus.Text = " Updated Successfully with image!!!";

                    }
                    catch (SqlException SQLexc)
                    {
                        Response.Write("Insert Failed. Error Details are: " + SQLexc.ToString());
                        //return 0;
                    }

                }
                else
                {
                    i = Convert.ToInt32(Request.QueryString["id"]);
                    s = "SELECT * FROM " + Session["tName"].ToString() + " where " + Session["FirstKey"] + " =" + i;
                    clsDataAccess myclass = new clsDataAccess();
                    myclass.openConnection();
                    myDA = myclass.getDataforUpdate(s);

                    SqlCommandBuilder mySCB = new SqlCommandBuilder(myDA);
                    myDA.Fill(myDataSet, tname);
                    DataTable myTable;
                    myTable = myDataSet.Tables[0];

                    for (i = 1; i < myTable.Columns.Count; i++)
                    {
                        myTable.Rows[0][i] = Request.Form[i + 1].ToString();
                    }
                    myDA.Update(myDataSet, tname);
                    myDataSet.AcceptChanges();
                    lblstatus.Text = " Updated Successfully !!!";
                }
            }
            catch (Exception f)
            {
                lblstatus.Text = f.Message + " Error while Updating";
                lblstatus.Font.Bold = true;
                lblstatus.ForeColor = Color.Red;
            }
            finally
            {


            }
        }

        private void btnBack_Click(object sender, System.EventArgs e)
        {
            Response.Redirect("db.aspx?table=" + Session["tName"]);
        }
        private SqlDbType EnumerateDataType(string s)
        {

            if (String.Compare(SqlDbType.BigInt.ToString().ToLower(), s) == 0)
                return SqlDbType.BigInt;

            if (String.Compare(SqlDbType.Binary.ToString().ToLower(), s) == 0)
                return SqlDbType.Binary;

            if (String.Compare(SqlDbType.Bit.ToString().ToLower(), s) == 0)
                return SqlDbType.Bit;

            if (String.Compare(SqlDbType.Char.ToString().ToLower(), s) == 0)
                return SqlDbType.Char;

            if (String.Compare(SqlDbType.DateTime.ToString().ToLower(), s) == 0)
                return SqlDbType.DateTime;

            if (String.Compare(SqlDbType.Decimal.ToString().ToLower(), s) == 0)
                return SqlDbType.Decimal;

            if (String.Compare(SqlDbType.Float.ToString().ToLower(), s) == 0)
                return SqlDbType.Float;

            if (String.Compare(SqlDbType.Image.ToString().ToLower(), s) == 0)
                return SqlDbType.Image;

            if (String.Compare(SqlDbType.Int.ToString().ToLower(), s) == 0)
                return SqlDbType.Int;

            if (String.Compare(SqlDbType.Money.ToString().ToLower(), s) == 0)
                return SqlDbType.Money;

            if (String.Compare(SqlDbType.NChar.ToString().ToLower(), s) == 0)
                return SqlDbType.NChar;

            if (String.Compare(SqlDbType.NText.ToString().ToLower(), s) == 0)
                return SqlDbType.NText;

            if (String.Compare(SqlDbType.NVarChar.ToString().ToLower(), s) == 0)
                return SqlDbType.NVarChar;

            if (String.Compare(SqlDbType.Real.ToString().ToLower(), s) == 0)
                return SqlDbType.Real;

            if (String.Compare(SqlDbType.SmallDateTime.ToString().ToLower(), s) == 0)
                return SqlDbType.SmallDateTime;

            if (String.Compare(SqlDbType.SmallInt.ToString().ToLower(), s) == 0)
                return SqlDbType.SmallInt;

            if (String.Compare(SqlDbType.SmallMoney.ToString().ToLower(), s) == 0)
                return SqlDbType.SmallMoney;

            if (String.Compare(SqlDbType.Text.ToString().ToLower(), s) == 0)
                return SqlDbType.Text;

            if (String.Compare(SqlDbType.Timestamp.ToString().ToLower(), s) == 0)
                return SqlDbType.Timestamp;

            if (String.Compare(SqlDbType.TinyInt.ToString().ToLower(), s) == 0)
                return SqlDbType.TinyInt;

            if (String.Compare(SqlDbType.UniqueIdentifier.ToString().ToLower(), s) == 0)
                return SqlDbType.UniqueIdentifier;

            if (String.Compare(SqlDbType.VarBinary.ToString().ToLower(), s) == 0)
                return SqlDbType.VarBinary;

            if (String.Compare(SqlDbType.VarChar.ToString().ToLower(), s) == 0)
                return SqlDbType.VarChar;

            if (String.Compare(SqlDbType.Variant.ToString().ToLower(), s) == 0)
                return SqlDbType.Variant;

            else
                return SqlDbType.Variant;

        }

        private void Button1_Click(object sender, System.EventArgs e)
        {
            Response.Redirect("db.aspx?table=" + Session["tName"]);
        }

        private void Button2_Click(object sender, System.EventArgs e)
        {
            Response.Redirect("Deletedata.aspx?id=" + Convert.ToInt32(Request.QueryString["id"]));
        }
    }
}