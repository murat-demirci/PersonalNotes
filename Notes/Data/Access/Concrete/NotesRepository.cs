using Core.DataModels;
using Core.Models;
using Dapper;
using Data.Access.Abstract;
using Data.Context;

namespace Data.Access.Concrete
{
    public class NotesRepository : INotesRepository
    {
        public async Task<NoteChildModel> AddChildNoteAsync(NoteChildModel noteChildModel)
        {
            using (var con = DapperContext.Singleton.CreateConnection())
            {
                await con.ExecuteAsync("insert into NotesDB(title,content,baseId,noteId) values(@title,@content,@baseId,@noteId)", noteChildModel);
                return (await con.QueryFirstAsync<NoteChildModel>("SELECT TOP 1 *  FROM NotesDB ORDER BY id DESC"));
            }
        }

        public async Task<NoteMainModel> AddMainNoteAsync(NoteMainModel noteMainModel)
        {
            using (var con = DapperContext.Singleton.CreateConnection())
            {
                await con.ExecuteAsync("insert into NotesBaseDB(title,content) values(@title,@content)", noteMainModel);
                return (await con.QueryFirstAsync<NoteMainModel>("SELECT TOP 1 *  FROM NotesBaseDB ORDER BY id DESC"));
            }
        }

        public async Task<bool> DeleteChildNoteAsync(int id, int[]? ids)
        {
            using (var con = DapperContext.Singleton.CreateConnection())
            {
                if (ids.Any())
                {
                    foreach (var i in ids)
                    {
                        int response = (await con.ExecuteAsync("delete NotesDB where id =" + i));
                        if (response <= 0)
                        {
                            return false;
                        }
                    }
                    return true;
                }
                return (await con.ExecuteAsync("delete NotesDB where id =" + id) == 1 ? true : false);
            }
        }

        public async Task<bool> DeleteMainNoteAsync(int id, int[]? ids)
        {
            using (var con = DapperContext.Singleton.CreateConnection())
            {
                int response = await con.ExecuteAsync("delete NotesBaseDB where id = " + ids[0]);
                if (response <= 0) return false;
                if (ids.Length > 1) ids = ids.Skip(1).ToArray();
                if (ids.Any())
                {
                    foreach (var i in ids)
                    {
                        int result = (await con.ExecuteAsync("delete NotesDB where id =" + i));
                        if (result <= 0)
                        {
                            return false;
                        }
                    }
                }
                return true;
            }
        }

        public async Task<IList<NoteDataModel>> GetAllNotesAsync()
        {
            using (var con = DapperContext.Singleton.CreateConnection())
            {
                var notes = (await con.QueryAsync<NoteDataModel>("select * from NotesBaseDB")).ToList();
                if (notes.Any())
                {
                    foreach (var note in notes)
                    {
                        note.notesDal = (await con.QueryAsync<NoteModel>("select * from NotesDB where baseId=" + note.id)).ToList();
                    }
                    return notes;
                }
                return notes;
            }
        }

        public Task<NoteMainModel> GetLastAddedAsync()
        {
            throw new NotImplementedException();
        }
    }
}
