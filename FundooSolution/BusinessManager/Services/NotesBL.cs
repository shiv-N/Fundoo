namespace BusinessManager.Services
{
    using BusinessManager.Interface;
    using CommonLayerModel.NotesModels;
    using CommonLayerModel.NotesModels.Request;
    using CommonLayerModel.NotesModels.Responce;
    using Microsoft.AspNetCore.Http;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// this is class NotesBL
    /// </summary>
    /// <seealso cref="BusinessManager.Interface.INotesBL" />
    public class NotesBL : INotesBL
    {
        /// <summary>
        /// The notes
        /// </summary>
        INotesRL notes;

        /// <summary>
        /// Initializes a new instance of the <see cref="NotesBL"/> class.
        /// </summary>
        /// <param name="notes">The notes.</param>
        public NotesBL(INotesRL notes)
        {
            this.notes = notes;
        }

        /// <summary>
        /// Adds the notes.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public async Task<bool> AddNotes(AddNotesRequestModel model, int userId)
        {
            return await notes.AddNotes(model,userId); 
        }

        /// <summary>
        /// Deletes the note.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public async Task<bool> DeleteNote(int Id, int userId)
        {
            try
            {
                return await notes.DeleteNote(Id, userId);
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Displays the notes.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public async Task<IList<DisplayResponceModel>> DisplayNotes(int userId)
        {
            return await notes.DisplayNotes(userId);
        }

        /// <summary>
        /// Edits the note.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public async Task<bool> EditNote(EditNoteRequestModel model, int userId)
        {
            return await notes.EditNote(model,userId);
        }

        /// <summary>
        /// Uploads the image.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="noteId">The note identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public async Task<bool> UploadImage(IFormFile file, int noteId, int userId)
        {
            return await notes.UploadImage(file, noteId,userId);
        }

        /// <summary>
        /// Archives the note.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public async Task<bool> ArchiveNote(int noteId, int userId)
        {
            return await notes.ArchiveNote(noteId, userId);
        }

        /// <summary>
        /// Pins the note.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public async Task<bool> PinNote(int noteId, int userId)
        {
            return await notes.PinNote(noteId, userId);
        }

        /// <summary>
        /// Trashes the note.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public async Task<bool> TrashNote(int noteId, int userId)
        {
            return await notes.TrashNote(noteId, userId);

        }

        /// <summary>
        /// Reminders the note.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="reminder">The reminder.</param>
        /// <returns></returns>
        public async Task<bool> ReminderNote(int noteId, int userId, AddReminderRequest reminder)
        {
            return await notes.ReminderNote(noteId,userId,reminder);
        }

        /// <summary>
        /// Colours the note.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="colourRequest">The colour request.</param>
        /// <returns></returns>
        public async Task<bool> ColourNote(int noteId, int userId, ColourRequestModel colourRequest)
        {
            return await notes.ColourNote(noteId, userId, colourRequest);
        }

        /// <summary>
        /// Gets the collaborators.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public async Task<IList<GetCollabratorResponce>> GetCollaborators(int userId)
        {
            return await notes.GetCollaborators(userId);
        }

        /// <summary>
        /// Adds the collaborators.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="collaborator">The collaborator.</param>
        /// <returns></returns>
        public async Task<AddCollaboratorResponce> AddCollaborators(int userId, AddCollaboratorRequest collaborator)
        {
            try
            {
                return await notes.AddCollaborators(userId, collaborator);
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Bulks the trash.
        /// </summary>
        /// <param name="NoteId">The note identifier.</param>
        /// <param name="UserId">The user identifier.</param>
        /// <returns></returns>
        public async Task<List<DisplayResponceModel>> BulkTrash(List<int> NoteId, int UserId)
        {
            try
            {
                return await notes.BulkTrash(NoteId, UserId);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Searches the keyword.
        /// </summary>
        /// <param name="keyword">The keyword.</param>
        /// <param name="UserId">The user identifier.</param>
        /// <returns></returns>
        public async Task<List<DisplayResponceModel>> SearchKeyword(string keyword, int UserId)
        {
            try
            {
                return await notes.SearchKeyword(keyword, UserId);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
