using Bussiness.Abstract;
using Core.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NotesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocalNotesApi : ControllerBase
    {
        private readonly INotesService _notesService;

        public LocalNotesApi(INotesService notesService)
        {
            _notesService = notesService;
        }

        // GET: api/<LocalNotesApi>
        [HttpGet(Name = "GetAllNotes")]
        public async Task<ActionResult> Get()
        {
            var result = await _notesService.GetAllAsync();
            return Ok(result);
        }

        // POST api/<LocalNotesApi>
        [HttpPost]
        [Route("AddMainNote")]
        public async Task<ActionResult> Post([FromBody] NoteMainModel noteMainModel)
        {
            return Ok(await _notesService.AddMainAsync(noteMainModel));
        }

        [HttpPost]
        [Route("AddChildNote")]
        public async Task<ActionResult> Post([FromBody] NoteChildModel noteChildModel)
        {
            return Ok(await _notesService.AddChildAsync(noteChildModel));
        }

        // DELETE api/<LocalNotesApi>/5
        [HttpDelete]
        [Route("DeleteMain")]
        public async Task<bool> Delete(int id, [FromBody] int[]? ids)
        {
            return await _notesService.DeleteAsync(id, ids);
        }

        [HttpDelete]
        [Route("DeleteChild")]
        public async Task<bool> DeleteChild(int id, [FromBody] int[]? ids)
        {
            return await _notesService.DeleteChildAsync(id, ids);
        }
    }
}
