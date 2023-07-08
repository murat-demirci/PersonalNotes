using Core.DataModels;
using Core.Models;

namespace Bussiness.Abstract
{
    public interface INotesService
    {
        Task<IList<NoteDataModel>> GetAllAsync();
        Task<NoteMainModel> AddMainAsync(NoteMainModel noteMainModel);
        Task<NoteChildModel> AddChildAsync(NoteChildModel noteChildModel);
        Task<bool> DeleteAsync(int id, int[]? ids);
        Task<bool> DeleteChildAsync(int id, int[]? ids);
    }
}
