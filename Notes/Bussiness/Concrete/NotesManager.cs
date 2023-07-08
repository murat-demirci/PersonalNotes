using Bussiness.Abstract;
using Core.DataModels;
using Core.Models;
using Data.Access.Abstract;

namespace Bussiness.Concrete
{
    public class NotesManager : INotesService
    {
        private readonly INotesRepository _notesRepository;

        public NotesManager(INotesRepository notesRepository)
        {
            _notesRepository = notesRepository;
        }

        public async Task<NoteChildModel> AddChildAsync(NoteChildModel noteChildModel)
        {
            if (noteChildModel == null) return null;
            return await _notesRepository.AddChildNoteAsync(noteChildModel);
        }

        public async Task<NoteMainModel> AddMainAsync(NoteMainModel noteMainModel)
        {
            if (noteMainModel == null) return null;
            return await _notesRepository.AddMainNoteAsync(noteMainModel);
        }

        public async Task<bool> DeleteAsync(int id, int[]? ids)
        {
            return (await _notesRepository.DeleteMainNoteAsync(id, ids));
        }

        public async Task<bool> DeleteChildAsync(int id, int[]? ids)
        {
            return (await _notesRepository.DeleteChildNoteAsync(id, ids));
        }

        public async Task<IList<NoteDataModel>> GetAllAsync()
        {
            var notes = await _notesRepository.GetAllNotesAsync();
            if (notes.Count <= 0) return null;
            return notes;
        }
    }
}
