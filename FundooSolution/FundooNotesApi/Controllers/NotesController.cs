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

namespace FundooNotesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class NotesController : ControllerBase
    {
        private readonly INotesBL note;
        public NotesController(INotesBL note)
        {
            this.note = note;
        }
        [HttpPost]
        public IActionResult AddNotes(AddNotesRequestModel model)
        {
            var userId = TokenUserId();
            return Ok(note.AddNotes(model, userId));
        }
        [HttpGet]
        public IActionResult DisplayNotes()
        {
            var userId = TokenUserId();
            return Ok(note.DisplayNotes(userId));
        }
        [HttpPut("edit")]
        public IActionResult EditNote(EditNoteRequestModel model)
        {
            var userId = TokenUserId();
            return Ok(note.EditNote(model, userId));
        }
        [HttpDelete("delete")]
        public IActionResult DeleteNote(DeleteNoteRequestModel model)
        {
            var userId = TokenUserId();
            return Ok(note.DeleteNote(model,userId));
        }
        private int TokenUserId()
        {
            return Convert.ToInt32(User.FindFirst("Id").Value);
        }
    }
}