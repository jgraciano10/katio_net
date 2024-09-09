using System;
using katio.Business.Interfaces;
using katio.Data.Models;
using katio.Business.Utilities;
using katio_net.Data;
using katio.Data.Dto;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore;
namespace katio.Business.Services;

public class BookService : IBookService
{
    public readonly katioContext _context;

    public BookService (katioContext context)
    {
        _context = context;
    }
    public async Task<BaseMessage<Book>> CreateBook(Book book)
    {
        var newBook = new Book()
        {
            Title = book.Title,
            ISBN10 = book.ISBN10,
            ISBN13 = book.ISBN13,
            Published = book.Published,
            Edition = book.Edition,
            DeweyIndex = book.DeweyIndex,
            AuthorId = book.AuthorId
        };

        try{
            await _context.Books.AddAsync(newBook);
            await _context.SaveChangesAsync();
        }
        catch(Exception ex)
        {
            return Utilities.Utilities.BuilResponse<Book>(HttpStatusCode.InternalServerError, $"{BaseMessageStatus.INTERNAL_SERVER_500} | {ex.Message}" );
        }
        return Utilities.Utilities.BuilResponse<Book>(HttpStatusCode.OK, BaseMessageStatus.OK_200, new List<Book>{newBook});
    }

    public async Task<BaseMessage<Book>> GetAllBooks()
    {
        
        var response = _context.Books.ToList();

        return response.Any()? Utilities.Utilities.BuilResponse<Book>(HttpStatusCode.OK, BaseMessageStatus.OK_200, response): Utilities.Utilities.BuilResponse(HttpStatusCode.NotFound, BaseMessageStatus.BOOK_NOT_FOUND, new List<Book>());

    }

    public async Task<BaseMessage<Book>> GetByAuthorId(int AuthorId)
    {
        var BookList = await _context.Books.Where(x => x.AuthorId == AuthorId).ToListAsync();
        return BookList.Any()? Utilities.Utilities.BuilResponse<Book>(HttpStatusCode.OK,BaseMessageStatus.OK_200,BookList): 
        Utilities.Utilities.BuilResponse(HttpStatusCode.NotFound,BaseMessageStatus.BOOK_NOT_FOUND, new List<Book>());
    }

    public async Task<BaseMessage<Book>> GetByAuthorName(string AuthorName)
    {
        var BookList = await _context.Books.Where(x => x.Author.Name.Contains(AuthorName, StringComparison.InvariantCultureIgnoreCase)).ToListAsync();
        return BookList.Any()? Utilities.Utilities.BuilResponse<Book>(HttpStatusCode.OK,BaseMessageStatus.OK_200,BookList): 
        Utilities.Utilities.BuilResponse(HttpStatusCode.NotFound,BaseMessageStatus.BOOK_NOT_FOUND, new List<Book>());
    }

    public async Task<IEnumerable<Book>> GetById(int id)
    {
        
        if (id<=0)
        {
            return new List<Book>();
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

    public async Task<BaseMessage<Book>> GetByName(string name)
    {
        var elemento =_context.Books.Where(x => x.Title.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();
        return elemento.Any()? Utilities.Utilities.BuilResponse<Book>(HttpStatusCode.OK,BaseMessageStatus.OK_200,elemento): Utilities.Utilities.BuilResponse(HttpStatusCode.NotFound,BaseMessageStatus.BOOK_NOT_FOUND, new List<Book>());
    }

    public async Task<BaseMessage<Book>> Update(Book book)
    {
        var newBook = new Book()
        {
            Title = book.Title,
            ISBN10 = book.ISBN10,
            ISBN13 = book.ISBN13,
            Published = book.Published,
            Edition = book.Edition,
            DeweyIndex = book.DeweyIndex
        };
        var elemento =_context.Books.Where(x => x.Title.Contains(newBook.Title, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();

        if(elemento!=null)
        {
            elemento.Title = newBook.Title;
            elemento.Edition = newBook.Edition;
            elemento.ISBN10 = newBook.ISBN10;
            elemento.ISBN13 = newBook.ISBN13;
            elemento.Published = newBook.Published;
            elemento.DeweyIndex= newBook.DeweyIndex;

            try {
                await _context.SaveChangesAsync();

                return Utilities.Utilities.BuilResponse<Book>(HttpStatusCode.OK,BaseMessageStatus.OK_200, new List<Book>(){
                    new Book()
        {
            Title = book.Title,
            ISBN10 = book.ISBN10,
            ISBN13 = book.ISBN13,
            Published = book.Published,
            Edition = book.Edition,
            DeweyIndex = book.DeweyIndex
        }
                });
            }
            catch(Exception ex)
            {
               return Utilities.Utilities.BuilResponse(HttpStatusCode.NotFound,BaseMessageStatus.BOOK_NOT_FOUND, new List<Book>());

            }
        }
        return Utilities.Utilities.BuilResponse(HttpStatusCode.NotFound,BaseMessageStatus.BOOK_NOT_FOUND, new List<Book>());
       
    }
}