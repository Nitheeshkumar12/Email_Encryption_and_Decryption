using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp.text.pdf;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using WordToPDF;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        PdfStampInExistingFile("@Sangeethkumar.com");
        
    }
 
    private void PdfStampInExistingFile(string text)
    {
        //string sourceFilePath = @"C:\Users\anand\Desktop\Test.pdf";
        string sourceFilePath = Server.MapPath("~/Upload/DocTo.pdf");
       
        byte[] bytes = File.ReadAllBytes(sourceFilePath);
        Bitmap bitmap = new Bitmap(200, 30, System.Drawing.Imaging.PixelFormat.Format64bppArgb);
        Graphics graphics = Graphics.FromImage(bitmap);
        graphics.Clear(Color.White);
        graphics.DrawString(text, new System.Drawing.Font("Arial", 12, FontStyle.Bold), new SolidBrush(Color.Red), new PointF(0.4F, 2.4F));
        bitmap.Save(Server.MapPath("~/Image.jpg"), ImageFormat.Jpeg);
        bitmap.Dispose();
        var img = iTextSharp.text.Image.GetInstance(Server.MapPath("~/Image.jpg"));
        img.SetAbsolutePosition(200, 400);
        img.SetAbsolutePosition(400, 200);
        PdfContentByte waterMark;
        using (MemoryStream stream = new MemoryStream())
        {
            PdfReader reader = new PdfReader(bytes);
            using (PdfStamper stamper = new PdfStamper(reader, stream))
            {
                int pages = reader.NumberOfPages;
                for (int i = 1; i <= pages; i++)
                {
                    waterMark = stamper.GetOverContent(1);
                   
                    waterMark.AddImage(img);
                }
            }
            bytes = stream.ToArray();
        }
        File.Delete(Server.MapPath("~/Image.jpg"));
        File.WriteAllBytes(sourceFilePath, bytes);
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        Word2Pdf objWorPdf = new Word2Pdf();
        string backfolder1 = "J:\\";
        string strFileName = "AtmBankJava.docx";
        object FromLocation = backfolder1 + "\\" + strFileName;
        string FileExtension = Path.GetExtension(strFileName);
        string ChangeExtension = strFileName.Replace(FileExtension, ".pdf");
        if (FileExtension == ".doc" || FileExtension == ".docx")
        {
            object ToLocation = backfolder1 + "\\" + ChangeExtension;
            objWorPdf.InputLocation = FromLocation;
            objWorPdf.OutputLocation = ToLocation;
            objWorPdf.Word2PdfCOnversion();
        } 
    }
}