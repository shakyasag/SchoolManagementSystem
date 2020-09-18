using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagementSystem.Application.Common
{
    public interface IDateTimeServices
    {
        DateTime Now { get; }
        DateTime UtcNow { get; }
    }
}
