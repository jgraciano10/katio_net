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

public class NarratorService : INarratorService
{
    
    public IUnitOfWork _unitOfWork;

    public NarratorService (IUnitOfWork unitOfWork)
    {
        
        _unitOfWork = unitOfWork;
    }

    public async Task<BaseMessage<Narrator>> CreateNarrator(Narrator narrator)
    {
        try{
            
            await _unitOfWork.NarratorRepository.AddAsync(narrator);
            await _unitOfWork.SaveAsync();
            
        }
        catch(Exception ex)
        {
            return Utilities.Utilities.BuilResponse<Narrator>(HttpStatusCode.InternalServerError, $"{BaseMessageStatus.INTERNAL_SERVER_500} | {ex.Message}" );
        }
        return Utilities.Utilities.BuilResponse<Narrator>(HttpStatusCode.OK, BaseMessageStatus.OK_200, new List<Narrator>{narrator});
    }

    public async Task<BaseMessage<Narrator>> DeleteNarratorById(int id)
    {
        try
        {
            await _unitOfWork.NarratorRepository.Delete(id);
            await _unitOfWork.SaveAsync();
        }
        catch(Exception ex)
        {
            return Utilities.Utilities.BuilResponse<Narrator>(HttpStatusCode.InternalServerError, $"{BaseMessageStatus.INTERNAL_SERVER_500} | {ex.Message}" );
        }
        return Utilities.Utilities.BuilResponse<Narrator>(HttpStatusCode.OK, BaseMessageStatus.OK_200, new List<Narrator>());
    }

    public async Task<BaseMessage<Narrator>> GetAllNarrators()
    {
        var narrators = await _unitOfWork.NarratorRepository.GetAllAsync();

        if (narrators.Any()){
            return Utilities.Utilities.BuilResponse<Narrator>(HttpStatusCode.OK, BaseMessageStatus.OK_200, narrators);
        }
        return Utilities.Utilities.BuilResponse<Narrator>(HttpStatusCode.NotFound, BaseMessageStatus.BOOK_NOT_FOUND, narrators);

    }
}
