using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagementSystem.Application.Common
{
    public interface  IUserAccessor
    {
        Guid UserId { get; }
        string UserName { get; }
    }
}
