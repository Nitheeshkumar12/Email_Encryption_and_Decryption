using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AdminLogin : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {

        if (TextBox1.Text == "admin" & TextBox2.Text == "admin")
        {
            Response.Redirect("AdminHome.aspx");
        }
        else
        {
            Response.Write("<Script> alert('Username or PAssword Incorrect!') </Script>");

        }

    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        TextBox1.Text = "";
        TextBox2.Text = "";


    }
}