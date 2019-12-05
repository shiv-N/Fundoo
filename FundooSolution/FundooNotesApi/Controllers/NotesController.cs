using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessManager.Interface;
using BusinessManager.Services;
using CommonLayerModel.NotesModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FundooNotesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly INotesBL note;
        public NotesController(INotesBL note)
        {
            this.note = note;
        }
        [HttpPost]
        public IActionResult CreateNotes(AddNotesRequestModel model)
        {
            return Ok(note.CreateNotes(model));
        }
    }
}