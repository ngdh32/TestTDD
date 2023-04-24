using System;
using System.Collections.Generic;
using System.Text;

namespace TestTddCore.Models.Queries
{
    public class GetBooksQueryResult
    {
        public List<GetBookQueryResult> Books { get; set; }
    }
}
