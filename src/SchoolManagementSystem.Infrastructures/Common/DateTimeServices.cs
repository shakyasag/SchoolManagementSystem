using SchoolManagementSystem.Application.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagementSystem.Infrastructures.Common
{
     public class DateTimeServices: IDateTimeServices
    {
        public DateTime Now => DateTime.Now;

        public DateTime UtcNow => DateTime.Now;

      
    }
}
