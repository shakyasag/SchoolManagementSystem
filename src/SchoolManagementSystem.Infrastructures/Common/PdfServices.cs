using DinkToPdf;
using DinkToPdf.Contracts;
using SchoolManagementSystem.Application.Common.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SchoolManagementSystem.Infrastructures.Common
{
    public class PdfServices : IPdfServices

    {
        private readonly IConverter _converter;
        public PdfServices(IConverter converter)
        {
            _converter = converter;
        }

        public byte[] CreatePdf(string HtmlContent)
        {
            var globalSettings = new GlobalSettings
            {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Portrait,
                PaperSize = PaperKind.A4,
                Margins = new MarginSettings { Top = 10 },
                DocumentTitle = "PDF Report",
                //  Out = $@"{Path.Combine(Directory.GetCurrentDirectory(), "reports", "instanceId")}/Employee_Report.{Guid.NewGuid()}.pdf"
            };

            var webRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            var Destination_DirectoryPath = Path.Combine(webRootPath, "Assets");

            var objectSettings = new ObjectSettings
            {
                PagesCount = true,
                HtmlContent = HtmlContent,
                WebSettings = { DefaultEncoding = "utf-8", UserStyleSheet = Path.Combine(Destination_DirectoryPath, "PDFstyle.css") },
                HeaderSettings = { FontName = "Arial", FontSize = 9, Right = "Page [page] of [toPage]", Line = true },
                FooterSettings = { FontName = "Arial", FontSize = 9, Line = true, Center = " " }
            };

            var pdf = new HtmlToPdfDocument()
            {
                GlobalSettings = globalSettings,
                Objects = { objectSettings }
            };

            //_converter.Convert(pdf); // for download
            //for pdf view in browser
            var file = _converter.Convert(pdf);

            return file;

        }
    }
}
