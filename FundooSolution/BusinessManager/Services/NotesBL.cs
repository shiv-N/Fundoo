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
        public async Task<string> DeleteNote(DeleteNoteRequestModel model, int userId)
        {
            return await notes.DeleteNote(model,userId);
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

        public async Task<string> UploadImage(IFormFile file, int noteId, int userId)
        {
            return await notes.UploadImage(file, noteId,userId);
        }

        public async Task<string> archiveNote(int noteId, int userId)
        {
            return await notes.archiveNote(noteId, userId);
        }

        public async Task<string> pinNote(int noteId, int userId)
        {
            return await notes.pinNote(noteId, userId);
        }
        public async Task<string> trashNote(int noteId, int userId)
        {
            return await notes.trashNote(noteId, userId);

        }

        public async Task<string> ReminderNote(int noteId, int userId, AddReminderRequest reminder)
        {
            return await notes.ReminderNote(noteId,userId,reminder);
        }
    }
}
