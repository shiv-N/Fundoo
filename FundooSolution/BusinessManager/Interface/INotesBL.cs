namespace BusinessManager.Interface
{
    using CommonLayerModel.NotesModels;
    using CommonLayerModel.NotesModels.Request;
    using CommonLayerModel.NotesModels.Responce;
    using Microsoft.AspNetCore.Http;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// this is interface INotesBL
    /// </summary>
    public interface INotesBL
    {
        /// <summary>
        /// Adds the notes.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        Task<bool> AddNotes(AddNotesRequestModel model, int userId);

        /// <summary>
        /// Displays the notes.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        Task<IList<DisplayResponceModel>> DisplayNotes(int userId);

        /// <summary>
        /// Edits the note.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        Task<bool> EditNote(EditNoteRequestModel model, int userId);

        /// <summary>
        /// Deletes the note.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        Task<bool> DeleteNote(int Id, int userId);

        /// <summary>
        /// Uploads the image.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="noteId">The note identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        Task<bool> UploadImage(IFormFile file, int noteId, int userId);

        /// <summary>
        /// Archives the note.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        Task<bool> ArchiveNote(int noteId, int userId);

        /// <summary>
        /// Pins the note.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        Task<bool> PinNote(int noteId, int userId);

        /// <summary>
        /// Trashes the note.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        Task<bool> TrashNote(int noteId, int userId);

        /// <summary>
        /// Reminders the note.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="reminder">The reminder.</param>
        /// <returns></returns>
        Task<bool> ReminderNote(int noteId, int userId, AddReminderRequest reminder);

        /// <summary>
        /// Colours the note.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="colourRequest">The colour request.</param>
        /// <returns></returns>
        Task<bool> ColourNote(int noteId, int userId, ColourRequestModel colourRequest);

        /// <summary>
        /// Gets the collaborators.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        Task<IList<GetCollabratorResponce>> GetCollaborators(int userId);

        /// <summary>
        /// Adds the collaborators.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="collaborator">The collaborator.</param>
        /// <returns></returns>
        Task<AddCollaboratorResponce> AddCollaborators(int userId, AddCollaboratorRequest collaborator);

        /// <summary>
        /// Bulks the trash.
        /// </summary>
        /// <param name="NoteId">The note identifier.</param>
        /// <param name="UserId">The user identifier.</param>
        /// <returns></returns>
        Task<List<DisplayResponceModel>> BulkTrash(List<int> NoteId, int UserId);

        /// <summary>
        /// Searches the keyword.
        /// </summary>
        /// <param name="keyword">The keyword.</param>
        /// <param name="UserId">The user identifier.</param>
        /// <returns></returns>
        Task<List<DisplayResponceModel>> SearchKeyword(string keyword, int UserId);
    }
}
