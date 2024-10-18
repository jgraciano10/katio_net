using katio.Data.Dto;
using katio.Data.Models;

public interface INarratorService
{
    Task<BaseMessage<Narrator>> CreateNarrator(Narrator narrator);
    Task<BaseMessage<Narrator>> GetAllNarrators();

    Task<BaseMessage<Narrator>> DeleteNarratorById(int id);

}