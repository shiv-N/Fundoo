using CommonLayerModel.NotesModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessManager.Interface
{
    public interface INotesBL
    {
        string CreateNotes(AddNotesRequestModel model);
    }
}
