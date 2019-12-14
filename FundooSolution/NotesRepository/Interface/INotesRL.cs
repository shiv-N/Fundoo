
namespace BusinessManager.Interface
{
    using CommonLayerModel.NotesModels;
    using Microsoft.AspNetCore.Http;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// this is interface INotesRL
    /// </summary>
    public interface INotesRL 
    {
        /// <summary>
        /// Adds the notes.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        Task<string> AddNotes(AddNotesRequestModel model, int userId);

        /// <summary>
        /// Displays the notes.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        IList<DisplayResponceModel> DisplayNotes(int userId);

        /// <summary>
        /// Edits the note.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        Task<string> EditNote(EditNoteRequestModel model, int userId);

        /// <summary>
        /// Deletes the note.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        Task<string> DeleteNote(DeleteNoteRequestModel model, int userId);
        Task<string> UploadImage(IFormFile file, int noteId, int userId);
        Task<string> archiveNote(int noteId,int userId);
        Task<string> pinNote(int noteId, int userId);
        Task<string> trashNote(int noteId, int userId);
        Task<string> ReminderNote(int noteId, int userId, AddReminderRequest reminder);
    }
}
