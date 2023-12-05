using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class Outbox : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=|Datadirectory|\maildb.mdf;Integrated Security=True;User Instance=True");
    SqlCommand cmd;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            cmd = new SqlCommand("select * from mailtb where  Umail='" + Session["uname"].ToString() + "' and MailType='INBOX' ", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            grid.DataSource = dt;
            grid.DataBind();
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow gvrow in grid.Rows)
        {

            CheckBox chk = (CheckBox)gvrow.FindControl("chkSelect");
            if (chk != null & chk.Checked)
            {
                string strValue = ((HiddenField)gvrow.Cells[0].FindControl("HiddenField1")).Value;


                Session["mailid"] = strValue;


                Response.Redirect("ReadMail1.aspx");


            }
            else
            {

            }

        }
    }
}