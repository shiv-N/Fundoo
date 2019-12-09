
namespace FundooNotesApi.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using BusinessManager.Interface;
    using BusinessManager.Services;
    using CommonLayerModel.NotesModels;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// this is class NotesController
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class NotesController : ControllerBase
    {
        /// <summary>
        /// The note
        /// </summary>
        private readonly INotesBL note;

        /// <summary>
        /// Initializes a new instance of the <see cref="NotesController"/> class.
        /// </summary>
        /// <param name="note">The note.</param>
        public NotesController(INotesBL note)
        {
            this.note = note;
        }

        /// <summary>
        /// Adds the notes.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost("add")]
        public async Task<IActionResult> AddNotes(AddNotesRequestModel model)
        {
            var userId = TokenUserId();
            return Ok(await note.AddNotes(model, userId));
        }

        /// <summary>
        /// Displays the notes.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult DisplayNotes()
        {
            var userId = TokenUserId();
            return Ok(note.DisplayNotes(userId));
        }

        /// <summary>
        /// Edits the note.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPut("edit")]
        public async Task<IActionResult> EditNote(EditNoteRequestModel model)
        {
            var userId = TokenUserId();
            return Ok(await note.EditNote(model, userId));
        }

        /// <summary>
        /// Deletes the note.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteNote(DeleteNoteRequestModel model)
        {
            var userId = TokenUserId();
            return Ok(await note.DeleteNote(model,userId));
        }

        /// <summary>
        /// Tokens the user identifier.
        /// </summary>
        /// <returns></returns>
        private int TokenUserId()
        {
            return Convert.ToInt32(User.FindFirst("Id").Value);
        }
    }
}