using System.Net;
using katio.Data.Dto;
using katio.Data.Models;
using katio_net.Data;

namespace katio.Business.Services;

public class UserService : IUserService
{
    public readonly katioContext _context;
    public PasswordHasher _passwordHasher;
    public readonly Repository<int, User> _repository;

    public UserService (katioContext context)
    {
        _context = context;
        _repository = GetRepository(_context);
        _passwordHasher = new PasswordHasher();
    }
     public Repository<int, User> GetRepository(katioContext context){
        return new Repository<int, User>(context);
    }
    public async Task<BaseMessage<User>> CreateUser(User user)
    {
        if(user.Passhash.Length>10)
        {
            user.Passhash = _passwordHasher.hash(user.Passhash);
            try
            {
               await _repository.AddAsync(user);
               await _context.SaveChangesAsync();

            }catch(Exception ex)
            {
                return Utilities.Utilities.BuilResponse<User>(HttpStatusCode.InternalServerError, $"{BaseMessageStatus.INTERNAL_SERVER_500} | {ex.Message}" );
            }
            return Utilities.Utilities.BuilResponse<User>(HttpStatusCode.OK, BaseMessageStatus.OK_200, new List<User>{user});
        }
        return Utilities.Utilities.BuilResponse<User>(HttpStatusCode.InternalServerError, $"{BaseMessageStatus.INTERNAL_SERVER_500} | Password debe tener minimo 10 caracteres" );
    }

    public async Task<BaseMessage<User>> GetAllUsers()
    {
        
            var listUsers = await _repository.GetAllAsync();
            return listUsers.Any()?Utilities.Utilities.BuilResponse<User>(HttpStatusCode.OK, BaseMessageStatus.OK_200, listUsers ):Utilities.Utilities.BuilResponse<User>(HttpStatusCode.InternalServerError, $"{BaseMessageStatus.INTERNAL_SERVER_500} | No se puso obtener" );
            
    }
    public async Task<BaseMessage<User>> Login(string email, string inputPassword)
    {
        var userList = _context.User.Where(x => x.Email.Contains(email, StringComparison.InvariantCultureIgnoreCase)).ToList();
        
        if (!userList.Any())
        {
            return Utilities.Utilities.BuilResponse<User>(HttpStatusCode.NotFound, BaseMessageStatus.NOT_FOUND_404, new List<User>());
        }
    
        var user = userList[0];
        var validator = _passwordHasher.verify(user.Passhash, inputPassword);
    
        if (validator)
        {
            return Utilities.Utilities.BuilResponse<User>(HttpStatusCode.OK, BaseMessageStatus.OK_200, userList);
        }
        else
        {
            return Utilities.Utilities.BuilResponse<User>(HttpStatusCode.NotFound, BaseMessageStatus.NOT_FOUND_404, new List<User>());
        }
    }



}