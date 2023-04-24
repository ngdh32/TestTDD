using System;
using System.Collections.Generic;
using System.Text;

namespace TestTddInfra.Entities.EF
{
    public class EfBook : EfBaseEntity
    {
        public string Title { get; set; }
        public DateTime CreateDate { get; set; }
        public EfAuthor Author { get; set; }
    }
}
