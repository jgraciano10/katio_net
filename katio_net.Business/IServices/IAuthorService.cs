
using katio.Data.Dto;
using katio.Data.Models;
namespace katio.Business.Interfaces;
public interface IAuthorService
{
        Task<BaseMessage<Author>> CreateAuthor(Author author);
         Task<BaseMessage<Author>> GetAllAuthors();
}