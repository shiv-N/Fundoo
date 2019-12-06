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
            return Ok(note.AddNotes(model));
        }
        [HttpPost("get")]
        public IActionResult DisplayNotes(DisplayNoteRequestModel userId)
        {
            return Ok(note.DisplayNotes(userId));
        }
    }
}