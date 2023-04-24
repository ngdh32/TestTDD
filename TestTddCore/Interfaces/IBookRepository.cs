using System;
using System.Collections.Generic;
using System.Text;
using TestTddCore.Entities;

namespace TestTddCore.Interfaces
{
    public interface IBookRepository
    {
        Book GetBook(int id);
        List<Book> GetBooks();
        Author GetAuthor(int id);
    }
}
