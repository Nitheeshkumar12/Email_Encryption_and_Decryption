using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Drawing;


public partial class ReadMail1 : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=|Datadirectory|\maildb.mdf;Integrated Security=True;User Instance=True");
    SqlCommand cmd;
    string Rmail, keys;
    protected void Page_Load(object sender, EventArgs e)
    {

        con.Open();
        cmd = new SqlCommand("Select * from mailtb where id='" + Session["mailid"].ToString() + "'", con);
        SqlDataReader dr = cmd.ExecuteReader();
        if (dr.Read())
        {

            Label6.Text = dr["Subject"].ToString();
            Label7.Text = dr["Umail"].ToString();
            Label8.Text = dr["Message"].ToString();
            Label9.Text = dr["Image1"].ToString();
            Label11.Text = dr["id"].ToString();
            keys = dr["Keys"].ToString();

        }
        con.Close();

    }

    string status;

    protected void Button1_Click(object sender, EventArgs e)
    {
        status = "";

        con.Open();
        cmd = new SqlCommand("Select * from keytb where MailId='" + Session["mailid"].ToString() + "' and  Status='Approved' ", con);
        SqlDataReader dr1 = cmd.ExecuteReader();
        if (dr1.Read())
        {
            status = "Accept";
        }
        else
        {
            status = "reject";
          
        }
        con.Close();

        if (status == "reject")
        {

            con.Open();
            cmd = new SqlCommand("Select * from mailtb where id='" + Session["mailid"].ToString() + "' and Image1 !='' ", con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {

                string aaa = "~/Encrypt/" + dr["Image1"].ToString();
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
            else
            {
                Response.Write("<Script> alert('No Attachement Found!') </Script>");

            }
            con.Close();

        }
        else
        {

            if (FileUpload1.HasFile)
            {



                string mm = FileUpload1.PostedFile.FileName;

               // string founder = mm;
                // Remove all characters after first 25 chars  
                mm = mm.Remove(0, 7);

                Response.Write("<Script> alert('" + mm + "')</Script>");

                string ss = "~/Hide/" + mm;



                con.Open();
                cmd = new SqlCommand("select * from keytb  where Image='" + ss + "' and Hkey='" + TextBox1.Text + "'", con);
                SqlDataReader dr12 = cmd.ExecuteReader();
                if (dr12.Read())
                {
                    Bitmap bmp = new Bitmap(Server.MapPath(ss));

                    Session["ency"] = hideiamge.extractText(bmp);



                    Response.Write("<Script> alert('Unhide Compeleted')</Script>");
                }
                else
                {

                    Session["ency"] = "";

                    Response.Write("<Script> alert('Unhide Key Incorrect')</Script>");
                }
                con.Close();






                if (Session["ency"] == "")
                {
                }
                else
                {



                    con.Open();
                    cmd = new SqlCommand("select * from keytb where MailId='" + Session["mailid"].ToString() + "' ", con);
                    SqlDataReader dr11 = cmd.ExecuteReader();
                    if (dr11.Read())
                    {
                        string keys = dr11["Keys"].ToString();
                        string filename = dr11["FileName"].ToString();
                        string filePath1 = Server.MapPath("~/Encrypt/" + filename);
                        string filePath2 = Server.MapPath("~/Decrypt/" + filename);
                        DecryptFile(filePath1, filePath2, keys);

                        string aaa = "~/Decrypt/" + filename;

                        if (aaa != string.Empty)
                        {
                            string filePath = aaa;
                            Response.ContentType = "doc/docx";
                            Response.AddHeader("Content-Disposition", "attachment;filename=\"" + aaa + "\"");
                            Response.TransmitFile(Server.MapPath(filePath));
                            Response.End();
                        }





                    }
                    else
                    {

                    }


                }
            }


        }



    }

    private void DecryptFile(string inputFile, string outputFile, string kkk)
    {

        {
            string keyss = keys;
            string password = @"myKey123";

            UnicodeEncoding UE = new UnicodeEncoding();
            byte[] key = UE.GetBytes(password);

            FileStream fsCrypt = new FileStream(inputFile, FileMode.Open);

            RijndaelManaged RMCrypto = new RijndaelManaged();

            CryptoStream cs = new CryptoStream(fsCrypt,
                RMCrypto.CreateDecryptor(key, key),
                CryptoStreamMode.Read);

            FileStream fsOut = new FileStream(outputFile, FileMode.Create);

            int data;
            while ((data = cs.ReadByte()) != -1)
                fsOut.WriteByte((byte)data);

            fsOut.Close();
            cs.Close();
            fsCrypt.Close();

        }
    }



    class hideiamge
    {
        public enum State
        {
            Hiding,
            Filling_With_Zeros
        };

        public static Bitmap invisbleText(string text, Bitmap bmp)
        {

            State state = State.Hiding;


            int charIndex = 0;


            int charValue = 0;


            long pixelElementIndex = 0;


            int zeros = 0;

            // hold pixel elements
            int R = 0, G = 0, B = 0;

            // pass through the rows
            for (int i = 0; i < bmp.Height; i++)
            {

                for (int j = 0; j < bmp.Width; j++)
                {

                    Color pixel = bmp.GetPixel(j, i);

                    // now, clear the least significant bit (LSB) from each pixel element
                    R = pixel.R - pixel.R % 2;
                    G = pixel.G - pixel.G % 2;
                    B = pixel.B - pixel.B % 2;

                    // for each pixel, pass through its elements (RGB)
                    for (int n = 0; n < 3; n++)
                    {
                        // check if new 8 bits has been processed
                        if (pixelElementIndex % 8 == 0)
                        {
                            // check if the whole process has finished
                            // we can say that it's finished when 8 zeros are added
                            if (state == State.Filling_With_Zeros && zeros == 8)
                            {
                                // apply the last pixel on the image
                                // even if only a part of its elements have been affected
                                if ((pixelElementIndex - 1) % 3 < 2)
                                {
                                    bmp.SetPixel(j, i, Color.FromArgb(R, G, B));
                                }

                                // return the bitmap with the text hidden in
                                return bmp;
                            }

                            // check if all characters has been hidden
                            if (charIndex >= text.Length)
                            {
                                // start adding zeros to mark the end of the text
                                state = State.Filling_With_Zeros;
                            }
                            else
                            {
                                // move to the next character and process again
                                charValue = text[charIndex++];
                            }
                        }


                        switch (pixelElementIndex % 3)
                        {
                            case 0:
                                {
                                    if (state == State.Hiding)
                                    {
                                        R += charValue % 2;


                                        charValue /= 2;
                                    }
                                } break;
                            case 1:
                                {
                                    if (state == State.Hiding)
                                    {
                                        G += charValue % 2;

                                        charValue /= 2;
                                    }
                                } break;
                            case 2:
                                {
                                    if (state == State.Hiding)
                                    {
                                        B += charValue % 2;

                                        charValue /= 2;
                                    }

                                    bmp.SetPixel(j, i, Color.FromArgb(R, G, B));
                                } break;
                        }

                        pixelElementIndex++;

                        if (state == State.Filling_With_Zeros)
                        {
                            // increment the value of zeros until it is 8
                            zeros++;
                        }
                    }
                }
            }

            return bmp;
        }

        public static string extractText(Bitmap bmp)
        {
            int colorUnitIndex = 0;
            int charValue = 0;

            // holds the text that will be extracted from the image
            string extractedText = String.Empty;

            // pass through the rows
            for (int i = 0; i < bmp.Height; i++)
            {
                // pass through each row
                for (int j = 0; j < bmp.Width; j++)
                {
                    Color pixel = bmp.GetPixel(j, i);

                    // for each pixel, pass through its elements (RGB)
                    for (int n = 0; n < 3; n++)
                    {
                        switch (colorUnitIndex % 3)
                        {
                            case 0:
                                {
                                    charValue = charValue * 2 + pixel.R % 2;
                                } break;
                            case 1:
                                {
                                    charValue = charValue * 2 + pixel.G % 2;
                                } break;
                            case 2:
                                {
                                    charValue = charValue * 2 + pixel.B % 2;
                                } break;
                        }

                        colorUnitIndex++;

                        // if 8 bits has been added, then add the current character to the result text
                        if (colorUnitIndex % 8 == 0)
                        {
                            // reverse? of course, since each time the process happens on the right (for simplicity)
                            charValue = reverseBits(charValue);

                            // can only be 0 if it is the stop character (the 8 zeros)
                            if (charValue == 0)
                            {
                                return extractedText;
                            }

                            // convert the character value from int to char
                            char c = (char)charValue;

                            // add the current character to the result text
                            extractedText += c.ToString();
                        }
                    }
                }
            }

            return extractedText;
        }

        public static int reverseBits(int n)
        {
            int result = 0;

            for (int i = 0; i < 8; i++)
            {
                result = result * 2 + n % 2;

                n /= 2;
            }

            return result;
        }
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        if (Label9.Text == "")
        {
            Response.Write("<Script> alert('No Attachement Found!') </Script>");
        }
        else
        {
            con.Open();
            cmd = new SqlCommand("Select * from keytb where MailId='" + Session["mailid"].ToString() + "' ", con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                Response.Write("<Script> alert('Already Send Requst Send') </Script>");
                dr.Close();
            }
            else
            {

                dr.Close();
               // con.Open();
                cmd = new SqlCommand("insert into keytb values('" + Session["mailid"].ToString() + "','" + Label7.Text + "','" + Session["uname"].ToString() + "','" + Label9.Text + "','" + keys + "','waiting','','')", con);
                cmd.ExecuteNonQuery();

                Response.Write("<Script> alert('Key Requst Send') </Script>");
                //con.Close();
            }
            con.Close();
        }
    }
}