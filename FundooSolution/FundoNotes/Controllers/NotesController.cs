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
    [Authorize]
    [Route("api/[controller]")]

    public class NotesController : ControllerBase
    {
        private readonly INotesBL note;
        public NotesController(INotesBL note)
        {
            this.note = note;
        }



        [HttpPost]
        [Route("addNotes")]
        public IActionResult CreateNotes(AddNotesRequestModel model)
        {
            return Ok(note.CreateNotes(model));
        }
    }
}