
using katio.Data.Models;
namespace katio.Business.Interfaces;
public interface IBookService
{
    Task<IEnumerable<Books>> GetAllBooks();

    Task<IEnumerable<Books>> GetById(int id);

    Task<IEnumerable<Books>> GetByName(string name);
    Task<IEnumerable<Books>> Update(Books book);
}