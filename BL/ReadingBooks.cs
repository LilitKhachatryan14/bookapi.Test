using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using bookapi.Test.DataInterface;
using bookapi.Test.Models;

namespace bookapi.Test.BL
{
    public class ReadingBooks
    {
        private readonly IBookReadingRepository _bookReadingRepository;

        public ReadingBooks(IBookReadingRepository bookReadingRepository)
        {
            _bookReadingRepository = bookReadingRepository;
        }

        public string DateRead { get; set; }
        public int Rating { get; set; } // btw 1 and 5

        public List<Book> GetBooks() { throw new NotImplementedException(); }
        public void AddBook(Book book, string dateRead, int rating)
        {
            if (book == null)
                throw new ArgumentNullException(nameof(book));

            _bookReadingRepository.Save(new BookRead
            {
                Title = "The Hobbit",
                Author = "J.R.R. Tolkein",
                Length = 320,
                Year = 1937
            });
        }
        public void DeleteBook() { }
        public List<Book> GetBooksByRating(int rating) { throw new NotImplementedException(); }
        public int NumberRead()
        { 
            return _bookReadingRepository.GetBooksNumber(); 
        }

        internal void AddBulk(List<Book> bookList)
        {
            _bookReadingRepository.SaveBulk(new List<BookRead>{
                new BookRead
            {
                Title = "The Hobbit",
                Author = "J.R.R. Tolkein",
                Length = 320,
                Year = 1937
            },
             new BookRead
             {
                Title = "The Catcher in the Rye",
                Author = "J. D. Salinger",
                Length = 115,
                Year = 1951

             } });
        }
    }
}
