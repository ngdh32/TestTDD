using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestTddCore.Interfaces;
using TestTddInfra;
using Xunit;

namespace TestTddUnitTest
{
    public class TestTddInfraUnitTest
    {
        private readonly IBookRepository _bookRepository;
        
        public TestTddInfraUnitTest()
        {
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void GetAuthor_ValidId_ReturnAuthor(int authorId)
        {
            var result = _bookRepository.GetAuthor(authorId);

            Assert.Equal(result.Id, authorId);
        }

        [Fact]
        public void GetAuthor_ValidId_ReturnNull()
        {
            int authorId = -999;
            var result = _bookRepository.GetAuthor(authorId);

            Assert.Null(result);
        }

        [Theory]
        [InlineData(1, 1)]
        [InlineData(2, 1)]
        public void GetBook_ValidId_ReturnBook(int bookId, int expectedAuthorId)
        {
            var result = _bookRepository.GetBook(bookId);

            Assert.Equal(result.Id, bookId);
            Assert.Equal(result.AuthorId, expectedAuthorId);
        }

        [Fact]
        public void GetBook_ValidId_ReturnNull()
        {
            int bookId = -999;
            var result = _bookRepository.GetBook(bookId);

            Assert.Null(result);
        }
    }
}
