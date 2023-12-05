using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class KeyRequest : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=|datadirectory|\maildb.mdf;Integrated Security=True;User Instance=True");
    SqlCommand cmd;

    protected void Page_Load(object sender, EventArgs e)
    {
        bind();
    }
    private void bind()
    {
        cmd = new SqlCommand("select * from keytb where Rmail='" + Session["uname"].ToString() + "' and Status ='Waiting'  ", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        GridView1.DataSource = dt;
        GridView1.DataBind();

        cmd = new SqlCommand("select * from keytb where  Rmail='" + Session["uname"].ToString() + "' and Status !='Waiting' ", con);
        SqlDataAdapter da1 = new SqlDataAdapter(cmd);
        DataTable dt1 = new DataTable();
        da1.Fill(dt1);
        GridView2.DataSource = dt1;
        GridView2.DataBind();

    }

    protected void lnkView_Click(object sender, EventArgs e)
    {
        GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
        string id = grdrow.Cells[0].Text;


        con.Open();
        cmd = new SqlCommand("Select * from keytb where id='" + id + "' ", con);
        SqlDataReader dr = cmd.ExecuteReader();
        if (dr.Read())
        {

            string aaa = dr["Image"].ToString();
            // Response.Write("<Script> alert('" + aaa + "') </script>");
            if (aaa != string.Empty)
            {
                string filePath = aaa;
                Response.ContentType = "doc/docx";
                Response.AddHeader("Content-Disposition", "attachment;filename=\"" + aaa + "\"");
                Response.TransmitFile(Server.MapPath(filePath));
                Response.End();
            }



        }
        
        con.Close();
       
      


       

    }
}