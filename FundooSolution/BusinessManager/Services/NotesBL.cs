namespace BusinessManager.Services
{
    using BusinessManager.Interface;
    using CommonLayerModel.NotesModels;
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
        public async Task<string> AddNotes(AddNotesRequestModel model, int userId)
        {
            return await notes.AddNotes(model,userId); 
        }

        /// <summary>
        /// Deletes the note.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public async Task<string> DeleteNote(int Id, int userId)
        {
            return await notes.DeleteNote(Id, userId);
        }

        /// <summary>
        /// Displays the notes.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public IList<DisplayResponceModel> DisplayNotes(int userId)
        {
            return notes.DisplayNotes(userId);
        }

        /// <summary>
        /// Edits the note.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public async Task<string> EditNote(EditNoteRequestModel model, int userId)
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
        public async Task<string> UploadImage(IFormFile file, int noteId, int userId)
        {
            return await notes.UploadImage(file, noteId,userId);
        }

        /// <summary>
        /// Archives the note.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public async Task<string> ArchiveNote(int noteId, int userId)
        {
            return await notes.ArchiveNote(noteId, userId);
        }

        /// <summary>
        /// Pins the note.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public async Task<string> PinNote(int noteId, int userId)
        {
            return await notes.PinNote(noteId, userId);
        }

        /// <summary>
        /// Trashes the note.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public async Task<string> TrashNote(int noteId, int userId)
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
        public async Task<string> ReminderNote(int noteId, int userId, AddReminderRequest reminder)
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
        public async Task<string> ColourNote(int noteId, int userId, ColourRequestModel colourRequest)
        {
            return await notes.ColourNote(noteId, userId, colourRequest);
        }
    }
}
