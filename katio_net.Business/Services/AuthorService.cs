using System;
using katio.Business.Interfaces;
using katio.Data.Models;
using katio.Business.Utilities;
using katio_net.Data;
using katio.Data.Dto;
using System.Net;
using Microsoft.EntityFrameworkCore;

namespace katio.Business.Services;

public class AuthorService : IAuthorService
{
    public readonly katioContext _context;

    public AuthorService (katioContext context)
    {
        _context = context;
    }
    public async Task<BaseMessage<Author>> CreateAuthor(Author author)
    {
        var newAuthor = new Author
        {
           Name = author.Name,
           LastName = author.LastName,
           Country = author.Country,
           BirthDate = author.BirthDate
        };

        try{
            await _context.Author.AddAsync(newAuthor);
            await _context.SaveChangesAsync();
        }
        catch(Exception ex)
        {
            return Utilities.Utilities.BuilResponse<Author>(HttpStatusCode.InternalServerError, $"{BaseMessageStatus.INTERNAL_SERVER_500} | {ex.Message}" );
        }
        return Utilities.Utilities.BuilResponse<Author>(HttpStatusCode.OK, BaseMessageStatus.OK_200, new List<Author>{newAuthor});
    }

    public async Task<BaseMessage<Author>> GetAllAuthors()
    {
        var response = await  _context.Author.ToListAsync();

        return response.Any()? Utilities.Utilities.BuilResponse<Author>(HttpStatusCode.OK, BaseMessageStatus.OK_200, response): Utilities.Utilities.BuilResponse(HttpStatusCode.NotFound, BaseMessageStatus.BOOK_NOT_FOUND, new List<Author>());
    }
}