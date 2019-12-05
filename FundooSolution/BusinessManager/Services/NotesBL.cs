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
        public string CreateNotes(AddNotesRequestModel model)
        {
            return notes.CreateNotes(model); 
        }
    }
}
