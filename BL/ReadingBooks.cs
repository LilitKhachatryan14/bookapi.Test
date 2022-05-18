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
        public ReadingBooks(IBookReadingRepository @object)
        {
            Object = @object;
        }

        public string DateRead { get; set; }
        public int Rating { get; set; } // btw 1 and 5
        public IBookReadingRepository Object { get; }

        public List<Book> GetBooks() { throw new NotImplementedException(); }
        public void AddBook(Book book, string dateRead, int rating) { }
        public void DeleteBook() { }
        public List<Book> GetBooksByRating(int rating) { throw new NotImplementedException(); }
        public int NumberRead() { throw new NotImplementedException(); }

        internal void AddBulk(List<Book> bookList)
        {
            throw new NotImplementedException();
        }

        //List<Book> Books = new List<Book>
        //    {
        //new Book {Title = "The Hobbit", Author = "J.R.R. Tolkein", Length = 320, Year = 1937 },
        //new Book {Title = "Alices Adventures in Wonderland", Author = "Lewis Carroll", Length = 544, Year = 1865 },
        //};

    }
}
