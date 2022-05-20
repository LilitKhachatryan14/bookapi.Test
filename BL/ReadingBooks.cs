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

        public List<Book> GetBooks() {
            return _bookReadingRepository.GetReadBooks().ToList();
        }
        public void AddBook(BookRequest book, string dateRead, int rating)
        {
            if (book == null)
                throw new ArgumentNullException(nameof(book));

            _bookReadingRepository.Save(CreateBook<Book>(book));
        }

        public void DeleteBook(string title) 
        {
            _bookReadingRepository.Remove(title);
        }
        public List<Book> GetBooksByRating(int rating) { throw new NotImplementedException(); }
        public int NumberRead()
        { 
            return _bookReadingRepository.GetBooksNumber(); 
        }

        internal void AddBulk(List<BookRequest> bookList)
        {
            List<Book> newBookList = new List<Book>();
            foreach (BookRequest book in bookList)
            {
                newBookList.Add(CreateBook<Book>(book));
            }

            _bookReadingRepository.SaveBulk(newBookList);
        }

        private T CreateBook<T>(BookRequest book) where T : BookBase, new()
        {
            return new T
            {
                Title = book.Title,
                Author = book.Author,
                Length = book.Length,
                Year = book.Year
            };
        }

    }
}
