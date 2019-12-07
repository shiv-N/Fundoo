using BusinessManager.Interface;
using CommonLayerModel.NotesModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessManager.Services
{
    public class NotesBL : INotesBL
    {
        INotesRL notes;
        public NotesBL(INotesRL notes)
        {
            this.notes = notes;
        }
        public string AddNotes(AddNotesRequestModel model, int userId)
        {
            return notes.AddNotes(model,userId); 
        }

        public string DeleteNote(DeleteNoteRequestModel model, int userId)
        {
            return notes.DeleteNote(model,userId);
        }

        public IList<DisplayResponceModel> DisplayNotes(int userId)
        {
            return notes.DisplayNotes(userId);
        }

        public string EditNote(EditNoteRequestModel model, int userId)
        {
            return notes.EditNote(model,userId);
        }
    }
}
