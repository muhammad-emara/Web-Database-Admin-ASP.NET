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
using DbMgmAdmin.classes;

namespace DbMgmAdmin
{
    public class DB : System.Web.UI.Page
    {
        protected System.Web.UI.WebControls.DataGrid DGtable;
        DataTable TempTable;
        DataView TempTableView;
        DataSet mydr;
        protected System.Web.UI.WebControls.Button Button1;
        //protected System.Web.UI.WebControls.Label lblstatus;
        protected System.Web.UI.WebControls.Button Button2;
        protected System.Web.UI.WebControls.TextBox txtSQL;
        public string SortField;
        public string sQuery = "";
        protected System.Web.UI.WebControls.Label lblSQLError;
        public bool userQuery = false;
        protected int ImageColumnNo = 0;
        protected string ImageColumnName = "Image";
        protected ArrayList ImageColumnNoAndNameList = new ArrayList();




        private void Page_Load(object sender, System.EventArgs e)
        {
            Session["tName"] = Request.QueryString["table"].ToString().Trim();
            Session["qry"] = "Select * from " + Session["tName"];
            getImageColumnNumberAndName();

            if (!IsPostBack)
            {
                lblSQLError.Text = "<font face = 'courier new' size = 2>Enter your SQL query above (<a href='http://www.w3schools.com/sql/sql_where.asp' target = '_blank' >Learn SQL query ^</a>)</font>";
                BindDataGrid();
            }
            else
            {
                try
                {
                    Session["qry"] = txtSQL.Text;
                    BindDataGrid();
                    lblSQLError.Text = "<font face = 'courier new' size = 2>Enter your SQL query above (<a href='http://www.w3schools.com/sql/sql_where.asp' target = '_blank' >Learn SQL query ^</a>)</font>";
                }
                catch (Exception e1)
                {
                    Session["qry"] = "Select * from " + Session["tName"];
                    txtSQL.Text = "Select * from " + Session["tName"];
                    BindDataGrid();
                    lblSQLError.Text = "<font face = 'courier new' size = 2><a href='http://www.w3schools.com/sql/sql_where.asp' target = '_blank' >Error:" + e1.Message + " </a></font>";
                }
            }

            //lblstatus.Text = "Results of the query: [<b>" + txtSQL.Text + "</b>]";

        }
        private void getImageColumnNumberAndName()
        {
            ImageColumnNoAndNameList.Clear();
            //int retValue=0;
            string s = "";
            //	System.Web.UI.WebControls.Image myImage ;

            SqlDataReader myReader;
            int j = 0;

            //i = Convert.ToInt32(Request.QueryString["id"]); 
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
            this.Button2.Click += new System.EventHandler(this.Button2_Click);
            this.DGtable.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DGtable_PageIndexChanged);
            this.DGtable.SortCommand += new System.Web.UI.WebControls.DataGridSortCommandEventHandler(this.DGtable_SortCommand);
            this.Load += new System.EventHandler(this.Page_Load);

        }
        #endregion
        private string GetFirstKeys()
        {
            string returnvalue = "Node";
            DataTable dt = new DataTable();

            string query;
            query = "Select * from " + Request.QueryString["table"].ToString().Trim();

            clsDataAccess myclass = new clsDataAccess();
            myclass.openConnection();
            mydr = myclass.getDatabyPaging(query);
            returnvalue = mydr.Tables[0].Columns[0].Caption;
            mydr.Clear();
            return returnvalue;
        }
        private void DGtable_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
        {
            DGtable.CurrentPageIndex = e.NewPageIndex;
            getImageColumnNumberAndName();
            BindDataGrid();
        }
        private void BindDataGrid()
        {
            DGtable.Columns.Clear();
            Session["FirstKey"] = GetFirstKeys();
            HyperLinkColumn urlView = new HyperLinkColumn();
            urlView.Text = "View";
            urlView.DataNavigateUrlField = Session["FirstKey"].ToString();
            urlView.HeaderText = "View";
            urlView.DataNavigateUrlFormatString = "Viewdata.aspx?id={0}";

            HyperLinkColumn urlEdit = new HyperLinkColumn();
            urlEdit.Text = "Edit";
            urlEdit.DataNavigateUrlField = Session["FirstKey"].ToString();
            urlEdit.HeaderText = "Edit";
            urlEdit.DataNavigateUrlFormatString = "Editdata.aspx?id={0}";

            HyperLinkColumn urlDelete = new HyperLinkColumn();
            urlDelete.Text = "Delete";
            urlDelete.DataNavigateUrlField = Session["FirstKey"].ToString();
            urlDelete.HeaderText = "Delete";
            urlDelete.DataNavigateUrlFormatString = "Deletedata.aspx?id={0}";

            //Response.End();
            HyperLinkColumn[] urlImages = new HyperLinkColumn[ImageColumnNoAndNameList.Count];

            for (int i = 0; i < ImageColumnNoAndNameList.Count; i++)
            {
                Photo p = (Photo)ImageColumnNoAndNameList[i];

                urlImages[i] = new HyperLinkColumn();
                urlImages[i].HeaderText = p.Caption;
                urlImages[i].DataNavigateUrlField = Session["FirstKey"].ToString();
                urlImages[i].DataNavigateUrlFormatString = "image.aspx?id={0}&cnt=" + p.PhotoID;
                urlImages[i].Target = "_blank";
                urlImages[i].DataTextField = Session["FirstKey"].ToString();
                urlImages[i].DataTextFormatString = "<img border = '0' src='image.aspx?id={0}&cnt=" + p.PhotoID + "' Width='75' >";
            }

            //				
            //			
            //			// Add three columns to collection.
            DGtable.Columns.Add(urlView);
            DGtable.Columns.Add(urlEdit);
            DGtable.Columns.Add(urlDelete);
            //			
            for (int i = 0; i < ImageColumnNoAndNameList.Count; i++)
            {
                DGtable.Columns.Add(urlImages[i]);
            }
            //			
            DGtable.DataSource = CreateDataSource();
            DGtable.DataBind();
        }

        private void DGtable_SortCommand(object source, System.Web.UI.WebControls.DataGridSortCommandEventArgs e)
        {
            SortField = (string)e.SortExpression;
            Session["qry"] = "Select * from " + Session["tName"].ToString();
            txtSQL.Text = Session["qry"].ToString() + " Order by " + SortField;
            //lblstatus.Text = "Results of the query: [<b>" + txtSQL.Text + "</b>]";
            DGtable.CurrentPageIndex = 0;
            getImageColumnNumberAndName();
            BindDataGrid();
            //txtSQL.Text =   txtSQL.Text + " SortField2:" + SortField;	

        }
        ICollection CreateDataSource()
        {

            DataTable dt = new DataTable();
            Session["tName"] = Request.QueryString["table"].ToString().Trim();
            string query;

            query = Session["qry"].ToString();

            if (SortField != null)
            {
                if (SortField.Length > 0)
                {
                    txtSQL.Text = query + " order by " + SortField;
                }

            }
            else
            {
                txtSQL.Text = Session["qry"].ToString();
            }

            sQuery = txtSQL.Text;

            clsDataAccess myclass = new clsDataAccess();
            myclass.openConnection();
            mydr = myclass.getDatabyPaging(query);
            //'mydr.Tables[0].PrimaryKey  
            TempTable = new DataTable();
            TempTable = mydr.Tables[0];
            TempTableView = new DataView(TempTable);
            TempTableView.Sort = SortField;
            // Get the number of elements in the array.
            return TempTableView;
        }



        private void Button1_Click(object sender, System.EventArgs e)
        {
            Response.Redirect("AddData.aspx");
        }

        private void Button2_Click(object sender, System.EventArgs e)
        {
            Session["qry"] = txtSQL.Text;
        }

        private void lblSQLError_Click(object sender, System.EventArgs e)
        {

        }

    }
    public class DataGridTemplate : ITemplate
    {
        ListItemType templateType;
        string columnName;

        public DataGridTemplate(ListItemType type, string colname)
        {
            templateType = type;
            columnName = colname;
        }

        public void InstantiateIn(System.Web.UI.Control container)
        {
            Literal lc = new Literal();
            System.Web.UI.WebControls.Image myImage = new System.Web.UI.WebControls.Image();

            switch (templateType)
            {
                case ListItemType.Header:
                    lc.Text = "<B>" + columnName + "</B>";
                    container.Controls.Add(lc);
                    break;
                case ListItemType.Item:
                    lc.Text = "<Img src='" + columnName + "{0}" + "&cnt=5'  Width='100' Height='75'>";
                    container.Controls.Add(lc);
                    break;
                case ListItemType.EditItem:
                    TextBox tb = new TextBox();
                    tb.Text = "";
                    container.Controls.Add(tb);
                    break;
                case ListItemType.Footer:
                    lc.Text = "<I>" + columnName + "</I>";
                    container.Controls.Add(lc);
                    break;
            }
        }
    }
}