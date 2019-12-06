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
        public string AddNotes(AddNotesRequestModel model)
        {
            return notes.AddNotes(model); 
        }

        public IList<AddNotesRequestModel> DisplayNotes(DisplayNoteRequestModel userId)
        {
            return notes.DisplayNotes(userId);
        }
    }
}
