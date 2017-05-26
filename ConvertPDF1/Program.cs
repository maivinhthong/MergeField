using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HiQPdf;
using iTextSharp.text.pdf;


//using SelectPdf;

namespace ConvertPDF1
{
    class Program
    {
        static void Main(string[] args)
        {
            var old = @"C:\Users\Administrator\Downloads\Documents\general-bill-of-sale-form-3.pdf";
            var newFile = @"C:\Users\Administrator\Downloads\Documents\general-bill-of-sale-form-4.pdf";

            Dictionary<String, String> formKeys = new Dictionary<string, string>()
            {
                {"Date", DateTime.Now.ToString()},
                {"I the undersigned seller name", "Test"},
                {"Sellers signature", "Test2" }
            };

            var data = GeneratePdf(formKeys, old);

            File.WriteAllBytes(newFile, data);
        }

        private static byte[] GeneratePdf(Dictionary<String, String> formKeys, String pdfPath)
        {
            var reader = new PdfReader(pdfPath);
            var outStream = new MemoryStream();
            var stamper = new PdfStamper(reader, outStream);

            var form = stamper.AcroFields;
            form.GenerateAppearances = true; //Added this line, fixed my problem
            var fieldKeys = form.Fields.Keys;

            foreach (KeyValuePair<String, String> pair in formKeys)
            {
                if (fieldKeys.Any(f => f == pair.Key))
                {
                    form.SetField(pair.Key, pair.Value);
                }
            }
            stamper.Close();
            reader.Close();

            return flattenPdf(outStream.ToArray());
        }

        private static byte[] flattenPdf(byte[] pdf)
        {
            var reader = new PdfReader(pdf);

            var outStream = new MemoryStream();
            var stamper = new PdfStamper(reader, outStream);

            stamper.FormFlattening = true;

            stamper.Close();
            reader.Close();

            return outStream.ToArray();
        }

        public void Convert1()
        {
            ////instantiate the html to pdf converter
            //HtmlToPdf converter = new HtmlToPdf();

            //// convert the url to pdf 
            ////PdfDocument doc = converter.ConvertUrl("http://vnexpress.net/");
            //PdfDocument doc = converter.ConvertHtmlString(File.ReadAllText(@"d:\pdf\test.txt"));
            //// save pdf document 
            //doc.Save(@"d:\pdf\test.pdf");

            //// close pdf document 
            //doc.Close();
        }

        public void Convert2()
        {
            //// create the HTML to PDF converter 
            //HtmlToPdf htmlToPdfConverter = new HtmlToPdf();

            //// set browser width 
            //htmlToPdfConverter.BrowserWidth = int.Parse(textBoxBrowserWidth.Text);

            //// set browser height if specified, otherwise use the default 
            //if (textBoxBrowserHeight.Text.Length > 0)
            //    htmlToPdfConverter.BrowserHeight = int.Parse(textBoxBrowserHeight.Text);

            //// set HTML Load timeout 
            //htmlToPdfConverter.HtmlLoadedTimeout = int.Parse(textBoxLoadHtmlTimeout.Text);

            //// set PDF page size and orientation 
            //htmlToPdfConverter.Document.PageSize = GetSelectedPageSize();
            //htmlToPdfConverter.Document.PageOrientation = GetSelectedPageOrientation();

            //// set PDF page margins 
            //htmlToPdfConverter.Document.Margins = new PdfMargins(0);

            //// set a wait time before starting the conversion 
            //htmlToPdfConverter.WaitBeforeConvert = int.Parse(textBoxWaitTime.Text);

            //// convert HTML to PDF 
            //byte[] pdfBuffer = null;

            //if (radioButtonConvertUrl.Checked)
            //{
            //    // convert URL to a PDF memory buffer 
            //    string url = textBoxUrl.Text;

            //    pdfBuffer = htmlToPdfConverter.ConvertUrlToMemory(url);
            //}
            //else
            //{
            //    // convert HTML code 
            //    string htmlCode = textBoxHtmlCode.Text;
            //    string baseUrl = textBoxBaseUrl.Text;

            //    // convert HTML code to a PDF memory buffer 
            //    pdfBuffer = htmlToPdfConverter.ConvertHtmlToMemory(htmlCode, baseUrl);
            //}

            //// inform the browser about the binary data format 
            //HttpContext.Current.Response.AddHeader("Content-Type", "application/pdf");

            //// instruct browser how to open PDF as attachment or inline and file name 
            //HttpContext.Current.Response.AddHeader("Content-Disposition",
            //    String.Format("{0}; filename=HtmlToPdf.pdf; size={1}",
            //    checkBoxOpenInline.Checked ? "inline" : "attachment",
            //    pdfBuffer.Length.ToString()));

            //// write the PDF buffer to HTTP response 
            //HttpContext.Current.Response.BinaryWrite(pdfBuffer);

            //// call End() method of HTTP response to stop ASP.NET page processing 
            //HttpContext.Current.Response.End();
        }
    }
}
