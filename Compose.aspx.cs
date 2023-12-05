using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Security.Cryptography;
using System.IO;
using System.Text;
using iTextSharp.text.pdf;
using iTextSharp.text;
using iTextSharp.text.pdf.parser;
using iDiTect.Converter;
using System.Drawing;
using System.Drawing.Imaging;
using Aspose;
using System.Net;





public partial class Compose : System.Web.UI.Page
{

    SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=|Datadirectory|\maildb.mdf;Integrated Security=True;User Instance=True");
    SqlCommand cmd;

    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownList1.Text == "Location" || DropDownList1.Text == "Group")
        {
            TextBox1.Visible = false;

            DropDownList2.Visible = true;



            if (DropDownList1.Text == "Location")
            {
                con.Open();

                cmd = new SqlCommand("select * from regtb ", con);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {


                    DropDownList2.Items.Add(dr["Location"].ToString());

                }


                con.Close();
            }
            else if (DropDownList1.Text == "Group")
            {
                con.Open();
                cmd = new SqlCommand("select * from regtb ", con);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {


                    DropDownList2.Items.Add(dr["GroupName"].ToString());

                }


                con.Close();
            }

        }
        else if (DropDownList1.Text == "Individual")
        {
            TextBox1.Visible = true;
            DropDownList2.Visible = false;

        }
        else
        {
            Response.Write("<Script> alert(Select Send Option') </Script>");
        }
    }

    string mailtype;
    public static string pass(int length)
    {
        const string chars = "abcdefghijklmnopqstuuvwxyz1234567890";
        var random = new Random();
        return new string(Enumerable.Repeat(chars, length)
          .Select(s => s[random.Next(s.Length)]).ToArray());
    }

    string mobile;
    string filename;

    int i = 0;

    int userid = 0;
    protected void Button1_Click(object sender, EventArgs e)
    {



        if (DropDownList1.Text == "Individual")
        {
            con.Open();
            cmd = new SqlCommand("select * from regtb where UserName='" + TextBox1.Text + "'", con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {


                userid = 1;

            }
            else
            {

                userid = 0;

            }

            con.Close();
        }



        if (userid == 1)
        {


            if (FileUpload1.HasFile)
            {




                filename = System.IO.Path.GetFileName(FileUpload1.PostedFile.FileName);
                string ext = System.IO.Path.GetExtension(FileUpload1.PostedFile.FileName);
                string withoutext = System.IO.Path.GetFileNameWithoutExtension(FileUpload1.PostedFile.FileName);


                FileUpload1.PostedFile.SaveAs(Server.MapPath("~/Upload/" + filename));

                string filePath = Server.MapPath("~/Upload/" + filename);
                string pdfPath = Server.MapPath("~/pdf/" + withoutext + ".pdf");
                string watmar = Server.MapPath("~/watermark/" + withoutext + ".pdf");

                string lastfilename = withoutext + ".pdf";


                if (ext == ".txt")
                {

                    texttopdf(filePath, pdfPath);


                }
                else if (ext == ".doc" || ext == ".docx")
                {

                    doctopdf(filePath, pdfPath);


                }
                else if (ext == ".pdf")
                {

                    FileUpload1.PostedFile.SaveAs(Server.MapPath("~/pdf/" + filename));
                }
                else if (ext == ".jpg" || ext == ".jpeg" || ext == ".png" || ext == ".gif")
                {

                    ConvertImageToPdf(filePath, pdfPath);

                }

                else
                {
                    Response.Write("<Script> alert('File Upload allowed only image,pdf,doc,txt file') </Script>");

                    i = 1;
                }





                if (i == 0)
                {

                    PdfStampInExistingFile(Session["uname"].ToString(), pdfPath, watmar);






                    List<string> optionList = new List<string>();

                    optionList.Clear();



                    if (DropDownList1.Text == "Location")
                    {

                        con.Open();
                        cmd = new SqlCommand("select * from regtb  where Location='" + DropDownList2.Text + "'", con);
                        SqlDataReader dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {


                            optionList.Add(dr["UserName"].ToString());

                        }
                        con.Close();
                    }
                    else if (DropDownList1.Text == "Group")
                    {
                        con.Open();
                        cmd = new SqlCommand("select * from regtb  where Group='" + DropDownList2.Text + "'", con);
                        SqlDataReader dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {


                            optionList.Add(dr["UserName"].ToString());

                        }
                        con.Close();


                    }
                    else if (DropDownList1.Text == "Individual")
                    {
                        optionList.Add(TextBox1.Text);

                    }




                    string filePath1 = Server.MapPath("~/Encrypt/" + lastfilename);
                    EncryptFile(watmar, filePath1, pass(5).ToString());

                    foreach (string item in optionList)
                    {




                        cmd = new SqlCommand("insert into mailtb values(@Umail,@Rmail,@Subject,@Message,@Image,@Image1,@ReadMail,@MailType,@date,@Time,@keys)", con);
                        cmd.Parameters.AddWithValue("@Umail", Session["uname"].ToString());
                        cmd.Parameters.AddWithValue("@Rmail", item);
                        cmd.Parameters.AddWithValue("@Subject", TextBox2.Text);
                        cmd.Parameters.AddWithValue("@Message", TextBox3.Text);
                        cmd.Parameters.AddWithValue("@Image", "~/img/3.png");
                        cmd.Parameters.AddWithValue("@Image1", lastfilename);
                        cmd.Parameters.AddWithValue("@ReadMail", "~/img/1.png");
                        cmd.Parameters.AddWithValue("@MailType", "inbox");
                        cmd.Parameters.AddWithValue("@date", System.DateTime.Now.ToShortDateString());
                        cmd.Parameters.AddWithValue("@Time", System.DateTime.Now.ToShortTimeString());
                        cmd.Parameters.AddWithValue("@Keys", pass(5).ToString());
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }








                   // Response.Write("<Script> alert('Mail Send File Encrypted') </Script>");

                    Label7.Text = "Mail Send File Encrypted";



                    i = 0;

                }



            }
            else
            {




                List<string> optionList = new List<string>();

                optionList.Clear();



                if (DropDownList1.Text == "Location")
                {

                    con.Open();
                    cmd = new SqlCommand("select * from regtb  where Location='" + DropDownList2.Text + "'", con);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {


                        optionList.Add(dr["UserName"].ToString());

                    }
                    con.Close();
                }
                else if (DropDownList1.Text == "Group")
                {
                    con.Open();
                    cmd = new SqlCommand("select * from regtb  where Group='" + DropDownList2.Text + "'", con);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {


                        optionList.Add(dr["UserName"].ToString());

                    }
                    con.Close();


                }
                else if (DropDownList1.Text == "Individual")
                {
                    optionList.Add(TextBox1.Text);

                }


                foreach (string item in optionList)
                {

                    cmd = new SqlCommand("insert into mailtb values(@Umail,@Rmail,@Subject,@Message,@Image,@Image1,@ReadMail,@MailType,@date,@Time,@Keys)", con);
                    cmd.Parameters.AddWithValue("@Umail", Session["uname"].ToString());
                    cmd.Parameters.AddWithValue("@Rmail", item);
                    cmd.Parameters.AddWithValue("@Subject", TextBox2.Text);
                    cmd.Parameters.AddWithValue("@Message", TextBox3.Text);
                    cmd.Parameters.AddWithValue("@Image", "~/img/4.png");
                    cmd.Parameters.AddWithValue("@Image1", "");
                    cmd.Parameters.AddWithValue("@ReadMail", "~/img/1.png");
                    cmd.Parameters.AddWithValue("@MailType", "Inbox");
                    cmd.Parameters.AddWithValue("@date", System.DateTime.Now.ToShortDateString());
                    cmd.Parameters.AddWithValue("@Time", System.DateTime.Now.ToShortTimeString());
                    cmd.Parameters.AddWithValue("@Keys", "");
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                }


                //Response.Write("<Script> alert('Mail Send') </Script>");

                Label7.Text = "Mail Send";

            }






        }
        else
        {
            //Response.Write("<Script> alert('Mailid Not Found!') </Script>");
            Label7.Text = "Mailid Not Found!";

            sendmessage("6383441458", "UserNAme " + Session["uname"].ToString() + " Send Mail Id" + TextBox1.Text);
        }






    }

    public void sendmessage(string targetno, string message)
    {


        String query = "http://smsserver9.creativepoint.in/api.php?username=fantasy&password=596692&to=" + targetno + "&from=FSSMSS&message=Dear user  your msg is " + message + " Sent By FSMSG FSSMSS&PEID=1501563800000030506&templateid=1507162882948811640";
        WebClient client = new WebClient();
        Stream sin = client.OpenRead(query);
        // MessageBox.Show("Message Send!");
        // Response.Write("<script> alert('Message Send') </script>");
    }


    private void texttopdf(string sourceFilePath,string outputFilePath)
    {
        StreamReader str = new StreamReader(sourceFilePath);
        iTextSharp.text.Document doc = new iTextSharp.text.Document();
        PdfWriter.GetInstance(doc, new FileStream(outputFilePath, FileMode.Create));
        doc.Open();
        doc.Add(new iTextSharp.text.Paragraph(str.ReadToEnd()));
        doc.Close();  


    }

    public static void ConvertImageToPdf(string srcFilename, string dstFilename)
    {
        iTextSharp.text.Rectangle pageSize = null;

        using (var srcImage = new Bitmap(srcFilename))
        {
            pageSize = new iTextSharp.text.Rectangle(0, 0, srcImage.Width, srcImage.Height);
        }
        using (var ms = new MemoryStream())
        {
            var document = new iTextSharp.text.Document(pageSize, 0, 0, 0, 0);
            iTextSharp.text.pdf.PdfWriter.GetInstance(document, ms).SetFullCompression();
            document.Open();
            var image = iTextSharp.text.Image.GetInstance(srcFilename);
            document.Add(image);
            document.Close();

            File.WriteAllBytes(dstFilename, ms.ToArray());
        }
    }

   // public Aspose.Words.Document();

    private void doctopdf(string sourceFilePath, string outputFilePath)
    {

        Aspose.Words.Document doc = new Aspose.Words.Document(sourceFilePath);
        // Save as PDF
        doc.Save(outputFilePath);




       // string filenaamme = FileUpload1.PostedFile.FileName;
       // FileUpload1.PostedFile.SaveAs(Server.MapPath("~/upload/" + filenaamme));

        //string ss = Server.MapPath("~/upload/" + filenaamme);

       // Microsoft.Office.Interop.Word.Application word = new Microsoft.Office.Interop.Word.Application();

       // // C# doesn't have optional arguments so we'll need a dummy value
       // object oMissing = System.Reflection.Missing.Value;
       // word.Visible = false;
       // word.ScreenUpdating = false; word.Visible = false;
       // word.ScreenUpdating = false;

       // Object filename = (Object)sourceFilePath;

       // // Use the dummy value as a placeholder for optional arguments
       // Microsoft.Office.Interop.Word.Document doc = word.Documents.Open(ref filename, ref oMissing,
       //     ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing,
       //     ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing,
       //     ref oMissing, ref oMissing, ref oMissing, ref oMissing);
       // doc.Activate();

       // object outputFileName = outputFilePath;
       // object fileFormat = WdSaveFormat.wdFormatPDF;

       // // Save document into PDF Format
       // doc.SaveAs(ref outputFileName,
       //     ref fileFormat, ref oMissing, ref oMissing,
       //     ref oMissing, ref oMissing, ref oMissing, ref oMissing,
       //     ref oMissing, ref oMissing, ref oMissing, ref oMissing,
       //     ref oMissing, ref oMissing, ref oMissing, ref oMissing);

       // // Close the Word document, but leave the Word application open.
       // // doc has to be cast to type _Document so that it will find the
       // // correct Close method.                
       // object saveChanges = WdSaveOptions.wdDoNotSaveChanges;
       // ((_Document)doc).Close(ref saveChanges, ref oMissing, ref oMissing);
       // doc = null;


       // ((_Application)word).Quit(ref oMissing, ref oMissing, ref oMissing);
       // word = null;

       //// Watermarker watermarker = new Watermarker(@"C:\Files\Watermark\sample.docx");
       
    }

    private void PdfStampInExistingFile(string text, string sourceFilePath, string outputFilePath)
    {
        //string sourceFilePath = @"C:\Users\anand\Desktop\Test.pdf";
         //sourceFilePath = Server.MapPath("~/Upload/DocTo.pdf");

       // byte[] bytes = File.ReadAllBytes(sourceFilePath);
       // Bitmap bitmap = new Bitmap(200, 30, System.Drawing.Imaging.PixelFormat.Format64bppArgb);
       // Graphics graphics = Graphics.FromImage(bitmap);
       // graphics.Clear(Color.White);
       // graphics.DrawString(text, new System.Drawing.Font("Arial", 12, FontStyle.Bold), new SolidBrush(Color.Red), new PointF(0.4F, 2.4F));
       // bitmap.Save(Server.MapPath("~/Image.jpg"), ImageFormat.Jpeg);
       // bitmap.Dispose();
       // var img = iTextSharp.text.Image.GetInstance(Server.MapPath("~/Image.jpg"));
       // img.SetAbsolutePosition(100, 200);
       // img.SetAbsolutePosition(400, 200);

       // PdfContentByte waterMark;
       // using (MemoryStream stream = new MemoryStream())
       // {
       //     PdfReader reader = new PdfReader(bytes);
       //     using (PdfStamper stamper = new PdfStamper(reader, stream))
       //     {
       //         int pages = reader.NumberOfPages;
       //         for (int i = 1; i <= pages; i++)
       //         {
       //             waterMark = stamper.GetUnderContent(i);
       //             waterMark.AddImage(img);
       //         }
       //     }
       //     bytes = stream.ToArray();
       // }
       //// File.Delete(Server.MapPath("~/Image.jpg"));
       // File.WriteAllBytes(outputFilePath, bytes);


        Aspose.Pdf.Text.TextState ts = new Aspose.Pdf.Text.TextState();
        // Set color for stroke
        ts.StrokingColor = Aspose.Pdf.Color.Gray;
        // Set text rendering mode
        ts.RenderingMode = Aspose.Pdf.Text.TextRenderingMode.StrokeText;
        // Load an input PDF document
        Aspose.Pdf.Facades.PdfFileStamp fileStamp = new Aspose.Pdf.Facades.PdfFileStamp(new Aspose.Pdf.Document(sourceFilePath));

        Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();
        stamp.BindLogo(new Aspose.Pdf.Facades.FormattedText("Email Server", System.Drawing.Color.Gray, "Arial", Aspose.Pdf.Facades.EncodingType.Winansi, true, 78));

        // Bind TextState
        stamp.BindTextState(ts);
        // Set X,Y origin
        stamp.SetOrigin(100, 100);
        stamp.Opacity = 5;
        stamp.BlendingSpace = Aspose.Pdf.Facades.BlendingColorSpace.DeviceRGB;
        stamp.Rotation = 45.0F;
        stamp.IsBackground = true;
        // Add Stamp
        fileStamp.AddStamp(stamp);
       
        fileStamp.Save(outputFilePath);
        fileStamp.Close();


    }



    //public void manipulatePdf(String src, String dest)
    //{
    //    PdfReader reader = new PdfReader(src);
    //    int n = reader.GetPageContent();
    //    PdfStamper stamper = new PdfStamper(reader, new FileOutputStream(dest));
    //    PdfContentByte under = stamper.getUnderContent(1);
    //    Font f = new Font(FontFamily.HELVETICA, 15);
    //    Phrase p = new Phrase(
    //        "This watermark is added UNDER the existing content", f);
    //    ColumnText.showTextAligned(under, Element.ALIGN_CENTER, p, 297, 550, 0);
    //    PdfContentByte over = stamper.getOverContent(1);
    //    p = new Phrase("This watermark is added ON TOP OF the existing content", f);
    //    ColumnText.showTextAligned(over, Element.ALIGN_CENTER, p, 297, 500, 0);
    //    p = new Phrase(
    //        "This TRANSPARENT watermark is added ON TOP OF the existing content", f);
    //    over.saveState();
    //    PdfGState gs1 = new PdfGState();
    //    gs1.setFillOpacity(0.5f);
    //    over.setGState(gs1);
    //    ColumnText.showTextAligned(over, Element.ALIGN_CENTER, p, 297, 450, 0);
    //    over.restoreState();
    //    stamper.close();
    //    reader.close();
    //}



    private void EncryptFile(string inputFile, string outputFile, string keys)
    {


        string password = @"myKey123";
        UnicodeEncoding UE = new UnicodeEncoding();
        byte[] key = UE.GetBytes(password);

        string cryptFile = outputFile;
        FileStream fsCrypt = new FileStream(cryptFile, FileMode.Create);

        RijndaelManaged RMCrypto = new RijndaelManaged();

        CryptoStream cs = new CryptoStream(fsCrypt,
            RMCrypto.CreateEncryptor(key, key),
            CryptoStreamMode.Write);

        FileStream fsIn = new FileStream(inputFile, FileMode.Open);

        int data;
        while ((data = fsIn.ReadByte()) != -1)
            cs.WriteByte((byte)data);

        fsIn.Close();
        cs.Close();
        fsCrypt.Close();
        Response.Write("Encryption Sucessfully Completed");

    }
    protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}