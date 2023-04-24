using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestTddCore.Entities;
using TestTddCore.Interfaces;

namespace TestTddInfra
{
    public class EfBookRepository : IBookRepository
    {
        private readonly BookDbContext _bookDbContext;
        private readonly IMapper _mapper;

        public EfBookRepository(BookDbContext bookDbContext, IMapper mapper)
        {
            _bookDbContext = bookDbContext;
            _mapper = mapper;
        }

        public Author GetAuthor(int id)
        {
            var author = _bookDbContext.Authors.FirstOrDefault(x => x.Id == id);
            if (author == null)
            {
                return null;
            }

            return new Author()
            {
                Id = author.Id,
                FirstName = author.FirstName,
                LastName = author.LastName
            };
        }

        public Book GetBook(int id)
        {
            var book = _bookDbContext.Books.Include(x => x.Author).FirstOrDefault(x => x.Id == id);
            if (book == null)
            {
                return null;
            }

            return new Book()
            {
                Id = book.Id,
                Title = book.Title,
                CreateDate = book.CreateDate,
                AuthorId = book.Author.Id
            };
        }

        public List<Book> GetBooks()
        {
            var books = new List<Book>();
            foreach(var book in _bookDbContext.Books.Include(x => x.Author))
            {
                books.Add(_mapper.Map<Book>(book));
            }

            return books;
        }
    }
}
