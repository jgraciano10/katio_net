using katio.Data.Dto;
using katio.Data.Models;

public interface IRoleService
{
    Task<BaseMessage<Role>> CreateRol(Role role);
    Task<BaseMessage<Role>> GetAllRoles();

    Task<BaseMessage<Role>> DeleteRolesById(int id);

}