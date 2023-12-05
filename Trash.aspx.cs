using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class Trash : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=|Datadirectory|\maildb.mdf;Integrated Security=True;User Instance=True");
    SqlCommand cmd;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            cmd = new SqlCommand("select * from mailtb where MailType='Trash' and Rmail='" + Session["uname"].ToString() + "' ", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            grid.DataSource = dt;
            grid.DataBind();
        }
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow gvrow in grid.Rows)
        {

            CheckBox chk = (CheckBox)gvrow.FindControl("chkSelect");
            if (chk != null & chk.Checked)
            {
                cmd = new SqlCommand("Delete from  mailtb where id='" + gvrow.Cells[1].Text + "'", con);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

            }
            else
            {

            }

        }

        cmd = new SqlCommand("select * from mailtb where MailType='Trash' and Rmail='" + Session["uname"].ToString() + "' ", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        grid.DataSource = dt;
        grid.DataBind();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow gvrow in grid.Rows)
        {

            CheckBox chk = (CheckBox)gvrow.FindControl("chkSelect");
            if (chk != null & chk.Checked)
            {



                Session["mailid"] = gvrow.Cells[1].Text;

                Response.Redirect("ReadMail.aspx");


            }
            else
            {

            }

        }
    }
}