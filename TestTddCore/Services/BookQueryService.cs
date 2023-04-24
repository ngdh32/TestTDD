using System;
using System.Collections.Generic;
using System.Text;
using TestTddCore.Interfaces;
using TestTddCore.Models.Queries;

namespace TestTddCore.Services
{
    public class BookQueryService : IBookQueryService
    {
        private readonly IBookRepository _bookRepository;
        public BookQueryService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public GetAuthorQueryResult GetAuthor(int id)
        {
            var author = _bookRepository.GetAuthor(id);
            if (author is null)
            {
                return null;
            }

            return new GetAuthorQueryResult()
            {
                Id = author.Id,
                FirstName = author.FirstName,
                LastName = author.LastName
            };
        }

        public GetBookQueryResult GetBook(int id)
        {
            // add some validation rules here...
            var book = _bookRepository.GetBook(id);
            if (book == null)
            {
                return null;
            }

            var author = _bookRepository.GetAuthor(book.AuthorId);
            if (author == null)
            {
                return null;
            }

            return new GetBookQueryResult()
            {
                BookId = book.Id,
                BookTitle = book.Title,
                AuthorId = author.Id,
                AuthorName = $"{author.FirstName} {author.LastName}",
                Created = book.CreateDate
            };
        }

        public GetBooksQueryResult GetBooks()
        {
            var result = new GetBooksQueryResult()
            {
                Books = new List<GetBookQueryResult>()
            };
            var books = _bookRepository.GetBooks();
            foreach (var book in books)
            {
                var author = _bookRepository.GetAuthor(book.AuthorId);
                if (author == null)
                {
                    return null;
                }

                result.Books.Add(new GetBookQueryResult()
                {
                    BookId = book.Id,
                    BookTitle = book.Title,
                    AuthorId = author.Id,
                    AuthorName = $"{author.FirstName} {author.LastName}",
                    Created = book.CreateDate
                });
            }

            return result;
        }
    }
}
