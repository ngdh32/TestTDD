using System;
using System.Collections.Generic;
using System.Text;

namespace TestTddCore.Entities
{
    public class Author : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
