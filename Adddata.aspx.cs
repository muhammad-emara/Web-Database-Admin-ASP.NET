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
using System.IO;
using DbMgmAdmin.classes;

namespace DbMgmAdmin
{
    public partial class Adddata : System.Web.UI.Page
    {
        protected System.Web.UI.WebControls.Table Table1;
        protected System.Web.UI.HtmlControls.HtmlForm frmViewState;
        protected System.Web.UI.WebControls.Table Table2;
        protected System.Web.UI.WebControls.Button btnBack;
        protected System.Web.UI.WebControls.Label lblstatus;
        protected System.Web.UI.WebControls.Button Button1;
        protected System.Web.UI.WebControls.Button btnSubmit;
        protected bool TableHasImage = false;

        protected ArrayList ImageColumnNoAndNameList = new ArrayList();
        System.Web.UI.HtmlControls.HtmlInputFile[] FileUploads = new System.Web.UI.HtmlControls.HtmlInputFile[10];


        protected ArrayList myDataTypeList = new ArrayList();

        private void Page_Load(object sender, System.EventArgs e)
        {

            if (IsPostBack)
            {

            }
            ShowData();
        }
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

        private void ShowData()
        {
            // Put user code to initialize the page here
            string s = "";

            getImageColumnNumberAndName();
            int imageCount = 0;

            SqlDataReader myReader;
            int j = 0, k = 0;//i =0,

            s = "SELECT * FROM " + Session["tName"].ToString();
            clsDataAccess myclass = new clsDataAccess();
            myclass.openConnection();
            myReader = myclass.getData(s);

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
                        TextBox1.Text = ""; //myReader.GetValue(j).ToString();
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
                                TableHasImage = true;
                                //		c.Controls.Add(new LiteralControl("&nbsp;<img src='images/placeholder-200.jpg' alt= 'Default image' Width='100' >" ));

                                FileUploads[imageCount] = new System.Web.UI.HtmlControls.HtmlInputFile();
                                FileUploads[imageCount].Size = 30;
                                FileUploads[imageCount].Accept = "image/jpg";
                                c.Controls.Add(FileUploads[imageCount]);
                                imageCount++;
                            }
                            else
                            {
                                c.Controls.Add(TextBox1);
                            }

                            if (String.Compare(myReader.GetFieldType(j).ToString(), "System.DateTime") == 0)
                            {
                                TextBox1.Text = DateTime.Now.ToString();
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

            myReader.Close();
            myclass.closeConnection();

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
            this.Button1.Click += new System.EventHandler(this.Button1_Click);
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            this.Load += new System.EventHandler(this.Page_Load);

        }
        #endregion
        private void btnSubmit_Click(object sender, System.EventArgs e)
        {

            string tname;
            tname = Session["tName"].ToString();

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

                    int j = 0;

                    mystring = "INSERT INTO " + tname + " VALUES(";

                    for (j = 1; j < myDataTypeList.Count - 1; j++)
                    {
                        mystring = mystring + " @" + j + ", ";

                    }
                    mystring = mystring + " @" + j + " )";
                    SqlCommand command = new SqlCommand(mystring, connection);
                    int k = 1;
                    for (j = 1; j < myDataTypeList.Count; j++)
                    {
                        //Response.Write ("<hr>" + myDataTypeList[j].ToString()  + "<BR>" + EnumerateDataType(myDataTypeList[j].ToString()));
                        string s = Convert.ToString(j);

                        myParams[j] = new SqlParameter("@" + s, EnumerateDataType(myDataTypeList[j].ToString()));

                        if (String.Compare(myDataTypeList[j].ToString(), "image") == 0)
                        {
                            Stream imgStream = FileUploads[imageCount].PostedFile.InputStream;
                            int imgLen = FileUploads[imageCount].PostedFile.ContentLength;
                            string imgContentType = FileUploads[imageCount].PostedFile.ContentType;
                            byte[] imgBinaryData = new byte[imgLen];
                            int n1 = imgStream.Read(imgBinaryData, 0, imgLen);

                            k = j - 1;
                            myParams[j].Value = imgBinaryData;
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
                    lblstatus.Text = "Added with Image Successfully !!!";

                }
                catch (SqlException SQLexc)
                {
                    Response.Write("Insert Failed. Error Details are: " + SQLexc.ToString());
                    //return 0;
                }
            }
            else
            {
                try
                {
                    string s;
                    SqlDataAdapter myDA = new SqlDataAdapter();
                    DataSet myDataSet = new DataSet();
                    int i = 0;//, j=0


                    s = "SELECT * FROM " + Session["tName"].ToString();
                    clsDataAccess myclass = new clsDataAccess();
                    myclass.openConnection();
                    myDA = myclass.getDataforUpdate(s);

                    SqlCommandBuilder mySCB = new SqlCommandBuilder(myDA);
                    myDA.Fill(myDataSet, tname);
                    DataTable myTable;

                    myTable = myDataSet.Tables[0];

                    DataRow newRow;

                    //myTable.NewRow(); 
                    newRow = myTable.NewRow();
                    for (i = 1; i < myTable.Columns.Count; i++)
                    {
                        newRow[i] = Request.Form[i + 1].ToString();
                    }
                    myTable.Rows.Add(newRow);
                    myDA.Update(myDataSet, tname);
                    myDataSet.AcceptChanges();
                    lblstatus.Text = "Added Successfully !!!";
                }
                catch (Exception f)
                {
                    lblstatus.Text = f.Message + " Error while Adding";
                    lblstatus.Font.Bold = true;
                }
            }

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


        private void btnBack_Click(object sender, System.EventArgs e)
        {
            Response.Redirect("db.aspx?table=" + Session["tName"]);
        }

        private void Button1_Click(object sender, System.EventArgs e)
        {
            Response.Redirect("db.aspx?table=" + Session["tName"]);
        }


    }
}
