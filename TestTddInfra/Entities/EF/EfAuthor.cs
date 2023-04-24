using System;
using System.Collections.Generic;
using System.Text;

namespace TestTddInfra.Entities.EF
{
    public class EfAuthor : EfBaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<EfBook> Books { get; set; }
    }
}
