using System;
using katio.Business.Interfaces;
using katio.Data.Models;
using katio.Business.Utilities;
namespace katio.Business.Services;

public class BookService : IBookService
{
    public async Task<IEnumerable<Books>> GetAllBooks()
    {
        var bookList = Utilities.Utilities.createABooksList();
        return bookList;      
    }

    public async Task<IEnumerable<Books>> GetById(int id)
    {
        
        if (id<=0)
        {
            return new List<Books>();
        }
        var list = Utilities.Utilities.createABooksList();
        // for (int i = 0; i< list.Count; i++)
        // {
        //     if (list[i].Id == id)
        //     {
        //         var listBooks = new List<Books>();
        //         listBooks.Add(list[i]);
        //         return listBooks;
        //     }
        // }

        var listBooks = list.Where(x => x.Id==id);
        return listBooks;
    }

    public async Task<IEnumerable<Books>> GetByName(string name)
    {
        
         var lista = Utilities.Utilities.createABooksList().Where(x => x.Title.Contains(name,StringComparison.InvariantCultureIgnoreCase));

        return lista;
    }

    public async Task<IEnumerable<Books>> Update(Books book)
    {
        var lista = Utilities.Utilities.createABooksList();
        
        var updatedBook = lista.Where(x => x.Title.Contains(book.Title)).FirstOrDefault();
        if(updatedBook!=null)
        {
            lista.RemoveAt(book.Id);
            lista.Add(book);

            return lista;
        }
        return lista;

        
    }
}