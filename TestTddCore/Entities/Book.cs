using System;
using System.Collections.Generic;
using System.Text;

namespace TestTddCore.Entities
{
    public class Book : BaseEntity
    {
        public string Title { get; set; }
        public int AuthorId { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
