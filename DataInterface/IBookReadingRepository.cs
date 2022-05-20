using bookapi.Test.Models;

namespace bookapi.Test.DataInterface
{
    public interface IBookReadingRepository
    {
        void Save(Book book);
        void SaveBulk(List<BookRequest> books);
        int GetBooksNumber();
        List<Book> GetReadBooks();
        void Remove(string title);
    }
}
