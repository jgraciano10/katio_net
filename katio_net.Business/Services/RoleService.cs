using System;
using katio.Business.Interfaces;
using katio.Data.Models;
using katio.Business.Utilities;
using katio_net.Data;
using katio.Data.Dto;
using System.Net;
using Microsoft.EntityFrameworkCore;
using Katio.Data;

namespace katio.Business.Services;

public class RoleService : IRoleService
{
    
    public IUnitOfWork _unitOfWork;

    public RoleService (IUnitOfWork unitOfWork)
    {
        
        _unitOfWork = unitOfWork;
    }

    public async Task<BaseMessage<Role>> CreateRol(Role role)
    {
        try{
            
            await _unitOfWork.RoleRepository.AddAsync(role);
            await _unitOfWork.SaveAsync();
            
        }
        catch(Exception ex)
        {
            return Utilities.Utilities.BuilResponse<Role>(HttpStatusCode.InternalServerError, $"{BaseMessageStatus.INTERNAL_SERVER_500} | {ex.Message}" );
        }
        return Utilities.Utilities.BuilResponse<Role>(HttpStatusCode.OK, BaseMessageStatus.OK_200, new List<Role>(){role});
    }

    public async Task<BaseMessage<Role>> DeleteRolesById(int id)
    {
        try
        {
            await _unitOfWork.RoleRepository.Delete(id);
            await _unitOfWork.SaveAsync();
        }
        catch(Exception ex)
        {
            return Utilities.Utilities.BuilResponse<Role>(HttpStatusCode.InternalServerError, $"{BaseMessageStatus.INTERNAL_SERVER_500} | {ex.Message}" );
        }
        return Utilities.Utilities.BuilResponse<Role>(HttpStatusCode.OK, BaseMessageStatus.OK_200, new List<Role>());
    }

    public async Task<BaseMessage<Role>> GetAllRoles()
    {
        var roles = await _unitOfWork.RoleRepository.GetAllAsync();

        if (roles.Any()){
            return Utilities.Utilities.BuilResponse<Role>(HttpStatusCode.OK, BaseMessageStatus.OK_200, roles);
        }
        return Utilities.Utilities.BuilResponse<Role>(HttpStatusCode.NotFound, BaseMessageStatus.BOOK_NOT_FOUND, roles);
    }
}    