
namespace FundooNotesApi.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using BusinessManager.Interface;
    using BusinessManager.Services;
    using CommonLayerModel.NotesModels;
    using CommonLayerModel.NotesModels.Request;
    using CommonLayerModel.NotesModels.Responce;
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
        [HttpPost]
        public async Task<IActionResult> AddNotes(AddNotesRequestModel model)
        {
            try
            {
                if (model != null)
                {
                    var userId = TokenUserId();
                    if (await note.AddNotes(model, userId))
                    {
                        return Ok(new { success = true, Message = "Note added" });
                    }
                    else
                    {
                        return BadRequest(new { success = false, Message = "Note did not added" });
                    }
                }
                else
                {
                    return BadRequest(new { success = false, Message = "Input should not be empty!!!" });
                }
            }
            catch(Exception e)
            {
                return BadRequest(new { success=false, e.Message });
            }
           
        }

        /// <summary>
        /// Displays the notes.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> DisplayNotes()
        {
            try
            {
                var userId = TokenUserId();
                if (userId != 0)
                {

                    IList <DisplayResponceModel> Data = await note.DisplayNotes(userId);
                    if (Data != null)
                    {
                        return Ok(new { success = true, Meassage = "Display notes operation is successful", Data });

                    }
                    else
                    {
                        return BadRequest(new { success = false, Meassage = "Display notes operation is not successful" });
                    }
                }
                else
                {
                    return BadRequest(new { success = false, Meassage = "Invalid user" });
                }
            }
            catch(Exception e)
            {
                return BadRequest(new { success = false, Meassage = e.Message });
            }
        }

        /// <summary>
        /// Displays the archive.
        /// </summary>
        /// <returns></returns>
        [HttpGet("archive")]
        public async Task<IActionResult> DisplayArchive()
        {
            try
            {
                var userId = TokenUserId();
                if (userId != 0)
                {

                    IList<DisplayResponceModel> Data = await note.DisplayArchive(userId);
                    if (Data != null)
                    {
                        return Ok(new { success = true, Meassage = "Display archive notes operation is successful", Data });

                    }
                    else
                    {
                        return BadRequest(new { success = false, Meassage = "Display archive notes operation is not successful" });
                    }
                }
                else
                {
                    return BadRequest(new { success = false, Meassage = "Invalid user" });
                }
            }
            catch (Exception e)
            {
                return BadRequest(new { success = false, Meassage = e.Message });
            }
        }

        [HttpGet("trash")]
        public async Task<IActionResult> DisplayTrash()
        {
            try
            {
                var userId = TokenUserId();
                if (userId != 0)
                {

                    IList<DisplayResponceModel> Data = await note.DisplayTrash(userId);
                    if (Data != null)
                    {
                        return Ok(new { success = true, Meassage = "Display trash notes operation is successful", Data });

                    }
                    else
                    {
                        return BadRequest(new { success = false, Meassage = "Display trash archive notes operation is not successful" });
                    }
                }
                else
                {
                    return BadRequest(new { success = false, Meassage = "Invalid user" });
                }
            }
            catch (Exception e)
            {
                return BadRequest(new { success = false, Meassage = e.Message });
            }
        }

        /// <summary>
        /// Edits the note.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPut("{noteId}")]
        public async Task<IActionResult> EditNote(int noteId, EditNoteRequestModel model)
        {
            if (noteId != 0 && (model.Message !=null || model.Title !=null ))
            {
                var userId = TokenUserId();
                if (await note.EditNote(noteId,model, userId))
                {
                    string status = "Note edited";
                    return Ok(new { status, userId,model });
                }
                else
                {
                    string status = "Note did not edited";
                    return BadRequest(new { status, userId,model });
                }
            }
            else
            {
                return BadRequest(new { success = false, Meassage = "Input should not be empty!!!" });
            }
        }

        /// <summary>
        /// Deletes the note.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteNote(int Id)
        {
            var userId = TokenUserId();
            if(await note.DeleteNote(Id, userId))
            {
                string status = "Note deleted";
                return Ok(new { status, userId, Id });
            }
            else
            {
                string status = "Note is not deleted";
                return BadRequest(new { status, userId, Id });
            }
        }

        /// <summary>
        /// Uploads the image.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="noteId">The note identifier.</param>
        /// <returns></returns>
        [HttpPut("Image/{noteId}")]
        public async Task<IActionResult> UploadImage(IFormFile file, int noteId)
        {
            if (file != null && noteId != 0)
            {
                var userId = TokenUserId();
                if (await note.UploadImage(file, noteId, userId))
                {
                    string status = "Image Uploaded SuccessFully";
                    return Ok(new { status, userId, noteId });
                }
                else
                {
                    string status = "Image is not Uploaded SuccessFully";
                    return BadRequest(new { status, userId, noteId });
                }
            }
            else
            {
                string result = "input should not be empty";
                return BadRequest(new { result });
            }
        }

        /// <summary>
        /// Archives the note.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <returns></returns>
        [HttpPut("archive/{noteId}")]
        public async Task<IActionResult> ArchiveNote(int noteId)
        {
            var userId = TokenUserId();
            if (await note.ArchiveNote(noteId, userId))
            {
                string status = "archive successful";
                return Ok(new { status, userId, noteId });
            }
            else
            {
                string status = "archive is not successful";
                return BadRequest(new { status, userId, noteId });
            }
        }

        /// <summary>
        /// Pins the note.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <returns></returns>
        [HttpPut("Pin/{noteId}")]
        public async Task<IActionResult> PinNote(int noteId)
        {
            var userId = TokenUserId();
            if (await note.PinNote(noteId, userId))
            {
                string status = "Pin successful";
                return Ok(new { status, userId, noteId });
            }
            else
            {
                string status = "Pin is not successful";
                return BadRequest(new { status, userId, noteId });
            }
        }

        /// <summary>
        /// Trashes the note.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <returns></returns>
        [HttpPut("Trash/{noteId}")]
        public async Task<IActionResult> TrashNote(int noteId)
        {
            var userId = TokenUserId();
            if (await note.TrashNote(noteId, userId))
            {
                string status = "Trash successful";
                return Ok(new { status, userId, noteId });
            }
            else
            {
                string status = "Trash is not successful";
                return BadRequest(new { status, userId, noteId });
            }
        }

        /// <summary>
        /// Reminders the note.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <param name="reminder">The reminder.</param>
        /// <returns></returns>
        [HttpPut("Reminder/{noteId}")]
        public async Task<IActionResult> ReminderNote(int noteId, AddReminderRequest reminder)
        {
            var userId = TokenUserId();
            if (await note.ReminderNote(noteId, userId, reminder))
            {
                string status = "reminder set successfully";
                return Ok(new { status, userId, noteId, reminder });
            }
            else
            {
                string status = "reminder is not set";
                return BadRequest(new { status, userId, noteId, reminder });
            }
        }

        /// <summary>
        /// Colours the note.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <param name="colourRequest">The colour request.</param>
        /// <returns></returns>
        [HttpPut("Color/{noteId}")]
        public async Task<IActionResult> ColourNote(int noteId, ColourRequestModel colourRequest)
        {
            var userId = TokenUserId();
            if (await note.ColourNote(noteId, userId, colourRequest))
            {
                string status = "color changed successfully";
                return Ok(new { status, userId, noteId, colourRequest });
            }
            else
            {
                string status = "color is not changed.";
                return BadRequest(new { status, userId, noteId, colourRequest });
            }
        }

        /// <summary>
        /// Gets the collaborators.
        /// </summary>
        /// <returns></returns>
        [HttpGet("Collabrators")]
        public async Task<IActionResult> GetCollaborators()
        {
            var userId = TokenUserId();
            IList<GetCollabratorResponce> data = await note.GetCollaborators(userId);
            if (data!=null)
            {
                return Ok(new { success=true,Meassage="Get Collabrators SucessessFully", data });
            }
            else
            {
                return BadRequest(new { success = false, Meassage = "Get Collabrators unsuceessful" });
            }
        }

        /// <summary>
        /// Adds the collaborators.
        /// </summary>
        /// <param name="collaborator">The collaborator.</param>
        /// <returns></returns>
        [HttpPost("AddCollaborators/{NoteId}")]
        public async Task<IActionResult> AddCollaborators(int NoteId, AddCollaboratorRequest collaborator)
        {
            try
            {
                if (collaborator.CollaboratorId != 0 && NoteId != 0)
                {
                    var userId = TokenUserId();
                    AddCollaboratorResponce data = await note.AddCollaborators(NoteId,userId, collaborator);
                    if (data.CollaborationRecordId != 0)
                    {
                        return Ok(new { success = true, Meassage = "Add Collabrators SucessessFully", data });
                    }
                    else
                    {
                        return BadRequest(new { success = false, Meassage = "Add Collabrators unsuceessful" });
                    }
                }
                else
                {
                    return BadRequest(new { success = false, Meassage = "Invalid Credentials" });
                }
            }
            catch(Exception e)
            {
                return BadRequest(new { success = false, Meassage = e.Message});
            }
        }

        /// <summary>
        /// Bulks the trash.
        /// </summary>
        /// <param name="NoteId">The note identifier.</param>
        /// <returns></returns>
        [HttpDelete("bulkDelete")]
        public async Task<IActionResult> BulkTrash(List<int> NoteId)
        {
            try
            {
                if (NoteId != null)
                {
                    var userId = TokenUserId();
                    List<DisplayResponceModel> data = await note.BulkTrash(NoteId, userId);
                    if(data.Count != 0)
                    {
                        return Ok(new { success = true, Meassage = "Bulk Trash SucessessFully", data });
                    }
                    else
                    {
                        return BadRequest(new { success = false, Meassage = "Bulk Trash unsuceessful" });
                    }
                }
                else
                {
                    return BadRequest(new { success = false, Meassage = "Invalid Input!" });
                }

            }
            catch (Exception e)
            {
                return BadRequest(new { success = false, Meassage = e.Message });
            }
        }

        /// <summary>
        /// Searches the keyword.
        /// </summary>
        /// <param name="keyword">The keyword.</param>
        /// <returns></returns>
        [HttpGet("search")]
        public async Task<IActionResult> SearchKeyword(string keyword)
        {
            try
            {
                if (keyword != null)
                {
                    var userId = TokenUserId();
                    List<DisplayResponceModel> data = await note.SearchKeyword(keyword, userId);
                    if (data.Count != 0)
                    {
                        return Ok(new { success = true, Meassage = "Search Keyword SucessessFully", data });
                    }
                    else
                    {
                        return BadRequest(new { success = false, Meassage = "Search Keyword unsuceessful" });
                    }
                }
                else
                {
                    return BadRequest(new { success = false, Meassage = "Invalid Input!" });
                }

            }
            catch (Exception e)
            {
                return BadRequest(new { success = false, Meassage = e.Message });
            }
        }



        [HttpPost("SearchCollaborators")]
        public async Task<IActionResult> SearchCollaborators(SearchRequestModel model)
        {
            try
            {
                if (model.Keyword != null && model.Keyword !=string.Empty)
                {
                    var userId = TokenUserId();
                    List<GetCollaboratorResponse> data = await note.SearchCollaborators(model.Keyword, userId);
                    if (data.Count != 0)
                    {
                        return Ok(new { success = true, Meassage = "Search Collaborators SucessessFully", data });
                    }
                    else
                    {
                        return BadRequest(new { success = false, Meassage = "Search Collaborators unsuceessful" });
                    }
                }
                else
                {
                    return BadRequest(new { success = false, Meassage = "Invalid Input!" });
                }

            }
            catch (Exception e)
            {
                return BadRequest(new { success = false, Meassage = e.Message });
            }
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