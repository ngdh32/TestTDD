using TestTddCore.Entities;
using TestTddCore.Interfaces;
using TestTddCore.Services;
using Xunit;
using Moq;
using Bogus;
using System.Linq;
using System.Collections.Generic;

namespace TestTddUnitTest
{
    public class TestTddCoreUnitTest
    {
        private readonly IBookQueryService _bookQueryService;
        private readonly Mock<IBookRepository> _bookRepository = new Mock<IBookRepository>();

        private static List<Author> _testAuthors;
        private static List<Book> _testBooks;

        public TestTddCoreUnitTest()
        {
            _bookQueryService = new BookQueryService(_bookRepository.Object);

            var bookIds = 1;
            var authorIds = 1;
            _testAuthors = new Faker<Author>()
                .StrictMode(true)
                .RuleFor(o => o.Id, f => authorIds++)
                .RuleFor(o => o.FirstName, f => f.Name.FirstName())
                .RuleFor(o => o.LastName, f => f.Name.LastName())
                .GenerateBetween(1, 10);

            _testBooks = new Faker<Book>()
                .StrictMode(true)
                .RuleFor(o => o.Id, f => bookIds++)
                .RuleFor(o => o.Title, (f, u) => f.Random.String())
                .RuleFor(o => o.CreateDate, f => f.Date.Past())
                .RuleFor(o => o.AuthorId, f => f.PickRandom(_testAuthors.Select(x => x.Id)))
                .GenerateBetween(1, 100);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(3)]
        [InlineData(5)]
        public void GetBook_ValidId_ReturnBook(int id)
        {
            var expectedBook = _testBooks.First(x => x.Id == id);
            var expectedAuthor = _testAuthors.First(x => x.Id == expectedBook.AuthorId);
            _bookRepository.Setup(x => x.GetBook(id)).Returns(expectedBook);
            _bookRepository.Setup(x => x.GetAuthor(expectedBook.AuthorId)).Returns(_testAuthors.First(x => x.Id == expectedBook.AuthorId));

            var result = _bookQueryService.GetBook(id);

            Assert.NotNull(result);
            Assert.Equal(result.BookId, expectedBook.Id);
            Assert.Equal(result.AuthorName, expectedAuthor.FirstName + " " + expectedAuthor.LastName);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-11)]
        [InlineData(1001)]
        public void GetBook_ValidId_ReturnNull(int id)
        {
            var bookId = -1;
            _bookRepository.Setup(x => x.GetBook(bookId)).Returns<Book>(null);
            var result = _bookQueryService.GetBook(bookId);

            Assert.Null(result);
        }

        [Fact]
        public void GetBooks_ReturnList()
        {
            _bookRepository.Setup(x => x.GetBooks()).Returns(_testBooks);
            _bookRepository.Setup(x => x.GetAuthor(It.IsAny<int>())).Returns((int id) =>
            {
                return _testAuthors.FirstOrDefault(y => y.Id == id);
            });


            var result = _bookQueryService.GetBooks();

            Assert.NotNull(result);
            Assert.Equal(result.Books.Count, _testBooks.Count);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void GetAuthor_ValidId_ReturnAuthor(int authorId)
        {
            var expectedAuthor = _testAuthors.First(x => x.Id == authorId);
            _bookRepository.Setup(x => x.GetAuthor(authorId)).Returns(expectedAuthor);

            var result = _bookQueryService.GetAuthor(authorId);

            Assert.NotNull(result);
            Assert.Equal(result.Id, expectedAuthor.Id);
            Assert.Equal(result.FirstName, expectedAuthor.FirstName);
            Assert.Equal(result.LastName, expectedAuthor.LastName);
        }
    }
}