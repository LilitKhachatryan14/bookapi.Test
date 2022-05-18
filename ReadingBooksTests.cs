
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

        private readonly Book _book;
        private readonly List<Book> _bookList;
        public ReadingBooksTests()
        {
            _availableBooks = new List<Book> { new Book() };
            _readingBooksRepositoryMock = new Mock<IBookReadingRepository>();
            _readingBooksRepositoryMock.Setup(x => x.GetBooksNumber())
                .Returns(_availableBooks.Count);
            _readingBooksRepositoryMock.Setup(x => x.GetReadBooks()).Returns(_availableBooks);

            _readingBooks = new ReadingBooks(_readingBooksRepositoryMock.Object);

            _book = new Book { Title = "The Hobbit", Author = "J.R.R. Tolkein", Length = 320, Year = 1937 };
            var book2 = new Book { Title = "Alices Adventures in Wonderland", Author = "Lewis Carroll", Length = 544, Year = 1865 }
            _bookList = new List<Book> { _book, book2 };
        }


        [Fact]
        public void ShouldThrowExceptionIfRequestIsNull()
        {
            // Act
            var exception = Assert.Throws<ArgumentNullException>(() => _readingBooks.AddBook(null, null, 0));

            // Assert
            Assert.Equal("request", exception.ParamName);
        }

        [Fact]
        public void ShouldAddBookIfRead()
        {
            BookRead savedBook = null;
            _readingBooksRepositoryMock.Setup(x => x.Save(It.IsAny<BookRead>()))
                .Callback<BookRead>(bookReading =>
                {
                    savedBook = bookReading;
                });

            _readingBooks.AddBook(_book, "", 3);

            _readingBooksRepositoryMock.Verify(x => x.Save(It.IsAny<BookRead>()), Times.Once);

            Assert.NotNull(savedBook);
            Assert.Equal(_book.Author, savedBook.Author);
            Assert.Equal(_book.Title, savedBook.Title);
            Assert.Equal(_book.Length, savedBook.Length);
            Assert.Equal(_book.Year, savedBook.Year);
        }

        [Fact]
        public void FirstVisitNumberOfBooks0()
        {
            _availableBooks.Clear();
            _readingBooksRepositoryMock.Verify(x => x.Save(It.IsAny<BookRead>()), Times.Never);

            Assert.Equal(0, _readingBooks.NumberRead());
        }

        [Fact]
        public void AddingFirstBookReturnsNumberOfBooks1()
        {
            _availableBooks.Clear();
            _readingBooks.AddBook(_book, "", 3);
            _readingBooksRepositoryMock.Verify(x => x.Save(It.IsAny<BookRead>()), Times.Once);

            Assert.Equal(1, _readingBooks.NumberRead());
        }

        [Fact]
        public void AddingMoreBooksReturnsNumberOfBooks()
        {
            List<BookRead> ReadBooks = null;
            _readingBooksRepositoryMock.Setup(x => x.SaveBulk(It.IsAny<List<BookRead>>()))
                .Callback<List<BookRead>>(bookReading =>
                {
                    ReadBooks = bookReading;
                });

            _readingBooks.AddBulk(_bookList);

            Assert.NotNull(ReadBooks);
            Assert.Equal(ReadBooks.Count, _readingBooks.NumberRead());
        }
    }
}