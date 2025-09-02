using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp_Project.Models.Base
{
    internal class BaseAuditable:BaseEntity
    {
        public bool IsDeleted { get; set; }
    }
}
