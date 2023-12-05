using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class Inbox : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=|Datadirectory|\maildb.mdf;Integrated Security=True;User Instance=True");
    SqlCommand cmd;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            cmd = new SqlCommand("select * from mailtb where MailType='inbox' and Rmail='" + Session["uname"].ToString() + "' ", con);
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
                string strValue = ((HiddenField)gvrow.Cells[0].FindControl("HiddenField1")).Value;

                int dd = Convert.ToInt32(strValue);

                cmd = new SqlCommand("Update mailtb set MailType='Trash' where id='" + dd + "'", con);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                Response.Write("<Script> alert('" + strValue + "') </Script>");
            }
            else
            {

            }

        }

        cmd = new SqlCommand("select * from mailtb where MailType='INBOX' and Rmail='" + Session["uname"].ToString() + "' ", con);
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


                string strValue = ((HiddenField)gvrow.Cells[0].FindControl("HiddenField1")).Value;

                int dd = Convert.ToInt32(strValue);


                Session["mailid"] = dd;


                string read = "~/img/2.png";

                cmd = new SqlCommand("update  mailtb set ReadMail='" + read + "'   where id='" + strValue + "'", con);
                con.Open();
                cmd.ExecuteReader();
                con.Close();


                cmd = new SqlCommand("update  mailtb set ReadMail='" + read + "'   where id='" + dd + "'", con);
                con.Open();
                cmd.ExecuteReader();
                con.Close();

                Response.Redirect("ReadMail1.aspx");


            }
            else
            {

            }

        }
    }
}
