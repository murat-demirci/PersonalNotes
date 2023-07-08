using Core.DataModels;
using Core.Models;

namespace Data.Access.Abstract
{
    public interface INotesRepository
    {
        Task<NoteMainModel> GetLastAddedAsync();
        Task<IList<NoteDataModel>> GetAllNotesAsync();
        Task<NoteMainModel> AddMainNoteAsync(NoteMainModel noteMainModel);
        Task<NoteChildModel> AddChildNoteAsync(NoteChildModel noteChildModel);
        Task<bool> DeleteMainNoteAsync(int id, int[]? ids);
        Task<bool> DeleteChildNoteAsync(int id, int[]? ids);
    }
}
