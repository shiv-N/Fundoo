using CommonLayerModel.NotesModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessManager.Interface
{
    public interface INotesRL 
    {
        string AddNotes(AddNotesRequestModel model);
        IList<AddNotesRequestModel> DisplayNotes(DisplayNoteRequestModel userId);
    }
}
