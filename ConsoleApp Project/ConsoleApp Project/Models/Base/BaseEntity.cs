using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp_Project.Models
{
    internal abstract class BaseEntity
    {
        public Guid Id { get; set; }
        protected BaseEntity()
        {
            Id = Guid.NewGuid();
        }
    }
}
