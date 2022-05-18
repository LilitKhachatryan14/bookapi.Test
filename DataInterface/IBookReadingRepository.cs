using bookapi.Test.Models;

namespace bookapi.Test.DataInterface
{
    public interface IBookReadingRepository
    {
        void Save(BookRead book);
        void SaveBulk(List<BookRead> books);
        int GetBooksNumber();
        List<Book> GetReadBooks();
    }
}
