using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagementSystem.Application.Common.Interface
{
    public interface IPdfServices
    {
        byte[] CreatePdf(string HtmlContent);

    }
}
