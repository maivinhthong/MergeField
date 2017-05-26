using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using iTextSharp.text.pdf;

namespace MergeFieldPDF.Controllers
{
    public class PdfService
    {
        public static byte[] MergeFiled(String pdfPath)
        {
            var reader = new PdfReader(pdfPath);
            var outStream = new MemoryStream();
            var stamper = new PdfStamper(reader, outStream);

            var form = stamper.AcroFields;
            form.GenerateAppearances = true; //Added this line, fixed my problem
            var fieldKeys = form.Fields.Keys;

            int couter = 0;
            foreach (var key in fieldKeys)
            {
                form.SetField(key, (couter++).ToString());
            }

            //foreach (KeyValuePair<String, String> pair in formKeys)
            //{
            //    if (fieldKeys.Any(f => f == pair.Key))
            //    {
            //        form.SetField(pair.Key, pair.Value);
            //    }
            //}
            stamper.Close();
            reader.Close();

            return FlattenPdf(outStream.ToArray());
        }

        private static byte[] FlattenPdf(byte[] pdf)
        {
            var reader = new PdfReader(pdf);

            var outStream = new MemoryStream();
            var stamper = new PdfStamper(reader, outStream);

            stamper.FormFlattening = true;

            stamper.Close();
            reader.Close();

            return outStream.ToArray();
        }
    }
}