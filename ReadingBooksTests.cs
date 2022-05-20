
using bookapi.Test.BL;
using bookapi.Test.DataInterface;
using bookapi.Test.Models;
using Moq;
using Xunit;

namespace bookapi.Test
{
    public class ReadingBooksTests
    {
        private readonly List<Book> _availableBooks;
        private readonly Mock<IBookReadingRepository> _readingBooksRepositoryMock;
        private readonly ReadingBooks _readingBooks;

        private readonly BookRequest _book;
        private readonly List<BookRequest> _bookList;
        public ReadingBooksTests()
        {
            _availableBooks = new List<Book> { };
            _readingBooksRepositoryMock = new Mock<IBookReadingRepository>();
            _readingBooksRepositoryMock.Setup(x => x.GetBooksNumber())
                .Returns(_availableBooks.Count);
            _readingBooksRepositoryMock.Setup(x => x.GetReadBooks()).Returns(_availableBooks);

            _readingBooks = new ReadingBooks(_readingBooksRepositoryMock.Object);

            _book = new BookRequest { Title = "The Hobbit", Author = "J.R.R. Tolkein", Length = 320, Year = 1937 };
            var book2 = new BookRequest { Title = "Alices Adventures in Wonderland", Author = "Lewis Carroll", Length = 544, Year = 1865 };
            _bookList = new List<BookRequest> { 
                new BookRequest
                {
                    Title = "The Hobbit",
                    Author = "J.R.R. Tolkien",
                    Length = 320,
                    Year = 1937
                },
                new BookRequest
                {
                    Title = "The Catcher in the Rye",
                    Author = "J. D. Salinger",
                    Length = 115,
                    Year = 1951

                 }  };
        }


        [Fact]
        public void ShouldThrowExceptionIfRequestIsNull()
        {
            // Act
            var exception = Assert.Throws<ArgumentNullException>(() => _readingBooks.AddBook(null, null, 0));

            // Assert
            Assert.Equal("book", exception.ParamName);
        }

        [Fact]
        public void ShouldAddBookIfRead()
        {
            Book savedBook = null;
            _readingBooksRepositoryMock.Setup(x => x.Save(It.IsAny<Book>()))
                .Callback<Book>(bookReading =>
                {
                    savedBook = bookReading;
                });

            _readingBooks.AddBook(_book, "", 3);

            _readingBooksRepositoryMock.Verify(x => x.Save(It.IsAny<Book>()), Times.Once);

            Assert.NotNull(savedBook);
            Assert.Equal(_book.Author, savedBook.Author);
            Assert.Equal(_book.Title, savedBook.Title);
            Assert.Equal(_book.Length, savedBook.Length);
            Assert.Equal(_book.Year, savedBook.Year);
        }

        [Fact]
        public void FirstVisitNumberOfBooks0()
        {
            _readingBooksRepositoryMock.VerifyNoOtherCalls();
            Assert.Equal(0, _readingBooks.NumberRead());
        }

        [Fact]
        public void AddingFirstBookReturnsNumberOfBooks1()
        {
            List<Book> savedBook = null;
            _readingBooksRepositoryMock.Setup(x => x.Save(It.IsAny<Book>()))
                .Callback<Book>(bookReading =>
                {
                    savedBook  = new List<Book> { bookReading };
                });

            _readingBooks.AddBook(_book, "", 3);
            var c = _readingBooks.GetBooks();
            _readingBooksRepositoryMock.Verify(x => x.Save(It.IsAny<Book>()), Times.Once);

            Assert.NotNull(savedBook);
            Assert.Single(savedBook);
        }

        [Fact]
        public void AddingMoreBooksReturnsNumberOfBooks()
        {
            List<Book> savedBook = null;
            _readingBooksRepositoryMock.Setup(x => x.SaveBulk(It.IsAny<List<Book>>()))
                .Callback<List<Book>>(bookReading =>
                {
                    savedBook = bookReading;
                });

            _readingBooks.AddBulk(_bookList);
            _readingBooksRepositoryMock.Verify(x => x.SaveBulk(It.IsAny<List<Book>>()), Times.Once);

            Assert.NotNull(savedBook);
            Assert.Equal(_bookList.Count, savedBook.Count);

        }
    }
}