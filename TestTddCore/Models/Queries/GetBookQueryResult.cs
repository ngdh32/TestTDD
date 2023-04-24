using System;
using System.Collections.Generic;
using System.Text;

namespace TestTddCore.Models.Queries
{
    public class GetBookQueryResult
    {
        public int BookId { get; set; }    
        public string BookTitle { get; set; }
        public int AuthorId { get; set; }
        public string AuthorName { get; set; }
        public DateTime Created { get; set; }
    }

    
}
