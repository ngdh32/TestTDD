using System;
using System.Collections.Generic;
using System.Text;
using TestTddCore.Models.Queries;

namespace TestTddCore.Interfaces
{
    public interface IBookQueryService
    {
        GetBookQueryResult GetBook(int id);

        GetBooksQueryResult GetBooks();
        GetAuthorQueryResult GetAuthor(int id);
    }
}
