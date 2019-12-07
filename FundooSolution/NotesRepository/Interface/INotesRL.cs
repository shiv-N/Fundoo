using CommonLayerModel.NotesModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessManager.Interface
{
    public interface INotesRL 
    {
        string AddNotes(AddNotesRequestModel model, int userId);
        IList<DisplayResponceModel> DisplayNotes(int userId);
        string EditNote(EditNoteRequestModel model, int userId);
        string DeleteNote(DeleteNoteRequestModel model, int userId);
    }
}
